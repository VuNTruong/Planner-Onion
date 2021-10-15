using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Command.AuthCommands
{
    public class UpdateWorkItem : IRequest<WorkItem>
    {
        // Id of the work item to be updated
        public int Id { get; set; }

        // New title of the work item
        public string Title { get; set; }

        // New content of the work item
        public string Content { get; set; }

        public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItem, WorkItem>
        {
            // Database context
            private readonly IApplicationDbContext _context;

            // Constructor
            public UpdateWorkItemHandler(IApplicationDbContext context)
            {
                // Initialize database context
                _context = context;
            }

            public async Task<WorkItem> Handle(UpdateWorkItem request, CancellationToken cancellationToken)
            {
                // Reference the database to get work item object of the work item
                // to be updated
                var workItemToBeUpdated = await _context.WorkItems
                    .FirstOrDefaultAsync(workItem => workItem.Id == request.Id);

                // Update the work item
                workItemToBeUpdated.Content = request.Content;
                workItemToBeUpdated.Title = request.Title;

                // Save changes
                await _context.SaveChangesAsync();

                // Return the updated work item object
                return workItemToBeUpdated;
            }
        }
    }
}
