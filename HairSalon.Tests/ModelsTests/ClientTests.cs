using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
 {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=cameron_anderson_test;";
    }
    
    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
    
    
    [TestMethod]
    public void GetAll_ReturnAllClients_ListClient()
    {
      Stylist stylist = new Stylist("Skywalker", "Luke");
      stylist.Save();
      Client newClient = new Client("smith", "john", stylist.GetId());
      newClient.Save();
      List<Client> testList = new List<Client>{newClient};
      List<Client> result = Client.GetAll();
      
      CollectionAssert.AreEqual(testList, result);
    }
    
    [TestMethod]
    public void Find_ReturnClientWithId_Client()
    {
      Stylist stylist = new Stylist("Skywalker", "Luke");
      stylist.Save();
      Client newClient = new Client("smith", "john", stylist.GetId());
      newClient.Save();
      Client result = Client.Find(newClient.GetId());
      
      Assert.AreEqual(newClient, result);
    }
    
    [TestMethod]
    public void Search_ReturnAllClientsWithStringInName_ListClient()
    {
      Stylist stylist = new Stylist("Skywalker", "Luke");
      stylist.Save();
      Client newClient = new Client("smith", "john", stylist.GetId());
      newClient.Save();
      List<Client> result = Client.Search("smi");
      
      CollectionAssert.AreEqual(new List<Client>{newClient}, result);
    }
    
    [TestMethod]
    public void EditName_ChangeNameAndUpdateDatabase_Void()
    {
      Stylist stylist = new Stylist("Skywalker", "Luke");
      stylist.Save();
      Client newClient = new Client("anderson", "cameron", stylist.GetId());
      newClient.Save();
      newClient.EditName("vader", "darth");
      
      Client result = Client.Find(newClient.GetId());
      
      Assert.AreEqual(newClient, result);
    }
  }
}