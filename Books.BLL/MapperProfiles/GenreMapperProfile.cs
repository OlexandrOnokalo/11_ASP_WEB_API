using AutoMapper;
using Books.BLL.Dtos.Genre;
using Books.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.BLL.MapperProfiles
{
    public class GenreMapperProfile : Profile
    {
        public GenreMapperProfile()
        {
            // GenreEntity -> GenreDto
            CreateMap<GenreEntity, GenreDto>();

            // CreateGenreDto -> GenreEntity
            CreateMap<CreateGenreDto, GenreEntity>();

            // UpdateGenreDto -> GenreEntity
            CreateMap<UpdateGenreDto, GenreEntity>();
        }
    }
}
