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
    
    public int GetId() {return _id;}
    public string GetFirst() {return _firstName;}
    public string GetLast() {return _lastName;}
    public int GetStylist() {return _stylistId;}
    
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
      cmd.Parameters.AddWithValue("@lastName", _lastName);
      cmd.Parameters.AddWithValue("@firstName", _firstName);
      cmd.Parameters.AddWithValue("@stylist", _stylistId);
      
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public static Client Find(int searchId)
    {      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @id;";
      cmd.Parameters.Add(new MySqlParameter("@id", searchId));
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      int id = 0;
      string firstName = "";
      string lastName = "";
      int stylistId = 0;
      Client client;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        lastName = rdr.GetString(1);
        firstName = rdr.GetString(2);
        stylistId = rdr.GetInt32(3);
      }
      client = new Client(lastName, firstName, stylistId, id);
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return client;
    }
    
    public static List<Client> Search(string searchString)
    {
      List<Client> clients = new List<Client>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE first_name LIKE @SearchString OR last_name LIKE @SearchString;";
      cmd.Parameters.AddWithValue("@SearchString", '%' + searchString + '%');
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string lastName = rdr.GetString(1);
        string firstName = rdr.GetString(2);
        int stylistId = rdr.GetInt32(3);
        Client newClient = new Client(lastName, firstName, stylistId, id);
        clients.Add(newClient);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return clients;
    }
    
    public void EditName(string last, string first)
    {
      _lastName = last;
      _firstName = first;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"
        UPDATE clients SET 
        last_name = @lastName,
        first_name = @firstName
        WHERE id = @id;
      ";
      cmd.Parameters.AddWithValue("@lastName", _lastName);
      cmd.Parameters.AddWithValue("@firstName", _firstName);
      cmd.Parameters.AddWithValue("@id", _id);
      
      cmd.ExecuteNonQuery();
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public void ChangeStylist(int stylistId)
    {
      _stylistId = stylistId;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"
        UPDATE clients SET 
        stylist = @stylist
        WHERE id = @id;
      ";
      cmd.Parameters.AddWithValue("@stylist", _stylistId);
      cmd.Parameters.AddWithValue("@id", _id);
      
      cmd.ExecuteNonQuery();
      
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
      cmd.CommandText = @"SELECT * FROM clients ORDER BY last_name, first_name;";
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
    
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @Id;";
      cmd.Parameters.AddWithValue("@Id", _id);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public static void DeleteAll()
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