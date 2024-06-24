using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantFaves.Models;

namespace RestaurantFaves.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        RestaurantFavesContext DbContext = new RestaurantFavesContext();
        //api/restaurant

        [HttpGet()]

        public IActionResult GetOrders(string? r = null, bool? orderagain = null)
        {
            List<RestaurantRating> result = DbContext.RestaurantRatings.ToList();
            if (r != null)
            {
                result = result.Where(b => b.Restaurant.ToLower().Contains(r.ToLower())).ToList();
            }
            if (orderagain != null)
            {
                result = result.Where(b => b.OrderAgain == orderagain).ToList();
            }
            return Ok(result);
        }
        //get by id
        //api/restaurantratings/2
        [HttpGet("{id})")]

        public IActionResult GetOrdersById(int id)
        {
            RestaurantRating result = DbContext.RestaurantRatings.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //add restaurant rating
        //api/restaurant/3
        [HttpPost()]
        public IActionResult AddOrder([FromBody] RestaurantRating newOrder)
        {
            newOrder.Id = 0;
            DbContext.RestaurantRatings.Add(newOrder);
            DbContext.SaveChanges();
            return Created($"/RestaurantRating/Api/{newOrder.Id}", newOrder);
        }
        //update order
        //api/restaurantratings/3
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] RestaurantRating updateOrder)
        {
             if (updateOrder.Id != id) { return BadRequest(); }
             if(!DbContext.RestaurantRatings.Any(b=> b.Id == id)) { return NotFound(); }
            DbContext.RestaurantRatings.Update(updateOrder);
            DbContext.SaveChanges() ;
            return NoContent(); 
        }
        //delete book
        //api/restaurantrating/4
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            RestaurantRating result = DbContext.RestaurantRatings.Find(id);
            if (result == null) { return NotFound();}
            DbContext.RestaurantRatings.Remove(result);
            DbContext.SaveChanges();
            return NoContent();
        }





    }
}
