using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using BusinessLayer;

namespace NetBankingApplication
{
    class Program
    {        
        public static void Main(string[] args)
        {
            Customer customer = new Customer()
            {
                FirstName = "Joe",
                LastName = "Zeng",
                Email = "Joezeng25@gmail.com",
                DOB = "09/12/1993"
            };
            BankBL bankBL = new BankBL();
            Console.WriteLine(BankBL.Register(customer));
            
            CheckingAccount account = new CheckingAccount();
            BankBL.OpenAccount(customer, account);
            account.PrintInfor();
            Console.WriteLine(BankBL.Deposit(account.AccountNum, 300.0));

            BusinessAccount ba = new BusinessAccount();
            BankBL.OpenAccount(customer, ba);
            ba.PrintInfor();
            Console.WriteLine(BankBL.Deposit(ba.AccountNum, 500.0));

            Console.WriteLine(BankBL.DisplayListAccount());

            Console.WriteLine(BankBL.Withdraw(account.AccountNum, 100.0));
            Console.WriteLine(BankBL.Withdraw(ba.AccountNum, 300.0));

            Console.WriteLine(BankBL.DisplayListAccount());

            Console.WriteLine(BankBL.DisplayTranction(10000));
            Console.WriteLine(BankBL.DisplayTranction(ba.AccountNum));

            Console.WriteLine(BankBL.Transfer(account.AccountNum,ba.AccountNum, 100.0));
            Console.WriteLine(BankBL.DisplayListAccount());


            Console.WriteLine(BankBL.Withdraw(ba.AccountNum, 400.0));

            Console.WriteLine(BankBL.DisplayListAccount());
            Console.WriteLine(BankBL.PayLoan(10000, 10002, 50.0));

            Console.WriteLine(BankBL.DisplayListAccount());

            TermDeposit tp = new TermDeposit();
            tp.PrintInfor();
            BankBL.OpenAccount(customer, tp);
            Console.WriteLine(BankBL.Deposit(10003, 2000));
            tp.termEnded= true;
            Console.WriteLine(BankBL.Withdraw(10003, 2000));

            Console.WriteLine(BankBL.ClosedAccount(tp.AccountNum));
            Console.WriteLine(BankBL.DisplayListAccount());


            try
            {
                Console.WriteLine(BankBL.CustomerOptions());
            }catch(Exception ex)
            {
                Console.WriteLine("Error! " + ex.Message);
            }
        }

    }//class
}//namespace
