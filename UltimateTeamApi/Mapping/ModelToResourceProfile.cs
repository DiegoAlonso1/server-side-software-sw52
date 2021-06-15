using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<Functionality, FunctionalityResource>();
            CreateMap<SessionStadistic, SessionStadisticResource>();
            CreateMap<Group, GroupResource>();
            CreateMap<Group, GroupMemberResource>();
            CreateMap<Administrator, AdministratorResource>();
            CreateMap<SubscriptionBill, SubscriptionBillResource>();
            CreateMap<SubscriptionType, SubscriptionTypeResource>();
        }
    }
}
