using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTaskWevService.Models.ApiModels
{
    public class UserAlbumApi: UserApi
    {
        IEnumerable<AlbumApi> Albums;
    }
}