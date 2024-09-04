using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;

public class SQLiteTest: MonoBehaviour
{
    
     private static string dbName = $"{Application.persistentDataPath}/Data.db"; //{Application.persistentDataPath}/
     
     private static SQLiteConnection _db;
     
     
     
     static SQLiteTest()
     {
         _db = new SQLiteConnection(dbName);
         _db.CreateTable<Stock>();
         //_db.Insert(new Stock("Yes"));
     }
     
     // Start is called before the first frame updat
     
     public static void GenerateDB(string query)
     {
         _db = new SQLiteConnection(dbName);
         _db.CreateTable<Stock>();
         _db.Insert(new Stock{Symbol = "Yes"});
     }

     /// <summary>
     /// when we want to pull anything from the database
     /// </summary>
     /// <param name="query"></param>
     public static void pullFromDataBase(string query)
     {
             var options = new SQLiteConnectionString(dbName, false);
             var conn= new SQLiteConnection(options);
    
             //string query = $"SELECT * FROM Stocks";
    
             var results = conn.Query<Stock>(query);

             foreach (var result in results)
             {
                 Debug.LogWarning(result);    
             }
             
             
             conn.Close();
     }

     /// <summary>
     /// when we want to create a new difficulty level
     /// </summary>
     public static void CreateNewDifficultyLevel()
     {
         //TODO: when a new player or existing player joins then we need to handle SQL to check if there is data
         var options = new SQLiteConnectionString(dbName, false);
         var conn= new SQLiteConnection(options);

         Debug.LogWarning(dbName);
         if (IsTherePreviousData())
         {
             Debug.LogWarning($"there is already data, lets reset it");
             UpdateDifficultyLevel(1, 100f);
         }
         else
         {
             Debug.LogWarning($"new person yippeee, establishing database");
             conn.CreateTable<Difficulty>();
             conn.Insert(new Difficulty { DifficultyLevel = 100f });     
         }
         
     }

     public static bool IsTherePreviousData()
     {
         //grabbing connection from database
         var options = new SQLiteConnectionString(dbName, false);
         var conn= new SQLiteConnection(options);

         string query = "Select id FROM DifficultyLevels WHERE Id = 1";

         try
         {
             var results = conn.Query<Difficulty>(query);
         }
         catch (Exception e)
         {
             Console.WriteLine(e);
             return false;
             throw;
         }

         return true; //there is data
         // if (results.Count >= 0) return true;
         //else 

     }
     
     /// <summary>
     /// when we want to get the difficulty level from the database
     /// </summary>
     /// <param name="id"></param>
     /// <returns></returns>
     public static float PullDifficultyLevel(int id)
     {
         var options = new SQLiteConnectionString(dbName, false);
         var conn= new SQLiteConnection(options);
    
         var results = conn.Query<Difficulty>("SELECT symbol FROM DifficultyLevels WHERE Id = id");
         
         conn.Close();
         foreach (var result in results)
         {
             Debug.LogWarning($"diff level we pulled {result.DifficultyLevel}");
             return result.DifficultyLevel;
         }

         return 0f;
     }

     /// <summary>
     /// when we want to update our difficulty level to a new value
     /// </summary>
     /// <param name="id"></param>
     /// <param name="updatedValue"></param>
     public static void UpdateDifficultyLevel(int id, float updatedValue)
     {
         var options = new SQLiteConnectionString(dbName, false);
         var conn= new SQLiteConnection(options);
    
         var results = conn.Update(new Difficulty {Id = id, DifficultyLevel = updatedValue});
         
         
         
         conn.Close();
     }

     private void Start()
     {
         //pullFromDataBase();
         // _db = new SQLiteConnection(dbName);
         //CreateNewDifficultyLevel();
     }
}

[Table("Stocks")]	 
public class Stock		
{		
    [PrimaryKey, AutoIncrement]
    [Column("id")]		
    public int Id { get; set; }	

    [Column("symbol")]			
    public string Symbol { get; set; }
    
}

[Table("DifficultyLevels")]	 
public class Difficulty		
{		
    [PrimaryKey, AutoIncrement]
    [Column("id")]		
    public int Id { get; set; }	

    [Column("symbol")]			
    public float DifficultyLevel { get; set; }
    
}
