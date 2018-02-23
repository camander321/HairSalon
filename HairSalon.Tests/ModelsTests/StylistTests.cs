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
      Stylist.Clear();
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
    
    [TestMethod]
    public void GetClients_ReturnStylistsClients_ListClient()
    {
      List<Client> testList = new List<Client>();
      
      Stylist stylist1 = new Stylist("smith", "john");
      stylist1.Save();  
      testList.Add(stylist1.AddClient("jones", "bill"));
      testList.Add(stylist1.AddClient("james", "lebron"));
      
      Stylist stylist2 = new Stylist("solo", "han");
      stylist2.Save();
      stylist2.AddClient("skywalker", "luke");
      stylist2.AddClient("kenobi", "obi wan");
      
      List<Client> result = stylist1.GetClients();
      
      Assert.AreEqual(testList.Count, result.Count);
      CollectionAssert.AreEqual(testList, result);
    }
  }
}