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
        private Piece dernierePiece;
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
            string Couleur = strCouleur2;
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
            Couleur = strCouleur1;
            for (int j = 0; j < 8; j++)
            {
                lstPiece.Add(new Pion(Couleur));
            }
            lstPiece.Add(new Tour(Couleur));
            lstPiece.Add(new Cavalier(Couleur));
            lstPiece.Add(new Fou(Couleur));
            lstPiece.Add(new Reine(Couleur));
            lstPiece.Add(new Roi(Couleur));
            lstPiece.Add(new Fou(Couleur));
            lstPiece.Add(new Cavalier(Couleur));
            lstPiece.Add(new Tour(Couleur));
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
            RemplissageTablePiece();
        }

        public void CreationJoueur(Joueur joueur1, Joueur joueur2)
        {
            lstJoueur.Add(joueur1);
            lstJoueur.Add(joueur2);
        } 

        public List<Piece> LstPiece
        {
            get
            {
                return lstPiece;
            }
        }
        public Piece[][] TabPiece
        {
            get
            {
                return tableauPiece;
            }
        }
        public void RemplissageTablePiece()
        {
            foreach (Piece p in lstPiece)
            {
                tableauPiece[p.PositionY][p.PositionX] = p;
            }
        }
        public void TournePlateau()
        {
            Piece[][] tempHorizontal = new Piece[tableauPiece.Length][];
            Piece[][] temp = new Piece[tableauPiece.Length][];
            for (int i = 0; i < tempHorizontal.Length; i++)
            {
                tempHorizontal[i] = new Piece[tableauPiece[i].Length];
                temp[i] = new Piece[tableauPiece[i].Length];
            }

            for (int i = 0; i < tableauPiece.Length; i++)
            {
                for (int j = 0; j < tableauPiece[i].Length; j++)
                {
                    tempHorizontal[i][j] = tableauPiece[i][tableauPiece[i].Length - j - 1];

                }
            }
            for (int i = 0; i < tempHorizontal.Length; i++)
            {
                for (int j = 0; j < tempHorizontal[i].Length; j++)
                {
                    temp[i][j] = tempHorizontal[tempHorizontal.Length - i - 1][j];
                }
            }
            tableauPiece = temp;
        }
        public string Couleur1
        {
            get
            {
                return strCouleur1;
            }
        }
        public string Couleur2
        {
            get
            {
                return strCouleur2;
            }
        }

        public List<String> DeplacementPiece(String[] str)
        {
            
            List<String> renvoie = new List<string>();

            int colonne = Convert.ToInt32(str[0]);
            int ligne = Convert.ToInt32(str[1]);

            dernierePiece = tableauPiece[colonne][ligne];
            renvoie = dernierePiece.DeplacementPossible(this);
            
            
            return renvoie;
        }

        public Piece DernierePiece
        {
            get
            {
                return dernierePiece;
            }
            set
            {
                dernierePiece = value;
            }
        }
    }
}
