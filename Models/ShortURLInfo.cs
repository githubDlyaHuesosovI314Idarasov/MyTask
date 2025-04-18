using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public sealed class ShortUrlInfo : EntityBase
    {
        private String _createdBy = null!;
        private DateTime _createdDate;
        private Int32 _shortUrlId;
        private ShortUrl _shortUrl = null!;

        public String CreatedBy { get { return _createdBy; } set { _createdBy = value; } }
        public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; } }

        public Int32 ShortUrlId { get { return _shortUrlId; } set { _shortUrlId = value; } }

        [ForeignKey(nameof(ShortUrlId))]
        public ShortUrl ShortUrl
        {
            get { return _shortUrl; }
            set { _shortUrl = value; }
        }
    }
}
