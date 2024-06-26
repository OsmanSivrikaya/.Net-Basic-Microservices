using AutoMapper;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using MongoDB.Driver;
using FreeCourse.Shared.Dtos;
using FreeCourse.Services.Catalog.Dtos;
using System.Net;

namespace FreeCourse.Services.Catalog.Services
{
    internal class CategoryService : ICategorService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), (int)HttpStatusCode.OK);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category){
            await _categoryCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), (int)HttpStatusCode.OK);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id){
            var category = await _categoryCollection.Find<Category>(x=>x.Id == id).FirstOrDefaultAsync();
            if(category is null)
                return Response<CategoryDto>.Fail("Category not found", (int)HttpStatusCode.NotFound);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), (int)HttpStatusCode.OK);
        }
    }
}