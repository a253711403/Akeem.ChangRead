using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models.FormData
{
    public class ConfigEdit: ConfigAdd
    {
        [Required]
        public int? Status { get; set; }
        [Required]
        public int? Id { get; set; }
    }
    public class ConfigAdd
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public string Memo { get; set; }
       
    }

}
