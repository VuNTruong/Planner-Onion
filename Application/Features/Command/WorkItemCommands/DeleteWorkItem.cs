using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Command.AuthCommands
{
    public class DeleteWorkItem : IRequest<int>
    {
        // Id of the work item to be removed
        public int WorkItemIdToBeRemoved { get; set; }

        // Constructor
        public DeleteWorkItem (int workItemId)
        {
            // Initialize work item id
            WorkItemIdToBeRemoved = workItemId;
        }

        public class DeleteWorkItemHandler : IRequestHandler<DeleteWorkItem, int>
        {
            // Database context
            private readonly IApplicationDbContext _context;

            // Constructor
            public DeleteWorkItemHandler(IApplicationDbContext context)
            {
                // Initialize database context
                _context = context;
            }

            // The function to handle work item delete operation
            public async Task<int> Handle(DeleteWorkItem request, CancellationToken cancellationToken)
            {
                // Reference the database to get work item object of the work
                // item to be removed
                var workItemToBeRemoved = await _context.WorkItems
                    .FirstOrDefaultAsync(workItem => workItem.Id == request.WorkItemIdToBeRemoved);

                // Remove the found work item
                _context.WorkItems.Remove(workItemToBeRemoved);

                // Save changes
                await _context.SaveChangesAsync();

                // Return id of the work item that has just been removed
                return workItemToBeRemoved.Id;
            }
        }
    }
}
