
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_Laborator13_
{
    public class BankAccount
    {
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
