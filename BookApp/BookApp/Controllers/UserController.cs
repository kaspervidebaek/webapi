using BookApp.Helper;
using Interfaces.Repositories;
using Interfaces.Services;
using Models.DomainModels;
using Models.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/account")]
    [EnableCors(origins: "*", headers: "accept,Auth-Key", methods: "*")]
    public class UserController : ApiController
    {
        private IBookRepository BookRepository;
        private IBookService BookService;
        private IUserService UserService;

        public UserController() {
        }
        public UserController(IBookRepository bookRepository, IUserService userService, IBookService bookService) {
            BookRepository = bookRepository;
            UserService = userService;
            BookService = bookService;
        }

        [HttpGet]
        [Route("GetUserById")]
        public User GetUserById(Guid userId) {
            if (userId == null || userId == Guid.Empty)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var user = UserService.GetUserById(userId);
            if (user != null)
                return user;
            else
                throw new APIDataException(4, "No user found", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("CreateUser")]
        public User SaveUser([FromBody]User user) {
            if (user == null)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid user object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            user = UserService.AddUser(user);
            if (user != null)
                return user;
            else
                throw new APIDataException(5, "Error Saving User", HttpStatusCode.NotFound);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public User UpdateUser([FromBody]User user) {
            if (user == null)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid user object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            user = UserService.UpdateUser(user);
            if (user != null)
                return user;
            else
                throw new APIDataException(6, "Error Updating User", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser([FromBody]Guid userId) {
            if (userId == null || userId == Guid.Empty)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var user = UserService.GetUserById(userId);
            if (user != null) {
                var result = UserService.DeleteUser(user);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Book was deleted");
                else
                    throw new APIDataException(7, "Error Deleting User", HttpStatusCode.InternalServerError); // KV: This is not a notfound-scenario
            } else
                throw new APIDataException(4, "No user found", HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("CreateUserBook")]
        public Book SaveBook([FromUri]Guid userId, [FromBody]Book book) {
            if (book == null)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid book object. Object can't be null.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            if (userId == null || userId == Guid.Empty)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            BookRepository.Add(book);
            BookRepository.SaveChanges();
            var result = BookRepository.GetBookByID(book.Id);
            if (result != null)
                return result;
            else
                throw new APIDataException(2, "Error Saving Book", HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("GetUserBooks")]
        public IEnumerable<BookExtended> GetUserBooks(Guid userId) {
            if (userId == null || userId == Guid.Empty)
                throw new APIInputException() {
                    ErrorCode = (int) HttpStatusCode.BadRequest,
                    ErrorDescription = "Bad Request. Provide valid userId guid. Can't be empty guid.",
                    HttpStatus = HttpStatusCode.BadRequest
                };
            var books = BookService.GetBooksByUserId(userId);
            if (books != null)
                return books;
            else
                throw new APIDataException(1, "No books found", HttpStatusCode.NotFound);
        }



    }
}
