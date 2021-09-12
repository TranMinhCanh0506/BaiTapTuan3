using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo3.Models;
using demo3.ID;
using System.Xml;
using System.Text.RegularExpressions;

namespace demo3.Rssfeed
{
   public class NewsParser
    {
        public List<Acticle> ParseXml(string xmlContent)
        {
            var document = new XmlDocument();
            document.LoadXml(xmlContent);

            var acticles = new List<Acticle>();
            var itemNodes = document.SelectNodes("//item");

            foreach (XmlNode node in itemNodes)
            {
                var news = new Models.Acticle()
                {
                    Title = node.SelectSingleNode("title").InnerText,
                    Description = StripHtml(node.SelectSingleNode("description").InnerText),
                    Link = node.SelectSingleNode("link").InnerText,
                    PublishedDate = ParseDate(node.SelectSingleNode("pubDate").InnerText)
                };
                acticles.Add(news);
            }
            return acticles;
        }

        private string StripHtml(string content)
        {
            return Regex.Replace(content, "<.*?>", String.Empty).Trim();
        }

        private DateTime ParseDate(string dateStr)
        {
            try
            {
                return DateTime.Parse(dateStr);
            }
            catch (Exception)
            {

                return DateTime.Now;
            }
        }
    }
}
