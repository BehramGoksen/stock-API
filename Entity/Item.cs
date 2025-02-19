using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Item
    {
        [Display(Name = "Id")]
        public int ItemId { get; set; }

        [Display(Name = "İsim")]
        public string ItemName { get; set; }

        [Display(Name = "Miktar")]
        public int Quantity { get; set; }
        public ICollection<CardItem>? CardItems { get; set; } = new List<CardItem>();
    }
}
