using AutoMapper;
using xSdk.Data.Converters.Mapper;

namespace xSdk.Data.Fakes
{
    internal class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            this.CreateMap<TestEntity, TestModel>()
                .ForMember(x => x.PrimaryKey, opts => opts.Ignore())
                .ForMember(x => x.Id, opts => opts.ConvertUsing(new PKGuidToString()))
                .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Age, opts => opts.MapFrom(x => x.Age))
                .ReverseMap()
                .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Age, opts => opts.MapFrom(x => x.Age))
                .ForMember(x => x.Id, opts => opts.ConvertUsing(new PKStringToGuid()))
                .ForMember(x => x.PrimaryKey, opts => opts.Ignore());
        }
    }
}
