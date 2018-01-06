using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Net.Http;
using TestTaskWevService.Interfaces;
using TestTaskWevService.Models.Entities;
using Newtonsoft.Json;
using static System.String;

namespace TestTaskWevService.Providers
{
    public class RestDataProvider : IDataProvider
    {
        protected string RestUrl { get; set; }
        public RestDataProvider()
        {
            RestUrl = "http://jsonplaceholder.typicode.com";
        }       
        public async Task<IEnumerable<Album>> GetAlbumsAsync(Expression<Func<Album, bool>> condition)
        {
            return await GetDataFromRemoteServer(condition);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(Expression<Func<User, bool>> condition)
        {
            return await GetDataFromRemoteServer(condition);
        }

        private async Task<IEnumerable<T>> GetDataFromRemoteServer<T>(Expression<Func<T, bool>> condition)
        {
            var client = new HttpClient();
            var uri = new Uri($"{GetUrlFromExpression(condition)}");
            var response = await client.GetStringAsync(uri);
            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(response);
            return items;
        }

        private string GetUrlFromExpression<T>(Expression<Func<T, bool>> expression)
        {
            var lambdaBody = expression.Body;
            var entity = typeof(T).Name;
            if (lambdaBody.NodeType == ExpressionType.Equal)
                return GetUrlFromEqualExpression(entity, lambdaBody);
            if (lambdaBody.NodeType == ExpressionType.Constant)
                return GetUrlFromConstantExpression(entity, lambdaBody);
            return Empty;
         }
        private string GetUrlFromEqualExpression(string entityName, Expression body)
        {
            var binaryBody = body as BinaryExpression;
            var left = binaryBody.Left as MemberExpression;
            var right = binaryBody.Right as ConstantExpression;
            var searchFieldName = ToCamelCase(left.Member.Name);
            var searchFieldValue = right.Value;
            var rightEntityName = $"{ToCamelCase(entityName)}s";

            var url = $"{RestUrl}/{rightEntityName}?{searchFieldName}={searchFieldValue}";

            return url;
        }
        private string GetUrlFromConstantExpression(string entityName, Expression body)
        {
            var binaryBody = body as ConstantExpression;
            var rightEntityName = $"{ToCamelCase(entityName)}s";
            if (binaryBody.Type == typeof(bool))                
                return $"{RestUrl}/{rightEntityName}";

            return Empty;
        }
        private string ToCamelCase(string value)
        {
            var valueCharArray = value.ToCharArray();
            var fisrtSymbol = valueCharArray[0];
            valueCharArray[0] = char.ToLowerInvariant(fisrtSymbol);
            return new String(valueCharArray);
        }        
    }
}