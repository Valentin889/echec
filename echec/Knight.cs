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
        public override void Storagepossible(Game game)
        {
            Game jeuCopie = game;
            jeuCopie.Players[0].LastPiece = this;
            int i;
            int j;
            Move = new List<string>();

            i = PositionY + 2;
            j = PositionX + 1;
            if(i<jeuCopie.TabPiece.Length&&j<jeuCopie.TabPiece[0].Length)
            {
                AddMove(jeuCopie, i, j);
            }

            j = PositionX - 1;
            if(i<jeuCopie.TabPiece.Length&&j>=0)
            {
                AddMove(jeuCopie, i, j);
            }


            i = PositionY + 1;
            j = PositionX + 2;
            if (i < jeuCopie.TabPiece.Length && j < jeuCopie.TabPiece[0].Length)
            {
                AddMove(jeuCopie, i, j);
            }

            j = PositionX - 2;
            if (i < jeuCopie.TabPiece.Length && j >= 0)
            {
                AddMove(jeuCopie, i, j);
            }

            i = PositionY - 1;
            j = PositionX + 2;
            if (i > 0&&j<jeuCopie.TabPiece[0].Length)
            {
                AddMove(jeuCopie, i, j);
            }

            j = PositionX - 2;
            if(i>=0&&j>=0)
            {
                AddMove(jeuCopie, i, j);
            }

            i = PositionY - 2;
            j = PositionX + 1;

            if(i>=0&&j<jeuCopie.TabPiece[0].Length)
            {
                AddMove(jeuCopie, i, j);
            }

            j = PositionX - 1;
            if (i >= 0&&j>=0)
            {
                AddMove(jeuCopie, i, j);
            }
            
               
        }

        public bool IsCheck(Game game)
        {
            if (Color == game.Color1)
            {
                foreach (string s in game.DicBlackPiece.Keys)
                {
                    Piece p = game.DicBlackPiece[s];
                    if(CalledByIsCheck(p,game))
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (string s in game.DicWhitePiece.Keys)
                {
                    Piece p = game.DicWhitePiece[s];
                    if (CalledByIsCheck(p, game))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CalledByIsCheck(Piece p, Game game)
        {
            p.Storagepossible(game);
            foreach (string s in p.Move)
            {
                string[] tmp = s.Split('/');
                int colonne = Convert.ToInt32(tmp[0]);
                int ligne = Convert.ToInt32(tmp[1]);

                if (PositionY == colonne && PositionX == ligne)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
