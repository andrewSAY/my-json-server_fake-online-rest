using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Cryptography;
using TestTaskWevService.Interfaces;
using TestTaskWevService.Models.ApiModels;
using static System.Text.Encoding;
using static System.Convert;

namespace TestTaskWevService.Services
{
    public class MainService : IMainService
    {
        private IDataProvider _dataProvider;

        public MainService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task<AlbumApi> GetAlbumByIdAsync(int albumId)
        {
            var album = await _dataProvider.GetAlbumsAsync(a => a.Id == albumId);
            return album.Select(a => new AlbumApi {
                Title = a.Title
            })
            .FirstOrDefault();
        }

        public async Task<IEnumerable<AlbumApi>> GetAllAlbumsAsync()
        {
            var albums = await _dataProvider.GetAlbumsAsync(a => true);
            return albums.Select(a => new AlbumApi
            {
                Title = a.Title
            });            
        }

        public async Task<UserAlbumApi> GetAllAlbumsForUserAsync(int userId, string publicRsaBase64Key)
        {
            var user = await GetUserByIdAsync(userId, publicRsaBase64Key);
            var albums = await _dataProvider.GetAlbumsAsync(a => a.UserId == userId);
            return new UserAlbumApi {
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Albums = albums.Select(a => new AlbumApi { Title = a.Title })
            };
        }

        public async Task<IEnumerable<UserApi>> GetAllUsersAsync(string publicRsaBase64Key)
        {
            var users = await _dataProvider.GetUsersAsync(u => true);
            foreach(var user in users)
            {
                user.Email = GetEncryptedValue(user.Email, publicRsaBase64Key);
            }
            return users.Select(u => new UserApi{
                Name = u.Name,
                UserName = u.Username,
                Email = u.Email
            });
        }

        public async Task<UserApi> GetUserByIdAsync(int userId, string publicRsaBase64Key)
        {
            var users = await _dataProvider.GetUsersAsync(u => u.Id == userId);
            var user = users.FirstOrDefault();
            user.Email = GetEncryptedValue(user.Email, publicRsaBase64Key);
            return new UserApi
            {
                Name = user.Name,
                UserName = user.Username,
                Email = user.Email
            };
        }

        protected string GetEncryptedValue(string originalValue, string publicRsaBase64Key)
        {            
            var publicRsaXmlKey = UTF8.GetString(FromBase64String(publicRsaBase64Key));
            var originalValueBytes = UTF8.GetBytes(originalValue);

            var cryptoProvider = new RSACryptoServiceProvider();
            cryptoProvider.FromXmlString(publicRsaXmlKey);

            var encryptedValueBytes = cryptoProvider.Encrypt(originalValueBytes, false);
            var encryptedValue = ToBase64String(encryptedValueBytes);

            return encryptedValue;
        }
    }
}