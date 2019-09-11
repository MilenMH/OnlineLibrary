using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data.DTOs
{
    public class UserRoleDTO
    {
        public string Email { get; set; }

        public string Role { get; set; }

        public bool ToGiveAdminRights { get; set; }
    }
}
