using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity;
using Services.Abstract;

namespace StokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IService<Item> _service;
        public ItemController(IService<Item> service)
        {
            _service = service;
        }


        [HttpGet("ItemList")]
        public IActionResult GetAllItems()
        {
            var allItems = _service.GetAll();
            return Ok(allItems);
        }


        [HttpGet("FindItem")]
        public IActionResult GetItem(int id)
        {
            var item = _service.Find(id);
            return Ok(item);
        }

        [HttpPost("CreateItem")]
        public IActionResult CreateItem(ItemDTO itemdto)
        {
            var item = itemdto.ToItemEntity();
            _service.Add(item);
            _service.Save();

            return Ok("Item has been created succesfully");

        }

        [HttpDelete("DeleteItem/{id}")]
        public IActionResult DeleteCard(int id)
        {

            var item = _service.Find(id);

            if (item == null)
            {
                return NotFound("Card not found");
            }
            _service.Delete(item);
            _service.Save();
            return Ok("Card has been deleted successfully");
        }

        [HttpPut("UpdateItem/{id}")]
        public IActionResult UpdateCard(int id, Item item)
        {
            var existingItem = _service.Find(id);
            if (existingItem == null)
            {
                return NotFound("Item not found");
            }

            existingItem.ItemName = item.ItemName;
            existingItem.Quantity = item.Quantity;
            _service.Save();

            return Ok("Item has been updated successfully");
        }

        [HttpPost("IncreaseStock/{id}")]
        public IActionResult IncreaseStocl(int id, int amount)
        {
            var item = _service.Find(id);
            if (item == null)
                return NotFound("Card not found");
            item.Quantity += amount;
            _service.Save();
            return Ok("Successfully");
        }

        [HttpPost("DecreaseStock/{id}")]
        public IActionResult DecreaseStock(int id, int amount)
        {
            var item = _service.Find(id);

            if (item == null)
            {
                return NotFound("Card not found.");
            }

            if (item.Quantity < amount)
            {
                return BadRequest("Insufficient stock to decrease.");
            }
            item.Quantity -= amount;
            _service.Save();

            return Ok("Successfuly");
        }
    }

}
