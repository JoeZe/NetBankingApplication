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
            BankBL.Register(customer);

            CheckingAccount account = new CheckingAccount();
            BankBL.OpenAccount(customer, account);
            account.PrintInfor();
            BankBL.Deposit(account.AccountNum, 300.0);

            BusinessAccount ba = new BusinessAccount();
            BankBL.OpenAccount(customer, ba);
            ba.PrintInfor();
            BankBL.Deposit(ba.AccountNum, 500.0);

            BankBL.DisplayListAccount();

            BankBL.Withdraw(account.AccountNum, 100.0);
            BankBL.Withdraw(ba.AccountNum, 300.0);

            BankBL.DisplayListAccount();

            BankBL.DisplayTranction(10000);
            BankBL.DisplayTranction(ba.AccountNum);

            BankBL.Transfer(account.AccountNum, ba.AccountNum, 100.0);
            BankBL.DisplayListAccount();


            BankBL.Withdraw(ba.AccountNum, 400.0);

            BankBL.DisplayListAccount();
            BankBL.PayLoan(10000, 10002, 50.0);

            BankBL.DisplayListAccount();

            TermDeposit tp = new TermDeposit();
            tp.PrintInfor();
            BankBL.OpenAccount(customer, tp);
            //BankBL.Deposit(10003, 2000);
           // tp.termEnded = true;
            //BankBL.Withdraw(10003, 2000);

            BankBL.ClosedAccount(tp.AccountNum);
            BankBL.DisplayListAccount();


            try
            {
                while (true)
                {
                    BankBL.CustomerOptions();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error! " + ex.Message);
            }

        }

    }//class
}//namespace
