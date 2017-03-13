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
        public override void Storagepossible(Game jeu)
        {
            Game jeuCopie = jeu;
            jeuCopie.Players[0].DernierePiece = this;
            int i;
            int j;
            Move.Clear();

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
    }
}
