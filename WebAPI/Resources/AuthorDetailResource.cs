using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Resources
{
    public class AuthorDetailResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }

        public IList<BookDisplayResource> Books { get; set; } = new List<BookDisplayResource>();
    }
}
