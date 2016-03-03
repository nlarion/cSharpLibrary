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
        Book newBook = Book.Find(Request.Form["book-name"]);
        newAuthor.Save();
        newBook.AddAuthor(newAuthor);
        List<Book> bookList = Book.GetAll();
        List<Author> authorList = Author.GetAll();
        returnDictionary.Add("bookList", bookList);
        returnDictionary.Add("authorList", authorList);
        return View["author.cshtml", returnDictionary];
      };
    }
  }
}
