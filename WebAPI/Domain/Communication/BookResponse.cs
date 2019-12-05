using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Communication
{
    public class BookResponse : BaseResponse
    {
        public Book Book { get; set; }

        private BookResponse(bool success, string message, Book book) : base(success, message)
        {
            Book = book;
        }

        /// <summary>
        /// Creates a success BookResponse
        /// </summary>
        /// <param name="book"></param>
        public BookResponse(Book book) : this(true, string.Empty, book)
        {

        }

        /// <summary>
        /// Creates an error BookResponse
        /// </summary>
        /// <param name="message"></param>
        public BookResponse(string message) : this(false, message, null)
        {

        }
    }
}
