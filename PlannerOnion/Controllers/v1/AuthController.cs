using System.Collections.Generic;
using System.Threading.Tasks;
using PlannerOnion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Application.Interfaces;
using Application.Features.Command.AuthCommands;

namespace PlannerOnion.Controllers.v1
{
    public class AuthController : BaseApiController {
        // Auto mapper
        private IMapper _mapper;

        // Error getter
        private IErrorGetter _errorGetter;

        // Constructor
        public AuthController(IMapper mapper, IErrorGetter errorGetter)
        {
            // Initialize auto mapper
            _mapper = mapper;

            // Initialize error getter
            _errorGetter = errorGetter;
        }

        // The function to sign in
        [HttpPost("SignIn")]
        public async Task<JsonResult> SignIn(SignInViewModel signInViewModel)
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to sign the user in
            var signInResult = await Mediator.Send(new SignIn
            {
                Email = signInViewModel.Email,
                Password = signInViewModel.Password
            });

            if (signInResult["status"] as string == "Done")
            {
                responseData.Add("status", "Done");
                responseData.Add("data", "You are logged in");
            } else
            {
                Response.StatusCode = 500;
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an error");
                responseData.Add("errors", signInResult["errors"] as List<string>);
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to sign up
        [HttpPost("SignUp")]
        public async Task<JsonResult> SignUp(SignUpViewModel signUpViewModel)
        {
            // Response data for the client
            var responseData = new Dictionary<string, object>();

            // Map sign up view model into sign up feature object
            var signUpFeatureObject = _mapper.Map<SignUp>(signUpViewModel);

            // Call the function to sign up a new user
            var signUpResult = await Mediator.Send(signUpFeatureObject);

            if (!ModelState.IsValid)
            {
                // Get list of validation errors
                List<string> validationErrors = _errorGetter.ValidationErrorsGenerator(ModelState);

                // Add data to the response data
                Response.StatusCode = 400;
                responseData.Add("status", "Not done");
                responseData.Add("errors", validationErrors);

                // Return response to the client
                return new JsonResult(responseData);
            }

            if (signUpResult == "Done")
            {
                // Add data to the response data
                responseData.Add("status", "Done");
                responseData.Add("data", "Account is created");
            } else
            {
                // Add data to the response data
                responseData.Add("status", "Not done");
                responseData.Add("data", "There seem to be an error");
            }

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to sign a user out
        [HttpPost("SignOut")]
        public async Task<JsonResult> SignOut()
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Call the function to perform sign out operation
            await Mediator.Send(new SignOut());

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", "You are signed out");

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to send password reset email to a user with specified email
        [HttpPost("SendPasswordResetEmail")]
        public async Task<JsonResult> SendPasswordResetEmail([FromBody] SendPasswordResetEmailViewModel sendPasswordResetEmailViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Map send password reset email view model into corresponding feature object
            var sendPasswordResetEmail = _mapper.Map<SendPasswordResetTokenEmail>(sendPasswordResetEmailViewModel);

            // Call the function to send password reset email to the user
            await Mediator.Send(sendPasswordResetEmail);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", $"An email has been sent to {sendPasswordResetEmailViewModel.Email}");

            // Return response to the client
            return new JsonResult(responseData);
        }

        // The function to reset password for the user
        [HttpPost("ResetPassword")]
        public async Task<JsonResult> ResetPassword([FromBody] ResetPasswordViewModel resetPasswordViewModel)
        {
            // Prepare response data for the client
            var responseData = new Dictionary<string, object>();

            // Map reset password view model into corresponding feature object
            var resetPassword = _mapper.Map<ResetPassword>(resetPasswordViewModel);

            // Call the function to reset password for the user
            await Mediator.Send(resetPassword);

            // Add data to the response data
            responseData.Add("status", "Done");
            responseData.Add("data", $"Password has been reset for {resetPasswordViewModel.Email}");

            // Return response to the client
            return new JsonResult(responseData);
        }
    }
}
