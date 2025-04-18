using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDAL.Repos
{
    internal interface IRepo<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);
        
        T? Get(Int32 id);
        T? Get(QueryOptions<T> options);
        
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();

    }
}
