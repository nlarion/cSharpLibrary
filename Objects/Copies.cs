using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Copies
  {
    private int _numberOf;
    // private Book _book;
    private int _bookId;
    private int _id;

    public Copies(int numberOf, int bookId, int id = 0 )
    {
      _numberOf = numberOf;
      _bookId = bookId;
      _id = id;
    }

    public override bool Equals(System.Object otherCopies)
    {
      if(!(otherCopies is Copies))
      {
        return false;
      }
      else
      {
        Copies newCopies = (Copies) otherCopies;
        bool idEquality = this.GetId() == newCopies.GetId();
        bool numberEquality = this.GetNumber() == newCopies.GetNumber();
        bool bookEquality = this.GetBookId() == newCopies.GetBookId();
        return (idEquality && numberEquality && bookEquality);
      }
    }

    //We need to be able to assign number of copies value to a book?

    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    public int GetNumber()
    {
      return _numberOf;
    }
    public void SetNumber(int numberOf)
    {
      _numberOf = numberOf;
    }
    public int GetBookId()
    {
      return _bookId;
    }
    public void SetBookId(int bookId)
    {
      _bookId = bookId;
    }

    public static List<Copies> GetAll()
    {
      List<Copies> allCopiess = new List<Copies>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("select * from copies;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int CopiesId = rdr.GetInt32(0);
        int CopiesNumber = rdr.GetInt32(1);
        int CopiesBookId = rdr.GetInt32(2);
        //need method to return authors here
        Copies newCopies = new Copies(CopiesNumber, CopiesBookId, CopiesId);
        allCopiess.Add(newCopies);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCopiess;
    }

    public static Copies Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM copies WHERE id = @CopiesId;", conn);

      SqlParameter CopiesIdParameter = new SqlParameter();
      CopiesIdParameter.ParameterName = "@CopiesId";
      CopiesIdParameter.Value = id.ToString();
      cmd.Parameters.Add(CopiesIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCopiesId = 0;
      int foundCopiesNumberOf = 0;
      int foundBookId = 0;

      while(rdr.Read())
      {
        foundCopiesId = rdr.GetInt32(0);
        foundCopiesNumberOf = rdr.GetInt32(1);
        foundBookId = rdr.GetInt32(2);
      }
      Copies foundCopies = new Copies(foundCopiesNumberOf, foundBookId, foundCopiesId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCopies;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO copies (number_of, book_id) OUTPUT INSERTED.id VALUES (@NumberOf, @BookId );", conn);

      SqlParameter numberOfParameter = new SqlParameter();
      numberOfParameter.ParameterName = "@NumberOf";
      numberOfParameter.Value = this.GetNumber();
      cmd.Parameters.Add(numberOfParameter);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetBookId();
      cmd.Parameters.Add(bookIdParameter);

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

    public void Update(int numberOf)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("update copies set number_of=@NewNumberOf where id = @CopyId;", conn);

      SqlParameter newNumberOfParameter = new SqlParameter();
      newNumberOfParameter.ParameterName = "@NewNumberOf";
      newNumberOfParameter.Value = numberOf;
      cmd.Parameters.Add(newNumberOfParameter);

      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@CopyId";
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

      SqlCommand cmd = new SqlCommand("delete from copies where id = @CopyId; delete from checkouts where copies_id = @CopyId;", conn);
      SqlParameter copyIdParameter = new SqlParameter();
      copyIdParameter.ParameterName = "@CopyId";
      copyIdParameter.Value = this._id;
      cmd.Parameters.Add(copyIdParameter);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM copies;", conn);
      cmd.ExecuteNonQuery();
    }


  }
}
