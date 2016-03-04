using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Library
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/Librarian"] = _ => {
        return View["librarian.cshtml"];
      };
      Post["/Librarian"] = _ => {
        List<Book> resultList = Book.Search(Request.Form["title"]);
        return View["librarian.cshtml",resultList];
      };
      Get["/Librarian/Author"]= _ =>{
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["author.cshtml", returnDictionary];
      };
      Post["/Librarian/Author"]= _ =>{
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        Author newAuthor = new Author(Request.Form["author-name"]);
        if(Request.Form["book-name"] != null)
        {
          Book newBook = Book.Find(Request.Form["book-name"]);
          newBook.AddAuthor(newAuthor);
        }
        newAuthor.Save();
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["author.cshtml", returnDictionary];
      };
      Get["/Librarian/Author/{id}"]= parameters =>{
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        Author foundAuthor = Author.Find(parameters.id);
        List<Book> authorsBooks = foundAuthor.GetBooks();
        returnDictionary.Add("foundAuthor", foundAuthor);
        returnDictionary.Add("authorsBooks", authorsBooks);
        return View["authorDetail.cshtml", returnDictionary];
      };
      Post["/Librarian/Author/{id}"]= parameters =>{

        Author foundAuthor = Author.Find(parameters.id);
        foundAuthor.Update(Request.Form["author-name"]);
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        List<Book> authorsBooks = foundAuthor.GetBooks();
        returnDictionary.Add("foundAuthor", foundAuthor);
        returnDictionary.Add("authorsBooks", authorsBooks);
        return View["authorDetail.cshtml", returnDictionary];
      };
      Get["/Librarian/Author/Delete/{id}"]= parameters =>{
        Author foundAuthor = Author.Find(parameters.id);
        foundAuthor.Delete();
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["author.cshtml", returnDictionary];
      };
      Get["/Librarian/Book"]= _ =>{
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["book.cshtml", returnDictionary];
      };
      Post["/Librarian/Book"]= _ =>{
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        DateTime dueDate = Convert.ToDateTime((string) Request.Form["due-date"]);
        Book newBook = new Book(Request.Form["book-name"],dueDate);
        newBook.Save();
        if(Request.Form["author-name"] != null)
        {
          Author newAuthor = Author.Find(Request.Form["author-name"]);
          newAuthor.AddBooks(newBook);
        }
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["book.cshtml", returnDictionary];
      };
      Post["/Librarian/Book/{id}"]= parameters =>{
        Book foundBook = Book.Find(parameters.id);
        DateTime dueDate = Convert.ToDateTime((string) Request.Form["due-date"]);
        foundBook.Update(Request.Form["book-name"], dueDate);
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        List<Author> foundAuthors = foundBook.GetAuthors();
        returnDictionary.Add("foundAuthors", foundAuthors);
        returnDictionary.Add("foundBook", foundBook);
        return View["bookDetail.cshtml", returnDictionary];
      };
      Get["/Librarian/Book/{id}"]= parameters =>{
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        Book foundBook = Book.Find(parameters.id);
        List<Author> foundAuthors = foundBook.GetAuthors();
        returnDictionary.Add("foundAuthors", foundAuthors);
        returnDictionary.Add("foundBook", foundBook);
        return View["bookDetail.cshtml", returnDictionary];
      };
      Get["/Librarian/Book/Delete/{id}"]= parameters =>{
        Book foundBook = Book.Find(parameters.id);
        foundBook.Delete();
        Dictionary<string, object> returnDictionary = new Dictionary<string, object> ();
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["book.cshtml", returnDictionary];
      };
    }
  }
}
