using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Communication
{
    public class CategoryResponse : BaseResponse
    {
        public Category Category { get; set; }

        private CategoryResponse(bool success, string message, Category category) : base(success, message)
        {
            Category = category;
        }

        /// <summary>
        /// Creates a success CategoryResponse
        /// </summary>
        /// <param name="category"></param>
        public CategoryResponse(Category category) : this(true, string.Empty, category)
        {

        }

        /// <summary>
        /// Creates an error CategoryResponse
        /// </summary>
        /// <param name="message"></param>
        public CategoryResponse(string message) : this(false, message, null)
        {

        }
    }
}
