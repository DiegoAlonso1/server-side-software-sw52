using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/administrators/{administratorId}/users")]
    [Produces("application/json")]
    [ApiController]
    public class AdministratorUsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdministratorUsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /******************************************/
            /*GET ALL USERS BY ADMIN ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Users By Administrator Id",
            Description = "Get List of All Users By Administrator Id",
            OperationId = "GetAllUsersByAdministratorId")]
        [SwaggerResponse(200, "List of Users By Administrator Id", typeof(IEnumerable<UserResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<UserResource>> GetAllByAdministratorIdAsync(int administratorId)
        {
            var users = await _userService.GetAllByAdministratorIdAsync(administratorId);
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }


    }

}
