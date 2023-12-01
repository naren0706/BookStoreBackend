using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookstoreModel
{
    public class OrderPlaced
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderPlacedId { get; set; }
        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage = "CustomerId is null")]
        public string CustomerId { get; set; }
        public virtual CustomerDetails CustomerDetails { get; set; }
        public int Cartid { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
