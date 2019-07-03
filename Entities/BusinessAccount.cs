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

        public BusinessAccount()
        {
            this.AccountNum = count++;
            this.Balance = 0;
        }

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Business account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your balance is: $" + Balance);
        }
    }
}
