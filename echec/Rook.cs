using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Rook : Piece
    {
        public Rook(string couleur)
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
                if(!AddMove(copyGame, i, PositionX))
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
            for(int i=PositionX+1;i<copyGame.TabPiece[0].Length;i++)
            {
                if(!AddMove(copyGame, PositionY, i))
                {
                    i = copyGame.TabPiece[i].Length;
                }
            }
            for(int i=PositionX-1;i>=0;i--)
            {
                if (!AddMove(copyGame, PositionY, i))
                {
                    i = -1;
                }
            }
        }
    }
}
