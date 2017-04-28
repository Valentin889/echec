using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Knights : Piece
    {
    
        public Knights(string couleur)
            : base(couleur)
        {
           
        }
        public override void SetPossibleMoves(Game game)
        {
            Game copyGame = game.Clone();
            copyGame.Players[0].LastPiece = this;
            int i;
            int j;
            Move = new List<string>();

            i = PositionY + 2;
            j = PositionX + 1;
            if(i<copyGame.TabPiece.Length&&j<copyGame.TabPiece[0].Length)
            {
                AddMove(copyGame, i, j);
            }

            j = PositionX - 1;
            if(i<copyGame.TabPiece.Length&&j>=0)
            {
                AddMove(copyGame, i, j);
            }


            i = PositionY + 1;
            j = PositionX + 2;
            if (i < copyGame.TabPiece.Length && j < copyGame.TabPiece[0].Length)
            {
                AddMove(copyGame, i, j);
            }

            j = PositionX - 2;
            if (i < copyGame.TabPiece.Length && j >= 0)
            {
                AddMove(copyGame, i, j);
            }

            i = PositionY - 1;
            j = PositionX + 2;
            if (i >= 0&&j<copyGame.TabPiece[0].Length)
            {
                AddMove(copyGame, i, j);
            }

            j = PositionX - 2;
            if(i>=0&&j>=0)
            {
                AddMove(copyGame, i, j);
            }

            i = PositionY - 2;
            j = PositionX + 1;

            if(i>=0&&j<copyGame.TabPiece[0].Length)
            {
                AddMove(copyGame, i, j);
            }

            j = PositionX - 1;
            if (i >= 0&&j>=0)
            {
                AddMove(copyGame, i, j);
            }
            
               
        }
    }
}
