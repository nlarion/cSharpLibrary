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

    [Fact]
    public void Test_Save_SavesBooksToDatabase()
    {
      //Arrange
      Book testBook = new Book("1984", new DateTime(2016,11,01), 1);
      testBook.Save();

      //Act
      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      //Assert
      Assert.Equal(testList, result);
    }



    [Fact]
    public void Test_AddAuthor_AddsAuthorToBook()
    {
      //Arrange
      Author testAuthor = new Author("Cindy Crawford", 1);
      testAuthor.Save();

      Book firstBook = new Book("My Journey", new DateTime(2016, 11, 01));
      firstBook.Save();

      Book secondBook = new Book("1984", new DateTime(2016, 11, 01));
      secondBook.Save();

      //Act
      firstBook.AddAuthor(testAuthor);
      secondBook.AddAuthor(testAuthor);

      List<Author> result = firstBook.GetAuthors();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAuthors_ReturnsAllAuthorBooks()
    {
      //Arrange
      Author testAuthor = new Author("Kanye West");
      testAuthor.Save();

      Book firstBook = new Book("Glow in the Dark", new DateTime(2016, 01, 01));
      firstBook.Save();

      Book secondBook = new Book("Something", new DateTime(2016, 01, 01));
      secondBook.Save();

      //Act
      testAuthor.AddBooks(firstBook);
      List<Author> savedAuthors = firstBook.GetAuthors();
      List<Author> testList = new List<Author> {testAuthor};

      //Assert
      Assert.Equal(testList, savedAuthors);
    }

    // [Fact]
    // public



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
