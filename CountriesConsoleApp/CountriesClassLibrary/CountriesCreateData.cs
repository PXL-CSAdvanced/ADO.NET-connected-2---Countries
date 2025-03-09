using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesClassLibrary
{
    public static class CountriesCreateData
    {
        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Countries;Integrated Security=True;Connect Timeout=30;";

        static CountriesCreateData()
        {
            CreateTableIfNotExists();
        }

        private static void CreateTableIfNotExists()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("IF EXISTS (SELECT * FROM sysobjects WHERE name='Countries' AND xtype='U') " +
                    "Drop table Countries", conn);
                cmd.ExecuteNonQuery();

                string query = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Countries' AND xtype='U')
                CREATE TABLE Countries (
                    Id INT IDENTITY PRIMARY KEY,
                    Name VARCHAR(100),
                    Capital VARCHAR(100),
                    Population INT,
                    Area INT,
                    Region VARCHAR(50)
                )";
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }


        public static void InsertFromCsv(string filePath)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int numberOfErrors = 0;
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        string text = "";
                        string[] line;
                        try
                        {
                            text = reader.ReadLine();
                            text = text.Replace("\"", "");
                            line = text.Split(',');

                            // Prepare the insert command
                            SqlCommand cmd = new SqlCommand("INSERT INTO Countries (Name, Capital, Population, Area, Region) VALUES (@Name, @Capital, @Population, @Area, @Region)", conn);

                            cmd.Parameters.AddWithValue("@Name", line[0].Trim());
                            cmd.Parameters.AddWithValue("@Capital", line[1].Trim());
                            cmd.Parameters.AddWithValue("@Population", int.Parse(line[2]));
                            cmd.Parameters.AddWithValue("@Area", double.Parse(line[3]));
                            cmd.Parameters.AddWithValue("@Region", line[4].Trim());

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            numberOfErrors++;
                            Console.WriteLine($"{numberOfErrors}: {text}\n");
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

    }
}
