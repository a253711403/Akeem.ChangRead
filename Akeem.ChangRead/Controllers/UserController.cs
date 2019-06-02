using Akeem.ChangRead.CommonUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akeem.ChangRead.Models;
using Akeem.ChangRead.Models.FormData;
using Microsoft.AspNetCore.Authorization;

namespace Akeem.ChangRead.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private protected VerifyCodeHelper _verifyCodeHelper;
        private protected IConfiguration configuration;
        private protected RedisCommon _redisCommon;
        private protected ChangReadContext _dbContext;
        public UserController(VerifyCodeHelper verifyCodeHelper,
            IConfiguration configuration,
            ChangReadContext changReadContext,
            RedisCommon redisCommon) : base(configuration)
        {
            this._verifyCodeHelper = verifyCodeHelper;
            this._dbContext = changReadContext;
            this._redisCommon = redisCommon;
        }
        [HttpPost]
        public IActionResult Verify()
        {
            var code = _verifyCodeHelper.CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.AbcVerifyCode);
            var image = _verifyCodeHelper.CreateByteByImgVerifyCode(code, 116, 36);
            HttpContext.Session.SetString("verify", code);
            return File(image, @"image/jpeg");
        }

        [HttpPost]
        public IActionResult OnLoginVerify()
        {
            var onLoginVerify = _redisCommon.GetValue("OnLoginVerify");
            return Json((onLoginVerify == "1").ToSuccess());
        }

        [HttpPost]
        public IActionResult SingIn([FromBody]SingIn paras)
        {
            //是否启用验证码
            if (_redisCommon.GetValue("OnLoginVerify") == "1")
            {
                var cache = HttpContext.Session.GetString("verify");
                if (string.IsNullOrEmpty(cache)) throw ExceptionEnum.Parameter.ToEx("验证码失效");
                if (!(paras.Verify ?? "").ToLower().Equals(cache.ToLower())) throw ExceptionEnum.Parameter.ToEx("验证码错误");
                HttpContext.Session.Clear();
            }

            //验证账户
            var model = _dbContext.SystemUser.FirstOrDefault(m => m.Code == paras.Code);
            if (model == null) throw ExceptionEnum.Operating.ToEx("用户不存在");
            var mix = (paras.Code + paras.Password).ToMd5();
            if (model.Password != mix) throw ExceptionEnum.Operating.ToEx("密码错误");
            if (model.Status != 1) throw ExceptionEnum.Disable.ToEx("账户被禁用或已删除");

            //登录记录
            _dbContext.LoginRecord.Add(new LoginRecord()
            {
                Address = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString(),
                LoginTime = DateTime.Now,
                UserId = model.Id,
                LoginType = 1
            });
            _dbContext.SaveChanges();

            var result = new
            {
                Token = GetToken(model),
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                Image = model.Image,
                Level = model.Level,
                Status = model.Status,
                Roles = model.Roles,
            };
            return Json(result.ToSuccess());
        }

        public IActionResult SingOut()
        {
            HttpContext.Session.Clear();
            return Json(true.ToSuccess());
        }

        [HttpPost]
        [Authorize]
        public IActionResult SingUp([FromBody]SingUp paras)
        {
            var model = _dbContext.SystemUser.FirstOrDefault(m => m.Code == paras.Code);
            if (model != null) throw ExceptionEnum.Operating.ToEx("用户已存在");
            Models.SystemUser user = new SystemUser()
            {
                Code = paras.Code,
                Image = paras.Image,
                Level = paras.Level.Value,
                Name = paras.Name,
                Roles = paras.Roles,
                Status = 1
            };
            user.Password = (paras.Code + paras.Password).ToMd5();
            _dbContext.SystemUser.Add(user);
            var result = _dbContext.SaveChanges();
            return Json((result > 0).ToResult());
        }

        [HttpPost]
        [Authorize]
        public IActionResult UserList([FromBody] QueryArrayModel paras)
        {
            var result = new ResultArray<Dictionary<string, object>>();
            IQueryable<SystemUser> allList = null;
            allList = _dbContext.SystemUser.Where(m => (string.IsNullOrEmpty(paras.Code) ? true : (m.Name.Contains(paras.Code) || m.Code.Contains(paras.Code))));
            result.total = allList.Count();
            result.success = true;
            result.data = new List<Dictionary<string, object>>();
            foreach (var item in allList.Skip((paras.PageIndex - 1) * paras.PageSize).Take(paras.PageSize))
            {
                var info = Common.ToDictionary(item);
                var lastInfo = _dbContext.LoginRecord.LastOrDefault(m => m.UserId == item.Id);
                if (lastInfo != null)
                {
                    info.Add("lastTime", lastInfo.LoginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    info.Add("lastIp", lastInfo.Address);
                    info.Add("loginCount", _dbContext.LoginRecord.Count(m => m.UserId == item.Id).ToString());
                }
                result.data.Add(info);
            }
            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UserEdit([FromBody]EditUser paras)
        {
            var user = _dbContext.SystemUser.First(m => m.Id == paras.Id.Value);
            if (user.Code.ToLower() == "administrator")
            {
                throw ExceptionEnum.NoPermission.ToEx("权限不够，无法修改超级管理员用户");
            }
            user.Name = paras.Name;
            user.Level = paras.Level.Value;
            user.Status = paras.Status.Value;
            _dbContext.SystemUser.Update(user);
            int result = _dbContext.SaveChanges();
            return Json((result > 0).ToResult());
        }
    }
}
