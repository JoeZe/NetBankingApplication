using Entities;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class BankDAL 
    {
        public static List<Transaction> transcation = new List<Transaction>();
        public static List<Account> account = new List<Account>();
        public static List<Customer> customer = new List<Customer>();

        public void Register(Object cust)
        {
            Customer newCustomer = cust as Customer;
            try
            {
                if (String.IsNullOrEmpty(newCustomer.FirstName))
                {
                    throw new AccountNumNotFoundException("First name can't be empty!");
                }
                else if (String.IsNullOrEmpty(newCustomer.LastName))
                {
                    throw new AccountNumNotFoundException("Last name can't be empty!");
                }
                else if (String.IsNullOrEmpty(newCustomer.DOB))
                {
                    throw new AccountNumNotFoundException("DOB can't be empty!");
                }
                else if (String.IsNullOrEmpty(newCustomer.Email))
                {
                    throw new AccountNumNotFoundException("Email can't be empty!");
                }
                else
                {
                    Console.WriteLine("New Customer account id is: " + newCustomer.Id);
                    customer.Add(newCustomer);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //creating new account for customer
        public void OpenNewAccount(object custm, object accountType)
        {
            Customer cust = custm as Customer;
            Account act = accountType as Account;
            act.customer = cust;
            act.IsActive = true;
            account.Add(act);
            act.customer.account = account;
        }

        //deposit the amount of money to the account number
        public void Deposit(int accountNum, double amount)
        {
            //account number could be null
            try
            {
                //Deposit deposit = new Deposit();
                Account act = account.Find(x => x.AccountNum == accountNum);
                
                if (act == null||amount<=0)
                {
                    throw new AccountNumNotFoundException("Account number not found or the amount of money is less than or equal to $0!");
                }
                act.Deposit(accountNum, amount);
                //deposit.account = act;
                Transaction tran = new Transaction();
                tran.account = act;
                tran.amount = amount;
                tran.date = DateTime.Now;
                tran.TranscationType = "Deposit";
                transcation.Add(tran);
                act.Transcation = transcation;
            }
            catch (Exception)
            {
                throw;

            }
        }

        //find the customer with the customer id
        public Object LookForCustomer(int customerID)
        {
            try
            {
                Customer cust = customer.Find(x => x.Id == customerID);
                if (cust == null)
                {
                    throw new AccountNumNotFoundException("Customer not found!");
                }
                return cust;
            }catch(Exception ex)
            {
                throw;
            }
        }

        // closed the account with the given account number
        public void ClosedAccount(int accountNum)
        {
            try
            {
                Account act = account.Find(x => x.AccountNum == accountNum);
                if (act == null)
                {
                    throw new AccountNumNotFoundException("Account number not found!");
                }
                if (act.Balance != 0)
                {
                    Withdraw(accountNum, act.Balance);
                }
                //account.Remove(act);
                act.IsActive = false;
                Console.WriteLine("Acccount removed.......");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //withdraw the amount of money from the account number
        public void Withdraw(int accountNum, double amount)
        {
            try
            {
                Account act = account.Find(x => x.AccountNum == accountNum);
                if (act == null|| amount<=0)
                {
                    throw new AccountNumNotFoundException("Account number not found or the amount of money is less than or equal to $0!");
                }
                act.Withdraw(accountNum, amount);
                //creating loan account
                if (act.Balance==0 && act.GetType().Name.Equals("BusinessAccount") && ((BusinessAccount) act).OverdraftAmount >-100 && ((BusinessAccount)act).AllowLoan == true)
                {
                    //creating new loan account
                    Loan newLoan = new Loan();
                    newLoan.Balance = 100 + ((BusinessAccount)act).OverdraftAmount;
                    ((BusinessAccount)act).AllowLoan = false;
                    newLoan.PrintInfor();
                    newLoan.customer = act.customer;
                    newLoan.IsActive = true;
                    account.Add(newLoan);
                    newLoan.customer.account = account;
                }
                //log the transcation to the db
               // Withdraw withdraw = new Withdraw();
                //withdraw.account = act;
                Transaction tran = new Transaction();
                tran.account = act;
                tran.amount = amount;
                tran.date = DateTime.Now;
                tran.TranscationType = "Withdraw";
                transcation.Add(tran);
                act.Transcation =transcation;
               // throw new AccountNumNotFoundException("Account number not found!");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //transfering the amount of money from the first account number to the second account number
        public void Transfer(int acccountNumFrom, int accountNumTo, double amount)
        {
            Account act = account.Find(x => x.AccountNum == acccountNumFrom);
            Withdraw(acccountNumFrom, amount);
            act = account.Find(x => x.AccountNum == accountNumTo);
            Deposit(accountNumTo, amount);
        }
        
        //paying the amount of money from the first account number to the loan account number
        public void PayLoan(int acccountNumFrom, int LoanAccountNum, double amount)
        {
            Transfer(acccountNumFrom, LoanAccountNum, amount);
        }

        //display all the account created in the program
        public void DisplayListAccount()
        {
            foreach(Account a in account)
            {
                Console.WriteLine("Account Number: " + a.AccountNum +" | Account Type: " +a.GetType().Name + "| Balance: "+ a.Balance +" | Is active: "+ a.IsActive);
            }
            Console.WriteLine();
        }

        //display the transaction for the specific account number
        public void DisplayTranction(int accountNum)
        {
            Console.WriteLine("Display transcation for an account.");
            int count = 1;
            foreach(Transaction tran in transcation)
            {
                if (tran.account.AccountNum == accountNum)
                {
                    Console.WriteLine(count++ + " ------ Transcation type: " + tran.TranscationType + " ------Amount: " + tran.amount);
                }

            }//for
            Console.WriteLine();

        }
    }
}
