using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public enum ExceptionEnum
    {

        /// <summary>
        /// 非法操作
        /// </summary>
        Illegal = 1,
        /// <summary>
        /// 操作异常 
        /// </summary>
        Operating = 2,
        /// <summary>
        /// 没有权限
        /// </summary>
        NoPermission = 3,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 4,
        /// <summary>
        /// 参数有误
        /// </summary>
        Parameter = 5,
        /// <summary>
        /// 没有登录
        /// </summary>
        LoginOut = 6,
        /// <summary>
        /// 没有找到相关数据
        /// </summary>
        NoModel = 7,
        /// <summary>
        /// Sql 错误
        /// </summary>
        Sql = 8,
    }
}
