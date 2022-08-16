using System;

namespace POS_Laborator13_
{
    class Program
    {
        //static Bank bank = Bank.GetBank();
        //Ar fi mai bine daca as face instantierea bancii in afara Main? :)
        //am vazut pe cineva facand asa in timp ce cautam despre Singleton pe net...
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.CashDeposit(200);
            bankAccount.CashWithdrawal(200);
            bankAccount.CashWithdrawal(10);
            bankAccount.CashDeposit(200);
            bankAccount.CashWithdrawal(200);

            BankAccount bankAccount2 = new BankAccount();
            BankAccount bankAccount3 = new BankAccount();

            Bank bank = Bank.GetBank();

            bank.AddBankAccount(bankAccount);
            bank.AddBankAccount(bankAccount);
            bank.AddBankAccount(bankAccount2);
            bank.AddBankAccount(bankAccount3);
            
            bank.CreateAccout();
            bank.CreateAccout();
            bank.CreateAccout();
            bank.CreateAccout();

            var credCard1 = bank.IssueCard(bankAccount);
            var bankAccount999 = new BankAccount();
            var creditCardNull = bank.IssueCard(bankAccount999);
            var credCard2 = bank.IssueCard(bankAccount);
            var credCard3 = bank.IssueCard(bankAccount);

            credCard3 = bank.IssueCard(bankAccount2);
            var credCard4 = bank.IssueCard(bankAccount2);

            var credCard5 = bank.IssueCard(bankAccount3);


            bankAccount.CashDeposit(200);
            bank.Pay(100, bankAccount999);
            bank.Pay(300, bankAccount);
            bank.Pay(100, bankAccount);

            bankAccount2.CashDeposit(500);
            bank.Pay(200, bankAccount2);

            bankAccount3.CashDeposit(1000);
            bank.Pay(100, bankAccount3);

            var POS = new POS();
            POS.Pay(50, credCard1);
            bank.activeConnetions = 3;
            POS.Pay(100, credCard4);
            bank.activeConnetions = 0;
            POS.Pay(100, credCard4);
            POS.Pay(1000, credCard4);
            POS.Pay(150, credCard5);
        }
    }
}
