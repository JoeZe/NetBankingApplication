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

        public override void PrintInfor()
        {
            Console.WriteLine("Creating a new Loan account..... ");
            Console.WriteLine("The account number is: " + AccountNum + " and your loan balance is: $" + Balance);
        }
    }
}
