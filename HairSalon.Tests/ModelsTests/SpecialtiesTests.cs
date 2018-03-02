using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
 {
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=cameron_anderson_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Specialty.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ReturnListOfAllSpecialties_ListSpecialty()
    {
      Specialty testSpecialty = new Specialty("buzz cut");
      testSpecialty.Save();

      CollectionAssert.AreEqual(new List<Specialty>{testSpecialty}, Specialty.GetAll());
    }

    [TestMethod]
    public void Find_ReturnSpecialtyWithId_Specialty()
    {
      Specialty testSpecialty = new Specialty("buzz cut");
      testSpecialty.Save();
      int id = testSpecialty.GetId();

      Assert.AreEqual(testSpecialty, Specialty.Find(id));
    }

    [TestMethod]
    public void Delete_DeleteSpecialtyFromDatabase_void()
    {
      Specialty testSpecialty1 = new Specialty("buzz cut");
      testSpecialty1.Save();
      Specialty testSpecialty2 = new Specialty("layering");
      testSpecialty2.Save();

      testSpecialty1.Delete();

      CollectionAssert.AreEqual(new List<Specialty>{testSpecialty2}, Specialty.GetAll());
    }

    [TestMethod]
    public void GetStylists_ReturnListOfAllStylists_ListStylist()
    {
      Stylist testStylist1 = new Stylist("Skywalker", "Luke");
      testStylist1.Save();
      Stylist testStylist2 = new Stylist("Ross", "Bob");
      testStylist2.Save();
      Stylist testStylist3 = new Stylist("Johnson", "Dwayne");
      testStylist3.Save();

      Specialty specialty = new Specialty("Buzz Cut");
      specialty.Save();

      specialty.AddStylist(testStylist1);
      specialty.AddStylist(testStylist3);

      CollectionAssert.AreEqual(new List<Stylist>{testStylist1, testStylist3}, specialty.GetStylists());
    }
  }
}