
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_Laborator13_
{
    public class Bank
    {
        /*
         Banca
Va avea o lista de conturi curente indexate intr-un dictionar in functie de id-ul (Guid) acestora.
Va avea o metoda „CreeazaCont” care
• Va creea un nou cont bancar
• Il va adauga in lista conturilor
• Va returna id-ul contului
Va avea o metoda „EmiteCard” care va primi ca parametru id-ul contului
• In cazul in care contul nu exista va arunca o exceptie corespunzatoare
• In cazul in care au fost emise deja 2 carduri pentru acel cont va arunca o exceptie.
• Cardul emis va primi id-ul contului.
Va avea o metoda „Plateste” care va primi 2 parametri: suma si id-ul contului.
• In cazul in care contul nu exista va arunca o exceptie corespunzatoare

        Suplimentar:
        Banca
La emiterea cardului
• Va memora intr-un dictionar id-ul contului corespunzator fiecarui id al cardului.
• In cazul in care au fost emise deja doua carduri pentru cont-ul cerut, nu va mai fi emis un nou
card ci va fi aruncata o exceptie
Metoda „Plateste” va primi ca parametru ID-ul cardului si inainte de a efectua plata va incerca
determinarea contului pe baza id-ului cardului
• Daca cardul nu poate fi gasit, va arunca o exceptie
• Daca contul nu poate fi gasit, va arunca o exceptie
Va avea o metoda „Connect”
• Va arunca o exceptie daca sunt mai mult de 3 conexiuni active.
• Va incrementa numarul de conexiuni active
• Va afisa un mesaj pe ecran , „Connected”
Va avea o metoda „Disconnect”
• Va decrementa numarul conexiunilor active
• Va afisa un mesaj pe ecran , „Disconnected”

         */
        public static readonly Bank _instance = new Bank();

        private List<BankAccount> listOfAccounts = new List<BankAccount>();
        private Dictionary<Guid, Guid> accountsID = new Dictionary<Guid, Guid>();
        private Dictionary<Guid, Guid> creditCardsPairedToAccounts = new Dictionary<Guid, Guid>();
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
        private bool CheckIfAccountIdExists(Guid ID)
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
        private bool CheckIfCardIdExists(Guid ID)
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
            if (CheckIfAccountIdExists(bankAccount.ID) == true)
            {
                throw new Exception("Account already existent in bank's database.");
            }
            this.accountsID.Add(bankAccount.ID, bankAccount.ID);
            this.listOfAccounts.Add(bankAccount);
        }
        //}
        /// <summary>
        /// Creates bank account.
        /// </summary>
        /// <returns></returns>
        public Guid CreateAccount()
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
            if (CheckIfAccountIdExists(bankAccount.ID) != true)
            {
                throw new Exception("Account doesn't exist in bank's database.");
            }
            if (GetNumberOfCreditCardsFromBank(bankAccount) >= 2)
            {
                throw new Exception("Account has allready 2 active credit cards.");
            }

            Card card = new Card(Guid.NewGuid(), bankAccount);
            bankAccount.IncreaseNumberOfCreditCards();
            this.creditCardsPairedToAccounts.Add(card.ID, card.bankAccount.ID);

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
            if (activeConnetions >= 3)
            {
                throw new Exception("Maximum connection limit is reached. Try later...");
            }
            activeConnetions++;
            Console.WriteLine("Connected");
            return true;
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
            bankAccount.Money -= ammount;
            Console.WriteLine("Pay done.");
        }
        /// <summary>
        /// Payment using bak account.
        /// </summary>
        /// <param name="ammount"></param>
        /// <param name="bankAccount"></param>
        public void Pay(int ammount, BankAccount bankAccount)
        {
            if (CheckIfAccountIdExists(bankAccount.ID) != true)
            {
                throw new Exception("Account doesn't exist in bank's database.");
            }
            if (bankAccount.Money < ammount)
            {
                throw new Exception("The payment ammount exceeds the available sold.");
            }
            bankAccount.Money -= ammount;
            Console.WriteLine("Pay done.");
        }
    }
}
