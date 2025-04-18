using Microsoft.AspNetCore.Identity;
using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Areas.AspNet.Identity.Data
{
    public sealed class ApplicationUser : IdentityUser
    {
        private String _nickname;
        private String _sex;


        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public String Nickname { get { return _nickname; } set { _nickname = value; } }

        [PersonalData]
        [Column(TypeName = "nvarchar(30)")]
        public String Sex { get { return _sex; } set { _sex = value; } }

        
    }
}
