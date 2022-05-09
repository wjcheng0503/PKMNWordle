namespace PokemonWordle_BackEnd.Models
{
    public class Pokemon
    {
        private PokemonDBContext context;

        public string Name { get; set; }

        public int PokedexNumber { get; set; }

        public string Type1 { get; set; }

        public string Type2 { get; set; }

        public string Region { get; set; }

        public string Classification { get; set; }
    }
}
