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
            game.Players[0].LastPiece = this;

            Move = new List<string>();
            if (this.Color == game.Color1)
            {
                if (PositionY - 1 >= 0)
                {
                    int i = PositionY - 1;
                    int j = PositionX;
                    if (game.TabPiece[i][j] == null)
                    {
                        Move.Add(i.ToString() + "/" + j.ToString());
                        if (!IsAlreadyMove)
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
                if (PositionY + 1 <= game.TabPiece.Length)
                {
                    int i = PositionY + 1;
                    int j = PositionX;
                    if (game.TabPiece[i][j] == null)
                    {
                        Move.Add(i.ToString() + "/" + j.ToString());
                        if (!IsAlreadyMove)
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

        public bool IsPassingRight { get; set; }
        public bool IsPassginLeft { get; set; }

        public override void SetPassingLeft(bool bValue) { IsPassginLeft = bValue; }
        public override void SetPassingRight(bool bValue) { IsPassingRight = bValue; }


    }
}
