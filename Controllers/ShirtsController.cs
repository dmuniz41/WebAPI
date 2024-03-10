
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Filters.ExceptionFilters;
using WebAPI.Models;
using WebAPI.Models.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }
        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtsById(int id)
        {

            return Ok(ShirtRepository.GetShirtById(id));
        }

        // ? LEE EL PARAMETRO COLOR DESDE LOS HEADERS DE LA PETICION
        // [HttpGet("{id}")]
        // public string GetShirtsById(int id, [FromHeader(Name = "Color")] string color)
        // {
        //     return $"Reading the shirt with ID: {id}, color: {color}";
        // }

        // ? LEE EL PARAMETRO COLOR DESDE LA URL DE LA PETICION
        // [HttpGet("{id}")]
        // public string GetShirtsById(int id, [FromRoute] string color)
        // {
        //     return $"Reading the shirt with ID: {id}, color: {color}";
        // }

        // [HttpPost]
        // public string CreateShirt()
        // {   
        //     return $"Creating a shirt";
        // }

        //? LEE EL CONTENIDO DEL BODY DE LA PETICION 

        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {

            ShirtRepository.AddShirt(shirt);
            return CreatedAtAction(nameof(GetShirtsById),
                new
                {
                    id = shirt.ShirtId
                },
                shirt
            );
        }

        // ? OTRA FORMA DE LEER LOS DATOS DESDE UNA PETICION
        // [HttpPost]
        // public string CreateShirt([FromForm] Shirt shirt)
        // {   
        //     return $"Creating a shirt: {shirt}";
        // }

        [HttpPut("{id}")]
        [Shirt_ValidateCreateShirtFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);

            return Ok(shirt);
        }

    }
}