using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public abstract class Piece
    {
        private List<String> lstMove;
        public String Color { get; private set; }

        public bool FirstTimeAddMove { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public List<String> Move
        {
            get
            {
                return lstMove;
            }
            set
            {
                lstMove = value;
            }
        }
        public abstract void Storagepossible(Game jeu);
        public string Picture { get; set; }
        public Piece(string couleur)
        {
            Color = couleur;
            FirstTimeAddMove = true;
        }
        public bool AddMove(Game game, int i, int j)
        {

            bool b = true;

            if (!FirstTimeAddMove)
            {
                IsMovePossible(game, i, j);
            }
            b = UsedByAddMove(game, i, j);
            return b;


        }
        private bool UsedByAddMove(Game game, int i, int j)
        {
            bool b = true;
            if (game.TabPiece[i][j] == null)
            {
                lstMove.Add(i.ToString() + "/" + j.ToString());
            }
            else
            {
                if (Color != game.TabPiece[i][j].Color)
                {
                    lstMove.Add(i.ToString() + "/" + j.ToString());
                }
                b = false;
            }

            return b;
        }

        private bool IsMovePossible(Game game, int i , int j)
        {

            Game CopieGame = game;
            int[] tmp = new int[2];

            tmp[0] = i;
            tmp[1] = j;
            CopieGame.Players[0].DernierePiece = this;
            CopieGame.Players[0].DernierPosition = tmp;
            CopieGame.Play();

            return CopieGame.KingCheck();
        }

    }
}
    

