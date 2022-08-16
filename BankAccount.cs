
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_Laborator13_
{
    public class BankAccount
    {
        /*
         * Contul bancar
Va avea un ID de tip Guid
Va avea o metoda DepuneNumerar
Va avea o metoda ExtrageNumerar
• Va avea ca parametru suma ce se doreste a fi extrasa
• In cazul in care suma nu este disponibila, contul bancar va arunca o exceptie
• Va fi folosita la de Banca atunci cand vor fi efectuate plati
        Suplimentar:
        Contul
Va persista numarul de carduri emise.

         */


        public Guid ID { get; set; }
        public int NumberOActivefCreditCards { get; private set; }
        public double Money { get; set; }
        /// <summary>
        /// Creates bank account.
        /// </summary>
        public BankAccount()
        {
            this.ID = Guid.NewGuid();
            this.Money = 0;
        }
        /// <summary>
        /// Increases number of active credit cards paired to one bak account.
        /// </summary>
        public void IncreaseNumberOfCreditCards()
        {
            this.NumberOActivefCreditCards++;
        }
        /// <summary>
        /// Retruns the number oc active credit cards from account.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        public int GetNumberOfCreditCardsFromAccount(BankAccount bankAccount)
        {
            return bankAccount.NumberOActivefCreditCards;
        }
        /// <summary>
        /// Makes cash deposit.
        /// </summary>
        /// <param name="depositAmmount"></param>
        public void CashDeposit(int depositAmmount)
        {
            this.Money += depositAmmount;
        }
        /// <summary>
        /// Makes cash withdrawal.
        /// </summary>
        /// <param name="withdrawalAmmount"></param>
        /// <returns></returns>
        public int CashWithdrawal(int withdrawalAmmount)
        {
            try
            {
                if (withdrawalAmmount > this.Money)
                {
                    throw new Exception("Insufficient funds");
                }
                else
                {
                    this.Money -= withdrawalAmmount;

                    return withdrawalAmmount;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }
    }
}
