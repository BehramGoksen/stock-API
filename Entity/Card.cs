using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Card
    {
        [Display(Name = "Id")]
        public int CardId { get; set; }

        [Display(Name = "İsim")]
        public string? CardName { get; set; }
        [Display(Name = "Miktar")]
        public int CardQuantity { get; set; }
        public ICollection<CardItem>? CardItems { get; set; }

    }
}
