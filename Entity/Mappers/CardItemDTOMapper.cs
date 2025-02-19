namespace Entity
{
    public static class CardItemDTOMapper
    {
        public static CardItemDTO ToDto(this CardItem cardıtem, CardItem cardItem)
        {
            return new CardItemDTO
            {
                CardItemId = cardıtem.CardItemId,
                CardId = cardıtem.CardId,
                CardName = cardıtem.CardName,
                ItemId = cardıtem.ItemId,
                ItemName = cardıtem.ItemName,
                itemquantity = cardıtem.itemquantity,
                Quantity = cardıtem.Quantity,
               
            };
        }

        public static CardItem ToCardItemEntity(this CardItemDTO model)
        {
            return new CardItem
            {
                CardItemId = model.CardItemId,
                CardId = model.CardId,
                CardName = model.CardName,
                ItemId = model.ItemId,
                ItemName = model.ItemName,
                itemquantity = model.itemquantity,
                Quantity = model.Quantity,
                
            };
        }

    }
}

