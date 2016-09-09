using SharedLudoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLudoLibrary.ClientClasses
{
    public class GameInfo : IGameInfo
    {
        List<IPuppet> puppetList;
        Random rnd = new Random();

        public GameInfo()
        {
            puppetList = new List<IPuppet>();
            puppetList.Add(new Puppet(11, 11, new Player(0, PlayerColor.RED)));
            puppetList.Add(new Puppet(12, 12, new Player(0, PlayerColor.RED)));
            puppetList.Add(new Puppet(12, 13, new Player(0, PlayerColor.RED)));
            puppetList.Add(new Puppet(13, 14, new Player(0, PlayerColor.RED)));

            puppetList.Add(new Puppet(21, 21, new Player(1, PlayerColor.BLUE)));
            puppetList.Add(new Puppet(22, 22, new Player(1, PlayerColor.BLUE)));
            puppetList.Add(new Puppet(23, 23, new Player(1, PlayerColor.BLUE)));
            puppetList.Add(new Puppet(24, 24, new Player(1, PlayerColor.BLUE)));

            puppetList.Add(new Puppet(31, 31, new Player(2, PlayerColor.YELLOW)));
            puppetList.Add(new Puppet(32, 32, new Player(2, PlayerColor.YELLOW)));
            puppetList.Add(new Puppet(33, 33, new Player(2, PlayerColor.YELLOW)));
            puppetList.Add(new Puppet(34, 34, new Player(2, PlayerColor.YELLOW)));

            puppetList.Add(new Puppet(41, 41, new Player(3, PlayerColor.GREEN)));
            puppetList.Add(new Puppet(42, 42, new Player(3, PlayerColor.GREEN)));
            puppetList.Add(new Puppet(43, 43, new Player(3, PlayerColor.GREEN)));
            puppetList.Add(new Puppet(44, 44, new Player(3, PlayerColor.GREEN)));
        }

        public void ChangePoz()
        {
            int idx = rnd.Next(16);
            puppetList[idx] = new Puppet(puppetList[idx].ID, rnd.Next(110, 150), puppetList[idx].Player);
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
