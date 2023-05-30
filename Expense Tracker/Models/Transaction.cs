using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key] 
        public int TransactionId { get; set; }

        //CategoryId
        [Range(1, int.MaxValue, ErrorMessage ="Please Choose the Category.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Choose the ReceiverCategory.")]
        //ReceiverCategory
        public int ReceiverCategoryId { get; set; }
        public Category? ReceiverCategory { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Please Choose the ReceiverId.")]
        //ReceiverId
        public int ReceiverId { get; set; }
        public Account? Receiver { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Choose the SenderId.")]
        //SenderId
        public int SenderId { get; set; }
        public Account? Sender { get; set; }

        [Range(10, int.MaxValue, ErrorMessage = "Amount should greater than 10 Dollars")]
        public int Amount { get; set; }

        //public int Balance { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Note { get; set; }

        //public string? NoteId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "": Category.Icon + " " + Category.Title;
            }
        }

        public string? ReceiverCategoryTitleWithIcon
        {
            get
            {
                return ReceiverCategory == null ? "" : ReceiverCategory.Icon + " " + ReceiverCategory.Title;
            }
        }

        [NotMapped]
        public string? TypeOfMoney
        {
            get
            {
                return "- " + Amount.ToString("C0");
            }
        }

        public string? TypeOfMoneyReceiver
        {
            get
            {
                return "+ " + Amount.ToString("C0");
            }
        }




    }
}
