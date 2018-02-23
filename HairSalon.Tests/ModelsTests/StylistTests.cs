using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
 {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=cameron_anderson_test;";
    }
    
    public void Dispose()
    {
      Stylist.Clear();
    }
    
    
    [TestMethod]
    public void GetAll_ReturnAllStylists_ListStylist()
    {
      Stylist newStylist = new Stylist("smith", "john");
      newStylist.Save();
      List<Stylist> testList = new List<Stylist>{newStylist};
      List<Stylist> result = Stylist.GetAll();
      
      Assert.AreEqual(testList.Count, result.Count);
      CollectionAssert.AreEqual(testList, result);
    }
  }
}