using Books.BLL.Dtos.Book;
using Books.DAL.Entities;
using Books.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.BLL.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        private readonly ImageService _imageService;

        public BookService(BookRepository bookRepository, ImageService imageService)
        {
            _bookRepository = bookRepository;
            _imageService = imageService;
        }

        public async Task<ServiceResponse> CreateAsync(CreateBookDto dto, string imagesPath)
        {
            var entity = new BookEntity
            {
                Title = dto.Title,
                Description = dto.Description,                
                Rating = dto.Rating,
                Pages = dto.Pages,
                PublishYear = dto.PublishYear,
                AuthorId = dto.AuthorId == 0 ? null : dto.AuthorId
            };

            if (dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                ServiceResponse response = await _imageService.SaveAsync(dto.Image, imagesPath);

                if (!response.Success)
                {
                    return response;
                }

                entity.Image = response.Payload!.ToString()!;
            }

            bool res = await _bookRepository.CreateAsync(entity);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Не вдалося додати книгу"
                };
            }

            return new ServiceResponse
            {
                Message = $"Книга '{entity.Title}' успішно додана",
                Payload = new BookDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Pages = entity.Pages,
                    PublishYear = entity.PublishYear,
                    AuthorId = entity.AuthorId
                }
            };
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateBookDto dto, string imagesPath)
        {
            var entity = await _bookRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Книги з id {dto.Id} не існує"
                };
            }

            string oldTitle = entity.Title;
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Rating = dto.Rating;
            entity.Pages = dto.Pages;
            entity.PublishYear = dto.PublishYear;
            entity.AuthorId = dto.AuthorId == 0 ? null : dto.AuthorId;

            if (dto.Image != null && !string.IsNullOrEmpty(imagesPath))
            {
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    string imagePath = Path.Combine(imagesPath, entity.Image);
                    var deleteResponse = _imageService.Delete(imagePath);

                    if (!deleteResponse.Success)
                    {
                        return deleteResponse;
                    }
                }

                var saveResponse = await _imageService.SaveAsync(dto.Image, imagesPath);

                if (!saveResponse.Success)
                {
                    return saveResponse;
                }

                entity.Image = saveResponse.Payload!.ToString()!;
            }

            bool res = await _bookRepository.UpdateAsync(entity);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Не вдалося оновити книгу"
                };
            }

            return new ServiceResponse
            {
                Message = $"Книга '{oldTitle}' успішно оновлена",
                Payload = new BookDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Pages = entity.Pages,
                    PublishYear = entity.PublishYear,
                    AuthorId = entity.AuthorId
                }
            };
        }

        public async Task<ServiceResponse> DeleteAsync(int id, string imagesPath)
        {
            var entity = await _bookRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Книги з id {id} не існує"
                };
            }

            if (!string.IsNullOrEmpty(entity.Image))
            {
                string imagePath = Path.Combine(imagesPath, entity.Image);
                var response = _imageService.Delete(imagePath);

                if (!response.Success)
                {
                    return response;
                }
            }

            bool res = await _bookRepository.DeleteAsync(entity);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Не вдалося видалити книгу"
                };
            }

            return new ServiceResponse
            {
                Message = $"Книга '{entity.Title}' успішно видалена",
                Payload = new BookDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Pages = entity.Pages,
                    PublishYear = entity.PublishYear,
                    AuthorId = entity.AuthorId
                }
            };
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _bookRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Книги з id {id} не існує"
                };
            }

            return new ServiceResponse
            {
                Message = "Книга успішно отримана",
                Payload = new BookDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Image = entity.Image,
                    Rating = entity.Rating,
                    Pages = entity.Pages,
                    PublishYear = entity.PublishYear,
                    AuthorId = entity.AuthorId
                }
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var dtos = await _bookRepository.Books
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.Image,
                    Rating = b.Rating,
                    Pages = b.Pages,
                    PublishYear = b.PublishYear,
                    AuthorId = b.AuthorId
                })
                .ToListAsync();

            return new ServiceResponse
            {
                Message = "Книги отримано",
                Payload = dtos
            };
        }
    }
}
