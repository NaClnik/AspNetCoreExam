using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    interface ISoftDeletable
    {
        // Флаг, который сообщает: удалён ли столбец.
        bool IsDeleted { get; set; } 
    }
}
