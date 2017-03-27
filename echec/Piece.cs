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
        }
        public bool AddMove(Game game, int i, int j)
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

        public Piece Clone()
        {
            Piece Clone;
            switch(this.ToString())
            {
                case "echec.Rook":
                    Clone = new Rook(this.Color);
                    break;
                case "echec.Queen":
                    Clone = new Queen(this.Color);
                    break;
                case "echec.Pawn":
                    Clone = new Pawn(this.Color);
                    Pawn pClone = (Pawn)Clone;
                    Pawn pOriginal = (Pawn)this;
                    pClone.PremierDeplacement = pOriginal.PremierDeplacement;
                    Clone = pClone;
                    break;
                case "echec.Knights":
                    Clone = new Knights(this.Color);
                    break;
                case "echec.King":
                    Clone = new King(this.Color);
                    break;
                case "echec.Bishop":
                    Clone = new Bishop(this.Color);
                    break;
                default:
                    Clone = null;
                    break;
                     
            }
            Clone.lstMove = this.lstMove;
            Clone.PositionX = this.PositionX;
            Clone.PositionY = this.PositionY;
            Clone.Picture = this.Picture;
            return Clone;



        }

        
    }
}
    

