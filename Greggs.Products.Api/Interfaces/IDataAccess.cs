using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Interfaces
{
    public interface IDataAccess<T>
    {
        Task<IEnumerable<T>> ListAsync(int? pageStart, int? pageSize, CancellationToken token);

        IEnumerable<T> List(int? pageStart, int? pageSize);


    }
}