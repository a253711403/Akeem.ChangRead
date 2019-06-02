using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    public class ResultModel
    {
        public bool success { get; set; }
        public string code { get; set; } = "00";
        public string msg { get; set; } = "";
        public object data { get; set; }
    }
    public class ResultArray<T> : ResultModel
    {
        public int total { get; set; }
        public new List<T> data { get; set; }
    }
}
