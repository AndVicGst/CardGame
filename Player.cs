using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Players
    {
        //список карт игрока
        public List<Cards> playerListCard = new List<Cards>();
        //номер игрока
        public int numberPlayer;

        public Players(int num)
        {
            numberPlayer = num;
        }

        //выводим список карт
        public void showCard()
        {
            int perenos = 0;
            foreach (var item in playerListCard)
            {
                //для вывода по 4 карты в ряд, но так не очень понятно когда карты перемещаются в конец списка 
                //if (perenos % 2 == 0) Console.WriteLine(); 
                // и WriteLine нужно заменить на Write

                if (item.cardValue == 0)
                { Console.WriteLine($"Шестерка {(char)item.cardSuit}\t"); perenos++; }
                if (item.cardValue == 1)
                { Console.WriteLine($"Семерка {(char)item.cardSuit}\t"); perenos++; }
                if (item.cardValue == 2)
                { Console.WriteLine($"Восьмерка {(char)item.cardSuit}\t"); perenos++; }
                if (item.cardValue == 3)
                { Console.WriteLine($"Девятка {(char)item.cardSuit}\t"); perenos++; }
                if (item.cardValue == 4)
                { Console.WriteLine($"Десятка {(char)item.cardSuit}\t"); perenos++; }
                if (item.cardValue == 5)
                { Console.WriteLine($"Валет {(char)item.cardSuit}\t\t"); perenos++; }
                if (item.cardValue == 6)
                { Console.WriteLine($"Дама {(char)item.cardSuit}\t\t"); perenos++; }
                if (item.cardValue == 7)
                { Console.WriteLine($"Король {(char)item.cardSuit}\t"); perenos++; }
                if (item.cardValue == 8)
                { Console.WriteLine($"Туз {(char)item.cardSuit}\t\t"); perenos++; }
            }
            //Console.WriteLine("\n");
        }

    }
}
