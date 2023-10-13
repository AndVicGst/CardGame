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
        public void listCardToPlayer(int countPlayers)
        {
            int cardCount = listCards.Count; //количество карт - 36 шт
            int cardToPlayer = cardCount / countPlayers; //сколько карт будет у игрока - 36/2 = 18
            List<Cards> listCardToPlayer = new List<Cards>(); //лист карт для каждого игрока - 
            listPlayers = new List<Players>(); //лист игроков         
            for (int i = 0; i < countPlayers; i++)
            {
                listPlayers.Add(new Players(i+1)); //создаем лист обьектов класса Player
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
                    Console.WriteLine($"Карты игрока {listPlayers[i].numberPlayer} всего {listPlayers[i].playerListCard.Count} шт.:\n");
                    listPlayers[i].showCard();
                    Console.WriteLine();
                }
                int maxCardValue = 0; //будет хранить наибольший номинал карты на столе
                int indexPlayerMaxCard = 0; //будет хранить номер игрока с наибольшей картой
                //создаем список карт игроков на карточном столе 
                for (int i = 0; i < listPlayers.Count; i++)
                {
                    Console.WriteLine($"Игрок {listPlayers[i].numberPlayer} положил карту на стол: {listPlayers[i].playerListCard[0].ToString()}");
                    //ищем наибольшую карту
                    if (maxCardValue < listPlayers[i].playerListCard[0].cardValue)
                    {
                        maxCardValue = listPlayers[i].playerListCard[0].cardValue;
                        indexPlayerMaxCard = listPlayers[i].numberPlayer-1;
                    }
                }

                //добавили в конец списка карт игроку с наибольшей картой, карты остальных игроков и его первую
                for (int i = 0; i < listPlayers.Count; i++)
                {
                    if (indexPlayerMaxCard != i)
                    {
                        listPlayers[indexPlayerMaxCard].playerListCard.Add(listPlayers[i].playerListCard[0]);
                    }                    
                }
                //добавили в конец списка карт игроку с наибольшей картой его первую по списку карту
                listPlayers[indexPlayerMaxCard].playerListCard.Add(listPlayers[indexPlayerMaxCard].playerListCard[0]);

                //удаляем первую карту из List карт игроков
                for (int i = 0; i < listPlayers.Count; i++)
                       listPlayers[i].playerListCard.RemoveAt(0);


                Console.WriteLine();
                Console.Write("Нажмите любую клавишу для продолжения.\n\n");
                Console.ReadKey();

                for (int i = 0; i < listPlayers.Count; i++)
                {
                    if (listPlayers[i].playerListCard.Count == 0) endGame = true; ; //конец игре если у любого игрока закончились карты                                       
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
            listCardToPlayer(countPlayers);
            playGame();
        }
    }
}
