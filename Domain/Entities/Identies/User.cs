using KiaKooshar.Domain.Entities.BaseEntities;
using KiaKooshar.Domain.Enums;
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
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PasswordHash{ get; set; } = null!;
        public string Avator{ get; set; } = null!;
        public string BirthDate{ get; set; } = null!;
        public string Gender{ get; set; } = null!;
        public string NationalCode{ get; set; } = null!;
        public UserStatus Status { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public virtual  ICollection<UserRole> UserRole { get; set; } = null!;
        public virtual ICollection<RefreshToken> RefreshToken { get; set; } = null!;
        public virtual ICollection<UserSession> UserSession { get; set; } = null!;
    }
}
