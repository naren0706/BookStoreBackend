using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int TypeId { get; set; }
        [Required(ErrorMessage = "TypeValue is null")]
        public string TypeValue { get; set; }

    }
}
