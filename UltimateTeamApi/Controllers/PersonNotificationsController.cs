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
    [Route("api/persons/{personId}/notifications")]
    [Produces("application/json")]
    [ApiController]
    public class PersonNotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public PersonNotificationsController(INotificationService notificationService, IMapper mapper, IPersonService personService)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _personService = personService;
        }



        /*****************************************************************/
        /*LIST OF NOTIFICATIONS SENT BY PERSON ID*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Get All Notifications Sent By Person Id",
         Description = "Get All Notifications Sent By Person Id",
         OperationId = "GetAllNotificationsSentByPersonId")]
        [SwaggerResponse(200, "List of Notifications Sent By Person Id", typeof(IEnumerable<NotificationResource>))]

        [HttpGet("sent")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<NotificationResource>> GetAllNotificationsSentByPersonIdAsync(int personId)
        {
            var notifications = await _notificationService.GetAllBySenderIdAsync(personId);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
            return resources;
        }

        /*****************************************************************/
        /*LIST OF NOTIFICATIONS RECEIVED BY PERSON ID*/
        /*****************************************************************/

        [SwaggerOperation(
         Summary = "Get All Notifications Received By Person Id",
         Description = "Get All Notifications Receeived By Person Id",
         OperationId = "GetAllNotificationsReceivedByPersonId")]
        [SwaggerResponse(200, "List of Notifications Received By Person Id", typeof(IEnumerable<NotificationResource>))]

        [HttpGet("received")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<NotificationResource>> GetAllNotificationsRecievedByPersonIdAsync(int personId)
        {
            var notifications = await _notificationService.GetAllByRemitendIdAsync(personId);
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
            return resources;
        }

        /*****************************************************************/
        /*GET NOTIFICATION BY ID AND PERSON ID*/
        /*****************************************************************/
        [SwaggerOperation(
          Summary = "Get Notification by Id and Person Id",
          Description = "Get Notification by Id and Person Id",
          OperationId = "GetNotificationByIdAndPersonId")]
        [SwaggerResponse(200, "Get Notification by Id and Person Id", typeof(NotificationResource))]

        [HttpGet("{notificationId}")]
        [ProducesResponseType(typeof(NotificationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByPersonIdAndNotificationIdAsync(int personId, int notificationId)
        {
            var result = await _notificationService.GetByIdAndPersonIdAsync(personId, notificationId);
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
        public async Task<IActionResult> PostAsync(int personId, [FromBody] SaveNotificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);
            var result = await _notificationService.SaveAsync(personId, notification);

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
        public async Task<IActionResult> DeleteAsync(int personId, int notificationId)
        {
            var result = await _notificationService.DeleteAsync(personId, notificationId);
            if (!result.Success)
                return BadRequest(result.Message);
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
            return Ok(notificationResource);
        }
    }
}
