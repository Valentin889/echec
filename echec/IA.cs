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
        private Piece lastPiece;
        private Piece Pieceplay;

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
            max = int.MinValue;
            min = int.MaxValue;

            CallMinMax(game.TabPiece, iDepth, true);
            LastPiece = Pieceplay;
            LastPosition = new int[2] { Pieceplay.PositionY, Pieceplay.PositionX }; 
            game.Play();
            game.NextPlayer();
        }


        private void CallMinMax(Piece[][] Table, int Depth, bool bIsMax)
        {
            
            if(bIsMax)
            {
                int tmp = Max(Table, Depth);
                if(tmp>max)
                {
                    Pieceplay = lastPiece.Clone();
                    max = tmp;
                }
            }
            else
            {
                int tmp = Min(Table, Depth);
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
                p.SetPossibleMoves(game);
                foreach (string s in p.Move)
                {
                    lastPiece = p;
                    string[] t = s.Split('/');
                    int NewY = Convert.ToInt32(t[0]);
                    int NewX = Convert.ToInt32(t[1]);

                    int OldY = p.PositionY;
                    int OldX = p.PositionX;
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { NewY, NewX});
                    Table[p.PositionY][p.PositionX] = p;
                    
                    CallMinMax(Table, Depth-1, false);
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { OldY, OldX });
                    Table[p.PositionY][p.PositionX] = p;
               
                }
            }
            return min;
        }

        private int Min(Piece[][] Table, int Depth)
        {
            if (Depth == 0 || game.IsCheckmat(game.Color1) || game.IsCheckmat(game.Color2))
            {
                return Eval(Table);
            }

            foreach (Piece p in DicPieceAdverse.Values)
            {
                p.SetPossibleMoves(game);
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

                    CallMinMax(Table, Depth-1, true);
                    
                    Table[p.PositionY][p.PositionX] = null;
                    p.SetPosition(new int[] { OldY, OldX });
                    Table[p.PositionY][p.PositionX] = p;
                }
            }

            return max;
        }

        private int Eval(Piece[][] table)
        {
            int iOwn = 0;
            int iOpp = 0;
            foreach(Piece p in DicPieceOwn.Values)
            {
                if(p.GetType()==typeof(Pawn))
                {
                    iOwn += 1;
                }
                else if(p.GetType()==typeof(Bishop)||p.GetType()==typeof(Knights))
                {
                    iOwn += 3;
                }
                else if(p.GetType()==typeof(Rook))
                {
                    iOwn += 5;
                }
                else if(p.GetType()==typeof(Queen))
                {
                    iOwn += 9;
                }
            }
            foreach (Piece p in DicPieceAdverse.Values)
            {
                if (p.GetType() == typeof(Pawn))
                {
                    iOpp += 1;
                }
                else if (p.GetType() == typeof(Bishop) || p.GetType() == typeof(Knights))
                {
                    iOpp += 3;
                }
                else if (p.GetType() == typeof(Rook))
                {
                    iOpp += 5;
                }
                else if (p.GetType() == typeof(Queen))
                {
                    iOpp += 9;
                }
            }
            return iOwn - iOpp;
        }

    }
}
