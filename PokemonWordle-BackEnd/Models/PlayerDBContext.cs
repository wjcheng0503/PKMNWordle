using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonWordle_BackEnd.Models
{
    public class PlayerDBContext
    {
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
        public void updatePlayerStats(string id, bool playerWon)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string id_noDashes = id.Replace("-", string.Empty);
                string values = playerWon ? $"('{id_noDashes}',1,0)" : $"('{id_noDashes}',0,1)";
                string onDuplicate = playerWon ? $"win_total=win_total+1" : $"loss_total=loss_total+1";
                
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO players (id,win_total,loss_total) VALUES {values} " +
                                                    $"ON DUPLICATE KEY UPDATE {onDuplicate};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    }
                }

            }

            return;
        }
    }
}
