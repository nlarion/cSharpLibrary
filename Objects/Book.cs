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

    public static Book Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books WHERE id = @BookId;", conn);

      SqlParameter BookIdParameter = new SqlParameter();
      BookIdParameter.ParameterName = "@BookId";
      BookIdParameter.Value = id.ToString();
      cmd.Parameters.Add(BookIdParameter);
      rdr = cmd.ExecuteReader();

      int foundBookId = 0;
      string foundBookTitle = null;
      DateTime foundBookDueDate = new DateTime(1999,01,01);

      while(rdr.Read())
      {
        foundBookId = rdr.GetInt32(0);
        foundBookTitle = rdr.GetString(1);
        foundBookDueDate = rdr.GetDateTime(2);
      }
      Book foundBook = new Book(foundBookTitle, foundBookDueDate, foundBookId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBook;
    }

    public static List<Book> Search(string title)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Book> searchResults = new List<Book>{};

      SqlCommand cmd = new SqlCommand("SELECT * FROM books WHERE title LIKE @SearchTitle;", conn);

      SqlParameter titleSearchParameter = new SqlParameter();
      titleSearchParameter.ParameterName = "@SearchTitle";
      titleSearchParameter.Value = "%" + title + "%";
      cmd.Parameters.Add(titleSearchParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int BookId = rdr.GetInt32(0);
        string BookTitle= rdr.GetString(1);
        DateTime BookDueDate = rdr.GetDateTime(2);
        //need method to return authors here
        Book newBook = new Book(BookTitle, BookDueDate, BookId);
        searchResults.Add(newBook);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return searchResults;
    }

    public void AddAuthor(Author newAuthor)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO book_authors (author_id, book_id) VALUES (@AuthorId, @BookId);", conn);

      SqlParameter authorIdParameter = new SqlParameter();
      authorIdParameter.ParameterName = "@BookId";
      authorIdParameter.Value = this.GetId();
      cmd.Parameters.Add(authorIdParameter);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@AuthorId";
      studentIdParameter.Value = newAuthor.GetId();
      cmd.Parameters.Add(studentIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Author> GetAuthors()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Author> authors = new List<Author>{};

      SqlCommand cmd = new SqlCommand("select a.id, a.name from authors a inner join book_authors ba on a.id = ba.author_id inner join books b on b.id = ba.book_id where b.id = @BookId", conn);
      SqlParameter BookIdParameter = new SqlParameter();

      BookIdParameter.ParameterName = "@BookId";
      BookIdParameter.Value = this.GetId();
      cmd.Parameters.Add(BookIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int authorId = rdr.GetInt32(0);
        string authorName = rdr.GetString(1);
        Author newAuthor = new Author(authorName, authorId);
        authors.Add(newAuthor);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return authors;
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO books (title, duedate) OUTPUT INSERTED.id VALUES (@BookTitle, @DueDate );", conn);

      SqlParameter titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@BookTitle";
      titleParameter.Value = this.GetTitle();
      cmd.Parameters.Add(titleParameter);

      SqlParameter dueDateParameter = new SqlParameter();
      dueDateParameter.ParameterName = "@DueDate";
      dueDateParameter.Value = this.GetDueDate();
      cmd.Parameters.Add(dueDateParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Update(string title, DateTime dueDate)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("update books set title=@NewTitle, duedate=@NewDueDate where id = @BookId;", conn);

      SqlParameter newTitleParameter = new SqlParameter();
      newTitleParameter.ParameterName = "@NewTitle";
      newTitleParameter.Value = title;
      cmd.Parameters.Add(newTitleParameter);

      SqlParameter newDueDateParameter = new SqlParameter();
      newDueDateParameter.ParameterName = "@NewDueDate";
      newDueDateParameter.Value = dueDate;
      cmd.Parameters.Add(newDueDateParameter);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this._id;
      cmd.Parameters.Add(bookIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("delete from books where id = @BookId; delete from book_authors where book_id = @BookId;", conn);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this._id;
      cmd.Parameters.Add(bookIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }



    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM books;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
