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
        [TestCase(15)]
        [TestCase(7)]
        public void GetAlbumsAsync_WhereIdEqualsExcepted_ReturnsAlbumWithTheSameId(int albumId)
        {
            var provider = GetProviderUnderTests();
            var album = provider.GetAlbumsAsync(a => a.Id == albumId).Result.FirstOrDefault();

            Assert.AreEqual(albumId, album.Id);
        }
        [Test]        
        [TestCase(7, 10)]
        public void GetAlbumsAsync_WhereUserUserIdEqualsExcepted_ReturnsExceptedAlbumsCount(int exceptedAlbumId, int exceptedAlbumsCount)
        {
            var provider = GetProviderUnderTests();
            var albums = provider.GetAlbumsAsync(a => a.UserId == exceptedAlbumId).Result;

            Assert.AreEqual(exceptedAlbumsCount, albums.Count());
        }
        [Test]
        public void GetAlbumsAsync_WhereTrue_Returns100Albums()
        {
            var provider = GetProviderUnderTests();
            var albums = provider.GetAlbumsAsync(a => true).Result;

            Assert.AreEqual(100, albums.Count());
        }
        [Test]
        [TestCase(5)]
        public void GetUsersAsync_WhereIdEqualsExcepted_ReturnsUserWihtTheSameId(int userId)
        {
            var provider = GetProviderUnderTests();
            var user = provider.GetUsersAsync(a => a.Id == userId).Result.FirstOrDefault();

            Assert.AreEqual(userId, user.Id);
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
