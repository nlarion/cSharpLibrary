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
    public void Test_Save_SavesCopiesOfBookToDatabase()
    {
      Book testBook = new Book("1984", new DateTime(2016,11,01), 1);

      Copies testCopy = new Copies(2, testBook.GetId(), 1);
      testCopy.Save();

      List<Copies> result = Copies.GetAll();
      List<Copies> testList = new List<Copies>{testCopy};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Update_UpdatesABookObjectInDB()
    {
      Book testBook = new Book("1984", new DateTime(1999,01,01), 1);
      Copies testCopies = new Copies(12,testBook.GetId());
      testCopies.Save();

      testCopies.Update(11);

      Copies afterCopies = Copies.Find(testCopies.GetId());

      Copies testCopies2 = new Copies(11,testBook.GetId(),afterCopies.GetId());
      // Console.WriteLine(afterCopies.GetId());
      // Console.WriteLine(testCopies2.GetId());
      Assert.Equal(afterCopies, testCopies2);
    }

    [Fact]
    public void Test_Find_FindsCopiesInDB()
    {
      //Arrange
      Book testBook = new Book("1984", new DateTime(1999,01,01), 1);
      Copies testCopies = new Copies(12,testBook.GetId());
      testCopies.Save();

      //Act
      Copies foundCopies = Copies.Find(testCopies.GetId());

      //Assert
      Assert.Equal(testCopies, foundCopies);
    }

    [Fact]
    public void Test_Delete_DeletesBookFromDatabase()
    {
      //Arrange
      Book testBook = new Book("1984", new DateTime(2016,11,01), 1);
      Copies testCopies = new Copies(12,testBook.GetId());
      testCopies.Save();

      Copies testCopies2 = new Copies(11,testBook.GetId());
      testCopies2.Save();

      //Act
      testCopies.Delete();
      List<Copies> resultList = Copies.GetAll();
      List<Copies> testList = new List<Copies> {testCopies2};

      //Assert
      Assert.Equal(resultList, testList);
    }

    [Fact]
    public void Dispose()
    {
      Patron.DeleteAll();
      Copies.DeleteAll();
      // Book.DeleteAll();
      // Author.DeleteAll();
    }
  }
}
