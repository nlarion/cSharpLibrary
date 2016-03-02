using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class BookTest : IDisposable
  {
    public BookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_BooksEmptyAtFirst()
    {
      //Arrange, Act
      int result = Book.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameBook()
    {
      //Arrange, Act
      Author testAuthor = new Author("John Steinbeck", 1);
      List<Author> listAuthor = new List<Author>{testAuthor};

      Book testBook = new Book("Grapes of Wrath", new DateTime(2016,06,01), 1);
      Book secondBook = new Book("Grapes of Wrath", new DateTime(2016,06,01), 1);

      //Assert
      Assert.Equal(testBook, secondBook);
    }



    // [Fact]
    // public void Test_Returns_AuthorsNamesFromBookId()
    // {
    //   Author testAuthor = new Author("Ken Kesey", 1);
    //   Author testAuthor2 = new Author("Cindy Crawford". 2);
    //   List<Author> listAuthor = new List<Author>{testAuthor, testAuthor2};
    //
    //
    //
    //
    //   List<Author> dummyList =
    // }

    [Fact]
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
    }

  }
}
