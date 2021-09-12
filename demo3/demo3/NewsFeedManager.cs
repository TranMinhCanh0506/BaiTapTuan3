using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo3.Models;
using demo3.ID;
using demo3.Rssfeed;
namespace demo3
{
    public class NewsFeedManager
    {
        private readonly INewRepository _newRepository;
        private List<Publisher> _publisher;
        private readonly RssReader _rssReader;
        public NewsFeedManager(INewRepository newRepository, RssReader rssReader)
        {
            _newRepository = newRepository;
            _rssReader = rssReader;
        }
        public List<Models.Publisher> GetNewFeed()
        {
            if (_publisher == null)
            {
                _publisher = _newRepository.GetNews();
            }
            return _publisher;
        }
        public void SaveChange()
        {
            _newRepository.Save(_publisher);
        }
        public void RemovePublish(string publisherName)
        {
            _publisher.RemoveAll(x => x.Name == publisherName);
            SaveChange();
        }
        public void RemoveCate(string publisherName, string categoryName)
        {
            var publisher = _publisher.Find(x => x.Name == publisherName);
            if (publisher == null)
                return;
            publisher.RemoveCate(categoryName);
            SaveChange();
        }
        public bool AddCategory(string publishName, string categoryName, string rsslink, bool UploadExists)
        {
            var pub = _publisher.Find(x => x.Name == publishName);
            if (pub == null)
            {
                pub = new Publisher()
                {
                    Name = publishName
                };
                _publisher.Add(pub);
            }
            return pub.AddCategory(categoryName, rsslink, UploadExists);
        }
        public List<Acticle> GetNews(string publisherName, string categoryName)
        {
            var publisher = _publisher.Find(x => x.Name == publisherName);
            if (publisher == null) return new List<Acticle>();

            var category = publisher.Categories.Find(x => x.Name == categoryName);
            if (category == null) return new List<Acticle>();

            if (category.Acticles.Count == 0)
            {
                category.Acticles = _rssReader.GetNews(category.RssLink);
            }
            return category.Acticles;
        }


    }
}
