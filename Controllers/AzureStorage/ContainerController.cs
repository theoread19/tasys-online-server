using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.AzureStorage;

namespace TASysOnlineProject.Controllers.AzureStorage
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerService _containerService;

        public ContainerController(IContainerService containerService)
        {
            this._containerService = containerService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllContainerAsync()
        {
            var responses = await this._containerService.GetAllContainerAsync();
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateContainerAsync([FromBody] ContainerRequest container)
        {
            var response = await this._containerService.CreateContainerAsync(container.Name);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteContainerAsync([FromBody] string[] containerNames)
        {
            var response = await this._containerService.DeleteContainer(containerNames);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteAllContainerAsync()
        {
            var response = await this._containerService.DeleteAllContainer();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> ChangeContainerNameAsync([FromBody] ContainerUpdateRequest containerUpdateRequest)
        {
            var response = await this._containerService.ChangeContainerNameAsync(containerUpdateRequest.oldContainerName, containerUpdateRequest.newContainerName);
            return StatusCode(response.StatusCode, response);
        }
    }
}
