using Microsoft.AspNetCore.Mvc;
using Entity;
using Services.Abstract;

namespace StokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CardController : ControllerBase
    {
        private readonly IService<Card> _cardService;
        public CardController(IService<Card>  cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("CardList")]
        public IActionResult GetAllCards()
        {
            var allCards = _cardService.GetAll();
            return Ok(allCards);
        }

        [HttpGet("FindCard")]
        public IActionResult GetCard(int id)
        {
            var card = _cardService.Find(id);

            if (card == null)
            {
                return NotFound(new { message = "Card not found with the provided ID." });
            }
            return Ok(card);
        }

        [HttpPost("CreateCard")]
        public IActionResult CreateCard(CardDTO carddto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var card = carddto.ToCardEntity();
            _cardService.Add(card);
            _cardService.Save();
            return Ok("Card has been created succesfully");

        }

        [HttpDelete("DeleteCard/{id}")]
        public IActionResult DeleteCard(int id)
        {

            var card = _cardService.Find(id);

            if (card == null)
            {
                return NotFound("Card not found");
            }
            _cardService.Delete(card);
            _cardService.Save();
            return Ok("Card has been deleted successfully");
        }


        [HttpPut("UpdateCard/{id}")]
        public IActionResult UpdateCard(int id, Card card)
        {
            var existingCard = _cardService.Find(id);
            if (existingCard == null)
            {
                return NotFound("Card not found");
            }

            existingCard.CardName = card.CardName;
            existingCard.CardQuantity = card.CardQuantity;
            _cardService.Save();

            return Ok("Card has been updated successfully");
        }
        [HttpPost("IncreaseStock/{id}")]
        public IActionResult IncreaseStocl(int id, int amount)
        {
            var card = _cardService.Find(id);
            if (card == null)
                return NotFound("Card not found");
            card.CardQuantity += amount;
            _cardService.Save();
            return Ok("Successfully");
        }

        [HttpPost("DecreaseStock/{id}")]
        public IActionResult DecreaseStock(int id, int amount)
        {
            var card = _cardService.Find(id);

            if (card == null)
            {
                return NotFound("Card not found.");
            }

            if (card.CardQuantity < amount)
            {
                return BadRequest("Insufficient stock to decrease.");
            }
            card.CardQuantity -= amount;
            _cardService.Save();

            return Ok("Successfuly");
        }

    }
}
