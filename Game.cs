using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Game
    {
        //номинал карты 
        public enum cardV { Six, Seven, Eight, Nine, Ten, Valet, Dama, King, Tus }

        //масть карты 
        public enum cardS { Hearts = 3, Bubni = 4, Trefi = 5, Piki = 6 }

        //лист обьектов класса Cards 
        public List<Cards>? listCards;

        //заполняем List картами - 36 шт
        public void SetListCards()
        {
            listCards = new List<Cards>();
            Random rnd = new Random();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 3; j < 7; j++) //из таблицы ANSII 3, 4, 5, 6 - масти карт в виде символов
                {
                    listCards.Add(new Cards(i, j));
                }
            }
            //перемешиваем карты в списке
            for (int i = listCards.Count - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                Cards temp = listCards[j];
                listCards[j] = listCards[i];
                listCards[i] = temp;
            }
        }

        //лист обьектов класса Players
        public List<Players>? listPlayers; //лист игроков

        //создаем лист игроков и для каждого свой лист карт 
        public void listCardToPlayer(List<Cards>? _listcards, int countPlayers)
        {
            int cardCount = _listcards.Count; //количество карт - 36 шт
            int cardToPlayer = cardCount / countPlayers; //сколько карт будет у игрока - 36/2 = 18
            List<Cards> listCardToPlayer = new List<Cards>(); //лист карт для каждого игрока - 
            listPlayers = new List<Players>(); //лист игроков         
            for (int i = 0; i < countPlayers; i++)
            {
                listPlayers.Add(new Players()); //создаем лист обьектов класса Player
                for (int j = cardCount - 1; j >= cardCount - cardToPlayer; j--)
                {
                    listPlayers[i].playerListCard.Add(listCards[j]); //собираем карты для игрока - в данном случае 18 шт             
                }
                cardCount -= cardToPlayer;
                listCardToPlayer.Clear(); //очищаем лист для записи карт следующего игрока
            }

        }

        //игра
        public void playGame()
        { 
            bool endGame = false;
            while (!endGame)
            {
                Console.Clear();
                Console.WriteLine($"Карточная игра на {listPlayers.Count} игроков\n");
                //выводим на экран карты игроков
                for (int i = 0; i < listPlayers.Count; i++)
                {
                    Console.WriteLine($"Карты игрока {i + 1}:\n");
                    listPlayers[i].showCard();
                    Console.WriteLine();
                }
                //для сравнения карт используем SortedList
                //куда в качестве ключа заносим номинал карты и он будет отсортирован
                //в качестве значения - индекс игрока в listPlayer - потом по этому индексу карты или добавяться, или нет
                int k = 1; //номера игроков
                Dictionary<int, List<Cards>> dictListcards = new Dictionary<int, List<Cards>>();

                //создаем список карт игроков на карточном столе - используется для сравнения карт - ключ номинал карты, значение - обьект игрока в listPlayer
                for (int i = 0; i < listPlayers.Count - 1; i++)
                {
                    Console.WriteLine($"Игрок {k++} положил карту на стол: {listPlayers[i].playerListCard[0].ToString()}");
                    dictListcards.Add(listPlayers[i].playerListCard[0].cardValue, listPlayers[i].playerListCard);
                    listPlayers[i].playerListCard.RemoveAt(0); //удаляем карту из List карт игрока, потом или добавятся (в результате сравнения), или нет
                }
                //если 1 и 2 данные по ключу в SortedList не равны, то все карты идут к игроку с индексом (значением в SortedList) соответсвующим первому ключу


                if (dictListcards.Keys[0] != dictListcards.Keys[1]) //сравниваем 1 и 2 ключ
                {
                    for (int i = 0; i < dictListcards.Count - 1; i++)
                        listPlayers[dictListcards.Keys[0]].playerListCard.Add(dictListcards.Values[i]);
                }


                    //по одной первой в List карте каждого игрока 
                    
                for (int i = 0; i < listPlayers.Count - 1; i++)
                {
                    Console.WriteLine($"Игрок {k++} положил карту: {listPlayers[i].playerListCard[0].ToString()}");

                    //перекладываем карты 
                    //if (listPlayers[i].playerListCard[0].cardValue > listPlayers[i + 1].playerListCard[0].cardValue)
                    //{
                    //    listPlayers[i].playerListCard.Add(listPlayers[i + 1].playerListCard[0]);
                    //    listPlayers[i].playerListCard.Add(listPlayers[i].playerListCard[0]);
                    //    listPlayers[i + 1].playerListCard.RemoveAt(0);
                    //    listPlayers[i].playerListCard.RemoveAt(0);
                    //}
                    //else
                    //{
                    //    listPlayers[i + 1].playerListCard.Add(listPlayers[i].playerListCard[0]);
                    //    listPlayers[i + 1].playerListCard.Add(listPlayers[i+1].playerListCard[0]);
                    //    listPlayers[i + 1].playerListCard.RemoveAt(0);
                    //    listPlayers[i].playerListCard.RemoveAt(0);
                    //}
                    Console.WriteLine();
                    Console.Write("Нажмите любую клавишу для продолжения.\n\n");
                    Console.ReadKey(); 
                    if (listPlayers[i].playerListCard.Count == 0)
                    {
                        endGame = true;
                    }
                }
            }
            int maxCardCountPlayer = 0; //наибольшее количество оставшихся у игроков карт - тот и победил
            int numberPlayer = 0;       //индекс победителя
            for (int i = 0; i < listPlayers.Count; i++)
            {
                if (maxCardCountPlayer < listPlayers[i].playerListCard.Count)
                {
                    maxCardCountPlayer = listPlayers[i].playerListCard.Count;
                    numberPlayer = i+1;
                }
            }
            Console.WriteLine($"Победил игрок: {numberPlayer}\n");
        }

        public Game(int countPlayers)
        {
            SetListCards();
            listCardToPlayer(listCards, countPlayers);
            playGame();
        }
    }
}
