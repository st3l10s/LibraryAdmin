using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ISBN { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public IList<CategoryBook> Categories { get; set; } = new List<CategoryBook>();
    }
}
