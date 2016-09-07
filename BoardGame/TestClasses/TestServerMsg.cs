using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class TestServerMsg : IGameInfo
    {
        Random rnd = new Random();
        List<IMan> menList;
        public TestServerMsg()
        {
            menList = new List<IMan>();
            menList.Add(new TestMan(11, 11, new TestPlayer(0, PlayerColor.RED)));
            menList.Add(new TestMan(12, 12, new TestPlayer(0, PlayerColor.RED)));
            menList.Add(new TestMan(12, 13, new TestPlayer(0, PlayerColor.RED)));
            menList.Add(new TestMan(13, 14, new TestPlayer(0, PlayerColor.RED)));

            menList.Add(new TestMan(21, 21, new TestPlayer(1, PlayerColor.BLUE)));
            menList.Add(new TestMan(22, 22, new TestPlayer(1, PlayerColor.BLUE)));
            menList.Add(new TestMan(23, 23, new TestPlayer(1, PlayerColor.BLUE)));
            menList.Add(new TestMan(24, 24, new TestPlayer(1, PlayerColor.BLUE)));

            menList.Add(new TestMan(31, 31, new TestPlayer(1, PlayerColor.YELLOW)));
            menList.Add(new TestMan(32, 32, new TestPlayer(1, PlayerColor.YELLOW)));
            menList.Add(new TestMan(33, 33, new TestPlayer(1, PlayerColor.YELLOW)));
            menList.Add(new TestMan(34, 34, new TestPlayer(1, PlayerColor.YELLOW)));

            menList.Add(new TestMan(41, 41, new TestPlayer(1, PlayerColor.GREEN)));
            menList.Add(new TestMan(42, 42, new TestPlayer(1, PlayerColor.GREEN)));
            menList.Add(new TestMan(43, 43, new TestPlayer(1, PlayerColor.GREEN)));
            menList.Add(new TestMan(44, 44, new TestPlayer(1, PlayerColor.GREEN)));
        }
        public void ChangePoz()
        {
            int idx = rnd.Next(16);
            menList[idx] = new TestMan(menList[idx].ID, rnd.Next(110, 150), menList[idx].Player);
            //menList[idx] = new TestMan(menList[idx].ID, rnd.Next(11,14), menList[idx].Player);
        }

        public int ActivePlayerID
        {
            get
            {
                if (rnd.Next(10) < 3)
                {
                    return 0;
                }
                else if (rnd.Next(10) < 5)
                {
                    return 1;
                }
                else if (rnd.Next(10) < 7)
                {
                    return 2;
                }
                return 3;
            }
        }

        public int Dice1
        {
            get
            {
                return rnd.Next(1, 7);
            }
        }

        public int Dice2
        {
            get
            {
                return rnd.Next(1, 7);
            }
        }

        public bool End
        {
            get
            {
                return false;
            }
        }

        public List<IMan> MenList
        {
            get
            {
                return menList;
            }
        }

        public string Msg
        {
            get
            {
                return "server msg";
            }
        }

        public bool OnManHit
        {
            get
            {
                return false; ////////////TODO
            }
        }

        public bool Reroll
        {
            get
            {
                return (Dice1 == 6 || Dice2 == 6) ? true : false;
            }
        }
    }
}
