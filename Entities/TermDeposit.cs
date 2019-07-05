using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TermDeposit: Account
    {
        public bool termEnded { get; set; }
        double Interest = 3.5;
        double penalty = 10;
        //due date=true
        public TermDeposit()
        {
            AccountNum = count++;
            Balance = 0;
            termEnded = false;
        }

        public double Earning()
        {
            double earning = 0;
            if (termEnded == true)
            {
                earning=this.Balance+(this.Balance * (Interest/100));
            }
            else
            {
                earning = this.Balance - (this.Balance * (penalty/100));
            }
            this.Balance = 0;
            return earning;
        }

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Term Deposit account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your balance is: $" + Balance);
        }

        public override void Deposit(int accountNum, double amount)
        {
            this.Balance += amount;
            Console.WriteLine("Depositing: $" + amount + " into the " + this.GetType().Name + " #" + accountNum + "\n");
        }

        public override void Withdraw(int accountNum, double amount)
        {
            if ((this.Balance != amount || this.Balance - amount < 0))
            {
                Console.WriteLine("You can't withdraw partially amount of money or more than the balance from the Term deposit account!");
            }
            else if (this.termEnded == true)
            {
                Console.WriteLine("Is the Term deposit acccount term ended?");
                this.termEnded = bool.Parse(Console.ReadLine());
                Console.WriteLine("You are withdrawing $" + this.Earning() + " from your term deposit account.");
            }
            else
            {
                this.Balance -= amount;
                Console.WriteLine("Withdrawing: $" + amount + " from the " + this.GetType().Name + " #" + accountNum + "\n");
            }
        }
    }
}
