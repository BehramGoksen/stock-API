//using DataAccess;
//using Entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services
//{
//    public class AddItemToCardService
//    {
//        public readonly Context context;

//        public AddItemToCardService(Context _context)
//        {
//            context = _context;
//        }
//        public void Additemtocard(int cardId, List<Tuple<int, int>> items)
//        {
//            var card = context.Cards.Find(cardId);
//            if (card == null)
//            {
//                throw new Exception($"CardId: {cardId} Bulunamadı");
//            }

//            foreach (var iteminfo in items)
//            {

//                int itemId = iteminfo.Item1;
//                int quantity = iteminfo.Item2;

//                var item = context.Items.Find(itemId);
//                if (item == null)
//                {
//                    throw new Exception($"ItemId: {itemId} Bulunamadı");
//                }

//                if (item.Quantity < quantity)
//                {
//                    throw new Exception("Yeterli Stok Yok");
//                }

//                var cardItem = new CardItem
//                {
//                    CardId = cardId,
//                    ItemId = itemId,

//                };
//                if (card.CardItems == null)
//                {
//                    card.CardItems = new List<CardItem>();
//                }

//                card.CardItems.Add(cardItem);
//                item.Quantity -= quantity;
//                card.CardQuantity -= 1;
//            }
//            context.SaveChanges();
//        }

//    }
//}
