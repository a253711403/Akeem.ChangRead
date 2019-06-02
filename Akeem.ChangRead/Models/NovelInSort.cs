using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 小说分类关联表
    /// </summary>
    public class NovelInSort
    {
        public int Id { get; set; }
        /// <summary>
        /// 小说ID
        /// </summary>
        public int NovelId { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int SortId { get; set; }
    }
}
