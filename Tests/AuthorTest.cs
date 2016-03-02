using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class AuthorTest : IDisposable
  {
    public void CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_AuthorEmptyAtFirst()
    {
      //Arrange, Act
      int result = Author.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameAuthor()
    {
      //Arrange, Act
      Author testAuthor = new Author("John Steinbeck", 1);
      Author secondAuthor = new Author("John Steinbeck", 1);

      //Assert
      Assert.Equal(testAuthor, secondAuthor);
    }

    [Fact]
    public void Test_Save_SavesAuthorToDatabase()
    {
      //Arrange
      Author testAuthor = new Author("George Orwell", 1);
      testAuthor.Save();

      //Act
      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetBooks_ReturnsAllBooksFromAuthor()
    {
      //Arrange
      Author testAuthor = new Author("Kanye West");
      testAuthor.Save();

      Book firstBook = new Book("Glow in the Dark", new DateTime(2016, 01, 01));
      firstBook.Save();

      Book secondBook = new Book("Something", new DateTime(2016, 01, 01));
      secondBook.Save();

      //Act
      testAuthor.AddAuthor(testAuthor);
      List<Book> savedBooks = testAuthor.GetBooks();
      List<Book> testList = new List<Book> {firstBook};

      //Assert
      Assert.Equal(testList, savedBooks);
    }

    [Fact]
    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }

  }
}
