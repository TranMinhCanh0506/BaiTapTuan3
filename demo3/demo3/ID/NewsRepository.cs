using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo3.Models;
namespace demo3.ID
{
    public class NewsRepository : INewRepository
    {
        private const string FilePath = "Data\\data.txt";
        public List<Publisher> GetNews()
        {
            var publisher = new List<Publisher>();
            Publisher office = null;
            string line;

            try
            {
                using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }
                            if (line.StartsWith("@"))
                            {
                                office = ParsePublishh(line);
                                publisher.Add(office);
                            }
                            else if (line.StartsWith("#") && office != null)
                            {
                                var category = ParseCate(line);
                                office.Categories.Add(category);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return publisher;
        }

        public void Save(List<Publisher> publishers)
        {
            using (var stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                using (var w = new StreamWriter(stream))
                {
                    foreach (var item in publishers)
                    {
                        w.WriteLine("@{0}", item.Name);
                        foreach (var cate in item.Categories)
                        {
                            w.WriteLine("#{0}^{1}", cate.Name, cate.RssLink);
                        }
                    }
                }
            }
        }
        private Publisher ParsePublishh(string info)
        {
            return new Publisher()
            {
                Name = info.Substring(1).Trim()
            };
        }

        private Category ParseCate(string line)
        {
            var lines = line.Substring(1).Split('^');
            return new Category()
            {
                Name = lines[0].Trim(),
                RssLink = lines[1].Trim()
            };
        }
    }
}
