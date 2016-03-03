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
    }
  }
}
