using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class AuthorTest
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

  }
}