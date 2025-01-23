using AutoMapper;
using Million.Application.Features.Properties.Commands.CreateProperty;
using Million.Application.Features.Properties.ViewModels;
using Million.Domain.Features.Properties.Entities;

namespace Million.Application.Features.Properties.Mappings
{
    internal class PropertyMapping: Profile
    {
        public PropertyMapping()
        {
            CreateMap<CreatePropertyCommand, Property>()
                .ForMember(x=> x.PropertyImages, x=> x.MapFrom(y=> new List<PropertyImage> { new PropertyImage
                {
                    File = y.Image.File,
                } }));

            CreateMap<Property, PropertyVm>();

            CreateMap<Property, PropertyDetailVm>()
                .ForMember(x => x.Image, x => x.MapFrom(y => y.PropertyImages.Any() ? y.PropertyImages.First().File : null));
        }
    }
}
