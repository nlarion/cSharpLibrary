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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM copies;", conn);
      cmd.ExecuteNonQuery();
    }


  }
}
