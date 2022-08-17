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
            /*
             * Scrieti un program care va modela operatiunile unui POS.

             Instantiati banca, creeati conturi, depuneti bani in conturi, instantiati un POS, emiteti carduri si
efectuati plati prin intermediul POS-ului
Definiti exceptiile, tratati exceptiile si afisati mesaje corespunzatoare
            Suplimentar:
            Banca este unica la nivel de aplicatie. Cititi despre Singleton design pattern si folositi-l.
            */


            BankAccount bankAccount = new BankAccount();
            bankAccount.CashDeposit(200);
            CashWitdrawal(bankAccount, 200);
            CashWitdrawal(bankAccount, 10);
            bankAccount.CashDeposit(200); 
            CashWitdrawal(bankAccount, 200);

            BankAccount bankAccount2 = new BankAccount();
            BankAccount bankAccount3 = new BankAccount();

            Bank bank = Bank.GetBank();

            AddAccountToBank(bankAccount);
            AddAccountToBank(bankAccount);
            AddAccountToBank(bankAccount2);
            AddAccountToBank(bankAccount3);

            bank.CreateAccount();
            bank.CreateAccount();
            bank.CreateAccount();
            bank.CreateAccount();

            var bankAccount999 = new BankAccount();
            var creditCardNull = IssueCreditCard(bankAccount999);

            var credCard1 = IssueCreditCard(bankAccount);
            var credCard2 = IssueCreditCard(bankAccount);
            var credCard3 = IssueCreditCard(bankAccount);
            credCard3 = IssueCreditCard(bankAccount2);
            var credCard4 = IssueCreditCard(bankAccount2);
            var credCard5 = IssueCreditCard(bankAccount3);
           
            bankAccount.CashDeposit(200);

            ///Pay by account
            Pay(bankAccount999, 100);
            Pay(bankAccount, 300);
            Pay(bankAccount, 100);
            bankAccount2.CashDeposit(500);
            Pay(bankAccount2, 200);
            bankAccount3.CashDeposit(1000);
            Pay(bankAccount3, 100);

            ///Pay by credit card
            Pay(credCard1, 50);
            bank.activeConnetions = 3;
            Pay(credCard4, 100);
            bank.activeConnetions = 0;
            Pay(credCard4, 100);
            Pay(credCard4, 1000);
            Pay(credCard5, 150);
        }
        static void CashWitdrawal(BankAccount bankAccount, int money)
        {
            try
            {
                bankAccount.CashWithdrawal(money);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static void AddAccountToBank(BankAccount bankAccount)
        {
            try
            {
                Bank.GetBank().AddBankAccount(bankAccount);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }
        static Card IssueCreditCard(BankAccount bankAccount)
        {
            Card creditCard = null;
            try
            {
                creditCard = Bank.GetBank().IssueCard(bankAccount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return creditCard;
        }
        static void Pay(BankAccount bankAccount, int money)
        {
            try
            {
                Bank.GetBank().Pay(money, bankAccount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        static void Pay(Card creditCard, int money)
        {
            var POS = new POS();
            creditCard.InsertCard();
            try
            {
                POS.Pay(money, creditCard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                creditCard.ExtractCard();
            }
        }
    }
}
