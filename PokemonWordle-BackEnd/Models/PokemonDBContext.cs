using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonWordle_BackEnd.Models
{
    public class PokemonDBContext
    {
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

        public List<Pokemon> GetRandomizedPokemonList()
        {

            List<Pokemon> pokemons= new List<Pokemon>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from pokemon order by rand();", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pokemons.Add(new Pokemon
                        {
                            Name = reader["name"].ToString(),
                            PokedexNumber = Convert.ToInt32(reader["pokedex_number"]),
                            Type1 = reader["type1"].ToString(),
                            Type2 = reader["type2"].ToString(),
                            Region = GetRegion(Convert.ToInt32(reader["generation"])),
                            Classification = reader["classification"].ToString()
                        });
                    }
                }
            }

            return pokemons;
        }
    }
}
