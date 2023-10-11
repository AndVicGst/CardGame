using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Cards
    {
        //номинал и масть карты
        public int cardValue { get; set; }
        public int cardSuit { get; set; }
        public Cards(int cardvalue, int cardsuit)
        {
            this.cardValue = cardvalue;
            this.cardSuit = cardsuit;
        }
        //вывод на экран - перегрузка ToString
        public override string ToString()
        {
            switch (cardValue)
            {
                case 0: return $"Шестерка {(char)cardSuit}"; break;
                case 1: return $"Семерка {(char)cardSuit}"; break;
                case 2: return $"Восьмерка {(char)cardSuit}"; break;
                case 3: return $"Девятка {(char)cardSuit}"; break;
                case 4: return $"Десятка {(char)cardSuit}"; break;
                case 5: return $"Валет {(char)cardSuit}"; break;
                case 6: return $"Дама {(char)cardSuit}"; break;
                case 7: return $"Король {(char)cardSuit}"; break;
                case 8: return $"Туз {(char)cardSuit}"; break;
                default: return $"карта"; 
            }
        }

    }
}
