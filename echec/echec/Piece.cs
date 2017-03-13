using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public abstract class Piece
    {
        private List<String> lstMove;
        public String Color { get; private set; }
       

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public List<String> Move
        {
            get
            {
                return lstMove;
            }
            set
            {
                lstMove = value;
            }
        }
        public abstract void Storagepossible(Game jeu);
        public string Image { get;  set; }
        public Piece(string couleur)
        {
            Color = couleur;
        }
        public bool AddMove(Game jeu, int i, int j)
        {

            bool b = true;
            if (jeu.TabPiece[i][j] == null)
            {
                lstMove.Add(i.ToString() + "/" + j.ToString());
            }
            else
            {
                if (Color != jeu.TabPiece[i][j].Color)
                {
                    lstMove.Add(i.ToString() + "/" + j.ToString());
                }
                b = false;
            }
            return b;
        }
    }
}
