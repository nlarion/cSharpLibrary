using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class CopiesTest : IDisposable
  {
    public CopiesTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CopiesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Copies.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameCopy()
    {
      //Arrange, Act
      Book testBook = new Book("Grapes of Wrath", new DateTime(2016,06,01), 1);

      Copies testCopy = new Copies(3, testBook.GetId(), 1);
      Copies testCopy2 = new Copies(3, testBook.GetId(), 1);

      //Assert
      Assert.Equal(testCopy, testCopy2);
    }

    [Fact]
    public void Dispose()
    {
      Copies.DeleteAll();
      Book.DeleteAll();
      // Author.DeleteAll();
    }
  }
}
