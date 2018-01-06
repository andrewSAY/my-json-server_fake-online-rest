using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskWevService.Tests.Fakes;
using TestTaskWevService.Services;
using TestTaskWevService.Models.Entities;
using System.Security.Cryptography;
using NUnit.Framework;
using static System.Text.Encoding;
using static System.Convert;

namespace TestTaskWevService.Tests.TestClasses
{
    [TestFixture]
    public class MainServiceTests
    {
        #region Preparin area
        const string PUBLIC_RSA_KEY_BASE64 = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPmwvWC9mdllDcUZNeFdKZFgvV0lnQnZBRXZtYlVDSWpRWk1sYUdkbnZ0ZlR2eFR4cXZsUW9la2ZzRGdpMS9LODhqVkNabXBiSnYwVU4wTnBaRmZXWDhRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
        const string PRIVATE_RSA_KEY_BASE64 = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPmwvWC9mdllDcUZNeFdKZFgvV0lnQnZBRXZtYlVDSWpRWk1sYUdkbnZ0ZlR2eFR4cXZsUW9la2ZzRGdpMS9LODhqVkNabXBiSnYwVU4wTnBaRmZXWDhRPT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPjdhTDF0QTBRdXRFTnRSUno1YkxoOWNCc20zVktGQ2swSXhJUlloand1ODg9PC9QPjxRPm83UXBjWndBUGI5MzA3dzFtWENpd3l4QWNZdTFZeHUrelJFQUIvVEtvRDg9PC9RPjxEUD5wMnBtUllucTNtS0hrS292R0lWVThjaTErek8vazhLUGk2R2dBNGRrbWpzPTwvRFA+PERRPklBdy9LTXlmaDNjYVlFc3lPdzIvNzNHVVZDWkRwbGxpS3djdStGL0Y0MFU9PC9EUT48SW52ZXJzZVE+Qm5RbE1qZ1JJVTBXRnNUbFRXazEvRTEwazZjV0YxcGVIZEdwM213TjRqdz08L0ludmVyc2VRPjxEPklzK0tvT1h3UStrUzQ4MS9yWjdkNFZqNGJUbVhGd0ZvWTc0d2NyMlhDU0pnSG05OEZOdllUUDlueG5aYVZUTkZCZmFSVGRuNVhlWVR3R3pJUFNWQjlRPT08L0Q+PC9SU0FLZXlWYWx1ZT4=";
        private FakeDataProvider GetDataProvider()
        {
            var provider = new FakeDataProvider();
            provider.Users.Add(new User { Id = 1, Name = "First User", Username = "firstuser", Email = "firstuser@mail.ru" });
            provider.Users.Add(new User { Id = 2, Name = "Second User", Username = "seconduser", Email = "seconduser@mail.ru" });
            provider.Albums.Add(new Album { Id = 1, UserId = 1, Title = "Album1" });
            provider.Albums.Add(new Album { Id = 2, UserId = 2, Title = "Album2" });
            provider.Albums.Add(new Album { Id = 3, UserId = 1, Title = "Album3" });

            return provider;
        }
        private MainService GetServiceUnderTests(FakeDataProvider dataProvider)
        {
            return new MainService(dataProvider);
        }
        #endregion
        [Test]
        public void GetAlbumByIdAsync_Always_ReturnAlbumWithExpectedTitle()
        {
            var dataProvider = GetDataProvider();
            var service = GetServiceUnderTests(dataProvider);

            var album = service.GetAlbumByIdAsync(3).Result;
            var expectedTitle = "Album3";

            Assert.AreEqual(expectedTitle, album.Title);
        }
        [Test]
        public void GetAllAlbumsForUserAsync_Always_ReturnedObjectHasExpectedAlbumCount()
        {
            var dataProvider = GetDataProvider();
            var service = GetServiceUnderTests(dataProvider);

            var albumsOnUser = service.GetAllAlbumsForUserAsync(1, PUBLIC_RSA_KEY_BASE64).Result;
            var expectedAlbumsCount = 2;

            Assert.AreEqual(expectedAlbumsCount, albumsOnUser.Albums.Count());
        }
        [Test]
        public void GetAllUsersAsync_Always_ReturnedExpectedUsersCount()
        {
            var dataProvider = GetDataProvider();
            var service = GetServiceUnderTests(dataProvider);

            var users = service.GetAllUsersAsync(PUBLIC_RSA_KEY_BASE64).Result;
            var expectedUsersCount = 2;

            Assert.AreEqual(expectedUsersCount, users.Count());
        }
        [Test]
        public void GetUserByIdAsync_Always_ReturnedUserHasExpectedUserName()
        {
            var dataProvider = GetDataProvider();
            var service = GetServiceUnderTests(dataProvider);

            var user = service.GetUserByIdAsync(2, PUBLIC_RSA_KEY_BASE64).Result;
            var expectedUsersname = "seconduser";

            Assert.AreEqual(expectedUsersname, user.UserName);
        }
        [Test]
        public void GetUserByIdAsync_WithoutDecryptingEmail_ReturnedUserHasEmailNotEqualExpectedOne()
        {
            var dataProvider = GetDataProvider();
            var service = GetServiceUnderTests(dataProvider);

            var user = service.GetUserByIdAsync(2, PUBLIC_RSA_KEY_BASE64).Result;
            var expectedEmail = "seconduser@mail.ru";

            Assert.AreNotEqual(expectedEmail, user.Email);
        }
        [Test]
        public void GetUserByIdAsync_AftertDecryptingEmail_ReturnedUserHasEmailEqualExpectedOne()
        {
            var dataProvider = GetDataProvider();
            var service = GetServiceUnderTests(dataProvider);

            var user = service.GetUserByIdAsync(2, PUBLIC_RSA_KEY_BASE64).Result;
            var privateRsaXmlKey = UTF8.GetString(FromBase64String(PRIVATE_RSA_KEY_BASE64));

            var cryptoProvider = new RSACryptoServiceProvider();
            cryptoProvider.FromXmlString(privateRsaXmlKey);
            var encryptedEmailBytes = FromBase64String(user.Email);
            var decryptedEmailBytes = cryptoProvider.Decrypt(encryptedEmailBytes, false);
            var returnedEmail = UTF8.GetString(decryptedEmailBytes);

            var expectedEmail = "seconduser@mail.ru";

            Assert.AreEqual(expectedEmail, returnedEmail);
        }
    }
}
