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
        List<Puppet> puppetList;
        Random rnd = new Random();

        public GameInfo()
        {
            puppetList = new List<Puppet>();
           
        }

        public void ChangePoz()
        {
            //int idx = rnd.Next(16);
            //puppetList[idx] = new Puppet(puppetList[idx].ID, rnd.Next(110, 150), puppetList[idx].Player);
            ////menList[idx] = new TestMan(menList[idx].ID, rnd.Next(11,14), menList[idx].Player);
        }

        public int ActivePlayerID
        {
            get;set;
        }

        public int Dice1
        {
            get;set;
        }

        public int Dice2
        {
            get;set;
        }

        public bool End
        {
            get;set;
        }

        public List<Puppet> PuppetList
        {
            get;set;
        }

        public string Msg
        {
            get;set;
        }

        public bool OnManHit
        {
            get;set;
        }

        public bool Reroll
        {
            get;set;
        }
    }
}
