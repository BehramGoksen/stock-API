namespace Entity
{
    public static class CardDTOMapper
    {
        public static CardDTO ToDto(this Card card)
        {
            return new CardDTO
            {
                CardId = card.CardId,
                CardName = card.CardName,
                CardQuantity = card.CardQuantity,
            };
        }

        public static Card ToCardEntity(this CardDTO model)
        {
            return new Card
            {
                CardId = model.CardId,
                CardName = model.CardName,
                CardQuantity = model.CardQuantity,
            };
        }

    }
}

