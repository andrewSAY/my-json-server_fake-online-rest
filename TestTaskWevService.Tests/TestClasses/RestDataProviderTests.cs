using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskWevService.Providers;
using NUnit.Framework;

namespace TestTaskWevService.Tests.TestClasses
{   [TestFixture]
    public class RestDataProviderTests
    {
        #region preparing area
        private RestDataProvider GetProviderUnderTests()
        {
            return new RestDataProvider();
        }
        #endregion
        [Test]
        public void GetAlbumsAsync_WhereIdEquals7_ReturnsAlbumWithId7()
        {
            var provider = GetProviderUnderTests();
            var album = provider.GetAlbumsAsync(a => a.Id == 7).Result.FirstOrDefault();

            Assert.AreEqual(7, album.Id);
        }
        [Test]
        public void GetAlbumsAsync_WhereUserUserIdEquals7_Returns10Albums()
        {
            var provider = GetProviderUnderTests();
            var albums = provider.GetAlbumsAsync(a => a.UserId == 7).Result;

            Assert.AreEqual(10, albums.Count());
        }
        [Test]
        public void GetAlbumsAsync_WhereTrue_Returns100Albums()
        {
            var provider = GetProviderUnderTests();
            var albums = provider.GetAlbumsAsync(a => true).Result;

            Assert.AreEqual(100, albums.Count());
        }
        [Test]
        public void GetUsersAsync_WhereIdEquals5_ReturnsUserWihtId5()
        {
            var provider = GetProviderUnderTests();
            var user = provider.GetUsersAsync(a => a.Id == 5).Result.FirstOrDefault();

            Assert.AreEqual(5, user.Id);
        }
        [Test]
        public void GetUsersAsync_WhereTrue_Returns10Users()
        {
            var provider = GetProviderUnderTests();
            var users = provider.GetUsersAsync(a => true).Result;

            Assert.AreEqual(10, users.Count());
        }

    }
}
