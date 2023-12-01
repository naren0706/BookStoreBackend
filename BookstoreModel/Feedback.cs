using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = "BookId is null")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string CustomerDescription { get; set; }
        [Required(ErrorMessage = "Rating is null")]
        [Range(0,5, ErrorMessage = "count should be greater than 20")]
        public int Rating { get; set; }

    }
}
