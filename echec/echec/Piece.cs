using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public abstract class Piece
    {
        private List<String> renvoie;
        public String Couleur { get; private set; }
        public int TailleX { get; private set; }
        public int tailleY { get; private set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public List<String> Renvoie
        {
            get
            {
                return renvoie;
            }
            set
            {
                renvoie = value;
            }
        }


        public abstract List<string> DeplacementPossible(Jeu jeu);
        public string Image { get;  set; }
        public Piece(string couleur)
        {
            Couleur = couleur;
        }

        public bool Deplacement(Jeu jeu, int i, int j)
        {
            bool b = true;
            if (jeu.TabPiece[i][j] == null)
            {
                renvoie.Add(i.ToString() + "/" + j.ToString());
            }
            else
            {
                if (Couleur != jeu.TabPiece[i][j].Couleur)
                {
                    renvoie.Add(i.ToString() + "/" + j.ToString());
                }
                b = false;
            }
            return b;
        }
    }
}
