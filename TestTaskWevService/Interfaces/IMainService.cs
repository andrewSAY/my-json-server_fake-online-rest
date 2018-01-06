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
        /// <param name="publicRsaKey">Открытая часть ключа широфания RSA</param>
        /// <returns></returns>
        IEnumerable<UserApi> GetAllUsers(string publicRsaKey);
        /// <summary>
        /// Возвращает данные пользователя по его идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="publicRsaKey">Открытая часть ключа широфания RSA</param>
        /// <returns></returns>
        UserApi GetUserById(int userId, string publicRsaKey);
        /// <summary>
        /// Возвращает список всех альбомов
        /// </summary>
        /// <returns></returns>
        IEnumerable<AlbumApi> GetAllAlbums();
        /// <summary>
        /// Возвращает список всех альбомов одного пользователя c зашифрованным email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="publicRsaKey">Открытая часть ключа широфания RSA</param>
        /// <returns></returns>
        IEnumerable<UserAlbumApi> GetAllAlbumsForUser(int userId, string publicRsaKey);
        /// <summary>
        /// Возвращает иалюбом по его идентификатору
        /// </summary>
        /// <param name="albumId">Идентификатор альбома</param>
        /// <returns></returns>
        AlbumApi GetAlbumById(int albumId);
    }
}
