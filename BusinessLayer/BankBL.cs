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
        static BankDAL accountDal;

        public BankBL()
        {
            accountDal = new BankDAL();

        }

        public static void Register(Object newCustomer)
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

        public static void ClosedAccount(int accountNum)
        {
            //accountDal.ClosedAccount(accountNum);
            try
            {
                accountDal.ClosedAccount(accountNum);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Deposit(int accountNum, double depositeAmount)
        {
            try
            {
                accountDal.Deposit(accountNum, depositeAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Transfer(int accountNumFrom, int accountNumTo, double amount)
        {
            accountDal.Transfer(accountNumFrom, accountNumTo, amount);
        }

        public static void PayLoan(int accountNumFrom, int LoanAccountNum, double amount)
        {
            accountDal.PayLoan(accountNumFrom, LoanAccountNum, amount);
        }

        public static void Withdraw(int accountNum, double withDrawAmount)
        {
            try
            {
                accountDal.Withdraw(accountNum, withDrawAmount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DisplayListAccount()
        {
            accountDal.DisplayListAccount();
        }

        public static void DisplayTranction(int accountNum)
        {
            accountDal.DisplayTranction(accountNum);
        }

        public static Customer LookForCustomer(int customerID)
        {
            var customer = accountDal.LookForCustomer(customerID);
            try
            {
                if (customer != null)
                {
                    return customer as Customer;
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

        public static void CustomerOptions()
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

            }
            else if (cust.Equals("Old Customer", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Type your option: open new account, transcation, close account, display transcation or Display list of account.");
                string option = Console.ReadLine();

                if (option.Equals("open new account"))
                {
                    Console.WriteLine("What is your id #?");
                    int customerID = Convert.ToInt32(Console.ReadLine());
                    //Console.WriteLine(customerID);
                    OpenSelectedAccount(LookForCustomer(customerID));
                }
                else if (option.Equals("transcation", StringComparison.OrdinalIgnoreCase))
                {
                    DoTranscation();
                }
                else if (option.Equals("Display transcation", StringComparison.OrdinalIgnoreCase))
                {
                    GetInfor();
                }
                else if (option.Equals("Display list of account", StringComparison.OrdinalIgnoreCase))
                {
                    DisplayListAccount();
                }
                else if (option.Equals("close account", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("What is your account number?");
                    int accountNum = Convert.ToInt32(Console.ReadLine());
                    ClosedAccount(accountNum);
                }
                else
                {
                    Console.WriteLine("Invalid option!");
                }

            }
            else
            {
                Console.WriteLine("Please type your are a new customer or old customer!");
            }
        }

        public static void GetInfor()
        {
            Console.WriteLine("What is your account number?");
            int accountNum = Convert.ToInt32(Console.ReadLine());
            DisplayTranction(accountNum);
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

        public static void DoTranscation()
        {
            Console.WriteLine("Type your option: deposit, widthdraw, pay loan, or transfer!");
            string option = Console.ReadLine();
            Console.WriteLine("What is your account number?");
            int accountNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the amount of money you want?");
            double amount = Convert.ToInt32(Console.ReadLine());

            if (option.Equals("deposit", StringComparison.OrdinalIgnoreCase))
            {
                Deposit(accountNum, amount);

            }
            else if (option.Equals("widthdraw", StringComparison.OrdinalIgnoreCase))
            {
                Withdraw(accountNum, amount);
            }
            else if (option.Equals("pay loan", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    Console.WriteLine("What is your loan account number you want to pay to?");
                    int loanAccountNum = Convert.ToInt32(Console.ReadLine());
                    PayLoan(accountNum, loanAccountNum, amount);
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
                Transfer(accountNum, accountNumTo, amount);
            }
            else
            {
                Console.WriteLine("Invlaid option!");
            }

        }//do transcation
    }
}
