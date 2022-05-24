using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonWordle_BackEnd.Models
{
    public class PlayerDBContext
    {
        //Methods and properties used for connecting to the database.
        public string ConnectionString { get; set; }

        public PlayerDBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            return conn;
        }
        
        // retrieves a player's data from the database based on their id
        // if no data was found (ie. new player), return a new player instance with 0 wins and losses
        public Player getPlayer(string id)
        {
            Player player = null;

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string id_noDashes = id.Replace("-", string.Empty);
                MySqlCommand cmd = new MySqlCommand($"select * from players where id='{id_noDashes}';", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        player = new Player
                        {
                            winCount = Convert.ToInt32(reader["win_total"]),
                            lossCount = Convert.ToInt32(reader["loss_total"])
                        };
                    }
                }
            }
            
            if (player == null) return new Player { winCount= 0, lossCount = 0 };

            else return player;
        }

        // update the player's wins and losses after each game
        // new entries are created in the database at this point for new players
        public void updatePlayerStats(string id, bool playerWon)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                // remove dashes from the UUID strings
                string id_noDashes = id.Replace("-", string.Empty);
                
                // define the queries based on whether the player won the game or not (via playerWon)
                string values = playerWon ? $"('{id_noDashes}',1,0)" : $"('{id_noDashes}',0,1)";
                string onDuplicate = playerWon ? $"win_total=win_total+1" : $"loss_total=loss_total+1";
                
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO players (id,win_total,loss_total) VALUES {values} " +
                                                    $"ON DUPLICATE KEY UPDATE {onDuplicate};", conn);

                cmd.ExecuteReader();
            }

            return;
        }
    }
}
