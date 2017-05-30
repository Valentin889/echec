using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class IA : Player
    {
        private int iDepth;
        private Game game;
        private Dictionary<string, Piece> DicPieceOwn;
        private Dictionary<string, Piece> DicPieceAdverse;
        private int max;
        private int min;

        public IA(string Color, int Depth)
            :base(Color)
        {
            iDepth = Depth;
        }
        public override void Jouer(Game game)
        {
            this.game = game.Clone();

            if (this.Color==game.Color1)
            {
                DicPieceOwn = this.game.DicWhitePiece;
                DicPieceAdverse = this.game.DicBlackPiece;
            }
            else
            {
                DicPieceOwn = this.game.DicBlackPiece;
                DicPieceAdverse = this.game.DicWhitePiece;
            }
            CallMinMax(game.TabPiece, iDepth, true);
        }


        private void CallMinMax(Piece[][] Table, int Depth, bool bIsMax)
        {
            max = int.MinValue;
            min = int.MaxValue;
            if(bIsMax)
            {
                int tmp = Max(Table, Depth - 1);
                if(tmp>max)
                {
                    max = tmp;
                }
            }
            else
            {
                int tmp = Min(Table, Depth - 1);
                if(tmp<min)
                {
                    min = tmp;
                }
            }
        }

        private int Max(Piece[][] Table, int Depth)
        {
            if (Depth == 0 || game.IsCheckmat(game.Color1) || game.IsCheckmat(game.Color2))
            {
                return Eval(Table);
            }


            foreach (Piece p in DicPieceOwn.Values)
            {
                foreach (string s in p.Move)
                {
                    string[] t = s.Split('/');
                    int NewY = Convert.ToInt32(t[0]);
                    int NewX = Convert.ToInt32(t[1]);

                    int OldY = p.PositionY;
                    int OldX = p.PositionX;
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { NewY, NewX});
                    Table[p.PositionY][p.PositionX] = p;
                    
                    CallMinMax(Table, Depth, false);
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { OldY, OldX });
                    Table[p.PositionY][p.PositionX] = p;
               
                }
            }

            return max;
        }

        private int Min(Piece[][] Table, int Depth)
        {
            if (Depth == 0 || game.IsCheckmat(game.Color1) || game.IsCheckmat(game.Color2))
            {
                return Eval(Table);
            }

            foreach (Piece p in DicPieceAdverse.Values)
            {
                foreach (string s in p.Move)
                {
                    string[] t = s.Split('/');
                    int Y = Convert.ToInt32(t[0]);
                    int X = Convert.ToInt32(t[1]);

                    int OldY = p.PositionY;
                    int OldX = p.PositionX;
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { Y, X });
                    Table[p.PositionY][p.PositionX] = p;

                    CallMinMax(Table, Depth, true);
                    
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { OldY, OldX });
                    Table[p.PositionY][p.PositionX] = p;
                }
            }

            return max;
        }

        private int Eval(Piece[][] table)
        {
            return 0;
        }

    }
}
