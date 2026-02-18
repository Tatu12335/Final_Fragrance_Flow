using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Fragrances/delete")]
    public class DeleteFragranceController : Controller
    {
        private readonly IFragranceRepo _repo;
        public DeleteFragranceController(IFragranceRepo repo) 
        { 
            _repo = repo;
        }
        [HttpDelete]
        public async Task <IActionResult> Delete(string username,int id)
        {
            var userId = await _repo.GetUserId(username);       

            try
            {
                await _repo.RemoveFragranceById(userId.id, id);
                return Ok(new {message = " Remove successful"});
            }
            catch (Exception ex)
            {
                throw new Exception($" An error occured : {ex.Message}");
                
            }
        }
    }
}
