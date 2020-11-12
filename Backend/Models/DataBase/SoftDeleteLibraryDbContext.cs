using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.DataBase
{
    public sealed class SoftDeleteLibraryDbContext : LibraryDbContext
    {
        #region Ансамбль конструкторов
        // Ансамбль конструкторов.
        // Конструктор по умолчанию.
        public SoftDeleteLibraryDbContext() { }

        // Конструктор с параметрами.
        public SoftDeleteLibraryDbContext(DbContextOptions options) : base(options)
        {
            // Чтобы вызвать этот метод в конструкторе, Intellisense посоветовал
            // Сделать запечатать класс (sealed).
            // TODO: Убрать при готовом проекте Database.EnsureDeleted().
            Database.EnsureCreated();
        } // ctorf.
        #endregion

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            // Получаем коллекцию элементов, которые должны удалиться.
            var deletedCollection = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in deletedCollection)
            {
                // Если таблица не является наследником интерфейса
                // для логического удаления, то идем дальше по циклу.
                if (!(entry.Entity is ISoftDeletable))
                    continue;

                // Меняем состояние вхождения.
                entry.State = EntityState.Unchanged;

                // Получить название таблицы вхождения.
                string table = entry.Metadata.GetTableName();

                // Выполнить SQL запрос.
                int count = await Database
                    .ExecuteSqlRawAsync($"UPDATE {table} SET IsDeleted = 1 WHERE Id = {((IEntity)entry.Entity).Id}", cancellationToken);

                ((ISoftDeletable) entry.Entity).IsDeleted = true;
            } // foreach.

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        } // SaveChangesAsync.
    } // SoftDeleteLibraryDbContext.
}
