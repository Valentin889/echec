using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class King : Piece
    {
        public King(string couleur)
            : base(couleur)
        {

        }
        public override void Storagepossible(Game game)
        {
            Game jeuCopie = game;
            jeuCopie.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY - 1; i <= PositionY + 1; i++)
            {
                if (i < jeuCopie.TabCase.Length && i >= 0)
                {
                    for (int j = PositionX - 1; j <= PositionX + 1; j++)
                    {
                        if(j<jeuCopie.TabCase.Length&&j>=0)
                        {
                            if(i==PositionY&&j==PositionX)
                            {
                                
                            }
                            else
                            {
                                AddMove(jeuCopie, i, j);
                            }
                        }
                    }
                }
            }
        }

        public bool IsCheck(Game game)
        {
            foreach (Piece p in game.ListPieces)
            {
                p.Storagepossible(game);
                foreach(string s in p.Move)
                {
                    string[] tmp = s.Split('/');
                    int colonne = Convert.ToInt32(tmp[0]);
                    int ligne = Convert.ToInt32(tmp[1]);

                    if(this.PositionY==colonne&&this.PositionX==ligne)
                    {
                        return true;
                    }
                }
            }


            return false;
        }

    }
}
