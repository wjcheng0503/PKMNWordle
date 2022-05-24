namespace PokemonWordle_BackEnd
{
    public class GameLogic
    {
        // The name of the Pokemon selected for the session
        public string CorrectAnswer { get; set; }

        // The hints associated with the currently selected Pokemon
        public List<Object> Hints { get; set; }
    }
}
