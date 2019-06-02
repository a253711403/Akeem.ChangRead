using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 小说信息
    /// </summary>
    public class NovelInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 小说备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 来源网站ID
        /// </summary>
        public int SourceWeb { get; set; }
        /// <summary>
        /// 本地图片路径
        /// </summary>
        public string ImageUrl { get; set; }
        public int Status { get; set; }
    }
}
