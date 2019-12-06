using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Resources
{
    public class BookDetailResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ISBN { get; set; }

        public AuthorDisplayResource Author { get; set; }

        public IList<CategoryBookDisplayResource> Categories { get; set; } = new List<CategoryBookDisplayResource>();
    }
}
