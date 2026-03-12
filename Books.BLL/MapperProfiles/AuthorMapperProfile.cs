using AutoMapper;
using Books.BLL.Dtos.Author;
using Books.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.BLL.MapperProfiles
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile()
        {
            // AuthorEntity -> AuthorDto
            CreateMap<AuthorEntity, AuthorDto>();

            // CreateAuthorDto -> AuthorEntity
            CreateMap<CreateAuthorDto, AuthorEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            // UpdateAuthorDto -> AuthorEntity
            CreateMap<UpdateAuthorDto, AuthorEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
