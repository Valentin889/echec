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

        private int iNewPosY;
        private int iNewPosX;


        public IA(string Color1, string Color2, int Depth)
            :base(Color1,Color2)
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
            CalculateMove(game.TabPiece);

            
            game.Play();
            game.NextPlayer();
        }

        private void CalculateMove(Piece[][] table)
        {
            foreach(Piece p in DicPieceOwn.Values)
            {
                p.SetPossibleMoves(game);
                foreach(string s in p.Move)
                {
                    
                       
                }
            }

        }

        private int ValeurMinMax(Piece[][] table, bool IsMax)
        {
            return 0;
        }
        private int Eval(Piece[][] table)
        {
            int iOwn = 0;
            int iOpp = 0;
           

            if(game.IsCheckmat(this.Color))
            {
                return int.MaxValue;
            }
            if(game.IsCheckmat(this.ColorAdverse))
            {
                return int.MinValue;
            }
            if(game.IsDraw(this.Color))
            {
                return 0;
            }


            for(int i=0; i<table.Length;i++)
            {
                for(int j=0; j<table[i].Length;j++)
                {
                    Piece p = table[i][j];
                    if (p != null)
                    {
                        if(p.Color==this.Color)
                        {
                            if(p.GetType()==typeof(Pawn))
                            {
                                iOwn += 1;
                            }
                            if (p.GetType() == typeof(Knights)||p.GetType()==typeof(Bishop))
                            {
                                iOwn += 3;
                            }
                            if (p.GetType() == typeof(Rook))
                            {
                                iOwn += 5;
                            }
                            if (p.GetType() == typeof(Queen))
                            {
                                iOwn += 9;
                            }
                        }
                        else
                        {
                            if (p.GetType() == typeof(Pawn))
                            {
                                iOpp += 1;
                            }
                            if (p.GetType() == typeof(Knights) || p.GetType() == typeof(Bishop))
                            {
                                iOpp += 3;
                            }
                            if (p.GetType() == typeof(Rook))
                            {
                                iOpp += 5;
                            }
                            if (p.GetType() == typeof(Queen))
                            {
                                iOpp += 9;
                            }
                        }
                    }
                }
            }



            return iOwn - iOpp;
        }

    }
}
