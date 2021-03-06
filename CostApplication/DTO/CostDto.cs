using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CostApplication.Enum;


namespace CostApplication.DTO
{
    public class CostDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Record created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Type of Costs")]
        public TypeOfCosts TypeOfCosts { get; set; }

        [Required]
        public float Amount { get; set; }

        public string Description { get; set; }
    }
}
