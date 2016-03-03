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

    [Fact]
    public void Test_Update_UpdatesABookObject()
    {
      Book testBook = new Book("Glow in the Dark", new DateTime(2016, 01, 01));
      testBook.Save();

      testBook.Update("Glow in the Dark", new DateTime(2016,05,01));

      Book afterBook = Book.Find(testBook.GetId());

      Assert.Equal(afterBook.GetTitle(), "Glow in the Dark");
      Assert.Equal(afterBook.GetDueDate(), new DateTime(2016,05,01));
    }

    [Fact]
    public void Test_Update_UpdatesABookObjectInDB()
    {
      Book testBook = new Book("Glow in the Dark", new DateTime(2016,01,01), 1);
      testBook.Save();

      testBook.Update("Glow in the Dark", new DateTime(2016,05,01));

      Book afterBook = Book.Find(testBook.GetId());

      Book testBook2 = new Book("Glow in the Dark", new DateTime(2016,05,01), testBook.GetId());
      // Console.WriteLine(afterBook.GetId());
      // Console.WriteLine(testBook2.GetId());
      Assert.Equal(afterBook, testBook2);
    }

    [Fact]
    public void Test_Find_FindsBookInDB()
    {
      //Arrange
      Book testBook = new Book("1984", new DateTime(1999,01,01), 1);
      testBook.Save();

      //Act
      Book foundBook = Book.Find(testBook.GetId());

      //Assert
      Assert.Equal(testBook, foundBook);
    }

    [Fact]
    public void Test_Delete_DeletesBookFromDatabase()
    {
      //Arrange
      Book testBook = new Book("1984", new DateTime(1999,01,01));
      testBook.Save();

      Book testBook2 = new Book("Glow in the Dark", new DateTime(2016,01,01));
      testBook2.Save();

      //Act
      testBook.Delete();
      List<Book> resultList = Book.GetAll();
      List<Book> testList = new List<Book> {testBook2};

      Console.WriteLine(testBook2.GetTitle());
      Console.WriteLine(testBook.GetTitle());
      //Assert
      Assert.Equal(resultList, testList);
    }

    [Fact]
    public void Test_SearchByTitle_ReturnsMatchingBookObject()
    {
      Book testBook = new Book("Where the Grass Grows", new DateTime(1999,01,01));
      testBook.Save();

      List<Book> testList = new List<Book> {testBook};

      List<Book> resultList = Book.Search("Where");

      Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Test_OverDueBooks_ReturnsBooksPastDueDate()
    {
      Book testBook = new Book("Where the Grass Grows", new DateTime(2016,02,14));
      testBook.Save();

      List<Book> testList = new List<Book> {testBook};

      List<Book> resultList = Book.OverDueBooks();

      Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
    }

  }
}
