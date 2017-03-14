using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Pawn : Piece
    {
        bool PremierDeplacement;
        public Pawn(string couleur)
            : base(couleur)
        {
            PremierDeplacement = true;
        }
        public override void Storagepossible(Game jeu)
        {
            Game jeuCopie = jeu;
            jeuCopie.Players[0].LastPiece = this;

            if (PositionY!=6)
            {
                PremierDeplacement = false;
            }
            Move = new List<string>();

            if (PositionY - 1 >=0)
            {
                AddMove(jeuCopie, PositionY - 1, PositionX);
                if (PremierDeplacement)
                {
                    if (PositionY - 2 >= 0)
                    {
                        AddMove(jeuCopie, PositionY - 2, PositionX);
                    }
                }
            }
        }
    }
}
