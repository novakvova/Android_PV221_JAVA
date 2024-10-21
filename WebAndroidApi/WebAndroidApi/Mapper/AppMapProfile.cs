using AutoMapper;
using WebAndroidApi.Data.Entities;
using WebAndroidApi.Models.Category;

namespace WebAndroidApi.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>();
        }
    }
}
