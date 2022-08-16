
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_Laborator13_
{
    public class Card
    {
        public BankAccount bankAccount;
        public Guid ID { get; set; }
        /// <summary>
        /// Creates Credit card.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="bankAccount"></param>
        public Card(Guid ID, BankAccount bankAccount)
        {
            this.ID = ID;
            this.bankAccount = bankAccount;
        }
        public void InsertCard()
        {
            Console.WriteLine("Card inserted.");
        }
        /// <summary>
        /// Returns card's ID.
        /// </summary>
        /// <returns></returns>
        public Guid GetCardData()
        {
            return this.ID;
        }
        public void ExtractCard()
        {
            Console.WriteLine("Card extracted.");
        }
    }
}
