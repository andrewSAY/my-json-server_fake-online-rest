using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestTaskWevService.Models.Entities;

namespace TestTaskWevService.Interfaces
{
    public interface IDataProvider
    {
        /// <summary>
        /// Возвращаеи всех пользователей, соответсвующих условию
        /// </summary>
        /// <param name="condition">Выраженеи, содержащее делегат, содержащий условие отбора</param>
        /// <returns>Список пользователей</returns>
       Task<IEnumerable<User>> GetUsersAsync(Expression<Func<User, bool>> condition);
        /// <summary>
        /// Возвращаеи все альбомы, соответсвующих условию
        /// </summary>
        /// <param name="condition">Выраженеи, содержащее делегат, содержащий условие отбора</param>
        /// <returns></returns>
        Task<IEnumerable<Album>> GetAlbumsAsync(Expression<Func<Album, bool>> condition);
    }
}
