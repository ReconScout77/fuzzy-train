using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace VideoGames.Models
{
  public class Games
  {
    private int _id;
    private string _title;

    public Games(string title, int Id = 0)
    {
      _id = Id;
      _title = title;
    }

    public string GetTitle()
    {
      return _title;
    }

    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherGames)
    {
      if (!(otherGames is Games))
      {
        return false;
      }
      else
      {
        Games newGame = (Games) otherGames;
        bool idEquality = (this.GetId() == newGame.GetId());
        bool titleEquality = (this.GetTitle() == newGame.GetTitle());
        return (idEquality && titleEquality);
      }
    }

    public static List<Games> GetAll()
    {
        List<Games> allGames = new List<Games> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM games;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int gameId = rdr.GetInt32(0);
          string gameName = rdr.GetString(1);
          Games newGame = new Games(gameName, gameId);
          allGames.Add(newGame);
        }
        return allGames;
    }

    public void Save()
    {
       MySqlConnection conn = DB.Connection();
       conn.Open();

       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO `games` (`title`) VALUES (@GamesTitle);";

       MySqlParameter title = new MySqlParameter();
       title.ParameterName = "@GamesTitle";
       title.Value = this._title;
       cmd.Parameters.Add(title);

       cmd.ExecuteNonQuery();
       _id = (int) cmd.LastInsertedId;
    }

    // public void Remove(int id)
    // {
    //    MySqlConnection conn = DB.Connection();
    //    conn.Open();
    //
    //    var cmd = conn.CreateCommand() as MySqlCommand;
    //    cmd.CommandText = @"DELETE FROM `games` (`title`) VALUES (@GamesTitle);";
    //
    //    MySqlParameter title = new MySqlParameter();
    //    title.ParameterName = "@GamesTitle";
    //    title.Value = this._title;
    //    cmd.Parameters.Add(title);
    //
    //    cmd.ExecuteNonQuery();
    //    _id = (int) cmd.LastInsertedId;
    // }

    public static void DeleteAll()
    {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"DELETE FROM games;";
       cmd.ExecuteNonQuery();
    }

    public static Games Find(int id)
   {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * FROM `games` WHERE id = @thisId;";

       MySqlParameter thisId = new MySqlParameter();
       thisId.ParameterName = "@thisId";
       thisId.Value = id;
       cmd.Parameters.Add(thisId);

       var rdr = cmd.ExecuteReader() as MySqlDataReader;

       int gameId = 0;
       string gamesTitle = "";

       while (rdr.Read())
       {
          gameId = rdr.GetInt32(0);
          gamesTitle = rdr.GetString(1);
       }
       Games foundGame= new Games(gamesTitle, gameId);
       return foundGame;
   }

  }
}




    //
    //
    // //...GETTERS AND SETTERS HERE...
    //
