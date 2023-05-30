using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models.ViewModel
{
    public class SignUpModel
    {
        [Key]
        public int AccountId { get; set; }

        public string? AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string AccountType { get; set; } = "Savings";

        
        [Display(Name = "UserName")]
        [RegularExpression(@"[a-zA-Z\d]+$", ErrorMessage = "The format should be Alphanumeric")]
        public string userName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Gender { get; set; } = "Male";

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(10, MinimumLength = 10)]
        public string Mobile { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required]
        public int Amount { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsRemember { get; set; }

        [Required]
        [StringLength(100, ErrorMessage="The {0} must be atleast {2} character", MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="The Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
