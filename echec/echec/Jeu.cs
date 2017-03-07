using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Jeu
    {
        private int iNbPiece;
        private int iNbPieceParCouleur;
        private List<Joueur> lstJoueur;
        private List<Piece> lstPiece;
        private string strCouleur1;
        private string strCouleur2;
        private frmJeu Affichage;
        private Piece[][] tableauPiece;
        public Jeu(frmJeu form)
        {
            iNbPiece = 32;
            iNbPieceParCouleur = iNbPiece / 2;
            lstJoueur = new List<Joueur>();
            lstPiece = new List<Piece>();
            Affichage = form;
            strCouleur1 = "blanc";
            strCouleur2 = "noir";
            tableauPiece = new Piece[8][];
            for(int i=0; i<tableauPiece.Length;i++)
            {
                tableauPiece[i] = new Piece[8];
            }
        }
        public void Creationpiece()
        {
            string Couleur = strCouleur1;
            for(int i=0; i<2; i++)
            {
                lstPiece.Add(new Tour(Couleur));
                lstPiece.Add(new Cavalier(Couleur));
                lstPiece.Add(new Fou(Couleur));
                lstPiece.Add(new Reine(Couleur));
                lstPiece.Add(new Roi(Couleur));
                lstPiece.Add(new Fou(Couleur));
                lstPiece.Add(new Cavalier(Couleur));
                lstPiece.Add(new Tour(Couleur));

                for (int j = 0; j < 8; j++)
                {
                    lstPiece.Add(new Pion(Couleur));
                }
                Couleur = strCouleur2;
            }
        }

        public void PositionPiece()
        {
            int x = 0;
            int y = 0;
            foreach(Piece p in lstPiece)
            {
                p.PositionX = x;
                p.PositionY = y;
                if (x < 7)
                {
                    x++;
                }
                else
                {
                    x = 0;
                    y++;
                    if (y > 1 && y < 7)
                    {
                        y = 6;
                    }
                }
            }
        }

        public void CreationJoueur(Joueur joueur1, Joueur joueur2)
        {
            lstJoueur.Add(joueur1);
            lstJoueur.Add(joueur2);
        } 

        public List<Piece> Piece
        {
            get
            {
                return lstPiece;
            }
        }

    }
}
