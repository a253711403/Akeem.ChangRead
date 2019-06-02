using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 小说信息来源
    /// </summary>
    public class NovelSource
    {
        public int Id { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebName { get; set; }
        /// <summary>
        /// 网站官网
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// 网站Code
        /// </summary>
        public string WebCode { get; set; }
        public int Status { get; set; }
    }
}
