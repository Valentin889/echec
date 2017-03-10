using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Fou : Piece
    {
        List<String> Renvoie;
        public Fou(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            Renvoie = new List<string>();

            for (int i = PositionY + 1, j = PositionX + 1; i < jeu.TabPiece.Length && j < jeu.TabPiece[0].Length; i++, j++)
            {
                if (!Deplacement(jeu, i, j))
                {
                    i = jeu.TabPiece.Length;
                    j = jeu.TabPiece[0].Length;
                }
            }

            for (int i = PositionY + 1, j = PositionX - 1; i < jeu.TabPiece.Length && j > 0; i++, j--)
            {
                if (!Deplacement(jeu, i, j))
                {
                    i = jeu.TabPiece.Length;
                    j = 0;
                }
            }

            for (int i = PositionY - 1, j = PositionX + 1; i > 0 && j < jeu.TabPiece[0].Length; i--, j++)
            {
                if (!Deplacement(jeu, i, j))
                {
                    i = 0;
                    j = jeu.TabPiece[0].Length;
                }
            }

            for (int i = PositionY - 1, j = PositionX - 1; i > 0 && j > 0; i--, j--)
            {
                if (!Deplacement(jeu, i, j))
                {
                    i = 0;
                    j = 0;
                }
            }

            return Renvoie;
        }
        private bool Deplacement(Jeu jeu, int i, int j)
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
