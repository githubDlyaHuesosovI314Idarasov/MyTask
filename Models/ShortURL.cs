using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Base;

namespace Models
{
    public sealed class ShortUrl : EntityBase
    {
        private String _url = null!;
        private String _short = null!;
        private ShortUrlInfo _shortUrlInfo = null!;


        public String Url { get { return _url; } set { _url = value; } }
        [ValidateNever]
        public String Short { get { return _short; } set { _short = value; } }
        [ValidateNever]
        public ShortUrlInfo ShortUrlInfo { get { return _shortUrlInfo; } set { _shortUrlInfo = value; } }

    }
}
