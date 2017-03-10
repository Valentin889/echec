using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Tour : Piece
    {
        public Tour(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            List<String> renvoie = new List<string>();

            Renvoie = renvoie; 

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
    }
}
