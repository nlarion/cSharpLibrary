using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class PatronTest : IDisposable
  {
    public PatronTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_PatronEmptyAtFirst()
    {
      //Arrange, Act
      int result = Patron.GetAll().Count;

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
    public void Test_Save_SavesPatronToDatabase()
    {
      //Arrange
      Patron testPatron = new Patron("Harry Harrison", 1);
      testPatron.Save();

      //Act
      List<Patron> result = Patron.GetAll();
      List<Patron> testList = new List<Patron>{testPatron};

      //Assert
      Assert.Equal(testList, result);
    }
    //
    // [Fact]
    // public void Test_GetBooks_ReturnsAllBooksFromPatron()
    // {
    //   //Arrange
    //   Author testAuthor = new Author("Kanye West");
    //   testAuthor.Save();
    //
    //   Book firstBook = new Book("Glow in the Dark", new DateTime(2016, 01, 01));
    //   firstBook.Save();
    //
    //   Book secondBook = new Book("Something", new DateTime(2016, 01, 01));
    //   secondBook.Save();
    //
    //   //Act
    //   testAuthor.AddBooks(firstBook);
    //   List<Book> savedBooks = testAuthor.GetBooks();
    //   List<Book> testList = new List<Book> {firstBook};
    //
    //   //Assert
    //   Assert.Equal(testList, savedBooks);
    // }

    [Fact]
    public void Test_Update_UpdatesPatronObject()
    {
      Patron testPatron = new Patron("Kanye West");
      testPatron.Save();

      testPatron.Update("Adam West");

      Patron afterPatron = Patron.Find(testPatron.GetId());

      Assert.Equal(afterPatron.GetName(), "Adam West");
    }

    [Fact]
    public void Test_Update_UpdatesPatronObjectInDB()
    {
      Patron testPatron = new Patron("Kanye West");
      testPatron.Save();

      testPatron.Update("Adam West");

      Patron afterPatron = Patron.Find(testPatron.GetId());

      Patron testPatron2 = new Patron("Adam West", testPatron.GetId());
      // Console.WriteLine(afterPatron.GetId());
      // Console.WriteLine(testPatron2.GetId());
      Assert.Equal(afterPatron, testPatron2);
    }

    // [Fact]
    // public void Test_AddBooks_AddsBookToAuthor()
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
    //   testAuthor.AddBooks(firstBook);
    //   testAuthor.AddBooks(secondBook);
    //
    //   List<Book> result = testAuthor.GetBooks();
    //   List<Book> testList = new List<Book>{firstBook, secondBook};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }

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

    // [Fact]
    // public void Test_SearchByTitle_ReturnsMatchingAuthorObject()
    // {
    //   Author testAuthor = new Author("Tom Clancy");
    //   testAuthor.Save();
    //
    //   List<Author> testList = new List<Author> {testAuthor};
    //
    //   List<Author> resultList = Author.Search("Tom");
    //
    //   Assert.Equal(testList, resultList);
    // }

    [Fact]
    public void Dispose()
    {
      Patron.DeleteAll();
      // Author.DeleteAll();
      // Book.DeleteAll();
    }

  }
}
