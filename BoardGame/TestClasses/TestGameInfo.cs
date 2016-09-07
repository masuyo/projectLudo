using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class TestGameInfo : IGameInfo
    {
        List<IPuppet> puppetList;
        Random rnd = new Random();

        public TestGameInfo()
        {
            puppetList = new List<IPuppet>();
            puppetList.Add(new TestPuppet(11, 11, new TestPlayer(0, PlayerColor.RED)));
            puppetList.Add(new TestPuppet(12, 12, new TestPlayer(0, PlayerColor.RED)));
            puppetList.Add(new TestPuppet(12, 13, new TestPlayer(0, PlayerColor.RED)));
            puppetList.Add(new TestPuppet(13, 14, new TestPlayer(0, PlayerColor.RED)));

            puppetList.Add(new TestPuppet(21, 21, new TestPlayer(1, PlayerColor.BLUE)));
            puppetList.Add(new TestPuppet(22, 22, new TestPlayer(1, PlayerColor.BLUE)));
            puppetList.Add(new TestPuppet(23, 23, new TestPlayer(1, PlayerColor.BLUE)));
            puppetList.Add(new TestPuppet(24, 24, new TestPlayer(1, PlayerColor.BLUE)));

            puppetList.Add(new TestPuppet(31, 31, new TestPlayer(2, PlayerColor.YELLOW)));
            puppetList.Add(new TestPuppet(32, 32, new TestPlayer(2, PlayerColor.YELLOW)));
            puppetList.Add(new TestPuppet(33, 33, new TestPlayer(2, PlayerColor.YELLOW)));
            puppetList.Add(new TestPuppet(34, 34, new TestPlayer(2, PlayerColor.YELLOW)));

            puppetList.Add(new TestPuppet(41, 41, new TestPlayer(3, PlayerColor.GREEN)));
            puppetList.Add(new TestPuppet(42, 42, new TestPlayer(3, PlayerColor.GREEN)));
            puppetList.Add(new TestPuppet(43, 43, new TestPlayer(3, PlayerColor.GREEN)));
            puppetList.Add(new TestPuppet(44, 44, new TestPlayer(3, PlayerColor.GREEN)));            
        }

        public void ChangePoz()
        {
            int idx = rnd.Next(16);
            puppetList[idx] = new TestPuppet(puppetList[idx].ID, rnd.Next(110, 150), puppetList[idx].Player);
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

        public List<IPuppet> PuppetList
        {
            get
            {
                return puppetList;
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
                return false; //TODO
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
