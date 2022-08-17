
using System;
using System.Collections.Generic;
using System.Text;

namespace POS_Laborator13_
{
    class POS
    {
        /*
         * POS-ul
Va avea o metoda „Plateste” care:
• va primi doi parametri: suma de plata si cardul
• va chema pe rand metodele
o introdu card
o get card data
o va incerca sa efectueze plata in banca
o va extrage cardul
• Se va asigura ca extragerea cardului a fost facuta si in situatia in care plata a esuat

        Suplimentar:
        Pos-ul
Va avea o metoda privata „Connect” care
• Va incerca de 2 ori conectarea la Banca.
• In cazul in care conectarea a esuat, va arunca o exceptie corespunzatoare
Metoda Connect va fi apelata dupa introducerea cardului.
Dupa efectuarea platii, Pos-ul se va deconecta de la banca
         */
        /// <summary>
        /// Payment with POS.
        /// </summary>
        /// <param name="ammount"></param>
        /// <param name="card"></param>
        public void Pay(int ammount, Card card)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Connect()==true)
                {
                    Bank.GetBank().Pay(ammount, card.GetCardData());
                    Bank.GetBank().Disconnect();
                    break;
                }
            }
        }
        /// <summary>
        /// Connects POS to Bank.
        /// </summary>
        /// <returns></returns>
        private bool Connect()
        {
            if (Bank.GetBank().Connect() != true)
            {
                throw new Exception("Connectiona failed...");
            }
            return true;
        }
    }
}
