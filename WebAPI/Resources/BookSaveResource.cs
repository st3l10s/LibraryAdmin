using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Resources
{
    public class BookSaveResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long? ISBN { get; set; }

        [Required]
        public int? AuthorId { get; set; }

        [Required]
        public IList<CategoryBookSaveResource> Categories { get; set; }
    }
}
