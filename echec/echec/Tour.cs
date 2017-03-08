using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Tour : Piece
    {
        private List<String> Renvoie;
        public Tour(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            Renvoie = new List<string>();

            for (int i = PositionY + 1; i < jeu.TabPiece.Length; i++)
            {
                if(!Deplacement(jeu, i, PositionX))
                {
                    i = jeu.TabPiece.Length;
                }
            }
            for (int i = PositionY - 1; i > 0; i--)
            {
                if (!Deplacement(jeu, i, PositionX))
                {
                    i = 0;
                }
            }
            for(int i=PositionX+1;i<jeu.TabPiece[0].Length;i++)
            {
                if(!Deplacement(jeu, PositionY, i))
                {
                    i = jeu.TabPiece[i].Length;
                }
            }
            for(int i=PositionX-1;i>0;i--)
            {
                if (!Deplacement(jeu, PositionY, i))
                {
                    i = 0;
                }
            }
            return Renvoie;
        }

        private bool Deplacement(Jeu jeu, int i,int j)
        {
            bool b = true;
            if (jeu.TabPiece[i][j] == null)
            {
                Renvoie.Add(i.ToString() + "/" + j.ToString());
            }
            else
            {
                if (Couleur != jeu.TabPiece[i][j].Couleur)
                {
                    Renvoie.Add(i.ToString() + "/" + j.ToString());
                }
                b = false;
            }
            return b;
        }
    }
}
