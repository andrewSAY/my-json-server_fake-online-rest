using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskWevService.Models.ApiModels;

namespace TestTaskWevService.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий основные методы сервиса.    
    /// </summary>
    public interface IMainService
    {
        /// <summary>
        /// Возвращает список всех пользователей c зашифрованным email
        /// </summary>
        /// <param name="publicRsaBase64Key">Открытая часть ключа широфания RSA(xml), конвертированная в bse64</param>
        /// <returns></returns>
        Task<IEnumerable<UserApi>> GetAllUsersAsync(string publicRsaBase64Key);
        /// <summary>
        /// Возвращает данные пользователя по его идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="publicRsaBase64Key">Открытая часть ключа широфания RSA(xml), конвертированная в bse64, конвертированная в bse64</param>
        /// <returns></returns>
        Task<UserApi> GetUserByIdAsync(int userId, string publicRsaBase64Key);
        /// <summary>
        /// Возвращает список всех альбомов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AlbumApi>> GetAllAlbumsAsync();
        /// <summary>
        /// Возвращает список всех альбомов одного пользователя c зашифрованным email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="publicRsaBase64Key">Открытая часть ключа широфания RSA(xml), конвертированная в bse64</param>
        /// <returns></returns>
        Task<UserAlbumApi> GetAllAlbumsForUserAsync(int userId, string publicRsaBase64Key);
        /// <summary>
        /// Возвращает иалюбом по его идентификатору
        /// </summary>
        /// <param name="albumId">Идентификатор альбома</param>
        /// <returns></returns>
        Task<AlbumApi> GetAlbumByIdAsync(int albumId);
    }
}
