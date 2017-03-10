using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Pion : Piece
    {
        bool PremierDeplacement;
        public Pion(string couleur)
            : base(couleur)
        {
            PremierDeplacement = true;
        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            List<String> renvoie = new List<string>();
            Renvoie = renvoie;

            if (PositionY + 1 > jeu.TabPiece.Length)
            {
                Deplacement(jeu, PositionY + 1, PositionX);
                if (PremierDeplacement)
                {
                    if (PositionY + 2 > jeu.TabPiece.Length)
                    {
                        Deplacement(jeu, PositionY + 2, PositionX);
                    }
                }
            }
            return Renvoie;
        }
    }
}
