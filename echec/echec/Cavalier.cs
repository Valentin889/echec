using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Cavalier : Piece
    {
    
        public Cavalier(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            int i;
            int j;
            List<String> renvoie = new List<string>();
            Renvoie = renvoie;

            i = PositionY + 2;
            j = PositionX + 1;
            if(i<jeu.TabPiece.Length&&j<jeu.TabPiece[0].Length)
            {
                Deplacement(jeu, i, j);
            }

            j = PositionX - 1;
            if(i<jeu.TabPiece.Length&&j>0)
            {
                Deplacement(jeu, i, j);
            }


            i = PositionY + 1;
            j = PositionX + 2;
            if (i < jeu.TabPiece.Length && j < jeu.TabPiece[0].Length)
            {
                Deplacement(jeu, i, j);
            }

            j = PositionX - 2;
            if (i < jeu.TabPiece.Length && j > 0)
            {
                Deplacement(jeu, i, j);
            }

            i = PositionY - 1;
            j = PositionX + 2;
            if (i > 0&&j<jeu.TabPiece[0].Length)
            {
                Deplacement(jeu, i, j);
            }

            j = PositionX - 2;
            if(i>0&&j>0)
            {
                Deplacement(jeu, i, j);
            }

            i = PositionY - 2;
            j = PositionX + 2;

            if(i>0&&j<jeu.TabPiece[0].Length)
            {
                Deplacement(jeu, i, j);
            }

            j = PositionX - 2;
            if (i > 0&&j>0)
            {
                Deplacement(jeu, i, j);
            }
            return Renvoie;
            
               
        }
    }
}
