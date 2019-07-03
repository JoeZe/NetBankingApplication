using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace DAL
{
    public class BankDAL
    {
        public static Dictionary<ITransaction, double> transcation = new Dictionary<ITransaction, double>();
        public static List<Account> account = new List<Account>();
        public static List<Customer> customers = new List<Customer>();

        public string Register(Customer newCustomer)
        {
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
                    customers.Add(newCustomer);
                }
                return "Successfully adding the new customer!";
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        //creating new account for customer
        public void OpenNewAccount(Customer cust, object accountType)
        {
            if(accountType.GetType().Name.Equals("CheckingAccount", StringComparison.OrdinalIgnoreCase))
            {
                // creating new checking account
                CheckingAccount ck = accountType as CheckingAccount;
                ck.customer = cust;
                ck.IsActive = true;
                account.Add(ck);
                ck.customer.accounts = account;

            }else if (accountType.GetType().Name.Equals("BusinessAccount", StringComparison.OrdinalIgnoreCase)){
                //creating new business account
                BusinessAccount ba = accountType as BusinessAccount;
                ba.customer = cust;
                ba.OverdraftAmount = 100.0;
                ba.IsActive = true;
                account.Add(ba);
                ba.customer.accounts = account;
            }
            else if (accountType.GetType().Name.Equals("Loan", StringComparison.OrdinalIgnoreCase))
            {
                //creating new loan account
                Loan loan = accountType as Loan;
                loan.customer = cust;
                loan.IsActive = true;
                account.Add(loan);
                loan.customer.accounts = account;
            }
            else
            {
                //creating new term deposit account
                TermDeposit termDeposit = accountType as TermDeposit;
                termDeposit.customer = cust;
                termDeposit.IsActive = true;
                account.Add(termDeposit);
                termDeposit.customer.accounts = account;
            }
        }

        //deposit the amount of money to the account number
        public string Deposit(int accountNum, double amount)
        {
            //account number could be null
            try
            {
                StringBuilder sb = new StringBuilder();
                Deposit deposit = new Deposit();
                Account act = account.Find(x => x.AccountNum == accountNum);
                if (act == null||amount<=0)
                {
                    throw new AccountNumNotFoundException("Account number not found or the amount of money is less than or equal to $0!");
                }
                if (act.GetType().Name.Equals("Loan"))
                {
                    act.Balance -= amount;
                    sb.AppendLine("Paying to the loan account: " + amount);
                   // Console.WriteLine("Paying to the loan account: " + amount);
                }
                else
                {
                    act.Balance += amount;
                    sb.AppendLine("Depositing: $" + amount + " into the " + act.GetType().Name + " #" + accountNum + "\n");
                }
                deposit.account = act;
                transcation.Add(deposit, amount);
                act.Transcation = transcation;
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;

            }
        }

        //find the customer with the customer id
        public Customer LookForCustomer(int customerID)
        {
            try
            {
                Customer cust = customers.Find(x => x.Id == customerID);
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
        public string CloasedAccount(int accountNum)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Account act = account.Find(x => x.AccountNum == accountNum);
                if (act == null)
                {
                    throw new AccountNumNotFoundException("Account number not found!");
                }
                //Withdraw(accountNum, act.Balance);
                //account.Remove(act);
                act.IsActive = false;
                sb.AppendLine("Acccount removed.......");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //withdraw the amount of money from the account number
        public string Withdraw(int accountNum, double amount)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Account act = account.Find(x => x.AccountNum == accountNum);
                if (act == null|| amount<=0)
                {
                    throw new AccountNumNotFoundException("Account number not found or the amount of money is less than or equal to $0!");
                }
                double balance = act.Balance;
                if (balance - amount < 0 && act.GetType().Name.Equals("BusinessAccount", StringComparison.OrdinalIgnoreCase))
                {
                    BusinessAccount ba = act as BusinessAccount;
                    double Overdraft;
                    Overdraft = amount - act.Balance;
                    if (Overdraft <= 100.0)
                    {
                        //loan is - payment
                        act.Balance = 0;
                        ba.OverdraftAmount -= Overdraft;
                        Loan newLoan = new Loan();

                        newLoan.Balance = Overdraft;
                        newLoan.PrintInfor();
                        OpenNewAccount(ba.customer, newLoan);
                    }
                    else
                    {
                        sb.AppendLine("Your maximum overdraft is $100!");
                    }
                }
                else if (balance - amount < 0 && act.GetType().Name.Equals("CheckingAccount", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("Your account do not have sufficient amount of money to withdraw. You only have $" + act.Balance);
                }
                else if ((balance != amount || balance - amount < 0) && act.GetType().Name.Equals("TermDeposit", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine("You can't withdraw partially amount of money or more than the balance from the Term deposit account!");
                }
                else
                {
                    if (act.GetType().Name.Equals("TermDeposit", StringComparison.OrdinalIgnoreCase)&& ((TermDeposit) act).termEnded==true)
                    {
                        Console.WriteLine("Is the Term deposit acccount term ended?");
                        ((TermDeposit)act).termEnded = bool.Parse(Console.ReadLine());
                        sb.AppendLine("You are withdrawing $" + ((TermDeposit)act).Earning() + " from your term deposit account.");
                    }
                    else
                    {
                        act.Balance -= amount;
                        sb.AppendLine("Withdrawing: $" + amount + " from the " + act.GetType().Name + " #" + accountNum + "\n");
                    }
                }
                //log the transcation to the db
                Withdraw withdraw = new Withdraw();
                withdraw.account = act;
                transcation.Add(withdraw, amount);
                act.Transcation = transcation;
                // throw new AccountNumNotFoundException("Account number not found!");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //transfering the amount of money from the first account number to the second account number
        public string Transfer(int acccountNumFrom, int accountNumTo, double amount)
        {
            StringBuilder sb = new StringBuilder();
            Account act = account.Find(x => x.AccountNum == acccountNumFrom);
            sb.AppendLine(Withdraw(acccountNumFrom, amount));
            act = account.Find(x => x.AccountNum == accountNumTo);
            sb.AppendLine(Deposit(accountNumTo, amount));
            return sb.ToString();
        }
        
        //paying the amount of money from the first account number to the loan account number
        public string PayLoan(int acccountNumFrom, int LoanAccountNum, double amount)
        {
            return Transfer(acccountNumFrom, LoanAccountNum, amount);
        }

        //display all the account created in the program
        public string DisplayListAccount()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Account a in account)
            {
                sb.AppendLine("Account Number: " + a.AccountNum +" | Account Type: " +a.GetType().Name + "| Balance: "+ a.Balance +" | Is active: "+ a.IsActive);
            }
            sb.AppendLine();
            return sb.ToString();
            
        }

        //display the transaction for the specific account number
        public string DisplayTranction(int accountNum)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Display transcation for an account.");
            int count = 1;
            foreach(var pair in transcation)
            {
                ITransaction key = pair.Key;
                double value = pair.Value;
                if (key.account.AccountNum == accountNum)
                {
                    sb.AppendLine(count++ + " ------ Transcation type: " + key.GetType().Name + " ------Amount: " + value);
                }

            }//for

            sb.AppendLine();
            return sb.ToString();

        }
    }
}
