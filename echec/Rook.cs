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
        public override void Storagepossible(Game jeu)
        {
            Game jeuCopie = jeu;
            jeuCopie.Players[0].LastPiece = this;
            Move = new List<string>();

            for (int i = PositionY + 1; i < jeuCopie.TabPiece.Length; i++)
            {
                if(!AddMove(jeuCopie, i, PositionX))
                {
                    i = jeuCopie.TabPiece.Length;
                }
            }
            for (int i = PositionY - 1; i >= 0; i--)
            {
                if (!AddMove(jeuCopie, i, PositionX))
                {
                    i = -1;
                }
            }
            for(int i=PositionX+1;i<jeuCopie.TabPiece[0].Length;i++)
            {
                if(!AddMove(jeuCopie, PositionY, i))
                {
                    i = jeuCopie.TabPiece[i].Length;
                }
            }
            for(int i=PositionX-1;i>=0;i--)
            {
                if (!AddMove(jeuCopie, PositionY, i))
                {
                    i = -1;
                }
            }
        }
    }
}
