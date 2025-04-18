using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Base
{
    public class EntityBase
    {
        [Key]
        public Int32 Id { get; set; }

    }
}
