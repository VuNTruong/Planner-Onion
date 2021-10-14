using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.WorkItemFeatures;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlannerOnion.ViewModels;

namespace PlannerOnion.Controllers.v1
{
    [Authorize]
    public class WorkItemController : BaseApiController
    {
        // Auto mapper
        private IMapper _mapper;

        // Current user service
        private ICurrentUser _currentUserService;

        public WorkItemController(IMapper mapper, ICurrentUser currentUserService)
        {
            // Initialize auto mapper
            _mapper = mapper;

            // Initialize current user service
            _currentUserService = currentUserService;
        }

        // The function to get all work items
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get list of work items
            List<WorkItem> workItems = (List<WorkItem>)await Mediator.Send(new GetAllWorkItems(0));

            // Map list of work items into list of work item view models
            List<WorkItemViewModel> workItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItemViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to get work items created by the currently logged in user
        [HttpGet("GetWorkItems")]
        public async Task<JsonResult> GetWorkItemsByCurrentUser()
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to get user id of the currently logged in user
            int currentUserId = await _currentUserService.GetCurrentUserId();

            // Call the function to get list of work items created by the currently logged in user
            List<WorkItem> workItems = (List<WorkItem>)await Mediator.Send(new GetAllWorkItems(currentUserId));

            // Map list of work items into list of work item view models
            List<WorkItemViewModel> workItemViewModels = _mapper.Map<List<WorkItemViewModel>>(workItems);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", workItemViewModels);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to create new work item
        [HttpPost]
        public async Task<JsonResult> CreateNew([FromBody]WorkItemViewModel workItemViewModel)
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Map request work item view model into create new work item feature object
            CreateNewWorkItem createNewWorkItem = _mapper.Map<CreateNewWorkItem>(workItemViewModel);

            // Call the function to create new work item
            // and get work item object of the newly created work item
            var createdWorkItem = await Mediator.Send(createNewWorkItem);

            // Map created work item object into work item view model
            var createdWorkItemViewModel = _mapper.Map<WorkItemViewModel>(createdWorkItem);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", createdWorkItemViewModel);

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to delete a work item
        [HttpDelete]
        public async Task<JsonResult> DeleteWorkItem(int workItemId)
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to delete a work item
            // and get work item id of the work item that has just been removed
            var deleteWorkItem = await Mediator.Send(new DeleteWorkItem(workItemId));

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", $"Work item id {deleteWorkItem} has been removed");

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to update a work item
        [HttpPatch]
        public async Task<JsonResult> UpdateWorkItem([FromBody]WorkItemViewModel workItemViewModel)
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Map the request work item view model into update work item feature object
            UpdateWorkItem updateWorkItem = _mapper.Map<UpdateWorkItem>(workItemViewModel);

            // Call the function to update a work item
            // and get the updated work item
            var updatedWorkItem = await Mediator.Send(updateWorkItem);

            // Map the updated work item object into work item view model
            var updatedWorkItemViewModel = _mapper.Map<WorkItemViewModel>(updatedWorkItem);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", updatedWorkItemViewModel);

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}