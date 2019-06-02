using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 章节表
    /// 第一次从网站或者文件获取并保存至Redis
    /// 第二次从Redis读取
    /// </summary>
    public class Chapter
    {
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径 
        /// 小说内容路径或者本地文件路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 小说Id
        /// </summary>
        public int NovelId { get; set; }
        /// <summary>
        /// 来源网站ID
        /// </summary>
        public int SourceWeb { get; set; }
        /// <summary>
        /// 1 直接从网上获取 
        /// 2 来源于单独文件
        /// </summary>
        public int FileType { get; set; }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int Content { get; set; }
    }
}
