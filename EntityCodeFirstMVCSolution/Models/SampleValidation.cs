using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EntityCodeFirstMVCSolution.Models
{
    public class SampleValidation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Price")]
        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal UnitPrice { get; set; }

    }

    public class StudentClass
    {
        [Required(ErrorMessage = "Enter Student Id")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Enter Student Name"),RegularExpression(@"^[a-zA-Z ]+$",ErrorMessage = "Name in alphabetically")]
        public string StudentName { get; set; }


        [DataType(DataType.EmailAddress)]
        [MaxLength(255), Required(ErrorMessage = "Enter Email"), MinLength(5)]
        //[RegularExpression(@"^([a-zA-Z._]+)([@]{1})([a-z]+)([.]{1})([a-z]+)$", ErrorMessage = "Enter Valid Email")]
        public string StudentEmail { get; set; }
    }
}