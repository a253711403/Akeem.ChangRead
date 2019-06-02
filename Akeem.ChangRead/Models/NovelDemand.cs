using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 小说愿望单
    /// </summary>
    public class NovelDemand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Memo { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 1、初次提交
        /// 2、已受理
        /// 3、已完成
        /// 4、违规
        /// </summary>
        public int Status { get; set; }
    }
}
