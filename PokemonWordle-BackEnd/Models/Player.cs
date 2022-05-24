namespace PokemonWordle_BackEnd.Models
{
    public class Player
    {
        // database context from which the middleware retrieve player data
        private PlayerDBContext context;

        // the number of wins by the player
        public int winCount { get; set; }

        // the number of losses by the player
        public int lossCount { get; set; }
    }
}
