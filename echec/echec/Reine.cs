using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Reine : Piece
    {
        List<String> Renvoie;
        public Reine(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            Renvoie = new List<string>();

            for (int i = PositionY + 1; i < jeu.TabPiece.Length; i++)
            {
                if (!Deplacemennt(jeu, i, PositionX))
                {
                    i = jeu.TabPiece.Length;
                }
            }
            for (int i = PositionY - 1; i > 0; i--)
            {
                if (!Deplacemennt(jeu, i, PositionX))
                {
                    i = 0;
                }
            }
            for (int i = PositionX + 1; i < jeu.TabPiece[0].Length; i++)
            {
                if (!Deplacemennt(jeu, PositionY, i))
                {
                    i = jeu.TabPiece[i].Length;
                }
            }
            for (int i = PositionX - 1; i > 0; i--)
            {
                if (!Deplacemennt(jeu, PositionY, i))
                {
                    i = 0;
                }
            }


            for (int i = PositionY+1, j = PositionX+1; i<jeu.TabPiece.Length&&j<jeu.TabPiece[0].Length;i++,j++)
            {
                if(!Deplacemennt(jeu, i, j))
                {
                    i = jeu.TabPiece.Length;
                    j = jeu.TabPiece[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < jeu.TabPiece.Length && j >0; i++, j--)
            {
                if (!Deplacemennt(jeu, i, j))
                {
                    i = jeu.TabPiece.Length;
                    j = 0;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i >0 && j < jeu.TabPiece[0].Length; i--, j++)
            {
                if (!Deplacemennt(jeu, i, j))
                {
                    i = 0;
                    j = jeu.TabPiece[0].Length;
                }
            }

            for (int i = PositionY -1, j = PositionX - 1; i >0 && j > 0; i--, j--)
            {
                if (!Deplacemennt(jeu, i, j))
                {
                    i = 0;
                    j = 0;
                }
            }



            return Renvoie;
        }
        private bool Deplacemennt(Jeu jeu, int i, int j)
        {
            bool b = true;
            if (jeu.TabPiece[i][j] == null)
            {
                Renvoie.Add(i.ToString() + "/" + j.ToString());
            }
            else
            {
                if (Couleur != jeu.TabPiece[i][j].Couleur)
                {
                    Renvoie.Add(i.ToString() + "/" + j.ToString());
                }
                b = false;
            }
            return b;
        }
    }
}
