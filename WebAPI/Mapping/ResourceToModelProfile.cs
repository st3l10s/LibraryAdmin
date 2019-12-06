using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;
using WebAPI.Resources;

namespace WebAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<AuthorSaveResource, Author>();

            CreateMap<BookSaveResource, Book>();

            CreateMap<CategorySaveResource, Category>();

            CreateMap<CategoryBookSaveResource, CategoryBook>();
        }
    }
}
