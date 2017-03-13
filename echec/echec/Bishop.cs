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
            jeuCopie.Players[0].DernierePiece = this;
            Move.Clear();

            for (int i = PositionY + 1, j = PositionX + 1; i < jeuCopie.TabPiece.Length && j < jeuCopie.TabPiece[0].Length; i++, j++)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = jeuCopie.TabPiece.Length;
                    j = jeuCopie.TabPiece[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < jeuCopie.TabPiece.Length && j >= 0; i++, j--)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = jeuCopie.TabPiece.Length;
                    j = -1;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i >= 0 && j < jeuCopie.TabPiece[0].Length; i--, j++)
            {
                if (!AddMove(jeuCopie, i, j))
                {
                    i = -1;
                    j = jeuCopie.TabPiece[0].Length;
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
