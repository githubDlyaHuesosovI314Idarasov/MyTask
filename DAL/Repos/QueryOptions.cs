using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDAL.Repos
{
    public class QueryOptions<T>
    {

        private String[] _includes = Array.Empty<String>();

        public Expression<Func<T, Object>> OrderBy { get; set; } = null!;
        public Expression<Func<T, Object>> ThenOrderBy { get; set; } = null!;
        public Expression<Func<T, Boolean>> Where { get; set; } = null!;
        
        public String Includes { 
            set => _includes = value.Replace(" ", "").Split(','); }

        public String[] GetIncludes()
        {
            return _includes;
        }

        public Boolean HasWhere => Where != null;
        public Boolean HasOrderBy => OrderBy != null;
        public Boolean HasThenOrderBy => ThenOrderBy != null;

    }
}
