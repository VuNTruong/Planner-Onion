using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WorkItem> WorkItems { get; set; }
        Task<int> SaveChangesAsync();
    }
}
