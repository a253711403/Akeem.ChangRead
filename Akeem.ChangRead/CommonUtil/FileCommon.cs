using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public class FileCommon
    {
        private protected IConfiguration _configuration;
        public FileCommon(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        #region 保存网络图片

        public string SaveImage(string webUrl)
        {
            try
            {
                WebClient web = new WebClient();
                var url = new Uri(webUrl);
                var fileName = Guid.NewGuid().ToString().Replace("-", "");
                var fileUrl = string.Empty;

                var directory = _configuration.GetConnectionString("DownloadDirectory");

                if (directory.Last() != '\\')
                {
                    directory += "\\";
                }
                directory += "images";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                fileUrl = $"{directory}\\{ fileName}";
                web.DownloadFile(webUrl, fileUrl);
                return $"images\\{ fileName}";
            }
            catch //(Exception ex)
            {
                return "";
            }
        }

        public byte[] GetImage(string fileName)
        {
            var directory = _configuration.GetConnectionString("DownloadDirectory");
            var fileUrl = $"{directory}\\{ fileName}";
            if (File.Exists(fileUrl))
            {
                return File.ReadAllBytes(fileUrl);
            }
            //没有封面图片 
            //返回默认图片 
            fileUrl = $"{directory}\\images\\defaultCover.jpg";
            if (!File.Exists(fileUrl))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    return stream.ToArray();
                }
            }
            return File.ReadAllBytes(fileUrl);
        }

        #endregion
    }
}
