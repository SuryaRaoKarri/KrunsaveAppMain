using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Krunsave.Data.IRepository;
using Krunsave.DTO;
using Krunsave.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Krunsave.Controllers
{
    [Authorize(Roles="admin, manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        private readonly IAddRepository _addrepo;
        public AddController(IAddRepository addrepo){
            _addrepo = addrepo;
        }
        
        [HttpPost("addstore")]
        public async Task<IActionResult> Add(StoreForRegisterDto store){
            var identity = (ClaimsIdentity)User.Identity;
            //return Ok(identity.Name);
            store.userID = Int32.Parse(identity.Claims.Where(c => c.Type == "userID").Select(c => c.Value).FirstOrDefault());
            if(! await _addrepo.Addstore(store)) return BadRequest("Store unable to add.");
            return Ok(201); 
        }

        [HttpPost("addfood")]
        public async Task<IActionResult> Addfood(FoodForRegisterDto foodForRegisterDto){
            var identity = (ClaimsIdentity)User.Identity;
            var userID = Int32.Parse(identity.Claims.Where(c => c.Type == "userID").Select(c => c.Value).FirstOrDefault());
            
            if(! await _addrepo.CheckstoreID(userID, foodForRegisterDto.storeID)) return BadRequest("Store not Available");
            if(! await _addrepo.AddAvailableFood(foodForRegisterDto)) return BadRequest("Dish unable to add.");
            return Ok(foodForRegisterDto); 
        }
    }
}