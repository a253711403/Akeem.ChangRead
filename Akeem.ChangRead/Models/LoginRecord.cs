using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Models
{
    /// <summary>
    /// 登录记录表
    /// </summary>
    public class LoginRecord
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 登录类型 
        /// 1 后台登录
        /// 2 微信登录
        /// 3 App登录
        /// </summary>
        public int LoginType { get; set; }
        /// <summary>
        /// 用户Id
        /// 来源表于LoginType相关
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录地址
        /// </summary>
        public string Address { get; set; }

    }
}
