using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public abstract class Piece
    {
        public String Couleur { get; private set; }
        public int TailleX { get; private set; }
        public int tailleY { get; private set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        

        public string Image { get;  set; }
        public Piece(string couleur)
        {
            Couleur = couleur;
        }
    }
}
