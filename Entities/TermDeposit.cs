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
    }
}
