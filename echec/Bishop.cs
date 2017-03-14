using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Bishop : Piece
    {
        public Bishop(string couleur)
            : base(couleur)
        {

        }
        public override void Storagepossible(Game jeu)
        {
            Game jeuCopie = jeu;
            jeuCopie.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY + 1, j = PositionX + 1; i < jeuCopie.TabCase.Length && j < jeuCopie.TabCase[0].Length; i++, j++)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = jeuCopie.TabCase.Length;
                    j = jeuCopie.TabCase[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < jeuCopie.TabCase.Length && j >= 0; i++, j--)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = jeuCopie.TabCase.Length;
                    j = -1;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i >= 0 && j < jeuCopie.TabCase[0].Length; i--, j++)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = -1;
                    j = jeuCopie.TabCase[0].Length;
                }
            }

            for (int i = PositionY - 1, j = PositionX - 1; i >= 0 && j >= 0; i--, j--)
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
