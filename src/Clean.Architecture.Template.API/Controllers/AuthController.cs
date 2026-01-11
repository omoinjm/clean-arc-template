using System.Net;
using Clean.Architecture.Template.Application.Common.IdentityCommands;
using Clean.Architecture.Template.Application.Response.Auth;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Application.Queries.General;
using Clean.Architecture.Template.Application.Response.Menu;
using Clean.Architecture.Template.Core.Specs;
using Clean.Architecture.Template.Application.Queries.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Template.API.Controllers
{
    public class AuthController(IMediator mediator, ILogger logger) : ApiController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger _logger = logger;

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(Clean.Architecture.Template.Application.Response.BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(CreateLoginCommand requestLogin)
        {
            _logger.LogInformation($"Login by {requestLogin.Email}");

            var result = await _mediator.Send(requestLogin);

            _logger.LogInformation($"Login result for {requestLogin.Email} {result.Success}");

            return Ok(ReturnSuccessModel(result, "Login successful!", (int)HttpStatusCode.OK, true, 0));
        }

        [Authorize]
        [HttpPost]
        [Route("Logout")]
        [ProducesResponseType(typeof(Clean.Architecture.Template.Application.Response.BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Logout()
        {
            return Ok(ReturnSuccessModel<UpdateResponse>(null, "Logout successful!", (int)HttpStatusCode.OK, true, 0));
        }

        [Authorize]
        [HttpGet]
        [Route("MenuStructure")]
        [ProducesResponseType(typeof(Clean.Architecture.Template.Application.Response.BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> MenuStructure()
        {
            var result = await _mediator.Send(new ListAllQuery<MenuResponse>());

            return Ok(ReturnSuccessModel<IList<MenuResponse>>(result));
        }

        [Authorize]
        [HttpGet]
        [Route("RefreshUser")]
        [ProducesResponseType(typeof(Clean.Architecture.Template.Application.Response.BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshUser()
        {
            var result = await _mediator.Send(new GetLoginCredentialsQuery());

            return Ok(ReturnSuccessModel(result, "User refreshed successfully!", (int)HttpStatusCode.OK, true, 0));
        }
    }
}
