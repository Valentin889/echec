using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Pawn : Piece
    {
        bool bPremierDeplacement;
        public Pawn(string couleur)
            : base(couleur)
        {
            bPremierDeplacement = true;
        }

        public bool PremierDeplacement
        {
            get
            {
                return bPremierDeplacement;
            }
            set
            {
                bPremierDeplacement = value;
            }
        }
        public override void Storagepossible(Game jeu)
        {
            Game jeuCopie = jeu;
            jeuCopie.Players[0].LastPiece = this;

            if (PositionY!=6)
            {
                bPremierDeplacement = false;
            }
            Move = new List<string>();

            if (PositionY - 1 >=0)
            {
                AddMove(jeuCopie, PositionY - 1, PositionX);
                if (bPremierDeplacement)
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
