namespace Entity
{
    public class CardItem
    {
        
        public int CardItemId { get; set; }
        
        public int? CardId { get; set; }
        public string? CardName { get; set; }
        public Card? Card { get; set; }
        
        public int? ItemId { get; set; }
        public string? ItemName{ get; set; }
        public Item? Item { get; set; }
        
        public int? itemquantity { get; set; }
        
        public int Quantity { get; set; }
        

    }
}
