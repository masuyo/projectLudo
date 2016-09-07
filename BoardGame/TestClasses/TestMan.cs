using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.TestClasses
{
    class TestMan : IMan
    {
        private int id;
        private int poz;
        private TestPlayer player;
        public TestMan(int id, int poz, TestPlayer player)
        {
            this.id = id;
            this.poz = poz;
            this.player = player;
        }

        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public TestPlayer Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
        public int Poz
        {
            get
            {
                return poz;
            }

            set
            {
                poz = value;
            }
        }
    }
}
