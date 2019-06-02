using Akeem.ChangRead.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public static class Common
    {
        private const string SuccessCode = "00";

        public static string ToMd5(this string code)
        {
            StringBuilder mixCode = new StringBuilder();
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组 
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(code));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得 
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                mixCode.Append(s[i].ToString("x"));
            }
            return mixCode.ToString();
        }
        public static ResultModel ToError(this Exception ex)
        {
            ResultModel result = new ResultModel();
            result.code = ex.HelpLink;
            result.msg = ex.Message;
            result.success = false;
            return result;
        }
        public static ResultModel ToError(this string message)
        {
            ResultModel result = new ResultModel();
            result.code = "91";
            result.msg = message;
            result.success = false;
            return result;
        }
        public static string ToJson(this ResultModel model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static string ToJson<T>(this ResultArray<T> model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static ResultModel ToResult(this bool status, string error = null)
        {
            var result = new ResultModel();
            result.success = status;
            result.code = status ? SuccessCode : "1";
            if (!status && string.IsNullOrEmpty(error))
            {
                result.msg = "操作失败";
            }
            else
            {
                result.msg = error;
            }
            return result;
        }



        /// <summary>
        /// 数据成功返回统一实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResultModel ToSuccess<T>(this T data)
        {
            var result = new ResultModel();
            result.success = true;
            result.data = data;
            result.code = SuccessCode;
            return result;
        }

        /// <summary>
        /// 实体类转Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(T model) where T : new()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (PropertyInfo pro in typeof(T).GetProperties())
            {
                object value = pro.GetValue(model, null);
                string name = pro.Name;
                dic.Add(name, value);
            }
            return dic;
        }
        public static Exception ToEx(this ExceptionEnum type, string message = null)
        {
            Exception ex = null;
            if (!string.IsNullOrEmpty(message))
            {
                ex = new Exception(message);
            }
            else
            {
                switch (type)
                {
                    case ExceptionEnum.Sql:
                        ex = new Exception("sql错误");
                        break;
                    case ExceptionEnum.Illegal:
                        ex = new Exception("非法操作");
                        break;
                    case ExceptionEnum.Operating:
                        ex = new Exception("操作失败");
                        break;
                    case ExceptionEnum.NoPermission:
                        ex = new Exception("没有相关权限");
                        break;
                    case ExceptionEnum.Disable:
                        ex = new Exception("已被禁用");
                        break;
                    case ExceptionEnum.Parameter:
                        ex = new Exception("参数有误");
                        break;
                    case ExceptionEnum.LoginOut:
                        ex = new Exception("未登录");
                        break;
                    case ExceptionEnum.NoModel:
                        ex = new Exception("没有找到相关数据");
                        break;
                    default:
                        ex = new Exception("未知错误");
                        break;
                }
            }
            ex.HelpLink = ((int)type).ToString();
            return ex;
        }
    }
}
