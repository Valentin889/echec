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
                    int i = PositionY - 1;
                    int j = PositionX;
                    if (game.TabPiece[i][j] == null)
                    {
                        Move.Add(i.ToString() + "/" + j.ToString());
                        if (bPremierDeplacement)
                        {
                            i -= 1;
                            if (game.TabPiece[i][j] == null)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    i = PositionY - 1;
                    if (PositionX != 0)
                    {
                        j = PositionX - 1;
                        if (game.TabPiece[i][j] != null)
                        {
                            if (game.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    if (PositionX < game.TabPiece[i].Length-1)
                    {
                        j += 2;
                        if (game.TabPiece[i][j] != null)
                        {
                            if (game.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
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
                    int i = PositionY + 1;
                    int j = PositionX;
                    if (game.TabPiece[i][j] == null)
                    {
                        Move.Add(i.ToString() + "/" + j.ToString());
                        if (bPremierDeplacement)
                        {
                            i += 1;
                            if (game.TabPiece[i][j] == null)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    i = PositionY + 1;
                    if (PositionX != 0)
                    {
                        j = PositionX - 1;
                        if (game.TabPiece[i][j] != null)
                        {
                            if (game.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    if (PositionX < game.TabPiece[i].Length-1)
                    {
                        j += 2;
                        if (game.TabPiece[i][j] != null)
                        {
                            if (game.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}
