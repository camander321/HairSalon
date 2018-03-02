using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
    private string _name;

    public Specialty(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        return _id == newSpecialty._id && _name == newSpecialty._name;
      }
    }

    public override int GetHashCode()
    {
      return _id.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@name)";
      cmd.Parameters.Add(new MySqlParameter("@name", _name));

      cmd.ExecuteNonQuery();

      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM specialties";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int SpecialtyId = rdr.GetInt32(0);
        string SpecialtyName = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
        allSpecialties.Add(newSpecialty);
      }

      conn.Close();
      if (conn != null)
        conn.Dispose();
      return allSpecialties;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM `specialties` WHERE id = @thisId;";
      cmd.Parameters.Add(new MySqlParameter("@thisId", id));
      MySqlDataReader rdr = cmd.ExecuteReader();

      int SpecialtyId = 0;
      string SpecialtyName = "";

      if(rdr.Read())
      {
        SpecialtyId = rdr.GetInt32(0);
        SpecialtyName = rdr.GetString(1);
      }
      Specialty foundSpecialty = new Specialty(SpecialtyName, SpecialtyId);

      conn.Close();
      if (conn !=null)
        conn.Dispose();

      return foundSpecialty;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = this._id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddStylist(Stylist stylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      Console.WriteLine(_id + " " + stylist.GetId());

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId)";
      cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", _id));
      cmd.Parameters.Add(new MySqlParameter("@StylistId", stylist.GetId()));
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }

    public List<Stylist> GetStylists()
    {
      List<Stylist> stylists = new List<Stylist>();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"
        SELECT stylists.* FROM specialties
        JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
        JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
        WHERE specialties.id = @ThisId;";
      cmd.Parameters.Add(new MySqlParameter("@ThisId", _id));
      MySqlDataReader rdr = cmd.ExecuteReader();
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
  }
}