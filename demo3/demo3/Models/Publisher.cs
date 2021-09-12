using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo3.Models
{
    public class Publisher
    {
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public Publisher()
        {
            Categories = new List<Category>();
        }
        public bool AddCategory(string name, string link, bool updateIfExist)
        {
            var category = Categories.Find(x => x.Name == Name);
            if (category == null)
            {
                category = new Category()
                {
                    Name = name,
                    RssLink = link
                };
                Categories.Add(category);
                return true;
            }
            if (updateIfExist)
            {
                category.RssLink = link;
                return true;
            }

            return false;
        }

        public void RemoveCate(string name)
        {
            Categories.RemoveAll(x => x.Name == name);
        }
    }
}
    

