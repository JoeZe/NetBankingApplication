using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CheckingAccount : Account
    {
        public CheckingAccount()
        {
            //Random random = new Random();
            //AccoutNum = random.Next(10000);
            this.AccountNum = count++;
            this.Balance = 0;
        }

        public override void Deposit(int accountNum, double amount)
        {
            this.Balance += amount;
            Console.WriteLine("Depositing: $" + amount + " into the " + this.GetType().Name + " #" + accountNum + "\n");
        }

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Checking account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your balance is: $" + Balance);
        }

        public override void Withdraw(int accountNum, double amount)
        {
            if (this.Balance - amount < 0)
            {
                Console.WriteLine("Your account do not have sufficient amount of money to withdraw. You only have $" + this.Balance);
            }
            else
            {
                this.Balance -= amount;
                Console.WriteLine("Withdrawing: $" + amount + " from the " + this.GetType().Name + " #" + accountNum + "\n");
            }
        }
    }
}
