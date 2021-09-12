using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demo3.Models;

namespace demo3.ID
{
    public interface INewRepository
    {
        List<Publisher> GetNews();
        void Save(List<Publisher> publishers);
    }
}
