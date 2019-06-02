using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models.FormData
{
    public class SingIn
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
        public string Verify { get; set; }
    }

    public class SingUp: SingIn
    {
        public string Image { get; set; }
        [Required]
        public int? Level { get; set; } 
        [Required]
        public string Roles { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class EditUser 
    {
        [Required]
        public int? Id { get; set; }
        public string Image { get; set; }
        [Required]
        public int? Level { get; set; }
        [Required]
        public string Roles { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Status { get; set; }
    }

}
