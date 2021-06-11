using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Extensions;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/users/{userId}/notifications")]
    [Produces("application/json")]
    [ApiController]
    public class UserNotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserNotificationsController(INotificationService notificationService, IMapper mapper, IUserService userService)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _userService = userService;
        }

        /*****************************************************************/
        /*LIST OF NOTIFICATIONS SENT BY USER ID*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "List Notifications Sent By User Id",
         Description = "List Notifications Sent By User Id",
         OperationId = "ListNotificationsSentByUserId")]
        [SwaggerResponse(200, "List of Notifications Sent By User Id", typeof(IEnumerable<NotificationResource>))]

        [HttpGet("sent")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<NotificationResource>> GetAllNotificationsSentByUserIdAsync(int userId)
        {
            var notifications = await _notificationService.GetAllBySenderIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
            return resources;
        }

        /*****************************************************************/
        /*LIST OF NOTIFICATIONS RECEIVED BY USER ID*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "List Notifications Received By User Id",
         Description = "List Notifications Receeived By User Id",
         OperationId = "ListNotificationsReceivedByUserId")]
        [SwaggerResponse(200, "List of Notifications Received By User Id", typeof(IEnumerable<NotificationResource>))]

        [HttpGet("received")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<NotificationResource>> GetAllNotificationsRecievedByUserIdAsync(int userId)
        {
            var notifications = await _notificationService.GetAllByRemitendIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
            return resources;
        }

        /*****************************************************************/
        /*GET NOTIFICATION BY ID AND USER ID*/
        /*****************************************************************/
        [SwaggerOperation(
          Summary = "Get Notification by Id and User Id",
          Description = "Get Notification by Id and User Id",
          OperationId = "GetNotificationByIdAndUserId")]
        [SwaggerResponse(200, "Get Notification by Id and User Id", typeof(NotificationResource))]

        [HttpGet("{notificationId}")]
        [ProducesResponseType(typeof(NotificationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByUserIdAndNotificationIdAsync(int userId, int notificationId)
        {
            var result = await _notificationService.GetByIdAndUserIdAsync(userId, notificationId);
            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
            return Ok(notificationResource);
        }

        /*****************************************************************/
        /*SAVE NOTIFICATIONS*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Save Notification",
         Description = "Save Notification",
         OperationId = "SaveNotification")]
        [SwaggerResponse(200, "Save Notification", typeof(NotificationResource))]

        [HttpPost]
        [ProducesResponseType(typeof(NotificationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveNotificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);
            var result = await _notificationService.SaveAsync(userId, notification);

            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
            return Ok(notificationResource);

        }

        /******************************************/
        /*DELETE NOTIFICATION*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Delete Notification",
           Description = "Delete a Notification",
           OperationId = "DeleteNotification")]
        [SwaggerResponse(200, "Notification Deleted", typeof(NotificationResource))]

        [HttpDelete("{notificationId}")]
        [ProducesResponseType(typeof(NotificationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int userId, int notificationId)
        {
            var result = await _notificationService.DeleteAsync(userId, notificationId);
            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
            return Ok(notificationResource);
        }
    }
}
