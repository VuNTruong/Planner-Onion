using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Command.AuthCommands
{
    public class CreateNewWorkItem : IRequest<WorkItem>
    {
        // Id of the work item
        public int Id { get; set; }

        // Title of the work item
        public string Title { get; set; }

        // Content of the work item
        public string Content { get; set; }

        // Id of the creator
        public int CreatorId { get; set; }

        // Date created of the work item
        public string DateCreated { get; set; }

        public class CreateWorkItemHandler : IRequestHandler<CreateNewWorkItem, WorkItem>
        {
            // Database context
            private readonly IApplicationDbContext _context;

            // Constructor
            public CreateWorkItemHandler(IApplicationDbContext context)
            {
                // Initialize database context which is injected via DI
                _context = context;
            }

            // The function to perform create work item operation
            public async Task<WorkItem> Handle(CreateNewWorkItem request, CancellationToken cancellationToken)
            {
                // The new work item object
                var newWorkItem = new WorkItem();

                // Fill out properties for the new work item
                newWorkItem.Title = request.Title;
                newWorkItem.Content = request.Content;
                newWorkItem.CreatorId = request.CreatorId;
                newWorkItem.DateCreated = request.DateCreated;

                // Add new work item to list of entities
                _context.WorkItems.Add(newWorkItem);

                // Save changes
                await _context.SaveChangesAsync();

                // Return list of work items
                return newWorkItem;
            }
        }
    }
}