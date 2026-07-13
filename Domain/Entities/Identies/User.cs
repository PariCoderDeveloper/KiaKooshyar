using KiaKooshar.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash{ get; set; }
        public string Avator{ get; set; }
        public string BirthDate{ get; set; }
        public string Gender{ get; set; }
        public string NationalCode{ get; set; }
        public string Status{ get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public virtual  ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
        public virtual ICollection<UserSession> UserSession { get; set; }
    }
}
