using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _id;
    public string _lastName;
    public string _firstName;
    
    public Stylist(string lastName, string firstName, int id = 0)
    {
      _id = id;
      _lastName = lastName;
      _firstName = firstName;
    }
    
    public int GetId() {return _id;}
    public string GetFirst() {return _firstName;}
    public string GetLast() {return _lastName;}
    
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
        return false;
      Stylist newStylist = (Stylist) otherStylist;
      return _lastName == newStylist._lastName && _firstName == newStylist._firstName && _id == newStylist._id;
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
      cmd.CommandText = @"INSERT INTO stylists (last_name, first_name) VALUES (@lastName, @firstName);";
      cmd.Parameters.Add(new MySqlParameter("@lastName", _lastName));
      cmd.Parameters.Add(new MySqlParameter("@firstName", _firstName));
      
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public List<Client> GetClients()
    {
      List<Client> clients = new List<Client>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist = @stylist ORDER BY last_name, first_name;";
      cmd.Parameters.AddWithValue("@stylist", _id);
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
    
    public static Stylist Find (int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @id;";
      cmd.Parameters.AddWithValue("@id", searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      int id = 0;
      string lastName = "";
      string firstName = "";
      Stylist stylist;
      if(rdr.Read())
      {
        id = rdr.GetInt32(0);
        lastName = rdr.GetString(1);
        firstName = rdr.GetString(2);
      }
      stylist = new Stylist(lastName, firstName, id);
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return stylist;
    }
    
    public static List<Stylist> Search(string searchString)
    {
      List<Stylist> stylists = new List<Stylist>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE first_name LIKE @SearchString OR last_name LIKE @SearchString;";
      cmd.Parameters.AddWithValue("@SearchString", '%' + searchString + '%');
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string lastName = rdr.GetString(1);
        string firstName = rdr.GetString(2);
        Stylist newStylist = new Stylist(lastName, firstName, id);
        stylists.Add(newStylist);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return stylists;
    }
    
    public void EditName(string last, string first)
    {
      _lastName = last;
      _firstName = first;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"
        UPDATE stylists SET 
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
    
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylist = new List<Stylist>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists ORDER BY last_name, first_name;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        int id = rdr.GetInt32(0);
        string lastName = rdr.GetString(1);
        string firstName = rdr.GetString(2);
        Stylist newStylist = new Stylist(lastName, firstName, id);
        allStylist.Add(newStylist);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return allStylist;
    }
    
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @Id;";
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
      cmd.CommandText = @"DELETE FROM stylists; ALTER TABLE stylists AUTO_INCREMENT = 1;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public void AddSpecialty(Specialty specialty)
    {
      specialty.AddStylist(_id);
    }

    public List<Specialty> GetSpecialties()
    {
      List<Specialty> specialties = new List<Specialty>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"
        SELECT specialties.* FROM stylists
        JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
        JOIN specialties ON (stylists_specialties.specialty_id = specialties.id)
        WHERE stylists.id = @ThisId;";
      cmd.Parameters.Add(new MySqlParameter("@ThisId", _id));
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(name, id);
        specialties.Add(newSpecialty);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();

      return specialties;
    }
  }
}