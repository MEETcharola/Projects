using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDekho_CRUD.Models
{
    class Bike
    {
        [Key]
        public int BikeId { get; set; }
        [Required]
        public string BikeModel { get; set; }
        [Required]
        public string BikeCompany { get; set; }
        [Required]
        public int BikePrice { get; set; }
        [Required]
        public string BikeDetails { get; set; }
        [Required]
        public byte[] BikePhoto { get; set; }

        public virtual User User { get; set; }
    }
}
