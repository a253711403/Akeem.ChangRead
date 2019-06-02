using Akeem.ChangRead.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akeem.ChangRead.CommonUtil
{
    public class ReptileCmmon
    {
        /// <summary>
        /// 1 : 无名小说网
        /// </summary>
        public ReptileCmmon(string Web, string url)
        {
            switch (Web)
            {
                case "无名小说网":
                    context = new Web_WMXS();
                    break;
                default:
                    context = new Web_WMXS();
                    break;
            }
            this.WebUrl = url;
        }
        private string WebUrl;
        private IReptile context;
        public NovelInfo GetNovelInfo()
        {
            return context.GetNovelInfo(WebUrl);
        }

        public List<Chapter> GetChapterList()
        {
            return context.GetChapterList(WebUrl);
        }

        public string GetChapterContent()
        {
            return context.GetChapterContent(WebUrl);
        }
    }
    public interface IReptile
    {
        NovelInfo GetNovelInfo(string url);
        List<Chapter> GetChapterList(string url);
        string GetChapterContent(string url);
    }

    public class Web_WMXS : IReptile
    {
        public string GetChapterContent(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("GBK");
            var htmlDoc = web.Load(url);
            string doc_articlecontent = htmlDoc.DocumentNode.SelectSingleNode("//p[@id='articlecontent']").InnerHtml;
            return doc_articlecontent;
        }

        public List<Chapter> GetChapterList(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HtmlWeb web = new HtmlWeb();
            web.OverrideEncoding = Encoding.GetEncoding("GBK");
            var htmlDoc = web.Load(url);
            var doc_ul = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='ml_list']/ul");
            var doc_li = doc_ul.SelectNodes("li");
            List<Chapter> list = new List<Chapter>();
            foreach (var item in doc_li)
            {
                var doc_a = item.SelectSingleNode("a");
                Chapter model = new Chapter();
                model.Url = $"{url}{doc_a.Attributes["href"].Value}";
                model.Name = doc_a.InnerHtml;
                list.Add(model);
            }
            return list;
        }

        public NovelInfo GetNovelInfo(string url)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                HtmlWeb web = new HtmlWeb();
                web.OverrideEncoding = Encoding.GetEncoding("GBK");
                var htmlDoc = web.Load(url);

                var catalog1 = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='catalog1']");
                NovelInfo info = new NovelInfo();
                info.ImageUrl = "https://www.wmtxt.com" + catalog1.SelectSingleNode("div[@class='pic']/img").Attributes["src"].Value;
                info.Title = catalog1.SelectSingleNode("div[@class='introduce']/h1").InnerText;
                info.Memo = catalog1.SelectSingleNode("div[@class='introduce']/p[@class='jj']").InnerText;
                info.Author = catalog1.SelectNodes("div[@class='introduce']/p[@class='bq']/span")[1].SelectSingleNode("a").InnerText;
                info.SourceWeb = 1;
                return info;
            }
            catch
            {
                throw ExceptionEnum.Parameter.ToEx("数据匹配失败");
            }
        }

    }
}
