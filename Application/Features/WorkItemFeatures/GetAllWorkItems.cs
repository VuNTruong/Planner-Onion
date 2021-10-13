using System.Collections.Generic;
using MediatR;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkItemFeatures
{
    public class GetAllWorkItems : IRequest<IEnumerable<WorkItem>>
    {
        public class GetAllWorkItemsHandler : IRequestHandler<GetAllWorkItems, IEnumerable<WorkItem>>
        {
            // The database context
            private readonly IApplicationDbContext _context;

            public GetAllWorkItemsHandler (IApplicationDbContext context)
            {
                // Initialize database context which is injected via DI
                _context = context;
            }

            public async Task<IEnumerable<WorkItem>> Handle(GetAllWorkItems request, CancellationToken cancellationToken)
            {
                // Get list of work items from the table
                var workItemList = await _context.WorkItems
                    .Include(workItem => workItem.Creator)
                    .ToListAsync();

                // If it is null, return null
                if (workItemList == null)
                {
                    return null;
                }

                // Return list work item view models
                return workItemList;
            }
        }
    }
}
