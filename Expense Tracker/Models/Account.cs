using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class Account
    {

        [Key]
        public int AccountId { get; set; }

        public string? AccountNumber { get; set; }

        public string? AccountType { get; set; }

        public string userName { get; set; }

        public string Gender { get; set; }
        public string Mobile { get; set; }

        public string? Email { get; set; }

        public string Address { get; set; }

        public int Amount { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsRemember { get; set; }

        public string Password { get; set;}

        public ICollection<Category> Categories { get; set; }

        //public ICollection<Transaction> Transactions { get; set; }


    }
}
