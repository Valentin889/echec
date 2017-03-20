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
        public override void Storagepossible(Game game)
        {
            game.Players[0].LastPiece = this;

            Move = new List<string>();
            if (this.Color == game.Color1)
            {

                if (PositionY != 6)
                {
                    bPremierDeplacement = false;
                }


                if (PositionY - 1 >= 0)
                {
                    AddMove(game, PositionY - 1, PositionX);
                    if (bPremierDeplacement)
                    {
                        if (PositionY - 2 >= 0)
                        {
                            AddMove(game, PositionY - 2, PositionX);
                        }
                    }
                }
            }
            else
            {
                if (PositionY != 1)
                {
                    bPremierDeplacement = false;
                }


                if (PositionY + 1 <= game.TabPiece.Length)
                {
                    AddMove(game, PositionY + 1, PositionX);
                    if (bPremierDeplacement)
                    {
                        if (PositionY + 2 <= game.TabPiece.Length)
                        {
                            AddMove(game, PositionY + 2, PositionX);
                        }
                    }
                }
            }
        }
    }
}
