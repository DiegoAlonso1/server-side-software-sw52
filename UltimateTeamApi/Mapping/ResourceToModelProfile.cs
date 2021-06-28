using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePersonResource, Person>();
            CreateMap<SaveGroupResource, Group>();
            CreateMap<SaveGroupMemberResource, Group>();
            CreateMap<SaveAdministratorResource, Administrator>();
            CreateMap<SaveSubscriptionBillResource, SubscriptionBill>();
            CreateMap<SaveSubscriptionTypeResource, SubscriptionType>();
            CreateMap<SaveSessionResource, Session>();
            CreateMap<SaveSessionParticipantResource, SessionParticipant>();
        }
    }
}
