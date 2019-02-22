using KKB.BLL.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KKB.web.Model
{
    public class ServiceMenu
    {
        private User user = null;
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Registration");
            Console.WriteLine("2. LogOn");
            Console.WriteLine("3. Exit");
            Console.Write(": ");
            int choice = 0;
            bool tryChoice = Int32.TryParse(Console.ReadLine(), out choice);
            if (tryChoice)
            {
                if (choice == 1)
                {
                    Console.Clear();
                    RegistrationMenu();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    LogOnMenu();
                }
                else return;
            }
            else
            {
                //Console.WriteLine("Неправильно введены данные");
                return;
            }
        }

        private void LogOnMenu()
        {
            string login = "";
            string password = "";
            string message = "";
            Console.Write("Enter Login: ");
            login = Console.ReadLine();
            Console.Write("Enter Password: ");
            password = Console.ReadLine();
            ServiceUser susr = new ServiceUser();
            user = susr.LogOn(login, password, out message);
            if (user != null)
            {
                ServiceAccount sa = new ServiceAccount();
                user.Accounts = sa.GetAccounts(user.id);
                AuthoriseUserMenu();
            }
            else
            {
                Console.WriteLine(message);
                Thread.Sleep(1000);
                MainMenu();
            }
        }

        private void RegistrationMenu()
        {
            User user = new User();
            Console.Write("Enter Login: ");
            user.login = Console.ReadLine();
            Console.Write("Enter Password: ");
            user.password = Console.ReadLine();
            Console.Write("Enter Full name: ");
            user.fullname = Console.ReadLine();            
            string message = "";
            ServiceUser susr = new ServiceUser();
            if (susr.Registration(user, out message))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                MainMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                MainMenu();
            }

        }

        public void AuthoriseUserMenu()
        {
            Console.Clear();
            Console.WriteLine("Приветствую вас {0}", user.fullname);
            if (user.Accounts.Count > 0)
            {
                Console.WriteLine("1. Вывод баланса");
                Console.WriteLine("2. Пополнение баланса");
                Console.WriteLine("3. Снять деньги со счета");
            }
            else
            {
                Console.WriteLine("5. Создать счет");
            }
            Console.WriteLine("4. Выход");
            
            int choice = 0;
            bool tryChoice = Int32.TryParse(Console.ReadLine(), out choice);
            if (tryChoice) {
                if (choice == 4)
                {
                    Console.Clear();
                    user = null;
                    MainMenu();
                }

                else if (choice == 5)
                {
                    Console.Clear();
                    CreateAccountMenu();

                }

                else if (choice == 1)
                {
                    Console.Clear();
                    ShowBalanceMenu();
                    Console.ReadKey();
                    AuthoriseUserMenu();
                }

                else if (choice == 2)
                {
                    Console.Clear();
                    AddMoney();
                    Console.ReadKey();
                    AuthoriseUserMenu();
                }
                else if (choice == 3)
                {
                    Console.Clear();
                    TakeMoney();
                    Console.ReadKey();
                    AuthoriseUserMenu();
                }                
            }
            else
            {
                user = null;
                MainMenu();
            }

        }

        private void ShowBalanceMenu()
        {            
            for (int i = 0; i < user.Accounts.Count; i++)
            {
                var sacc = user.Accounts[i];

                Console.WriteLine(string.Format("{2}.{0:20}-{1:0.00}", sacc.AccountNumber,
                    sacc.Balance, sacc.Id));                
            }
        }

        private void CreateAccountMenu()
        {
            ServiceAccount sa = new ServiceAccount();
            var acc = sa.CreateAccount(user.id, Currency.KZT);
            string message = "";
            bool isCreated = sa.CreateAccountDb(acc, out message);
            if (isCreated)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
                user.Accounts = sa.GetAccounts(user.id);

                Thread.Sleep(1000);
                AuthoriseUserMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void AddMoney()
        {
            ShowBalanceMenu();
            Console.Write("Выберите счет:");
            int accid = Int32.Parse(Console.ReadLine());            
            Console.Write("Вводите сумму:");
            bool isGood = false;
            string message = "";
            try
            {
                double cash = Double.Parse(Console.ReadLine());
                ServiceAccount sa = new ServiceAccount();                
                isGood = sa.MoneyAppend(accid, cash, out message);
            }
            catch (Exception exc)
            {
                message = exc.Message;
            }               
            
            if (isGood)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void TakeMoney()
        {
            ShowBalanceMenu();
            Console.Write("Выберите счет:");
            int accid = Int32.Parse(Console.ReadLine());
            Console.Write("Вводите сумму:");
            bool isGood = false;
            string message = "";
            try
            {
                double cash = Double.Parse(Console.ReadLine());
                ServiceAccount sa = new ServiceAccount();
                isGood = sa.MoneyTake(accid, cash, out message);
            }
            catch (Exception exc)
            {
                message = exc.Message;
            }
            if (isGood)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
