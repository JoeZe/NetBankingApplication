using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Loan: Account
    {
        public double Interest = 3.5;

        public Loan()
        {
            AccountNum = count++;
            Balance = 0;
        }

        public override void Deposit(int accountNum, double amount)
        {
            this.Balance -= amount;
            Console.WriteLine("Paying: $" + amount + " into the " + this.GetType().Name + " #" + accountNum + "\n");
        }

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Loan account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your loan balance is: $" + Balance);
        }

        public override void Withdraw(int accountNum, double amount)
        {
            Console.WriteLine("You are not allow to widthdraw from the Loan account! ");
        }
    }
}
