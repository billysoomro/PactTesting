using System.Collections.Generic;
using Guitars.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guitars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuitarsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var guitars = new List<Guitar>
            {
                new Guitar("PRS" , "Tremonti Signature Model", "Solid body", 6),
                new Guitar("Gibson" , "Les Paul", "Solid body", 6),
                new Guitar("Fender" , "Stratocaster", "Solid body", 6),
                new Guitar("Ibanez" , "RG Series", "Solid body", 7),
                new Guitar("ESP" , "KH-2 VINTAGE", "Solid body", 6)
            };

            return Ok(guitars);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Guitar guitar)
        {
            return Created("api/guitars/1", guitar);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Guitar guitar)
        {
            return Ok(guitar);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] string id)
        {
            return Ok("Deleted");
        }
    }
}
