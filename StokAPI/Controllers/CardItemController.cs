using Entity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace StokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardItemController : ControllerBase
    {
        private readonly IService<CardItem> _service;
        
        public CardItemController(IService<CardItem> service)
        {
            _service = service;
        }

        [HttpGet("CardList")]
        public IActionResult GetAllCards()
        {
            var allCardItems = _service.GetAll();
            return Ok(allCardItems);
        }

        [HttpGet("FindCard")]
        public IActionResult GetCard(int id)
        {
            var card = _service.Find(id);

            if (card == null)
            {
                return NotFound(new { message = "Card not found with the provided ID." });
            }
            return Ok(card);
        }

        [HttpPost("CreateCard")]
        public IActionResult CreateCard(CardItemDTO cardItemdto)
        {
            var cardItem = cardItemdto.ToCardItemEntity();
            _service.Add(cardItem);
            _service.Save();
            return Ok("Card has been created succesfully");
        }

        [HttpDelete("DeleteCard/{id}")]
        public IActionResult Delete(int id)
        {
            
            
            var item = _service.Find(id);
            if (item == null)
                return NotFound("Bulunamadı");
            _service.Delete(item);
            _service.Save();
            return Ok("Başarılı");
        } 

        

        [HttpPut("UpdateCard/{id}")]
        public IActionResult UpdateCard(int id, CardItemDTO cardıtemdto)
        {

            var cardıtem = cardıtemdto.ToCardItemEntity();
            var existingCard = _service.Find(id);
            if (existingCard == null)
            {
                return NotFound("Card not found");
            }

                //existingCard.CardItemId = cardıtem.CardItemId;
                //existingCard.CardId = cardıtem.CardId;
                //existingCard.CardName = cardıtem.CardName;
                //existingCard.ItemId = cardıtem.ItemId;
                //existingCard.ItemName = cardıtem.ItemName;
                existingCard.itemquantity = cardıtem.itemquantity;
                existingCard.Quantity = cardıtem.Quantity;
                

            _service.Save();

            return Ok("Card has been updated successfully");
        }
    }
}
