using NotificationsApi.V1.Boundary.Response;
using NotificationsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using NotificationsApi.V1.Boundary.Request;

namespace NotificationsApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/notifications")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class NotificationsApiController : BaseController
    {
        private readonly IGetAllNotificationCase _getAllNotificationCase;
        private readonly IGetByIdNotificationCase _getByIdNotificationCase;
        private readonly IAddNotificationUseCase _addNotificationUseCase;
        private readonly IUpdateNotificationUseCase _updateNotificationUseCase;
        private readonly IGetTargetDetailsCase _getTargetDetailsCase;
        public NotificationsApiController(IGetAllNotificationCase getAllNotificationCase, IGetByIdNotificationCase getByIdNotificationCase,
                                           IAddNotificationUseCase addNotificationUseCase, IUpdateNotificationUseCase updateNotificationUseCase,
                                           IGetTargetDetailsCase getTargetDetailsCase)
        {
            _getAllNotificationCase = getAllNotificationCase;
            _getByIdNotificationCase = getByIdNotificationCase;
            _addNotificationUseCase = addNotificationUseCase;
            _updateNotificationUseCase = updateNotificationUseCase;
            _getTargetDetailsCase = getTargetDetailsCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(NotificationResponseObjectList), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> ListNotificationAsync()
        {
            return Ok(await _getAllNotificationCase.ExecuteAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">No ? found for the specified ID</response>
        /// 
        [ProducesResponseType(typeof(NotificationResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{targetId}")]
        public async Task<IActionResult> GetAsync(Guid targetId)
        {
            var result = await _getByIdNotificationCase.ExecuteAsync(targetId).ConfigureAwait(false);
            if (result == null)
                return NotFound();

            return Ok(result);
            //return Ok(await _getTargetDetailsCase.ExecuteAsync(id).ConfigureAwait(false));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] NotificationRequest request)
        {

            var result = await _addNotificationUseCase.ExecuteAsync(request).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetAsync), new { id = result });

        }

        [ProducesResponseType(typeof(ActionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch]
        [Route("{targetId}")]
        public async Task<IActionResult> UpdateAsync(Guid targetId, [FromBody] AppprovalRequest request)
        {
            var result = await _getByIdNotificationCase.ExecuteAsync(targetId).ConfigureAwait(false);
            if (result == null)
                return NotFound();
            var updateResult = await _updateNotificationUseCase.ExecuteAsync(targetId, request).ConfigureAwait(false);
            if (updateResult.Status)
                return Ok(updateResult);

            return BadRequest(updateResult);

        }
    }
}
