using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class Account
    {
        public Customer customer { get; set; }
        public int AccountNum { get; set; }
        public double Balance { get; set; }
        public static int count = 10000;
        public List<Transaction> Transcation { get; set; }
        public bool IsActive { get; set; }
        public abstract void PrintInfor();
        public abstract void Deposit(int accountNum, double amount);
        public abstract void Withdraw(int accountNum, double amount);

    }
}
