using BookApp.Helper;
using Interfaces.Services;
using Models.DomainModels;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/books")]
    [EnableCors(origins: "*", headers: "accept,Auth-Key", methods: "*")]
    public class BookController : ApiController
    {
        private IBookService BookService;

        public BookController() {
        }
        public BookController(IBookService bookService) {
            BookService = bookService;
        }

        [HttpGet]
        [Route("GetBookById")]
        public Book GetBookById(Guid bookId) {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid bookId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var book = BookService.GetBookById(bookId);
            if (book != null)
                return book;
            else
                throw new APIDataException(1, "No book found", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("CreateBook")]
        public Book SaveBook([FromBody]Book book) {
            if (book == null)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            book = BookService.AddBook(book);
            if (book != null)
                return book;
            else
                throw new APIDataException(2, "Error Saving Book", HttpStatusCode.NotFound);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public Book UpdateBook([FromBody]Book book) {
            if (book == null)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            book = BookService.UpdateBook(book);
            if (book != null)
                return book;
            else
                throw new APIDataException(3, "Error Updating Book", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("DeleteBook")]
        public HttpResponseMessage DeleteBook([FromBody]Guid bookId) {
            if (bookId == null || bookId == Guid.Empty)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid bookId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var book = BookService.GetBookById(bookId);
            if (book != null) {
                var result = BookService.DeleteBook(book);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Book was deleted");
                else
                    throw new APIDataException(3, "Error Deleting Book", HttpStatusCode.InternalServerError); // KV: Not a notfound scenario
            } else
                throw new APIDataException(1, "No book found", HttpStatusCode.NotFound);
        }

    }
}
