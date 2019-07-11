# Web API prototype project
This repository stores a REST API project. It is a small prototype that is similar to a real project, that will be relevant for the back-end position.

Below is a few tasks that we have prepared for you. We only expect you to spend around 3 hours on this – not days. The most important is for us to get insight into your understanding/thinking. We ask you to do the following:

1. Fork this repo to your own GitHub account and clone your fork to your machine. Run the application and get an overview over how it is working.
2. Review the code base and think about how it could be improved – especially the general structure and code patterns.
3. Choose to do some relevant changes, accompanied by inline comments explaining the change and why.
4. More overall thoughts/suggestions/ideas for the code or architecture should be added below in this README. This also gives you a chance to mention changes that you did not have time to do in the short timeframe.
5. Push and make a pull request to this repository with your changes.

----

#### Add general thoughts/suggestions/ideas here:

My assumption is that code reached the WebPAI endpoints UserController and BookController, is the relevant code parts for the assignemnt. The BookApp application contains very big chunk of boilerplate code that I removed, since it did not have any practical purpose, with this focus, and just was noisy.

Structural perspective:
- Why are there a seperate project for implementations, interfaces, models and repositories? If it's for organizing, that's what folders are for. Seperate projects are for seperate binary output.
  - Also, Models project is referencing Interface-project. This seems like a code-smell.
- IoC seems kinda overkill for such a small project (Unity dependency and boilerplate-code is like 10%-20% of the code base)
- Generally, I do not see any big structural changes. It's relatively "down to earth" for my taste.

Minor changes:
- Added a global JsonFormatter, instead of one pr. controller.
- Generally changed Controllers to return their actual type, instead of HttpResponseMessage. This gives more static checking, and better reasoning about types on the client side.
- Renamed APIException to APIInputException. Seems more fitting for it's use (validating input parameters).
- Did some refactoring of the ExceptionFilter, to actually make use of the IAPIException-interface. Also for future exception-types boilerplate code wont be needed.
- Made BooksExtended inherit from Books. But what's the point of having two types here at all? 

Comments
- Kinda of annoying that Routes and static names are slightly off (UserController is /account/, SaveUser is /CreateUser)
- Why is CORS enabled?
- For save methods (both users and books)  it's the clients responsebility to supply the identity. This is bad practice. 
  - Creating a user that already exists gives internal server error.
- From UserRepository.cs: UserName = b.User.FirstName + " " + b.User.LastName
  - Username is usually a selfasigned designation, not a Users actual name. What is the point of this?
- Consider using an Enum for actual Error scenarios in the APIExceptions
- Do not use HttpStatus codes for ErrorCode in APIExceptions. They are already stored in the HttpStatus.

  
