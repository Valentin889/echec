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
        private Dictionary<string,Piece> dicWhitePiece;
        private Dictionary<string, Piece> dicBlackPiece;

        private string strColor1;
        private string strColor2;
        private frmGame Affichage;
        private Piece[][] tabPiece;

        public Game(frmGame form)
        {
            iNumerberPiece = 32;
            iNumberPiecePerColor = iNumerberPiece / 2;
            lstPlayer = new List<Player>();
            dicWhitePiece = new Dictionary<string, Piece>();
            dicBlackPiece = new Dictionary<string, Piece>();

            
            Affichage = form;
            strColor1 = "White";
            strColor2 = "Black";
            tabPiece = new Piece[8][];
            for (int i = 0; i < tabPiece.Length; i++)
            {
                tabPiece[i] = new Piece[8];
            }
        }
        public void Creationpiece()
        {
            string Couleur = strColor2;

            dicBlackPiece.Add("Rook1" + Couleur, new Rook(Couleur));
            dicBlackPiece.Add("Knights1" + Couleur, new Knights(Couleur));
            dicBlackPiece.Add("Bishop1" + Couleur, new Bishop(Couleur));
            dicBlackPiece.Add("Queen" + Couleur, new Queen(Couleur));
            dicBlackPiece.Add("King" + Couleur, new King(Couleur));
            dicBlackPiece.Add("Bishop2" + Couleur, new Bishop(Couleur));
            dicBlackPiece.Add("Knights2" + Couleur, new Knights(Couleur));
            dicBlackPiece.Add("Rook2" + Couleur, new Rook(Couleur));

            for (int i =1; i<9;i++)
            {
                dicBlackPiece.Add("Pawn" + i + Couleur, new Pawn(Couleur));
            }
            Couleur = strColor1;

            for (int i = 1; i < 9; i++)
            {
                dicWhitePiece.Add("Pawn" + i + Couleur, new Pawn(Couleur));
            }
            dicWhitePiece.Add("Rook1" + Couleur, new Rook(Couleur));
            dicWhitePiece.Add("Knights1" + Couleur, new Knights(Couleur));
            dicWhitePiece.Add("Bishop1" + Couleur, new Bishop(Couleur));
            dicWhitePiece.Add("Queen" + Couleur, new Queen(Couleur));
            dicWhitePiece.Add("King" + Couleur, new King(Couleur));
            dicWhitePiece.Add("Bishop2" + Couleur, new Bishop(Couleur));
            dicWhitePiece.Add("Knights2" + Couleur, new Knights(Couleur));
            dicWhitePiece.Add("Rook2" + Couleur, new Rook(Couleur));







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
            foreach(string s in dicBlackPiece.Keys)
            {
                dicBlackPiece[s].PositionX = x;
                dicBlackPiece[s].PositionY = y;
                x++;
                if(x==8)
                {
                    y++;
                    x = 0;
                }
            }
            x = 0;
            y = 6;
            foreach(string s in dicWhitePiece.Keys)
            {
                dicWhitePiece[s].PositionX = x;
                dicWhitePiece[s].PositionY = y;
                x++;
                if(x==8)
                {
                    y++;
                    x = 0;
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
            if(lstPlayer[0].LastPiece==dicBlackPiece["KingBlack"]||lstPlayer[0].LastPiece==dicWhitePiece["KingWhite"])
            {
                King k = (King)lstPlayer[0].LastPiece;
                k.AddRock(this);
            }
            NoCheck(lstPlayer[0].LastPiece);

        }
        public void NoCheck(Piece piece)
        {
            Piece pieceClone = piece.Clone();


            List<String> listTemp = new List<string>();

            foreach(string s in pieceClone.Move)
            {
                Game copyGame = this.Clone();
                
                string[] tmp = s.Split('/');
                int[] Coup = new int[2];
                int colonne = Convert.ToInt32(tmp[0]);
                int ligne = Convert.ToInt32(tmp[1]);
                Coup[0] = colonne;
                Coup[1] = ligne;

                Players[0].LastPosition = Coup;

                if(copyGame.tabPiece[colonne][ligne]!=null)
                {
                    copyGame.CatchUpPiece(copyGame.tabPiece[colonne][ligne]);
                }
                copyGame.tabPiece[pieceClone.PositionY][pieceClone.PositionX] = null;
                pieceClone.PositionY=lstPlayer[0].LastPosition[0] ;
                pieceClone.PositionX=lstPlayer[0].LastPosition[1];
                copyGame.tabPiece[pieceClone.PositionY][pieceClone.PositionX] = pieceClone;

                if(copyGame.KingCheck(pieceClone))
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

            foreach(string s in dicBlackPiece.Keys)
            {
                Piece p = dicBlackPiece[s];
                clone.dicBlackPiece.Add(s, p);
                clone.tabPiece[p.PositionY][p.PositionX] = p;
            }
            foreach (string s in dicWhitePiece.Keys)
            {
                Piece p = dicWhitePiece[s];
                clone.dicWhitePiece.Add(s, p);
                clone.tabPiece[p.PositionY][p.PositionX] = p;
            }
            clone.RemplissageTablePiece();
            clone.strColor1 = this.strColor1;
            clone.strColor2 = this.strColor2;
            return clone;
        }
        public void DoRock(string color)
        {
            if (lstPlayer[0].LastPosition[1] == 6)
            {
                DoSmallRock(color);
            }
            else
            {
                doBigRock(color);
            }

        }
        private void DoSmallRock(string color)
        {
            if(color==strColor1)
            {
                lstPlayer[0].LastPiece = tabPiece[7][7];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 5;
                Affichage.PlayDisplayMove();
                Play();

                lstPlayer[0].LastPiece = tabPiece[7][4];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 6;
                Affichage.PlayDisplayMove();
                Play();
            }
            else
            {
                lstPlayer[0].LastPiece = tabPiece[0][7];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 5;
                if(Affichage.IsGameTurned)
                {
                    Affichage.TurnGame();
                }
                Affichage.PlayDisplayMove();
                Play();


                lstPlayer[0].LastPiece = tabPiece[0][4];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 6;
                Affichage.PlayDisplayMove();
                Play();
            }
        }
        private void doBigRock(string color)
        {
            if (color == strColor1)
            {
                lstPlayer[0].LastPiece = tabPiece[7][0];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 3;
                Affichage.PlayDisplayMove();
                Play();

                lstPlayer[0].LastPiece = tabPiece[7][4];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 2;
                Affichage.PlayDisplayMove();
                Play();
            }
            else
            {
                lstPlayer[0].LastPiece = tabPiece[0][0];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 3;
                if (Affichage.IsGameTurned)
                {
                    Affichage.TurnGame();
                }
                Affichage.PlayDisplayMove();
                Play();

                lstPlayer[0].LastPiece = tabPiece[0][4];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 2;
                Affichage.PlayDisplayMove();
                Play();
            }
        }
        public bool IsBigRock(string color)
        {
            //implémente une série de test
            if(color==strColor1)
            {
                try
                {
                    Rook r = (Rook)tabPiece[7][0];
                }
                catch
                {
                    return false;
                }
                if(tabPiece[7][1]!=null||tabPiece[7][2]!=null||tabPiece[7][3]!=null)
                {
                    return false;
                }
                try
                {
                    King k = (King)tabPiece[7][4].Clone();
                    if(k.AlreadyMove)
                    {
                        return false;
                    }



                    k.PositionX = 3;
                    if (k.IsCheck(this))
                    {
                        return false;
                    }
                    k.PositionX = 2;
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
            else
            {
                try
                {
                    Rook r = (Rook)tabPiece[0][0];
                }
                catch
                {
                    return false;
                }
                if (tabPiece[0][1] != null || tabPiece[0][2] != null|| tabPiece[0][3] != null)
                {
                    return false;
                }
                try
                {
                    King k = (King)tabPiece[0][4].Clone();
                    if (k.AlreadyMove)
                    {
                        return false;
                    }


                    k.PositionX = 3;
                    if (k.IsCheck(this))
                    {
                        return false;
                    }
                    k.PositionX = 2;
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
        public bool IsSmallRock(string color)
        {
            if (color == strColor1)
            {
                try
                {
                    Rook r = (Rook)tabPiece[7][7];
                }
                catch
                {
                    return false;
                }
                if (tabPiece[7][6] != null || tabPiece[7][5] != null)
                {
                    return false;
                }
                try
                {
                    King k = (King)tabPiece[7][4].Clone();
                    if (k.AlreadyMove)
                    {
                        return false;
                    }
                    
                    
                    k.PositionX = 5;
                    if(k.IsCheck(this))
                    {
                        return false;
                    }
                    k.PositionX = 6;
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
                    Rook r = (Rook)tabPiece[0][7];
                }
                catch
                {
                    return false;
                }
                if (tabPiece[0][6] != null || tabPiece[0][5] != null)
                {
                    return false;
                }
                try
                {
                    King k = (King)tabPiece[0][4].Clone();
                    if (k.AlreadyMove)
                    {
                        return false;
                    }
                    /*
                    k.PositionX = 5;
                    if(k.IsCheck(this))
                    {
                        return false;
                    }
                    k.PositionX = 6;
                    if(k.IsCheck(this))
                    {
                        return false;
                    }
                    */
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public void Play()
        {
            Piece p = lstPlayer[0].LastPiece;
            if(p.GetType()==typeof(King))
            {
                King k = (King)p;
                k.AlreadyMove = true;
            }
            if(tabPiece[lstPlayer[0].LastPosition[0]][lstPlayer[0].LastPosition[1]]!=null)
            {
                CatchUpPiece(tabPiece[lstPlayer[0].LastPosition[0]][lstPlayer[0].LastPosition[1]]);
            }
            tabPiece[p.PositionY][p.PositionX] = null;
            p.PositionY = lstPlayer[0].LastPosition[0];
            p.PositionX = lstPlayer[0].LastPosition[1];
            tabPiece[p.PositionY][p.PositionX] = p;

        }

        public bool isPawnLastLine(Piece p)
        {
            if(p.PositionY==0||p.PositionY==tabPiece.Length-1)
            {
                return true;
            }
            return false;
        }
        public bool KingCheck(Piece pieceClone)
        {
            King k = null;
            if (lstPlayer[0].Color == strColor1)
            {
                k = (King)dicWhitePiece["KingWhite"].Clone();
                if (k.GetType() == pieceClone.GetType())
                {
                    k.PositionX = pieceClone.PositionX;
                    k.PositionY = pieceClone.PositionY;
                }
            }
            else
            {
                k = (King)dicBlackPiece["KingBlack"].Clone();
                if (k.GetType() == pieceClone.GetType())
                {
                    k.PositionX = pieceClone.PositionX;
                    k.PositionY = pieceClone.PositionY;
                }
            }
            
            return k.IsCheck(this);
        }

        private void CatchUpPiece(Piece piece)
        {
            Dictionary<String, Piece> TmpDico = new Dictionary<string, Piece>();
            if(piece.Color==strColor1)
            {
                foreach(string s in dicWhitePiece.Keys)
                {
                    if(piece==dicWhitePiece[s])
                    {
                        TmpDico.Add(s, dicWhitePiece[s]);
                    }
                }
                foreach(string s in TmpDico.Keys)
                {
                    dicWhitePiece.Remove(s);
                }
            }
            else
            {
                foreach (string s in dicBlackPiece.Keys)
                {
                    if (piece == dicBlackPiece[s])
                    {
                        TmpDico.Add(s, dicBlackPiece[s]);
                    }
                }
                foreach (string s in TmpDico.Keys)
                {
                    dicBlackPiece.Remove(s);
                }
            }
        }
        public void ChangePawn(Type type)
        {
            Piece p = lstPlayer[0].LastPiece;
            String strRemove="";
            if(p.Color == strColor1)
            {
                foreach (string s in dicWhitePiece.Keys)
                {
                    if (p == dicWhitePiece[s])
                    {
                        strRemove = s;
                    }
                }
               dicWhitePiece.Remove(strRemove);

                if(type==typeof(Queen))
                {
                    Piece newPiece = new Queen(p.Color);
                    newPiece.PositionX = p.PositionX;
                    newPiece.PositionY = p.PositionY;
                    newPiece.Picture = "";  

                    DicWhitePiece.Add("Queen1" + p.Color, new Queen(p.Color));
                }
                if (type == typeof(Rook))
                {
                    DicWhitePiece.Add("Rook3" + p.Color, new Rook(p.Color));
                }
                if (type == typeof(Bishop))
                {
                    DicWhitePiece.Add("Bishop3" + p.Color, new Bishop(p.Color));
                }
                if (type == typeof(Knights))
                {
                    DicWhitePiece.Add("Knight3" + p.Color, new Knights(p.Color));
                }
            }


        }

        public Dictionary<string,Piece> DicWhitePiece
        {
            get
            {
                return dicWhitePiece;
            }
        }
        public Dictionary<String,Piece> DicBlackPiece
        {
            get
            {
                return dicBlackPiece;
            }
        }
        public List<Player> Players
        {
            get
            {
                return lstPlayer;
            }
        }
        public Piece[][] TabPiece
        {
            get
            {
                return tabPiece;
            }
        }
        public void RemplissageTablePiece()
        {
            foreach(string s in dicWhitePiece.Keys)
            {
                Piece p = dicWhitePiece[s];
                tabPiece[p.PositionY][p.PositionX] = p;
            }
            foreach (string s in dicBlackPiece.Keys)
            {
                Piece p = dicBlackPiece[s];
                tabPiece[p.PositionY][p.PositionX] = p;
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
