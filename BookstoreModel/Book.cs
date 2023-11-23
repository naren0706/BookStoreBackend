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
        public string BookDescription { get; set; }
        [Required(ErrorMessage = "BookAuthor is null")]
        public string BookAuthor { get; set; }
        [Required(ErrorMessage = "BookImage is null")]
        public string BookImage { get; set; }
        [Required(ErrorMessage = "BookCount is null")]
        [Range(20, 100, ErrorMessage = "count should be greater than 20")]
        public int BookCount { get; set; }
        [Required(ErrorMessage = "BookPrize is null")]
        public int BookPrize { get; set; }
        [Required(ErrorMessage = "Name is null")]
        public int Rating { get; set; }
        public bool isAvailable { get; set; }
        

    }
}
