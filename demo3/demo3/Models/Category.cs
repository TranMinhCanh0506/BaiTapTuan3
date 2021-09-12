using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo3.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string RssLink { get; set; }
        public List<Acticle> Acticles { get; set; }
        public Category()
        {
            Acticles = new List<Acticle>();
        }
    }
}
