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
        public override void SetPossibleMoves(Game game)
        {
            game.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY - 1; i <= PositionY + 1; i++)
            {
                if (i < game.TabPiece.Length && i >= 0)
                {
                    for (int j = PositionX - 1; j <= PositionX + 1; j++)
                    {
                        if (j < game.TabPiece.Length && j >= 0)
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
        }

        public void AddRock(Game game)
        {
            Game copyGame = game.Clone();
            if (copyGame.IsSmallRock(this.Color))
            {
                if (this.Color == copyGame.Color1)
                {
                    Specialmove.Add("7/6");
                }
                else
                {
                    Specialmove.Add("0/6");
                }
            }
            if (copyGame.IsBigRock(this.Color))
            {
                if (this.Color == copyGame.Color1)
                {
                    Specialmove.Add("7/2");
                }
                else
                {
                    Specialmove.Add("0/2");
                }
            }
        }

        public bool IsCheck(Game game)
        {
            if(this.Color==game.Color2)
            {
                foreach(string s in game.DicWhitePiece.Keys)
                {
                    Piece p = game.DicWhitePiece[s];
                    if (CalledByIsCheck(p, game))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (string s in game.DicBlackPiece.Keys)
                {
                    Piece p = game.DicBlackPiece[s];
                    if (CalledByIsCheck(p, game))
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }
        private bool CalledByIsCheck(Piece p,Game game)
        {
            p.SetPossibleMoves(game);
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
            return false;
        }

        public List<String> Specialmove { get; set; }
    }
}
