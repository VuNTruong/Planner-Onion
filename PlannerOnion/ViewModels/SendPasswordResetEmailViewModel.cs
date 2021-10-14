using System;
using System.ComponentModel.DataAnnotations;

namespace PlannerOnion.ViewModels
{
    public class SendPasswordResetEmailViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
