using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _lastName;
    private string _firstName;
    private int _stylistId;
    
    public Client(string lastName, string firstName, int stylist = 0, int id = 0)
    {
      _id = id;
      _lastName = lastName;
      _firstName = firstName;
      _stylistId = stylist;
    }
    
     public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
        return false;
      Client newClient = (Client) otherClient;
      return _lastName == newClient._lastName && _firstName == newClient._firstName && _id == newClient._id && _stylistId == newClient._stylistId;
    }
    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
    
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (last_name, first_name, stylist) VALUES (@lastName, @firstName, @stylist);";
      cmd.Parameters.Add(new MySqlParameter("@lastName", _lastName));
      cmd.Parameters.Add(new MySqlParameter("@firstName", _firstName));
      cmd.Parameters.Add(new MySqlParameter("@stylist", _stylistId));
      
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string lastName = rdr.GetString(1);
        string firstName = rdr.GetString(2);
        int stylistId = rdr.GetInt32(3);
        Client newClient = new Client(lastName, firstName, stylistId, id);
        allClients.Add(newClient);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return allClients;
    }
    
    public static void Clear()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients; ALTER TABLE clients AUTO_INCREMENT = 1;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
  }
}