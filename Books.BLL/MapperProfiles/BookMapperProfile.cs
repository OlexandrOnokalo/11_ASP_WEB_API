using AutoMapper;
using Books.BLL.Dtos.Book;
using Books.DAL.Entities;

namespace Books.BLL.MapperProfiles
{
    public class BookMapperProfile : Profile
    {
        public BookMapperProfile()
        {
            // BookEntity -> BookDto
            CreateMap<BookEntity, BookDto>();

            // CreateBookDto -> BookEntity
            CreateMap<CreateBookDto, BookEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId == 0 ? null : src.AuthorId));

            // UpdateBookDto -> BookEntity
            CreateMap<UpdateBookDto, BookEntity>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId == 0 ? null : src.AuthorId));
        }
    }
}