using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ShippingDetails
    {
        //kişi bu bilgileri dolduracak
        //Bazı alanlar zorunlu olacagı için Required koyacagım.
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //Yani Email formatında email girilsin dedim.
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(15,80)]
        public int Age { get; set; }
        //Ürünleri alacak kişi min 15 max 80 yasında olmalı dedim 
    }
}
