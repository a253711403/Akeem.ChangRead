using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public string Roles { get;  set; }
    }
}
