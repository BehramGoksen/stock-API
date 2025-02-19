using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ItemDTO
    {
        public int ItemId {get; set;}
        public string ItemName {get; set;}
        public int Quantity {get; set;}
    }
}
