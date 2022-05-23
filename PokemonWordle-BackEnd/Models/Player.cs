namespace PokemonWordle_BackEnd.Models
{
    public class Player
    {
        private PlayerDBContext context;

        public int winCount { get; set; }

        public int lossCount { get; set; }
    }
}
