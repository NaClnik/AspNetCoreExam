﻿using System;
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
            Database.EnsureDeleted();
            Database.EnsureCreated();
        } // ctorf.
        #endregion

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                // Получаем коллекцию элементов, которые должны удалиться.
                var deletedCollection = ChangeTracker
                    .Entries()
                    .Where(e => e.State == EntityState.Deleted);

                Debug.WriteLine(deletedCollection);
            } // try.
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            } // catch.

            //    foreach (var item in deletedCollection)
            //    {
            //        if (item.Entity is ISoftDeletable)
            //        {
            //            await Database.ExecuteSqlRawAsync("", cancellationToken: cancellationToken);
            //        } // if.
            //    } // foreach.

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        } // SaveChangesAsync.
    } // SoftDeleteLibraryDbContext.
}
