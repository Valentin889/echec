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
            Specialmove = new List<string>();
        }
        public override void Storagepossible(Game game)
        {
            game.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY - 1; i <= PositionY + 1; i++)
            {
                if (i < game.TabCase.Length && i >= 0)
                {
                    for (int j = PositionX - 1; j <= PositionX + 1; j++)
                    {
                        if (j < game.TabCase.Length && j >= 0)
                        {
                            if (i == PositionY && j == PositionX)
                            {

                            }
                            else
                            {
                                AddMove(game, i, j);
                            }
                        }
                    }
                }
            }
            if (game.IsSmallrock(this.Color))
            {
                if (this.Color == game.Color1)
                {
                    Specialmove.Add("7/6");
                }
                else
                {
                    Specialmove.Add("7/1");
                }
            }
            if(game.IsBigRock(this.Color))
            {
                if (this.Color == game.Color1)
                {
                    Specialmove.Add("7/2");
                }
                else
                {
                    Specialmove.Add("7/5");
                }
            }

        }

        public bool IsCheck(Game game)
        {
            foreach (Piece p in game.ListPieces)
            {
                if (p.Color != this.Color)
                {
                    p.Storagepossible(game);
                    foreach (string s in p.Move)
                    {
                        string[] tmp = s.Split('/');
                        int colonne = Convert.ToInt32(tmp[0]);
                        int ligne = Convert.ToInt32(tmp[1]);

                        if (this.PositionY == colonne && this.PositionX == ligne)
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
        }

        public List<String> Specialmove { get; set; }
        public bool AlreadyMove { get; set; }

    }
}
