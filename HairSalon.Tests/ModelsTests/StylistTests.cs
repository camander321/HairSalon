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
      Stylist.DeleteAll();
      Client.DeleteAll();
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
      
      Client client1 = new Client("james", "lebron", stylist1.GetId());
      client1.Save();
      Client client2 = new Client("jones", "bill", stylist1.GetId());
      client2.Save();
      
      testList.Add(client1);
      testList.Add(client2);
      
      
      Stylist stylist2 = new Stylist("solo", "han");
      stylist2.Save();
      
      Client client3 = new Client("skywalker", "luke", stylist2.GetId());
      client3.Save();
      Client client4 = new Client("kenobi", "obi wan", stylist2.GetId());
      client4.Save();
      
      
      List<Client> result = stylist1.GetClients();
      
      Assert.AreEqual(testList.Count, result.Count);
      CollectionAssert.AreEqual(testList, result);
    }
    
    [TestMethod]
    public void Find_ReturnStylistWithId_Stylist()
    {
      Stylist newStylist = new Stylist("smith", "john");
      newStylist.Save();
      Stylist result = Stylist.Find(newStylist.GetId());
      
      Assert.AreEqual(newStylist, result);
    }
    
    [TestMethod]
    public void Search_ReturnAllStylistsWithStringInName_ListStylist()
    {
      Stylist newStylist = new Stylist("smith", "john");
      newStylist.Save();
      List<Stylist> result = Stylist.Search("smi");
      
      CollectionAssert.AreEqual(new List<Stylist>{newStylist}, result);
    }
    
    [TestMethod]
    public void EditName_ChangeNameAndUpdateDatabase_Void()
    {
      Stylist newStylist = new Stylist("smith", "john");
      newStylist.Save();
      
      newStylist.EditName("doe", "jane");
      
      Stylist result = Stylist.Find(newStylist.GetId());
      
      Assert.AreEqual(newStylist.GetId(), result.GetId());
      Assert.AreEqual(newStylist.GetLast(), result.GetLast());
      Assert.AreEqual(newStylist.GetFirst(), result.GetFirst());
      Assert.AreEqual(newStylist, result);
    }
  }
}