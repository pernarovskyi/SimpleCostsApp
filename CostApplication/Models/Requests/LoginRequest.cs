﻿using System.ComponentModel.DataAnnotations;


namespace CostApplication.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
