using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace echec
{
    public class Game
    {
        private int iNumerberPiece;
        private int iNumberPiecePerColor;
        private List<Player> lstPlayer;
        private List<Piece> lstPiece;
        private string strColor1;
        private string strColor2;
        private frmJeu Affichage;
        private Piece[][] tabPiece;
        public Game(frmJeu form)
        {
            iNumerberPiece = 32;
            iNumberPiecePerColor = iNumerberPiece / 2;
            lstPlayer = new List<Player>();
            lstPiece = new List<Piece>();
            Affichage = form;
            strColor1 = "blanc";
            strColor2 = "noir";
            tabPiece = new Piece[8][];
            for (int i = 0; i < tabPiece.Length; i++)
            {
                tabPiece[i] = new Piece[8];
            }
        }
        public void Creationpiece()
        {
            string Couleur = strColor2;
            lstPiece.Add(new Rook(Couleur));
            lstPiece.Add(new Knights(Couleur));
            lstPiece.Add(new Bishop(Couleur));
            lstPiece.Add(new Queen(Couleur));
            lstPiece.Add(new King(Couleur));
            lstPiece.Add(new Bishop(Couleur));
            lstPiece.Add(new Knights(Couleur));
            lstPiece.Add(new Rook(Couleur));

            for (int j = 0; j < 8; j++)
            {
                lstPiece.Add(new Pawn(Couleur));
            }
            Couleur = strColor1;
            for (int j = 0; j < 8; j++)
            {
                lstPiece.Add(new Pawn(Couleur));
            }
            lstPiece.Add(new Rook(Couleur));
            lstPiece.Add(new Knights(Couleur));
            lstPiece.Add(new Bishop(Couleur));
            lstPiece.Add(new Queen(Couleur));
            lstPiece.Add(new King(Couleur));
            lstPiece.Add(new Bishop(Couleur));
            lstPiece.Add(new Knights(Couleur));
            lstPiece.Add(new Rook(Couleur));
        }
        public void CreationJoueur(Player joueur1, Player joueur2)
        {
            lstPlayer.Add(joueur1);
            lstPlayer.Add(joueur2);
        }
        public void PositionPiece()
        {
            int x = 0;
            int y = 0;
            foreach (Piece p in lstPiece)
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
        public void BeginGame()
        {
            lstPlayer[0].Jouer();
        }
        public void NextPlayer()
        {
            lstPlayer.Add(lstPlayer[0]);
            lstPlayer.Remove(lstPlayer[0]);
            lstPlayer[0].Jouer();
        }
        public void SetMovePiece(String[] str)
        {
            int colonne = Convert.ToInt32(str[0]);
            int ligne = Convert.ToInt32(str[1]);
            lstPlayer[0].LastPiece = tabPiece[colonne][ligne];
            lstPlayer[0].LastPiece.Storagepossible(this);

            Game copyGame = this.Clone();
            copyGame.NoCheck(lstPlayer[0].LastPiece);

        }
        
        public void NoCheck(Piece piece)
        {
            Piece pieceClone = piece.Clone();

            
            List<String> listTemp = new List<string>();

            foreach(string s in pieceClone.Move)
            {
                string[] tmp = s.Split('/');
                int[] Coup = new int[2];
                int colonne = Convert.ToInt32(tmp[0]);
                int ligne = Convert.ToInt32(tmp[1]);
                Coup[0] = colonne;
                Coup[1] = ligne;

                Players[0].DernierPosition = Coup;

                tabPiece[pieceClone.PositionY][pieceClone.PositionX] = null;
                pieceClone.PositionY=lstPlayer[0].DernierPosition[0] ;
                pieceClone.PositionX=lstPlayer[0].DernierPosition[1];
                tabPiece[pieceClone.PositionY][pieceClone.PositionX] = pieceClone;

                if(KingCheck(pieceClone))
                {
                    listTemp.Add(s);
                }


            }
            foreach(string s in listTemp)
            {
                pieceClone.Move.Remove(s);
            }
            this.Players[0].LastPiece = piece;
        }

        public Game Clone()
        {
            Game clone =new Game(Affichage);
            clone.iNumerberPiece = this.iNumerberPiece;
            clone.iNumberPiecePerColor = this.iNumberPiecePerColor;
            foreach(Player p in this.lstPlayer)
            {
                clone.lstPlayer.Add(p.Clone());
            }
            foreach(Piece p in this.lstPiece)
            {
                clone.lstPiece.Add(p.Clone());
                clone.tabPiece[p.PositionY][p.PositionX] = p;
            }
            clone.strColor1 = this.strColor1;
            clone.strColor2 = this.strColor2;
            clone.tabPiece[0][0] = null;
            return clone;
        }
        public bool IsSmallrock(string color)
        {
            //implémente une série de test
            if(color==strColor1)
            {
                try
                {
                    Rook r = (Rook)tabPiece[7][7];
                }
                catch
                {
                    return false;
                }
                if(tabPiece[7][6]!=null||tabPiece[7][5]!=null)
                {
                    return false;
                }
                try
                {
                    King k = (King)tabPiece[7][4];
                    if(k.AlreadyMove)
                    {
                        return false;
                    }
                    if(k.IsCheck(this))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                
            }
            else
            {
                try
                {
                    Rook r = (Rook)tabPiece[7][0];
                }
                catch
                {
                    return false;
                }
                if (tabPiece[7][1] != null || tabPiece[7][2] != null)
                {
                    return false;
                }
                try
                {
                    King k = (King)tabPiece[7][3];
                    if (k.AlreadyMove)
                    {
                        return false;
                    }
                    if (k.IsCheck(this))
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsBigRock(string color)
        {

            return true;
        }
        public void Play()
        {
            Piece p = lstPlayer[0].LastPiece;
            tabPiece[p.PositionY][p.PositionX] = null;
            p.PositionY = lstPlayer[0].DernierPosition[0];
            p.PositionX = lstPlayer[0].DernierPosition[1];
            tabPiece[p.PositionY][p.PositionX] = p;
        }

        public bool KingCheck(Piece pieceClone)
        {
            foreach (Piece p in lstPiece)
            {
                if(p.Color == lstPlayer[0].Color)
                {
                    if(p.ToString()==pieceClone.ToString())
                    {
                        p.PositionX = pieceClone.PositionX;
                        p.PositionY = pieceClone.PositionY;
                    }
                    if(p.ToString()=="echec.King")
                    {
                        King k = (King)p;
                        return k.IsCheck(this);
                    }
                }
            }

            return false;
        }
        public List<Piece> ListPieces
        {
            get
            {
                return lstPiece;
            }
        }
        public List<Player> Players
        {
            get
            {
                return lstPlayer;
            }
        }
        public Piece[][] TabCase
        {
            get
            {
                return tabPiece;
            }
        }
        public void RemplissageTablePiece()
        {
            foreach (Piece p in lstPiece)
            {
                tabPiece[p.PositionY][p.PositionX] = p;
            }
        }
        public void TournGameAround()
        {
            Piece[][] tempHorizontal = new Piece[tabPiece.Length][];
            Piece[][] temp = new Piece[tabPiece.Length][];
            for (int i = 0; i < tempHorizontal.Length; i++)
            {
                tempHorizontal[i] = new Piece[tabPiece[i].Length];
                temp[i] = new Piece[tabPiece[i].Length];
            }

            for (int i = 0; i < tabPiece.Length; i++)
            {
                for (int j = 0; j < tabPiece[i].Length; j++)
                {
                    tempHorizontal[i][j] = tabPiece[i][tabPiece[i].Length - j - 1];

                }
            }
            for (int i = 0; i < tempHorizontal.Length; i++)
            {
                for (int j = 0; j < tempHorizontal[i].Length; j++)
                {
                    temp[i][j] = tempHorizontal[tempHorizontal.Length - i - 1][j];
                }
            }
            tabPiece = temp;
            for (int i = 0; i < tabPiece.Length; i++)
            {
                for (int j = 0; j < tabPiece[i].Length; j++)
                {
                    if (tabPiece[i][j] != null)
                    {
                        tabPiece[i][j].PositionY = i;
                        tabPiece[i][j].PositionX = j;
                    }
                }
            }
        }
        public string Color1
        {
            get
            {
                return strColor1;
            }
        }
        public string Color2
        {
            get
            {
                return strColor2;
            }
        }
    }
}
