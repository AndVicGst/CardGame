using System;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {


            int countPlayers = 2; //количество игроков, должно без остатка делить 36 карт поровну


            Game game = new Game(countPlayers);

        }
    }
}
