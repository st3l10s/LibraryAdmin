using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;
using WebAPI.Resources;

namespace WebAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Author, AuthorDisplayResource>();
            CreateMap<Author, AuthorDetailResource>();

            CreateMap<Book, BookDisplayResource>();
            CreateMap<Book, BookDetailResource>();

            CreateMap<Category, CategoryDisplayResource>();
            CreateMap<Category, CategoryDetailResource>();

        }
    }
}
