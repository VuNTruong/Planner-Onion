using System;
using System.ComponentModel.DataAnnotations;

namespace PlannerOnion.ViewModels
{
    public class ResetPasswordViewModel
    {
        // Email of the user who needs to get password reset
        [Required]
        public string Email { get; set; }

        // Password reset token of the user
        [Required]
        public string PasswordResetToken { get; set; }

        // New password of the user
        [Required]
        public string NewPassword { get; set; }

        // user may need to confirm new password as well
        [Required]
        [Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }
    }
}
