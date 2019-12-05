using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Resources
{
    public class BookSaveResource
    {
        public string Name { get; set; }
        public long ISBN { get; set; }

        public int AuthorId { get; set; }
    }
}
