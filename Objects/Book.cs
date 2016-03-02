using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Book
  {
    private string _title;
    // private List<Author> _authors;
    private DateTime _dueDate;
    private int _id;


    public Book(string title, DateTime dueDate, int id = 0 )
    {
      _title = title;
      // _authors = authors;
      _dueDate = dueDate;
      _id = id;
    }

    public override bool Equals(System.Object otherBook)
    {
      if(!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool idEquality = this.GetId() == newBook.GetId();
        // bool authorsEquality = this.GetAuthors() == newBook.GetAuthor();
        bool titleEquality = this.GetTitle() == newBook.GetTitle();
        bool dueDateEquality = this.GetDueDate() == newBook.GetDueDate();
        return (idEquality && titleEquality && dueDateEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    // public string GetAuthors()
    // {
    //   return _authors;
    // }
    // public void SetAuthors(List<Author> authors)
    // {
    //   _authors = authors;
    // }
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle(string title)
    {
      _title = title;
    }
    public DateTime GetDueDate()
    {
      return _dueDate;
    }
    public void SetDueDate(DateTime dueDate)
    {
      _dueDate = dueDate;
    }



    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("select * from books;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int BookId = rdr.GetInt32(0);
        string BookTitle= rdr.GetString(1);
        DateTime BookDueDate = rdr.GetDateTime(2);
        //need method to return authors here
        Book newBook = new Book(BookTitle, BookDueDate, BookId);
        allBooks.Add(newBook);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allBooks;
    }
  }
}
