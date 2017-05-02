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
            game.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY + 1; i < game.TabPiece.Length; i++)
            {
                if (!AddMove(game, i, PositionX))
                {
                    i = game.TabPiece.Length;
                }
            }
            for (int i = PositionY - 1; i >= 0; i--)
            {
                if (!AddMove(game, i, PositionX))
                {
                    i = -1;
                }
            }
            for (int i = PositionX + 1; i < game.TabPiece[0].Length; i++)
            {
                if (!AddMove(game, PositionY, i))
                {
                    i = game.TabPiece[i].Length;
                }
            }
            for (int i = PositionX - 1; i >= 0; i--)
            {
                if (!AddMove(game, PositionY, i))
                {
                    i = -1;
                }
            }


            for (int i = PositionY+1, j = PositionX+1; i<game.TabPiece.Length&&j<game.TabPiece[0].Length;i++,j++)
            {
                if(!AddMove(game, i, j))
                {
                    i = game.TabPiece.Length;
                    j = game.TabPiece[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < game.TabPiece.Length && j >=0; i++, j--)
            {
                if (!AddMove(game, i, j))
                {
                    i = game.TabPiece.Length;
                    j = -1;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i >=0 && j < game.TabPiece[0].Length; i--, j++)
            {
                if (!AddMove(game, i, j))
                {
                    i = -1;
                    j = game.TabPiece[0].Length;
                }
            }

            for (int i = PositionY -1, j = PositionX - 1; i >=0 && j >= 0; i--, j--)
            {
                if (!AddMove(game, i, j))
                {
                    i = -1;
                    j = -1;
                }
            }
        }
    }
}
