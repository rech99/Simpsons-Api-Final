using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using web_api_simpsons.Modules;
using web_api_simpsons.Dependencies;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlClient;


namespace web_api_simpsons.Controllers
{
    [Route("simpsons/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CharacterController : ICharacter
    {
        List<Character> listOfCharacters => new List<Character>
        {
            new Character
            {
                FirstName = "Homero",
                SecondName = "Jay",
                LastName = "Simpsons",
                Age = 34
            },
            new Character
            {
                FirstName = "Bartolomeo",
                SecondName = " ",
                LastName = "Simpsons",
                Age = 10
            }
        };
        
        string connectionString = @"data source=DESKTOP-57M28NI\SQLEXPRESS; initial catalog=db_simpsons; user id = simpsons; password = 1234";


        [HttpGet("{id}")]
        public Character GetCharacter(int id)
        {
            return listOfCharacters[id];
        }

        [HttpGet]
        public List<Character> GetCharacterList()
        {
            List<Character> characters = new List<Character>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select * from tb_simpsons_characters", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Character character = new Character
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstName"))
                };
                characters.Add(character);
            }
            conn.Close();
            return characters;
        }
    }
}