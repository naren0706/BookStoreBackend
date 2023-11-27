using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int BookCount { get; set; }
        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required(ErrorMessage = "BookId is null")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
