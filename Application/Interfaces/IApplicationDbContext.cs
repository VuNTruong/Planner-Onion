using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WorkItem> WorkItems { get; set; }
        Task<int> SaveChanges();
    }
}
