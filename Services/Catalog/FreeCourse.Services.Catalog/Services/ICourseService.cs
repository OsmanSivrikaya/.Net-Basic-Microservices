using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    /// <summary>
    /// ICourseService arayüzü, kurs veritabanı işlemlerini tanımlar
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Tüm kursları getirir.
        /// </summary>
        /// <returns>List<CourseDto> türünde tüm kursları döner.</returns>
        Task<Response<List<CourseDto>>> GetAllAsync();

        /// <summary>
        /// Belirli bir ID'ye sahip kursu getirir.
        /// </summary>
        /// <param name="id">Kurs ID'si.</param>
        /// <returns>CourseDto türünde kursu döner.</returns>
        Task<Response<CourseDto>> GetByIdAsync(string id);

        /// <summary>
        /// Belirli bir kullanıcıya ait tüm kursları getirir.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <returns>List<CourseDto> türünde kursları döner.</returns>
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);

        /// <summary>
        /// Yeni bir kurs oluşturur.
        /// </summary>
        /// <param name="courseCreateDto">Kurs oluşturma verileri.</param>
        /// <returns>CourseDto türünde yeni kursu döner.</returns>
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);

        /// <summary>
        /// Var olan bir kursu günceller.
        /// </summary>
        /// <param name="courseUpdateDto">Kurs güncelleme verileri.</param>
        /// <returns>Güncelleme sonucunu döner.</returns>
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);

        /// <summary>
        /// Belirli bir ID'ye sahip kursu siler.
        /// </summary>
        /// <param name="id">Kurs ID'si.</param>
        /// <returns>Silme sonucunu döner.</returns>
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
