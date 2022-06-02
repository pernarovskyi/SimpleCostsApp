using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CostApplication.DTO
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? LastVisitedDate { get; set; }
    }
}
