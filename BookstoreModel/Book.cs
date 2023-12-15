using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required(ErrorMessage = "BookName is null")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "BookDescription is null")]
        public string BookDescription { get; set; }
        [Required(ErrorMessage = "BookAuthor is null")]
        public string BookAuthor { get; set; }
        public string BookImage { get; set; }
        [Required(ErrorMessage = "BookCount is null")]
        [Range(20, 100, ErrorMessage = "count should be greater than 20")]
        public int BookCount { get; set; }
        [Required(ErrorMessage = "BookPrize is null")]
        public int BookPrize { get; set; }
        public decimal Rating { get; set; }
        public bool IsAvailable { get; set; }
    }
}
