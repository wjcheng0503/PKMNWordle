using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonWordle_BackEnd.Models;
using System.Diagnostics;

namespace PokemonWordle_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameLogicController : ControllerBase
    {
        
        private static readonly string[] TestAnswers = new[]
            {
                "Squirtle", "Vulpix", "Magikarp", "Charmander", "Bulbasaur", "Snorlax", "Mewtwo"
            };

        private bool gotPokemon;

        private readonly ILogger<GameLogicController> _logger;

        public GameLogicController(ILogger<GameLogicController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetGameLogic")]
        public GameLogic Get()
        {

            PokemonDBContext context = HttpContext.RequestServices.GetService(typeof(PokemonDBContext)) as PokemonDBContext;
            List<Pokemon> pokemons = context.GetRandomizedPokemonList();
            Pokemon pkmn = pokemons[0];

            GameLogic gL = new GameLogic
            {
                CorrectAnswer = pkmn.Name.ToUpper(),
                Hints = new List<Object> { pkmn.PokedexNumber, pkmn.Type1, pkmn.Type2, pkmn.Region, pkmn.Classification },
                PokemonList = new List<String>(),
                PlayerID = "I"
            };

            foreach (Pokemon p in pokemons)
            {
                gL.PokemonList.Add(p.Name.ToUpper());
            }

            Debug.WriteLine(gL.CorrectAnswer);

            return gL;
        }
    }
}
