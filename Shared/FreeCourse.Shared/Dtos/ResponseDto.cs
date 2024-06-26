using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    /// <summary>
    /// Genel yanıt veri transfer nesnesi.
    /// </summary>
    /// <typeparam name="T">Dönen veri türü.</typeparam>
    public class ResponseDto<T>
    {
        /// <summary>
        /// Yanıt verisi.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// HTTP durum kodu.
        /// </summary>
        [JsonIgnore]
        public int StatusCode { get; private set; }

        /// <summary>
        /// Yanıtın başarılı olup olmadığını gösterir.
        /// </summary>
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// Hata mesajları listesi.
        /// </summary>
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Başarılı bir yanıt oluşturur.
        /// </summary>
        /// <param name="data">Dönen veri.</param>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <returns>Başarılı yanıt DTO'su.</returns>
        public static ResponseDto<T> Success(T data, int statusCode) => 
            new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };

        /// <summary>
        /// Verisiz başarılı bir yanıt oluşturur.
        /// </summary>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <returns>Başarılı yanıt DTO'su.</returns>
        public static ResponseDto<T> Success(int statusCode) => 
            new ResponseDto<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };

        /// <summary>
        /// Hatalı bir yanıt oluşturur.
        /// </summary>
        /// <param name="errors">Hata mesajları listesi.</param>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <returns>Hatalı yanıt DTO'su.</returns>
        public static ResponseDto<T> Fail(List<string> errors, int statusCode) =>
            new ResponseDto<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };

        /// <summary>
        /// Tek bir hata mesajı ile hatalı bir yanıt oluşturur.
        /// </summary>
        /// <param name="error">Hata mesajı.</param>
        /// <param name="statusCode">HTTP durum kodu.</param>
        /// <returns>Hatalı yanıt DTO'su.</returns>
        public static ResponseDto<T> Fail(string error, int statusCode) =>
            new ResponseDto<T>
            {
                Errors = new List<string>(){error},
                StatusCode = statusCode,
                IsSuccessful = false
            };
    }
}
