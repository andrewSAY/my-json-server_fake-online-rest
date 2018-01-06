using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestTaskWevService.Interfaces;
using TestTaskWevService.Models.Entities;

namespace TestTaskWevService.Tests.Fakes
{
    class FakeDataProvider : IDataProvider
    {
        public readonly List<Album> Albums = new List<Album>();
        public readonly List<User> Users = new List<User>();
        public async Task<IEnumerable<Album>> GetAlbumsAsync(Expression<Func<Album, bool>> condition)
        {
            return Get(condition, Albums);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(Expression<Func<User, bool>> condition)
        {
            return Get(condition, Users);
        }

        private List<T> Get<T>(Expression<Func<T, bool>> condition, IEnumerable<T> collection)
        {
            var outputList = new List<T>();
            var conditionDelegate = condition.Compile();
            foreach (var item in collection)
            {
                if (conditionDelegate(item))
                    outputList.Add(item);
            }
            return outputList;
        }
    }
}
