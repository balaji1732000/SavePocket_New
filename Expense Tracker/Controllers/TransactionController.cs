using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;
using Expense_Tracker.Db;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
       
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transaction.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            PopulateReceiverAccount(userId);
            PopulateSenderAccount();
            PopulateCategories();
            if(id == 0)
            {
                var transaction = new Transaction();
                transaction.SenderId = userId;
                return View(transaction);
            }
            else
            {
                var transaction = _context.Transaction.Find(id);
                if(transaction.SenderId != userId) 
                { 
                   return RedirectToAction("Index", "Dashboard");
                }

                return View(transaction);
            }
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId, CategoryId, ReceiverId, SenderId, Amount, Note, Date")] Transaction transaction)
        {
            if(!ModelState.IsValid)
            {
                var errorMessages = new List<string>();
                foreach (var modelStatevalue in ModelState.Values)
                {
                    foreach (var error in modelStatevalue.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
                ViewBag.ErrorMessage = errorMessages;
            }
            else
            {
                if(transaction.TransactionId == 0)
                {
                    //Get the Sender and reciver accounts
                    int userID = (int)HttpContext.Session.GetInt32("userId");
                    var senderAccount = await _context.Account.FindAsync(userID);
                    var ReceiverAccount = await _context.Account.FindAsync(transaction.ReceiverId);

                    
                    //Check if sender and receiver accounts exists
                    if(senderAccount == null && ReceiverAccount == null)
                    {
                        return RedirectToAction("Index");
                    }
                    //Check if sender has sufficient balance
                    if (senderAccount.Amount < transaction.Amount)
                    {
                        //Handle error: Insufficient Balance
                        return RedirectToAction("Index");
                    }

                    //Update Sender and Receiver Account Balance
                    senderAccount.Amount -= transaction.Amount;
                    ReceiverAccount.Amount += transaction.Amount;
                    //await _context.SaveChangesAsync();


                    var incomeCategory = _context.Category.FirstOrDefault(c => c.CategoryId == transaction.CategoryId);
                    if (incomeCategory != null) 
                    {
                        transaction.CategoryId = incomeCategory.CategoryId;
                        transaction.ReceiverCategoryId = 0;
             
                    }
                    //Save changes to the database
                    _context.Update(senderAccount);
                    _context.Update(ReceiverAccount);
                    _context.Add(transaction);
                }
                else
                {
                    _context.Update(transaction);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            int user = (int)HttpContext.Session.GetInt32("userId");
            PopulateReceiverAccount(user);
            PopulateCategories();
            PopulateSenderAccount();
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transaction'  is null.");
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Populate Categories
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Category.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Income" };
            CategoryCollection.Insert(0,DefaultCategory);
            ViewBag.Category = CategoryCollection;
        }

        //Populate SenderAccount

        public void PopulateSenderAccount()
        {
            int user = (int)HttpContext.Session.GetInt32("userId");
            var SenderAccountCollection = _context.Account.Where(u => u.AccountId == user).ToList();
            Account DefaultSenderAccount = new Account() { AccountId = 0, };
            SenderAccountCollection.Insert(0,DefaultSenderAccount);
            ViewBag.SenderAccount = SenderAccountCollection;
        }

        //PopulateReceiverAccount

        public void PopulateReceiverAccount(int senderAccountId)
        {
            // query to filter out the accounts where the AccountId is equal to the senderAccountId
            var ReceiverAccountCollection = _context.Account.Where(a => a.AccountId != senderAccountId).ToList();
            var DefaultAccount = new Account()
            {
                AccountId = 0,
            };
            ReceiverAccountCollection.Insert(0, DefaultAccount);
            ViewBag.ReceiverAccount = ReceiverAccountCollection;

        }


        
    }
}
