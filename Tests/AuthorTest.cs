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
      testAuthor.AddBooks(firstBook);
      List<Book> savedBooks = testAuthor.GetBooks();
      List<Book> testList = new List<Book> {firstBook};

      //Assert
      Assert.Equal(testList, savedBooks);
    }

    [Fact]
    public void Test_Update_UpdatesAuthorObject()
    {
      Author testAuthor = new Author("Kanye West");
      testAuthor.Save();

      testAuthor.Update("Adam West");

      Author afterAuthor = Author.Find(testAuthor.GetId());

      Assert.Equal(afterAuthor.GetAuthor(), "Adam West");
    }

    [Fact]
    public void Test_Update_UpdatesAuthorObjectInDB()
    {
      Author testAuthor = new Author("Kanye West");
      testAuthor.Save();

      testAuthor.Update("Adam West");

      Author afterAuthor = Author.Find(testAuthor.GetId());

      Author testAuthor2 = new Author("Adam West", testAuthor.GetId());
      // Console.WriteLine(afterAuthor.GetId());
      // Console.WriteLine(testAuthor2.GetId());
      Assert.Equal(afterAuthor, testAuthor2);
    }

    [Fact]
    public void Test_AddBooks_AddsBookToAuthor()
    {
      //Arrange
      Author testAuthor = new Author("Cindy Crawford", 1);
      testAuthor.Save();

      Book firstBook = new Book("My Journey", new DateTime(2016, 11, 01));
      firstBook.Save();

      Book secondBook = new Book("1984", new DateTime(2016, 11, 01));
      secondBook.Save();

      //Act
      testAuthor.AddBooks(firstBook);
      testAuthor.AddBooks(secondBook);

      List<Book> result = testAuthor.GetBooks();
      List<Book> testList = new List<Book>{firstBook, secondBook};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Delete_DeletesAuthorFromDatabase()
    {
      //Arrange
      Author testAuthor = new Author("Cindy Crawford");
      testAuthor.Save();

      Author testAuthor2 = new Author("Kanye West");
      testAuthor2.Save();

      //Act
      testAuthor.Delete();
      List<Author> resultList = Author.GetAll();
      List<Author> testList = new List<Author> {testAuthor2};

      //Assert
      Assert.Equal(resultList, testList);
    }

    [Fact]
    public void Test_SearchByTitle_ReturnsMatchingAuthorObject()
    {
      Author testAuthor = new Author("Tom Clancy");
      testAuthor.Save();

      List<Author> testList = new List<Author> {testAuthor};

      List<Author> resultList = Author.Search("Tom");

      Assert.Equal(testList, resultList);
    }

    [Fact]
    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }

  }
}
