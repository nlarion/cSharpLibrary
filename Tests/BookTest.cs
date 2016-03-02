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



    // [Fact]
    // public void Test_AddBook_AddsBookToAuthor()
    // {
    //   //Arrange
    //   Author testAuthor = new Author("Cindy Crawford", 1);
    //   testAuthor.Save();
    //
    //   Book firstBook = new Book("My Journey", new DateTime(2016, 11, 01));
    //   firstBook.Save();
    //
    //   Book secondBook = new Book("1984", new DateTime(2016, 11, 01));
    //   secondBook.Save();
    //
    //   //Act
    //   testAuthor.AddBook(firstBook);
    //   testAuthor.AddBook(secondBook);
    //
    //   List<Book> result = testAuthor.GetBooks();
    //   List<Book> testList = new List<Book>{firstBook, secondBook};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }
    //
    // [Fact]
    // public void Test_GetBooks_ReturnsAllAuthorBooks()
    // {
    //   //Arrange
    //   Course testCourse = new Course("Science", 101);
    //   testCourse.Save();
    //
    //   Book firstBook = new Book("Magic Johnson", new DateTime(2016, 01, 01));
    //   firstBook.Save();
    //
    //   Book secondBook = new Book("Magic James", new DateTime(2016, 01, 01));
    //   secondBook.Save();
    //
    //   //Act
    //   testCourse.AddBook(firstBook);
    //   List<Book> savedBooks = testCourse.GetBooks();
    //   List<Book> testList = new List<Book> {firstBook};
    //
    //   //Assert
    //   Assert.Equal(testList, savedBooks);
    // }



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
