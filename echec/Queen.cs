using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Queen : Piece
    {
        public Queen(string couleur)
            : base(couleur)
        {

        }
        public override void SetPossibleMoves(Game game)
        {
            Game copyGame = game.Clone();
            copyGame.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY + 1; i < copyGame.TabPiece.Length; i++)
            {
                if (!AddMove(copyGame, i, PositionX))
                {
                    i = copyGame.TabPiece.Length;
                }
            }
            for (int i = PositionY - 1; i >= 0; i--)
            {
                if (!AddMove(copyGame, i, PositionX))
                {
                    i = -1;
                }
            }
            for (int i = PositionX + 1; i < copyGame.TabPiece[0].Length; i++)
            {
                if (!AddMove(copyGame, PositionY, i))
                {
                    i = copyGame.TabPiece[i].Length;
                }
            }
            for (int i = PositionX - 1; i >= 0; i--)
            {
                if (!AddMove(copyGame, PositionY, i))
                {
                    i = -1;
                }
            }


            for (int i = PositionY+1, j = PositionX+1; i<copyGame.TabPiece.Length&&j<copyGame.TabPiece[0].Length;i++,j++)
            {
                if(!AddMove(copyGame, i, j))
                {
                    i = copyGame.TabPiece.Length;
                    j = copyGame.TabPiece[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < copyGame.TabPiece.Length && j >=0; i++, j--)
            {
                if (!AddMove(copyGame, i, j))
                {
                    i = copyGame.TabPiece.Length;
                    j = -1;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i >=0 && j < copyGame.TabPiece[0].Length; i--, j++)
            {
                if (!AddMove(copyGame, i, j))
                {
                    i = -1;
                    j = copyGame.TabPiece[0].Length;
                }
            }

            for (int i = PositionY -1, j = PositionX - 1; i >=0 && j >= 0; i--, j--)
            {
                if (!AddMove(copyGame, i, j))
                {
                    i = -1;
                    j = -1;
                }
            }
        }
    }
}
