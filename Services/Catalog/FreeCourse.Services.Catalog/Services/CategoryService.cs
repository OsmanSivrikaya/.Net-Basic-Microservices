using AutoMapper;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using MongoDB.Driver;
using FreeCourse.Shared.Dtos;
using FreeCourse.Services.Catalog.Dtos;
using System.Net;

namespace FreeCourse.Services.Catalog.Services
{
    /// <summary>
    /// Kategori servisi. Kategori işlemleri için metotlar sağlar.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        /// <summary>
        /// Kategori servisini başlatır.
        /// </summary>
        /// <param name="databaseSettings">Veritabanı ayarları.</param>
        /// <param name="mapper">AutoMapper nesnesi.</param>
        public CategoryService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            // MongoDB client'ını ve veritabanını başlat
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            // Kategori koleksiyonunu ayarla
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm kategorileri asenkron olarak alır.
        /// </summary>
        /// <returns>Kategorilerin listesi.</returns>
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            // Kategorileri MongoDB'den al ve DTO'ya dönüştür
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Yeni bir kategori oluşturur.
        /// </summary>
        /// <param name="category">Oluşturulacak kategori.</param>
        /// <returns>Oluşturulan kategori.</returns>
        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            // Yeni kategoriyi MongoDB'ye ekle
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(categoryDto));
            return Response<CategoryDto>.Success(categoryDto, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// ID'ye göre kategoriyi asenkron olarak alır.
        /// </summary>
        /// <param name="id">Kategori ID'si.</param>
        /// <returns>İstenen kategori.</returns>
        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            // Verilen ID'ye göre kategoriyi MongoDB'den al
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if (category is null)
                return Response<CategoryDto>.Fail("Category not found", (int)HttpStatusCode.NotFound);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), (int)HttpStatusCode.OK);
        }
    }
}
