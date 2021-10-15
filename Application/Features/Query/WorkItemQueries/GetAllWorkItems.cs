using System.Collections.Generic;
using MediatR;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Features.Query.WorkItemQueries
{
    public class GetAllWorkItems : IRequest<IEnumerable<WorkItem>>
    {
        // User id to get work items for
        private int userIdToGetWorkItem;

        // Constructor
        public GetAllWorkItems(int userId)
        {
            userIdToGetWorkItem = userId;
        }

        public class GetAllWorkItemsHandler : IRequestHandler<GetAllWorkItems, IEnumerable<WorkItem>>
        {
            // The database context
            private readonly IApplicationDbContext _databaseContext;

            // Constructor
            public GetAllWorkItemsHandler (IApplicationDbContext context)
            {
                // Initialize database context which is injected via DI
                _databaseContext = context;
            }

            public async Task<IEnumerable<WorkItem>> Handle(GetAllWorkItems request, CancellationToken cancellationToken)
            {
                // List of work items
                List<WorkItem> workItems;

                if (request.userIdToGetWorkItem == 0)
                {
                    // Get list of work items from the table
                    workItems = await _databaseContext.WorkItems
                        .Include(workItem => workItem.Creator)
                        .ToListAsync();
                } else
                {
                    workItems = await _databaseContext.WorkItems
                        .Include(workItem => workItem.Creator)
                        .Where(workItem => workItem.CreatorId == request.userIdToGetWorkItem)
                        .ToListAsync();
                }

                // Return list work item view models
                return workItems;
            }
        }
    }
}
