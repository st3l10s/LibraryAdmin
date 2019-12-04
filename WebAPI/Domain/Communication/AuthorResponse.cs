using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Communication
{
    public class AuthorResponse : BaseResponse
    {
        public Author Author { get; set; }

        private AuthorResponse(bool success, string message, Author author) : base(success, message)
        {
            Author = author;
        }

        /// <summary>
        /// Creates a success AuthorResponse
        /// </summary>
        /// <param name="author"></param>
        public AuthorResponse(Author author) : this(true, string.Empty, author)
        {

        }

        /// <summary>
        /// Creates an error AuthorResponse
        /// </summary>
        /// <param name="message"></param>
        public AuthorResponse(string message) : this(false, message, null)
        {

        }
    }
}
