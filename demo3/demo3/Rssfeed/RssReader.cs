using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using demo3.Models;
using demo3.Rssfeed;

namespace demo3.Rssfeed
{
    public class RssReader
    {
        private readonly NewsParser _parser;

        public RssReader(NewsParser parser)
        {
            _parser = parser;
        }

        public List<Acticle> GetNews(string rssLink)
        {
            var feedData = DownloadFeed(rssLink);
            return _parser.ParseXml(feedData);
        }

        private string DownloadFeed(string rssLink)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            return client.DownloadString(rssLink);
        }
    }
}
