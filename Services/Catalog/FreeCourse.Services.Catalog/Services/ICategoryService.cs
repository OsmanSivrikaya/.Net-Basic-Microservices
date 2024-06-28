using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Tüm kategorileri asenkron olarak alır.
        /// </summary>
        /// <returns>Kategorilerin listesi.</returns>
        Task<Response<List<CategoryDto>>> GetAllAsync();
         /// <summary>
        /// Yeni bir kategori oluşturur.
        /// </summary>
        /// <param name="CategoryDto">Oluşturulacak kategori.</param>
        /// <returns>Oluşturulan kategori.</returns>
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        /// <summary>
        /// ID'ye göre kategoriyi asenkron olarak alır.
        /// </summary>
        /// <param name="id">Kategori ID'si.</param>
        /// <returns>İstenen kategori.</returns>
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}