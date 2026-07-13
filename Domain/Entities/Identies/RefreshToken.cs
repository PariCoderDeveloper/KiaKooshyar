using KiaKooshar.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? Revoked { get; set; }
        public string Device { get; set; }
        public string IP { get; set; }
        public long UserId{ get; set; }
        public virtual User User { get; set; }
        public bool IsRevoked => Revoked != null;
        public bool IsExpired => DateTime.Now >= ExpireDate;
    }
}
