using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CostApplication.Enum;


namespace CostApplication.Models
{
    public class Cost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Created on")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } // hide from front-end

        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; } // hide from front-end

        [Required]
        [DisplayName("Type of Costs")]
        public TypeOfCosts TypeOfCosts { get; set; }

        [Required]
        public float Amount { get; set; }
        public string Description { get; set; }

        public string SensetiveData { get; set; }   // hide from front-end
    }
}
