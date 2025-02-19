namespace Entity
{
    public static class ItemDTOMapper
    {
        public static ItemDTO ToDto(this Item ıtem)
        {
            return new ItemDTO
            {
                ItemId = ıtem.ItemId,
                ItemName = ıtem.ItemName,
                Quantity = ıtem.Quantity,
            };
        }

        public static Item ToItemEntity(this ItemDTO model)
        {
            return new Item
            {
                ItemId = model.ItemId,
                ItemName = model.ItemName,
                Quantity = model.Quantity,
            };
        }

    }
}

