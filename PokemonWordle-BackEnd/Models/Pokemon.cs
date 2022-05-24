namespace PokemonWordle_BackEnd.Models
{
    public class Pokemon
    {
        // database context from which the middleware retrieve Pokemon data
        private PokemonDBContext context;

        // name of the pokemon
        public string Name { get; set; }

        // pokedex number associated with the pokemon
        public int PokedexNumber { get; set; }

        // first type of the pokemon
        public string Type1 { get; set; }

        // second type of the pokemon
        public string Type2 { get; set; }

        // region from which the pokemon originates
        public string Region { get; set; }

        // the classification of the pokemon in the pokedex
        public string Classification { get; set; }
    }
}
