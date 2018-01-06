using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using TestTaskWevService.Interfaces;
using TestTaskWevService.Models.ApiModels;

namespace TestTaskWevService.Controllers
{
    public class MainServiceController : ApiController
    {
        private readonly IMainService _service;
        public MainServiceController(IMainService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<UserApi>> GetAllUsers(string publicKey)
        {
            return await _service.GetAllUsersAsync(publicKey);
        }
        public async Task<UserApi> GetUserById(int userId, string publicKey)
        {
            return await _service.GetUserByIdAsync(userId, publicKey);
        }
        public async Task<IEnumerable<AlbumApi>> GetAllAlbums()
        {
            return await _service.GetAllAlbumsAsync();
        }
        public async Task<UserAlbumApi> GetAllAlbumsForUser(int userId, string publicKey)
        {
            return await _service.GetAllAlbumsForUserAsync(userId, publicKey);
        }
        public async Task<AlbumApi> GetAlbumById(int albumId)
        {
            return await _service.GetAlbumByIdAsync(albumId);
        }
    }
}