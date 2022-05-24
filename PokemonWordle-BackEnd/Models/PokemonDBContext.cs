using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonWordle_BackEnd.Models
{
    public class PokemonDBContext
    {
        //Methods and properties used for connecting to the database.
        public string ConnectionString { get; set; }

        public PokemonDBContext (string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            return conn;
        }

        // recontextualizes the 'generation' column from the database into a named region
        public string GetRegion(int generation)
        {
            switch (generation)
            {
                case 1:
                    return "Kanto";
                case 2:
                    return "Johto";
                case 3:
                    return "Hoenn";
                case 4:
                    return "Sinnoh";
                case 5:
                    return "Unova";
                case 6:
                    return "Kalos";
                case 7:
                    return "Alola";
                default:
                    return "";
            }
        }

        // retrieves a random pokemon from the database 
        public Pokemon GetRandomPokemon()
        {
            // initialize a pokemon instance;
            Pokemon pokemon = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from pokemon order by rand();", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pokemon = new Pokemon
                        {
                            Name = reader["name"].ToString(),
                            PokedexNumber = Convert.ToInt32(reader["pokedex_number"]),
                            Type1 = reader["type1"].ToString(),
                            Type2 = reader["type2"].ToString(),
                            Region = GetRegion(Convert.ToInt32(reader["generation"])),
                            Classification = reader["classification"].ToString()
                        };
                        break;
                    }
                }
            }

            return pokemon;
        }

        /* 
         * checks if the input is a Pokemon name
         * a name that does not exist within the database is not valid.
        */
        internal bool isValid(string pokemon)
        {
            bool isValid = false;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                pokemon = pokemon.Replace("'", "''");
                MySqlCommand cmd = new MySqlCommand($"select * from pokemon where name='{pokemon}';", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
    }
}
