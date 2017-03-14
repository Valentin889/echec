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
        public override void Storagepossible(Game jeu)
        {
            Game jeuCopie = jeu;
            jeuCopie.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY + 1; i < jeuCopie.TabCase.Length; i++)
            {
                if (!AddMove(jeuCopie, i, PositionX))
                {
                    i = jeuCopie.TabCase.Length;
                }
            }
            for (int i = PositionY - 1; i >= 0; i--)
            {
                if (!AddMove(jeuCopie, i, PositionX))
                {
                    i = -1;
                }
            }
            for (int i = PositionX + 1; i < jeuCopie.TabCase[0].Length; i++)
            {
                if (!AddMove(jeuCopie, PositionY, i))
                {
                    i = jeuCopie.TabCase[i].Length;
                }
            }
            for (int i = PositionX - 1; i >= 0; i--)
            {
                if (!AddMove(jeuCopie, PositionY, i))
                {
                    i = -1;
                }
            }


            for (int i = PositionY+1, j = PositionX+1; i<jeuCopie.TabCase.Length&&j<jeuCopie.TabCase[0].Length;i++,j++)
            {
                if(!AddMove(jeuCopie, i, j))
                {
                    i = jeuCopie.TabCase.Length;
                    j = jeuCopie.TabCase[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < jeuCopie.TabCase.Length && j >=0; i++, j--)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = jeuCopie.TabCase.Length;
                    j = -1;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i >=0 && j < jeuCopie.TabCase[0].Length; i--, j++)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = -1;
                    j = jeuCopie.TabCase[0].Length;
                }
            }

            for (int i = PositionY -1, j = PositionX - 1; i >=0 && j >= 0; i--, j--)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = -1;
                    j = -1;
                }
            }
        }
    }
}
