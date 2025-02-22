using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Interfaces
{
    public interface IRepositoryToFind<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetFromId(long id);
    }
}
