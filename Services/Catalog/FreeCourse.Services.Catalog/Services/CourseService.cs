using System.Net;
using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    /// <summary>
    /// CourseService sınıfı, kurs veritabanı işlemlerini yönetir
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        /// <summary>
        /// CourseService sınıfını başlatır.
        /// </summary>
        /// <param name="databaseSettings">Veritabanı ayarları.</param>
        /// <param name="mapper">AutoMapper nesnesi.</param>
        public CourseService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            // MongoDB client'ını ve veritabanını başlat
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            // Course ve Category koleksiyonlarını ayarla
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm kursları getirir.
        /// </summary>
        /// <returns>List<CourseDto> türünde tüm kursları döner.</returns>
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            // Tüm kursları getir
            var courses = await _courseCollection.Find(x => true).ToListAsync();

            if (courses.Any())
            {
                // Her kurs için ilgili kategoriyi getir ve ata
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                // Eğer kurs yoksa boş bir liste ata
                courses = new List<Course>();
            }

            // Kursları DTO'ya map'leyip başarılı bir yanıt döner
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kursu getirir.
        /// </summary>
        /// <param name="id">Kurs ID'si.</param>
        /// <returns>CourseDto türünde kursu döner.</returns>
        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            // Belirtilen ID'ye sahip kursu getir
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (course is null)
            {
                // Eğer kurs bulunamazsa hata döner
                return Response<CourseDto>.Fail("Course not found", (int)HttpStatusCode.NotFound);
            }

            // İlgili kategoriyi getir ve ata
            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();

            // Kursu DTO'ya map'leyip başarılı bir yanıt döner
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Belirli bir kullanıcıya ait tüm kursları getirir.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <returns>List<CourseDto> türünde kursları döner.</returns>
        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            // Belirtilen kullanıcıya ait tüm kursları getir
            var courses = await _courseCollection.Find(x => x.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                // Her kurs için ilgili kategoriyi getir ve ata
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                // Eğer kurs yoksa boş bir liste ata
                courses = new List<Course>();
            }

            // Kursları DTO'ya map'leyip başarılı bir yanıt döner
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Yeni bir kurs oluşturur.
        /// </summary>
        /// <param name="courseCreateDto">Kurs oluşturma verileri.</param>
        /// <returns>CourseDto türünde yeni kursu döner.</returns>
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            // CourseCreateDto'yu Course türüne map'ler ve oluşturma zamanını ayarlar
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;

            // Yeni kursu veritabanına ekler
            await _courseCollection.InsertOneAsync(newCourse);

            // Yeni kursu DTO'ya map'leyip başarılı bir yanıt döner
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Var olan bir kursu günceller.
        /// </summary>
        /// <param name="courseUpdateDto">Kurs güncelleme verileri.</param>
        /// <returns>Güncelleme sonucunu döner.</returns>
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            // CourseUpdateDto'yu Course türüne map'ler
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            // Belirtilen ID'ye sahip kursu günceller
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);

            if (result is null)
            {
                // Eğer kurs bulunamazsa hata döner
                return Response<NoContent>.Fail("Course not found", (int)HttpStatusCode.NotFound);
            }

            // Başarılı bir güncelleme yanıtı döner
            return Response<NoContent>.Success((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kursu siler.
        /// </summary>
        /// <param name="id">Kurs ID'si.</param>
        /// <returns>Silme sonucunu döner.</returns>
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            // Belirtilen ID'ye sahip kursu siler
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                // Başarılı bir silme yanıtı döner
                return Response<NoContent>.Success((int)HttpStatusCode.NoContent);
            }

            // Eğer kurs bulunamazsa hata döner
            return Response<NoContent>.Fail("Course not found", (int)HttpStatusCode.NotFound);
        }
    }
}
