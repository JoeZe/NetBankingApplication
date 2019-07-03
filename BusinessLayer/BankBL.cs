using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

namespace BusinessLayer
{
    public class BankBL
    {
        static BankDAL accountDal = new BankDAL();

        public static void Register(Customer newCustomer)
        {
            try
            {
                accountDal.Register(newCustomer);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static void OpenAccount(Customer customer, object accountType)
        {
            accountDal.OpenNewAccount(customer, accountType); 
        }

        public static string ClosedAccount(int accountNum)
        {
            try
            {
                return accountDal.CloasedAccount(accountNum);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static string Deposit(int accountNum, double depositeAmount)
        {
            try
            {
                return accountDal.Deposit(accountNum, depositeAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Transfer(int accountNumFrom, int accountNumTo, double amount)
        {
            return accountDal.Transfer(accountNumFrom, accountNumTo, amount);
        }

        public static string PayLoan(int accountNumFrom, int LoanAccountNum, double amount)
        {
            return accountDal.PayLoan(accountNumFrom, LoanAccountNum, amount);
        }

        //withdraw money form the account number
        public static string Withdraw(int accountNum, double withDrawAmount)
        {
            try
            {
                return accountDal.Withdraw(accountNum, withDrawAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string DisplayListAccount()
        {
            return accountDal.DisplayListAccount();
        }

        public static string DisplayTranction(int accountNum)
        {
            return accountDal.DisplayTranction(accountNum);
        }

        public static Customer LookForCustomer(int customerID)
        {
            var customer = accountDal.LookForCustomer(customerID);
            try
            {
                if (customer != null)
                {
                    return customer;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw;
            }


            
        }

        public static string CustomerOptions()
        {
            Console.WriteLine("Are you a New Customer or Old Customer?");
            string cust = Console.ReadLine();
            if (cust.Equals("New Customer", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("What is your first name?");
                string firstName = Console.ReadLine();
                Console.WriteLine("What is your last name?");
                string lastName = Console.ReadLine();
                Console.WriteLine("What is your DOB?");
                string dob = Console.ReadLine();
                Console.WriteLine("What is your Email?");
                string email = Console.ReadLine();
                Customer custm = new Customer(firstName, lastName, dob, email);
                Register(custm);
                OpenSelectedAccount(custm);
                return "Success!";

            }
            else if (cust.Equals("Old Customer", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Type your option: open new account, transcation, or get account information.");
                string option = Console.ReadLine();

                if (option.Equals("open new account"))
                {
                    Console.WriteLine("What is your id #?");
                    int customerID = Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine(customerID);
                    OpenSelectedAccount(LookForCustomer(customerID));
                    return "Success!";
                }
                else if (option.Equals("transcation", StringComparison.OrdinalIgnoreCase))
                {
                    return DoTranscation();
                }
                else if (option.Equals("Get account information", StringComparison.OrdinalIgnoreCase))
                {
                    return GetInfor();
                }
                else
                {
                    return ("Invalid option!");
                }

            }
            else
            {
                return "Please type your are a new customer or old customer!";
            }
        }

        public static string GetInfor()
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("What is your account number?");
            int accountNum = Convert.ToInt32(Console.ReadLine());
            sb.AppendLine(DisplayTranction(accountNum));
            sb.AppendLine(DisplayListAccount());
            return sb.ToString();
        }

        public static void OpenSelectedAccount(Customer customer)
        {
            Console.WriteLine("What kind of account you want to open?" +
                "\nex: checking account, business account, loan account, term deposit.");
            //Account a = new CheckingAccount();
            string act = Console.ReadLine();
            if (act.Equals("Checking account", StringComparison.OrdinalIgnoreCase))
            {
                CheckingAccount ck = new CheckingAccount();
                OpenAccount(customer, ck);
                ck.PrintInfor();
            }
            else if (act.Equals("Business account", StringComparison.OrdinalIgnoreCase))
            {
                BusinessAccount ba = new BusinessAccount();
                OpenAccount(customer, ba);
                ba.PrintInfor();
            }
            else if (act.Equals("Term deposit", StringComparison.OrdinalIgnoreCase))
            {
                TermDeposit tm = new TermDeposit();
                OpenAccount(customer, tm);
                tm.PrintInfor();
            }
            else
            {
                Console.WriteLine("Invalid account!");
            }

        }

        public static string DoTranscation()
        {
            Console.WriteLine("Type your option: deposit, widthdraw, pay loan, or transfer!");
            string option = Console.ReadLine();
            Console.WriteLine("What is your account number?");
            int accountNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the amount of money you want?");
            double amount = Convert.ToInt32(Console.ReadLine());

            if (option.Equals("deposit", StringComparison.OrdinalIgnoreCase))
            {
                return Deposit(accountNum, amount);

            }
            else if (option.Equals("widthdraw", StringComparison.OrdinalIgnoreCase))
            {
                return Withdraw(accountNum, amount);
            }
            else if (option.Equals("pay loan", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    Console.WriteLine("What is your loan account number you want to pay to?");
                    int loanAccountNum = Convert.ToInt32(Console.ReadLine());
                    return PayLoan(accountNum, loanAccountNum, amount);
                }
                catch (Exception)
                {
                    throw;
                }

            }
            else if (option.Equals("transfer", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("What is the account number that you want to transfer to?");
                int accountNumTo = Convert.ToInt32(Console.ReadLine());
                return Transfer(accountNum, accountNumTo, amount);
            }
            else
            {
                return("Invlaid option!");
            }

        }//do transcation
    }
}
