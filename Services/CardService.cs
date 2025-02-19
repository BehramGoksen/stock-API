using DataAccess;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CardService
    {
        public readonly Context context;

        public CardService(Context _context)
        {
            context = _context;
        }

        public void CreatCard(string CardName, int quantity )
        {

            var card = new Card
            {
                CardName = CardName,
                CardQuantity = quantity,
                
            };
            context.Cards.Add(card);
            context.SaveChanges();
        }

        public void IncreaseCard(int CardId, int quantity)
        {
            var card = context.Cards.Find(CardId);
            if (card != null)
            {
                card.CardQuantity += quantity;
                context.SaveChanges();
            }
            
        }
        public void DecreaseCard(int CardId, int quantity)
        {
            var card = context.Cards.Find(CardId);
            if (card != null)
            {
                card.CardQuantity -= quantity;
                context.SaveChanges();
            }
            
        }
        public void DeleteCard(int CardId)
        {
            var card = context.Cards.Find(CardId);
            if (card != null)
            {
                context.Cards.Remove(card);
                context.SaveChanges();
            }
            
        }
    }
}
