using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models.FormData
{
    public class QueryArrayModel
    {
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 1;
        public string Code { get; set; }
    }
}
