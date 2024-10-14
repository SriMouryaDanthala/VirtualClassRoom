using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class UserRoleModel
    {
        [Key]
        public Guid UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public DateTime UserRoleTimestamp { get; set; }

        public ICollection<UserModel> User { get; set; }
    }
}
