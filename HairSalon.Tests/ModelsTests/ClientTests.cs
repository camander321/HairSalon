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
      Client.Clear();
    }
    
    public void Dispose()
    {
      Client.Clear();
    }
    
    
    [TestMethod]
    public void GetAll_ReturnAllClients_ListClient()
    {
      Client newClient = new Client("smith", "john");
      newClient.Save();
      List<Client> testList = new List<Client>{newClient};
      List<Client> result = Client.GetAll();
      
      CollectionAssert.AreEqual(testList, result);
    }
  }
}