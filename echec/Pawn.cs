using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Pawn : Piece
    {
        public Pawn(string Color)
            : base(Color)
        {
            IsPassingRight = false;
            IsPassginLeft = false;
        }
        public override void SetPossibleMoves(Game game)
        {
            Game copyGame = game.Clone();
            copyGame.Players[0].LastPiece = this;

            Move = new List<string>();
            if (this.Color == copyGame.Color1)
            {
                if (PositionY - 1 >= 0)
                {
                    int i = PositionY - 1;
                    int j = PositionX;
                    if (copyGame.TabPiece[i][j] == null)
                    {
                        Move.Add(i.ToString() + "/" + j.ToString());
                        if (!IsAlreadyMove)
                        {
                            i -= 1;
                            if (copyGame.TabPiece[i][j] == null)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    i = PositionY - 1;
                    if (PositionX != 0)
                    {
                        j = PositionX - 1;
                        if (copyGame.TabPiece[i][j] != null)
                        {
                            if (copyGame.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    if (PositionX < copyGame.TabPiece[i].Length-1)
                    {
                        j += 2;
                        if (copyGame.TabPiece[i][j] != null)
                        {
                            if (copyGame.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }

                    if(IsPassingRight)
                    {
                        j = PositionX + 1;
                        i = PositionY - 1;
                        Move.Add(i.ToString() + "/" + j.ToString());
                    }
                    if(IsPassginLeft)
                    {
                        j = PositionX - 1;
                        i = PositionY - 1;
                        Move.Add(i.ToString() + "/" + j.ToString());
                    }

                }
            }
            else
            {
                if (PositionY + 1 <= copyGame.TabPiece.Length)
                {
                    int i = PositionY + 1;
                    int j = PositionX;
                    if (copyGame.TabPiece[i][j] == null)
                    {
                        Move.Add(i.ToString() + "/" + j.ToString());
                        if (!IsAlreadyMove)
                        {
                            i += 1;
                            if (copyGame.TabPiece[i][j] == null)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    i = PositionY + 1;
                    if (PositionX != 0)
                    {
                        j = PositionX - 1;
                        if (copyGame.TabPiece[i][j] != null)
                        {
                            if (copyGame.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }
                    if (PositionX < copyGame.TabPiece[i].Length-1)
                    {
                        j += 1;
                        if (copyGame.TabPiece[i][j] != null)
                        {
                            if (copyGame.TabPiece[i][j].Color != this.Color)
                            {
                                Move.Add(i.ToString() + "/" + j.ToString());
                            }
                        }
                    }


                    if (IsPassingRight)
                    {
                        j = PositionX + 1;
                        i = PositionY + 1;
                        Move.Add(i.ToString() + "/" + j.ToString());
                    }
                    if (IsPassginLeft)
                    {
                        j = PositionX - 1;
                        i = PositionY + 1;
                        Move.Add(i.ToString() + "/" + j.ToString());
                    }
                }
            }
        }

        public bool IsPassingRight { get;  private set; }
        public bool IsPassginLeft { get; private set; }

        public override void SetPassingLeft(bool bValue) { IsPassginLeft = bValue; }
        public override void SetPassingRight(bool bValue) { IsPassingRight = bValue; }


    }
}
