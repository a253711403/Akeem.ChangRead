using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 网站关键字替换
    /// </summary>
    public class KeywordReplace
    {
        public int Id { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 替换成什么
        /// </summary>
        public string Replace { get; set; }
        public int SourceWeb { get; set; }

    }
}
