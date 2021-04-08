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

        [HttpPut]
        public IActionResult Put(Guitar guitar)
        {
            return Ok(guitar);
        }

        [HttpPost]
        public IActionResult Post(Guitar guitar)
        {
            return Created("api/guitars/1", guitar);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Deleted");
        }
    }
}
