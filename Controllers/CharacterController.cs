using web_api_simpsons.Dependencies;
using System.Collections.Generic;
using web_api_simpsons.Modules; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlClient;

namespace web_api_simpsons.Controllers
{
    [Route("simpsons/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    
    public class CharacterController : ICharacter
    {
        List<Character> characterList => new List<Character>
        {
            new Character
            {
                FirstName = "Homero",
                SecondName = "Jay",
                LastName = "Simpson",
                Age = 34,
                Description = "Esposo de Marge y padre de Bart, Lisa y Maggie Simpsons."
            },
            new Character
            {
                FirstName = "Bartolomeo",
                SecondName = "Jay",
                LastName = "Simpson",
                Age = 10,
                Description = "Hijo de Homero y Marge, y hermano mayor de Lisa y Maggie Simpsons."
            },
            new Character
            {
                FirstName = "Margory",
                LastName = "Simpson",
                Age = 34,
                Description = "Esposa de Homero y madre de Bart, Lisa y Maggie Simpsons."
            },
            new Character
            {
                FirstName = "Lisa",
                LastName = "Simpson",
                Age = 8,
                Description = "Hija de Homero y Marge, y hermana de Bart y Maggie Simpsons."
            }
        };

        string connectionString = @"data source = LAPTOP-UCMOS94G\SQLEXPRESS; initial catalog = db_simpsons; user id = simpsons; password = 12345";

        
        [HttpGet("{id}")]
        public Character GetCharacter(int id)
        {
            return characterList[id];
        }

        [HttpGet]
        public List<Character> GetCharacterList()
        {
            List<Character> characters = new List<Character>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select * from tbl_character", conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Character character = new Character
                {
                    Id = reader.GetInt64(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    SecondName = reader.GetString(reader.GetOrdinal("secondname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Age = reader.GetInt32(reader.GetOrdinal("age")),
                    Description = reader.GetString(reader.GetOrdinal("descp"))
                };
                characters.Add(character);
            }
            conn.Close();

            return characters;
        }
    }
}