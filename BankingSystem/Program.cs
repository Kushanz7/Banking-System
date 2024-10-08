using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    class Account
    {
        public int AccountNumber;
        public string AccountHolder;
        public double Balance;

        public Account(int accountNumber, string accountHolder)
        {
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            Balance = 0;
        }

        public virtual void Deposit(double amount)
        {
            Balance += amount;
            Console.WriteLine(amount + " deposited. New balance: " + Balance);
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine(amount + " withdrawn. New balance: " + Balance);
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine("Balance: " + Balance);
        }
    }


    class SavingsAccount : Account
    {
        public double InterestRate;

        public SavingsAccount(int accountNumber, string accountHolder, double interestRate)
            : base(accountNumber, accountHolder)
        {
            InterestRate = interestRate;
        }

        public void ApplyInterest()
        {
            double interest = Balance * InterestRate;
            Balance += interest;
            Console.WriteLine("Interest of " + interest + " applied. New balance: " + Balance);
        }
    }


    class CurrentAccount : Account
    {
        public double OverdraftLimit;

        public CurrentAccount(int accountNumber, string accountHolder, double overdraftLimit)
            : base(accountNumber, accountHolder)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= Balance + OverdraftLimit)
            {
                Balance -= amount;
                Console.WriteLine(amount + " withdrawn. New balance: " + Balance);
            }
            else
            {
                Console.WriteLine("Overdraft limit exceeded.");
            }
        }
    }


    class Customer
    {
        public string Name;
        public List<Account> Accounts;

        public Customer(string name)
        {
            Name = name;
            Accounts = new List<Account>();
        }

        public void OpenAccount(Account account)
        {
            Accounts.Add(account);
            Console.WriteLine("Account " + account.AccountNumber + " opened for " + Name + ".");
        }

        public Account GetAccount(int accountNumber)
        {
            return Accounts.Find(acc => acc.AccountNumber == accountNumber);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string customerName = Console.ReadLine();

            Customer customer = new Customer(customerName);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nBanking System Menu:");
                Console.WriteLine("1. Open Savings Account");
                Console.WriteLine("2. Open Current Account");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Check Balance");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        OpenSavingsAccount(customer);
                        break;
                    case 2:
                        OpenCurrentAccount(customer);
                        break;
                    case 3:
                        Deposit(customer);
                        break;
                    case 4:
                        Withdraw(customer);
                        break;
                    case 5:
                        CheckBalance(customer);
                        break;
                    case 6:
                        running = false;
                        Console.WriteLine("Exiting the banking system. Goodbye!");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void OpenSavingsAccount(Customer customer)
        {
            Console.Write("Enter account number: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter interest rate (e.g., 0.03 for 3%): ");
            double interestRate = Convert.ToDouble(Console.ReadLine());

            SavingsAccount savingsAccount = new SavingsAccount(accountNumber, customer.Name, interestRate);
            customer.OpenAccount(savingsAccount);
            Console.WriteLine("Savings account created successfully.");
        }

        static void OpenCurrentAccount(Customer customer)
        {
            Console.Write("Enter account number: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter overdraft limit: ");
            double overdraftLimit = Convert.ToDouble(Console.ReadLine());

            CurrentAccount currentAccount = new CurrentAccount(accountNumber, customer.Name, overdraftLimit);
            customer.OpenAccount(currentAccount);
            Console.WriteLine("Current account created successfully.");
        }

        static void Deposit(Customer customer)
        {
            Console.Write("Enter account number: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());
            Account account = customer.GetAccount(accountNumber);

            if (account != null)
            {
                Console.Write("Enter deposit amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void Withdraw(Customer customer)
        {
            Console.Write("Enter account number: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());
            Account account = customer.GetAccount(accountNumber);

            if (account != null)
            {
                Console.Write("Enter withdrawal amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void CheckBalance(Customer customer)
        {
            Console.Write("Enter account number: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());
            Account account = customer.GetAccount(accountNumber);

            if (account != null)
            {
                account.CheckBalance();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }
}
