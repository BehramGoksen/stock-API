using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CardDTO
    {
        public int CardId {get; set;}
        public string? CardName {get; set;}
        public int CardQuantity {get; set;}
    }
}
