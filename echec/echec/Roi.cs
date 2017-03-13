using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Roi : Piece
    {
        public Roi(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            List<String> renvoie = new List<string>();
            Renvoie = renvoie;

            for (int i = PositionY - 1; i <= PositionY + 1; i++)
            {
                if (i < jeu.TabPiece.Length && i >= 0)
                {
                    for (int j = PositionX - 1; j <= PositionX + 1; j++)
                    {
                        if(j<jeu.TabPiece.Length&&j>=0)
                        {
                            if(i==PositionY&&j==PositionX)
                            {
                                
                            }
                            else
                            {
                                Deplacement(jeu, i, j);
                            }
                        }
                    }
                }
            }
            return Renvoie;
        }
    }
}
