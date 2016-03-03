using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Author
  {
    private string _author;
    private int _id;

    public Author(string author, int id = 0)
    {
      _id = id;
      _author = author;
    }
    public override bool Equals(System.Object otherAuthor)
    {
      if (!(otherAuthor is Author))
      {
        return false;
      }
      else
      {
        Author newAuthor = (Author) otherAuthor;
        bool idEquality = this.GetId() == newAuthor.GetId();
        bool nameEquality = this.GetAuthor() == newAuthor.GetAuthor();
        return (idEquality && nameEquality);
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
    public string GetAuthor()
    {
      return _author;
    }
    public void SetAuthor(string author)
    {
      _author = author;
    }
    public void Update(string author)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("update authors set name=@NewAuthor where id = @AuthorId;", conn);

      SqlParameter newAuthorParameter = new SqlParameter();
      newAuthorParameter.ParameterName = "@NewAuthor";
      newAuthorParameter.Value = author;
      cmd.Parameters.Add(newAuthorParameter);

      SqlParameter authorIdParameter = new SqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = this._id;
      cmd.Parameters.Add(authorIdParameter);

      cmd.ExecuteNonQuery();

      this._author = author;

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static List<Author> Search(string name)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Author> searchResults = new List<Author>{};

      SqlCommand cmd = new SqlCommand("SELECT * FROM authors WHERE name LIKE @SearchName;", conn);

      SqlParameter nameSearchParameter = new SqlParameter();
      nameSearchParameter.ParameterName = "@SearchName";
      nameSearchParameter.Value = "%" + name + "%";
      cmd.Parameters.Add(nameSearchParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int AuthorId = rdr.GetInt32(0);
        string AuthorName= rdr.GetString(1);
        //need method to return authors here
        Author newAuthor = new Author(AuthorName, AuthorId);
        searchResults.Add(newAuthor);
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

    public static Author Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM authors WHERE id = @AuthorId;", conn);

      SqlParameter AuthorIdParameter = new SqlParameter();
      AuthorIdParameter.ParameterName = "@AuthorId";
      AuthorIdParameter.Value = id.ToString();
      cmd.Parameters.Add(AuthorIdParameter);
      rdr = cmd.ExecuteReader();

      int foundAuthorId = 0;
      string foundAuthorName = null;

      while(rdr.Read())
      {
        foundAuthorId = rdr.GetInt32(0);
        foundAuthorName = rdr.GetString(1);
      }
      Author foundAuthor = new Author(foundAuthorName, foundAuthorId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAuthor;
    }

    public void AddBooks(Book newBook)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO book_authors (author_id, book_id) VALUES (@AuthorId, @BookId)", conn);

      SqlParameter authorIdParameter = new SqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = this.GetId();
      cmd.Parameters.Add(authorIdParameter);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@BookId";
      studentIdParameter.Value = newBook.GetId();
      cmd.Parameters.Add(studentIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Book> GetBooks()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Book> books = new List<Book>{};

      SqlCommand cmd = new SqlCommand("select b.id, b.title, b.duedate from books b inner join book_authors ba on b.id = ba.book_id inner join authors a on a.id = ba.author_id where a.id = @AuthorId", conn);
      SqlParameter AuthorIdParameter = new SqlParameter();

      AuthorIdParameter.ParameterName = "@AuthorId";
      AuthorIdParameter.Value = this.GetId();
      cmd.Parameters.Add(AuthorIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookName = rdr.GetString(1);
        DateTime bookDueDate = rdr.GetDateTime(2);
        Book newBook = new Book(bookName, bookDueDate, bookId);
        books.Add(newBook);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return books;
    }

    public static List<Author> GetAll()
    {
      List<Author> allAuthors = new List<Author>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("select * from authors", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int AuthorId = rdr.GetInt32(0);
        string AuthorName= rdr.GetString(1);
        Author newAuthor = new Author(AuthorName, AuthorId);
        allAuthors.Add(newAuthor);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAuthors;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO authors (name) OUTPUT INSERTED.id VALUES (@AuthorName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AuthorName";
      nameParameter.Value = this.GetAuthor();
      cmd.Parameters.Add(nameParameter);

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

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("delete from authors where id = @AuthorId; delete from book_authors where author_id = @AuthorId;", conn);

      SqlParameter authorIdParameter = new SqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = this._id;
      cmd.Parameters.Add(authorIdParameter);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM authors;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
