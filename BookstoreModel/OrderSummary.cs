using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class OrderSummary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SummaryId{ get; set; }
        [Required(ErrorMessage = "CartId is null")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
