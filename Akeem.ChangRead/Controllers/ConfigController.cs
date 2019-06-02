using Akeem.ChangRead.CommonUtil;
using Akeem.ChangRead.Models;
using Akeem.ChangRead.Models.FormData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigController : BaseController
    {
        private protected VerifyCodeHelper _verifyCodeHelper;
        private protected IConfiguration configuration;
        private protected RedisCommon _redisCommon;
        private protected ChangReadContext _dbContext;
        public ConfigController(VerifyCodeHelper verifyCodeHelper,
            IConfiguration configuration,
            ChangReadContext changReadContext,
            RedisCommon redisCommon) : base(configuration)
        {
            this._verifyCodeHelper = verifyCodeHelper;
            this._dbContext = changReadContext;
            this._redisCommon = redisCommon;
        }

        //获取配置文件列表
        [HttpPost]
        [Authorize]
        public IActionResult List([FromBody]QueryArrayModel paras)
        {
            ResultArray<SystemConfig> result = new ResultArray<SystemConfig>();
            IQueryable<SystemConfig> allList = null;
            allList = _dbContext.SystemConfig.Where(m => (string.IsNullOrEmpty(paras.Code) ? true :
            m.Key.Contains(paras.Code)
            || m.Name.Contains(paras.Code)
            || m.Value.Contains(paras.Code)
            || m.Memo.Contains(paras.Code)));
            result.total = allList.Count();
            result.success = true;
            result.data = allList.Skip((paras.PageIndex - 1) * paras.PageSize).Take(paras.PageSize).ToList();
            return Json(result);
        }
        //编辑配置文件
        [HttpPost]
        [Authorize]
        public IActionResult Edit([FromBody]ConfigEdit paras)
        {
            var has = _dbContext.SystemConfig.FirstOrDefault(m => m.Key == paras.Key && m.Id != paras.Id);
            if (has != null) throw ExceptionEnum.Operating.ToEx("键已存在");

            SystemConfig config = new SystemConfig()
            {
                Id = paras.Id.Value,
                Key = paras.Key,
                Memo = paras.Memo,
                Name = paras.Name,
                Status = paras.Status.Value,
                Value = paras.Value
            };
            _dbContext.SystemConfig.Update(config);
            int result = _dbContext.SaveChanges();
            return Json((result > 0).ToResult());
        }
        //添加配置温家安
        [HttpPost]
        [Authorize]
        public IActionResult Add([FromBody]ConfigAdd paras)
        {
            var has = _dbContext.SystemConfig.FirstOrDefault(m => m.Key == paras.Key);
            if (has != null) throw ExceptionEnum.Operating.ToEx("键已存在");
            SystemConfig config = new SystemConfig()
            {
                Key = paras.Key,
                Memo = paras.Memo,
                Name = paras.Name,
                Status = 1,
                Value = paras.Value
            };
            _dbContext.SystemConfig.Add(config);
            int result = _dbContext.SaveChanges();
            return Json((result > 0).ToResult());
        }
        //刷新redis
        [HttpPost]
        [Authorize]
        public IActionResult Redis()
        {
            var result = _dbContext.SystemConfig.ToList();
            foreach (var item in result)
            {
                if (item.Status != 1)
                {
                    _redisCommon.DeleteKey(item.Key);
                }
                else
                {
                    _redisCommon.SetValue(item.Key, item.Value);
                }
            }
            return Json(true.ToResult());
        }
    }
}
