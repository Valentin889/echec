using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Piece[][] tabCase;
        public Game(frmJeu form)
        {
            iNumerberPiece = 32;
            iNumberPiecePerColor = iNumerberPiece / 2;
            lstPlayer = new List<Player>();
            lstPiece = new List<Piece>();
            Affichage = form;
            strColor1 = "blanc";
            strColor2 = "noir";
            tabCase = new Piece[8][];
            for (int i = 0; i < tabCase.Length; i++)
            {
                tabCase[i] = new Piece[8];
            }
        }
        public Game (List<Player> player, List<Piece> lpiece, Piece[][] tpiece,frmJeu form)
            :this(form)
        {
            lstPlayer = player;
            lstPiece = lpiece;
            tabCase = tpiece;
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
            lstPlayer[0].LastPiece = tabCase[colonne][ligne];
            lstPlayer[0].LastPiece.Storagepossible(this);
        }

        public void NoCheck(Piece piece)
        {
            int posXInitial = piece.PositionX;
            int posyInitial = piece.PositionY;

            
            List<String> listTemp = new List<string>();

            foreach(string s in piece.Move)
            {
                string[] tmp = s.Split('/');
                int[] Coup = new int[2];
                int colonne = Convert.ToInt32(tmp[0]);
                int ligne = Convert.ToInt32(tmp[1]);
                Coup[0] = colonne;
                Coup[1] = ligne;

                Players[0].DernierPosition = Coup;

                tabCase[piece.PositionY][piece.PositionX] = null;
                piece.PositionY=lstPlayer[0].DernierPosition[0] ;
                piece.PositionX=lstPlayer[0].DernierPosition[1];
                tabCase[piece.PositionY][piece.PositionX] = piece;

                if(KingCheck())
                {
                    listTemp.Add(s);
                }


            }
            foreach(string s in listTemp)
            {
                piece.Move.Remove(s);
            }
            piece.PositionY = posyInitial;
            piece.PositionX = posXInitial;
            this.Players[0].LastPiece = piece;
        }

        public void Play()
        {
            Piece p = lstPlayer[0].LastPiece;


            tabCase[p.PositionY][p.PositionX] = null;
            p.PositionY = lstPlayer[0].DernierPosition[0];
            p.PositionX = lstPlayer[0].DernierPosition[1];
            tabCase[p.PositionY][p.PositionX] = p;
        }

        public bool KingCheck()
        {
            foreach (Piece p in lstPiece)
            {
                if(p.Color != lstPlayer[0].Color)
                {
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
                return tabCase;
            }
        }
        public void RemplissageTablePiece()
        {
            foreach (Piece p in lstPiece)
            {
                tabCase[p.PositionY][p.PositionX] = p;
            }
        }
        public void TournGameAround()
        {
            Piece[][] tempHorizontal = new Piece[tabCase.Length][];
            Piece[][] temp = new Piece[tabCase.Length][];
            for (int i = 0; i < tempHorizontal.Length; i++)
            {
                tempHorizontal[i] = new Piece[tabCase[i].Length];
                temp[i] = new Piece[tabCase[i].Length];
            }

            for (int i = 0; i < tabCase.Length; i++)
            {
                for (int j = 0; j < tabCase[i].Length; j++)
                {
                    tempHorizontal[i][j] = tabCase[i][tabCase[i].Length - j - 1];

                }
            }
            for (int i = 0; i < tempHorizontal.Length; i++)
            {
                for (int j = 0; j < tempHorizontal[i].Length; j++)
                {
                    temp[i][j] = tempHorizontal[tempHorizontal.Length - i - 1][j];
                }
            }
            tabCase = temp;
            for (int i = 0; i < tabCase.Length; i++)
            {
                for (int j = 0; j < tabCase[i].Length; j++)
                {
                    if (tabCase[i][j] != null)
                    {
                        tabCase[i][j].PositionY = i;
                        tabCase[i][j].PositionX = j;
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
