using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public abstract class Player
    {
        public abstract void Jouer();

        public Player(string couleur)
        {
            Color = couleur;
        }


       public Piece DernierePiece { get; set; }
       public int[] DernierPosition { get; set; }

        public string Color { get; private set; }

    }
}
