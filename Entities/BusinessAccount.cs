using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class BusinessAccount : Account
    {
       // public Customer customer { get; set; }
       // public int AccoutNum { get; set; }
       // public double Balance { get; set; }
        public double OverdraftAmount { get; set; }
        public bool AllowLoan { get; set; }

        public BusinessAccount()
        {
            this.AccountNum = count++;
            this.Balance = 0;
            OverdraftAmount = -100;
            AllowLoan = true;
        }

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Business account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your balance is: $" + Balance);
        }

        public override void Deposit(int accountNum, double amount)
        {
            this.Balance += amount;
            Console.WriteLine("Depositing: $" + amount + " into the " + this.GetType().Name + " #" + accountNum + "\n");
        }

        public override void Withdraw(int accountNum, double amount)
        {
            if (this.Balance - amount < OverdraftAmount)
            {
                Console.WriteLine("Your Business account only allow to overdraft $100. You only have $" + this.Balance + " on your account and your overdraft amout is: $"+ -OverdraftAmount);
            }
            else if(this.Balance - amount >= OverdraftAmount && this.Balance - amount < 0)
            {
                //loan is - payment 
                this.OverdraftAmount = this.OverdraftAmount - (this.Balance - amount);
                //Loan newLoan = new Loan();              
                //newLoan.Balance = -(this.Balance - amount);
                this.Balance = 0;
                //newLoan.PrintInfor();
                //OpenNewAccount(this.customer, newLoan);
            }
            else
            {
                this.Balance -= amount;
                Console.WriteLine("Withdrawing: $" + amount + " from the " + this.GetType().Name + " #" + accountNum + "\n");
            }

        }
    }
}
