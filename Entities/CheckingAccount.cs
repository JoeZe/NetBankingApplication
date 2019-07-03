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

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Checking account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your balance is: $" + Balance);
        }
    }
}
