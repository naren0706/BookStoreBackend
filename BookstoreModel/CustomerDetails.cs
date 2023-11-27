using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class CustomerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "FullName is null")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "MobileNum is null")]
        public string MobileNum { get; set; }
        [Required(ErrorMessage = "Address is null")]
        public string Address { get; set; }
        [Required(ErrorMessage = "CityOrTown is null")]
        public string CityOrTown { get; set; }
        [Required(ErrorMessage = "State is null")]
        public string State { get; set; }
        [Required(ErrorMessage = "TypeId is null")]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
