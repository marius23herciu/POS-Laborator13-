
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_Laborator13_
{
    public class Bank
    {

        public static readonly Bank _instance = new Bank();

        private List<BankAccount> listOfAccounts = new List<BankAccount>();
        private Dictionary<int, Guid> accountsID = new Dictionary<int, Guid>();
        private Dictionary<Guid, Guid> creditCardsPairedToAccounts = new Dictionary<Guid, Guid>();
        private int counterOfAccounts = 0;
        public int activeConnetions = 0;
        private Bank()
        {

        }
        /// <summary>
        /// Returns single instance of Bank.
        /// </summary>
        /// <returns></returns>
        public static Bank GetBank()
        {
            return _instance;
        }
        /// <summary>
        /// Checks for account Id in bank's database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool CheckIfAccountIdExists(Guid ID)
        {
            foreach (var exsistentAccount in this.accountsID)
            {
                if (exsistentAccount.Value == ID)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Checks for card Id in bank's database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool CheckIfCardIdExists(Guid ID)
        {
            foreach (var exsistentCard in this.creditCardsPairedToAccounts)
            {
                if (exsistentCard.Key == ID)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Adds bank account to bank's database.
        /// </summary>
        /// <param name="bankAccount"></param>
        public void AddBankAccount(BankAccount bankAccount)
        {
            int flag = 0;
            try
            {
                if (CheckIfAccountIdExists(bankAccount.ID) == true)
                {
                    flag++;
                    throw new Exception("Account already existent in bank's database.");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            if (flag == 0)
            {
                counterOfAccounts++;
                this.accountsID.Add(counterOfAccounts, bankAccount.ID);
                this.listOfAccounts.Add(bankAccount);
            }
        }
        //}
        /// <summary>
        /// Creates bank account.
        /// </summary>
        /// <returns></returns>
        public Guid CreateAccout()
        {
            BankAccount bankAccount = new BankAccount();
            AddBankAccount(bankAccount);
            return bankAccount.ID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        private int GetNumberOfCreditCardsFromBank(BankAccount bankAccount)
        {
            int result = 0;
            foreach (var account in this.accountsID)
            {
                if (account.Value == bankAccount.ID)
                {
                    result = bankAccount.GetNumberOfCreditCardsFromAccount(bankAccount);
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Creates card connected to a bank account and adds its ID to bank's database.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public Card IssueCard(BankAccount bankAccount)
        {
            Card card = null;
            try
            {
                if (CheckIfAccountIdExists(bankAccount.ID) != true)
                {
                    throw new Exception("Account doesn't exist in bank's database.");
                }
                if (GetNumberOfCreditCardsFromBank(bankAccount) >= 2)
                {
                    throw new Exception("Account has allready 2 active credit cards.");
                }
                else
                {
                    card = new Card(Guid.NewGuid(), bankAccount);
                    bankAccount.IncreaseNumberOfCreditCards();
                    this.creditCardsPairedToAccounts.Add(card.ID, card.bankAccount.ID);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                if (card == null)
                {
                    throw new Exception("Error creating credit card: null refference.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return card;
        }
        /// <summary>
        /// Gets bank account from ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public BankAccount GetBankAccount(Guid ID)
        {
            for (int i = 0; i < listOfAccounts.Count; i++)
            {
                if (ID == listOfAccounts[i].ID)
                {
                    return listOfAccounts[i];
                }
            }

            return null;
        }
        /// <summary>
        /// Gets bank account ID from card's ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Guid GetBankAccountIDFromCardID(Guid ID)
        {
            foreach (var exsistentCard in this.creditCardsPairedToAccounts)
            {
                if (exsistentCard.Key == ID)
                {
                    return exsistentCard.Value;
                }
            }

            return new Guid();
        }
        /// <summary>
        /// Connects to bank for payment.
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                if (activeConnetions>=3)
                {
                    throw new Exception("Maximum connection limit is reached. Try later...");
                }
                else
                {
                    activeConnetions++;
                    Console.WriteLine("Connected");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        /// <summary>
        /// Disconnects from bank after payment.
        /// </summary>
        public void Disconnect()
        {
            activeConnetions--;
            Console.WriteLine("Disconnected");
        }
        /// <summary>
        /// Payment using card's ID.
        /// </summary>
        /// <param name="ammount"></param>
        /// <param name="ID"></param>
        public void Pay(int ammount, Guid ID)
        {
            try
            {
                BankAccount bankAccount = new BankAccount();
                if (CheckIfCardIdExists(ID) == true)
                {
                    bankAccount = GetBankAccount(GetBankAccountIDFromCardID(ID));
                }
                if (CheckIfCardIdExists(ID) != true)
                {
                    throw new Exception("Credit card ID doesn't exist in bank's database.");
                }
                if (bankAccount.Money < ammount)
                {
                    throw new Exception("The payment ammount exceeds the available sold.");
                }
                else
                {
                    bankAccount.Money -= ammount;
                    Console.WriteLine("Pay done.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// Payment using bak account.
        /// </summary>
        /// <param name="ammount"></param>
        /// <param name="bankAccount"></param>
        public void Pay(int ammount, BankAccount bankAccount)
        {
            try
            {
                if (CheckIfAccountIdExists(bankAccount.ID) != true)
                {
                    throw new Exception("Account doesn't exist in bank's database.");
                }
                if (bankAccount.Money < ammount)
                {
                    throw new Exception("The payment ammount exceeds the available sold.");
                }
                else
                {
                    bankAccount.Money -= ammount;
                    Console.WriteLine("Pay done.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
