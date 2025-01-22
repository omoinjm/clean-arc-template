using AutoMapper;
using Clean.Architecture.Template.Application.Response;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Application.Response.Menu;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Entity.Common;
using Clean.Architecture.Template.Core.Results;
using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Application.Mapper
{
    public class ConfigureMappingProfile : Profile
    {
        public ConfigureMappingProfile()
        {
            // Common
            CreateMap<UpdateResponse, UpdateRecordResult>().ReverseMap();
            CreateMap<CreateResponse, CreateRecordResult>().ReverseMap();
            CreateMap<DeleteResponse, DeleteRecordResult>().ReverseMap();

            // Lookups
            CreateMap<LookupEntity, LookupResponse>().ReverseMap();
            CreateMap<GeneralSpecParams, LookupParams>().ReverseMap();
            CreateMap<DataList<LookupEntity>, DataList<LookupResponse>>().ReverseMap();

            // Users
            CreateMap<UserEntity, UserResponse>().ReverseMap();
            CreateMap<Pagination<UserEntity>, Pagination<UserResponse>>().ReverseMap();

            // Menu
            CreateMap<MenuEntity, MenuResponse>().ReverseMap();
        }
    }
}