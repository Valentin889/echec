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
        // déclaration variable 
        private int iNumerberPiece;
        private int iNumberPiecePerColor;
        private List<Player> lstPlayer;
        private Dictionary<string,Piece> dicWhitePiece;
        private Dictionary<string, Piece> dicBlackPiece;
        private string strColor1;
        private string strColor2;
        private frmGame Affichage;
        private Piece[][] tabPiece;
        private int iPawnChange;

        //constructeur
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
            iPawnChange = 0;
        }

        //methode

        /// <summary>
        /// appelé au démarage du jeu la méthode crée les pièces et les ajoutes dans les dictionnaire de pièces correspondant
        /// </summary>
        public void CreatPiece()
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

        /// <summary>
        /// ajoute dans la liste des joueurs deux joueurs reçu en paramètre
        /// </summary>
        /// <param name="joueur1"></param>
        /// <param name="joueur2"></param>
        public void CreatPlayer(Player joueur1, Player joueur2)
        {
            lstPlayer.Add(joueur1);
            lstPlayer.Add(joueur2);
        }

        /// <summary>
        /// appelé au début de la partie cette méthode place les pièces aux bons endroits 
        /// </summary>
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

        /// <summary>
        /// quand le jeu commence cette méthode demande un coup au premier joueur
        /// </summary>
        public void BeginGame()
        {
            lstPlayer[0].Jouer();
        }

        /// <summary>
        /// rempli le tableau de pièce avec les pièces contenu dans les dictionnaire
        /// </summary>
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
        
        /// <summary>
        /// quand un joueur fini de jouer un coup cette méthode est appelé pour faire jouer le joueur suivant
        /// </summary>
        public void NextPlayer()
        {
            lstPlayer.Add(lstPlayer[0]);
            lstPlayer.Remove(lstPlayer[0]);
            lstPlayer[0].Jouer();
        }

        /// <summary>
        /// reçoit une position en paramètre récupère la pièce à cette position et initialise tout les déplacement possible de cette pièce grace à la méthode Storagepossible(Game game) contenu dans pièce
        /// </summary>
        /// <param name="str"></param>
        public void SetMovePiece(Piece p)
        {
            lstPlayer[0].LastPiece = p;
            p.SetPossibleMoves(this);
            if(p==dicBlackPiece["KingBlack"]||p==dicWhitePiece["KingWhite"])
            {
                King k = (King)lstPlayer[0].LastPiece;
                k.AddRock(this);
            }
            NoCheck(lstPlayer[0].LastPiece);
        }

        /// <summary>
        /// la méthode reçoit une pièce en paramètre et joue virtuellement chacun des déplacement possible de cette pièce, quand un coup est joué la méthode va vérifier si le roi se trouve en échec
        /// si tel est le cas le coup est impossible et est donc retiré de la liste des déplacement possible de la pièce reçu
        /// </summary>
        /// <param name="piece"></param>
        public void NoCheck(Piece piece)
        {
            List<String> listTemp = new List<string>();

            foreach(string s in piece.Move)
            {
                Game copyGame = this.Clone();
                Piece pieceClone = piece.Clone();
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

                if(copyGame.isKingCheck(pieceClone))
                {
                    listTemp.Add(s);
                }


            }
            foreach(string s in listTemp)
            {
                piece.Move.Remove(s);
            }
        }

        /// <summary>
        /// cette méthode permet de cloner le jeu intégralement et revoit une parfaite copy
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// exécute le petit ou le grand rock en fonction de la position du roi
        /// </summary>
        /// <param name="color"></param>
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

        /// <summary>
        /// éxécute un petit rock de la couleur reçu
        /// </summary>
        /// <param name="color"></param>
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

        /// <summary>
        /// écécute un grand rock de la couleur reçu
        /// </summary>
        /// <param name="color"></param>
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

        /// <summary>
        /// test si un petit rock est possible
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
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
                    if(k.IsAlreadyMove)
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
                    if (k.IsAlreadyMove)
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

        /// <summary>
        /// test si un grand rock est possible
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsSmallRock(string color)
        {
            if (color == strColor1)
            {
                try
                {
                    Rook r = (Rook)tabPiece[7][7];
                    if(r.IsAlreadyMove)
                    {
                        return false;
                    }
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
                    if (k.IsAlreadyMove)
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
                    if(r.IsAlreadyMove)
                    {
                        return false;
                    }
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
                    if (k.IsAlreadyMove)
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
            return true;
        }

        /// <summary>
        /// cette méthode joue le dernier coup enregistré
        /// </summary>
        public void Play()
        {

            Piece p = lstPlayer[0].LastPiece;
            if(tabPiece[lstPlayer[0].LastPosition[0]][lstPlayer[0].LastPosition[1]]!=null)
            {
                CatchUpPiece(tabPiece[lstPlayer[0].LastPosition[0]][lstPlayer[0].LastPosition[1]]);
            }

            if(p.GetType()==typeof(Pawn)&&!p.IsAlreadyMove)
            {
                if(p.Color==strColor1)
                {

                    if(lstPlayer[0].LastPosition[0]==4)
                    {
                        SetPassing();
                    }
                }
                else
                {
                    if (lstPlayer[0].LastPosition[0] == 3)
                    {
                        SetPassing();
                    }
                }
            }
            tabPiece[p.PositionY][p.PositionX] = null;
            p.SetPosition(lstPlayer[0].LastPosition);
            tabPiece[p.PositionY][p.PositionX] = p;
            p.IsAlreadyMove = true;

            Dictionary<string, Piece> d;
            if(p.Color==strColor1)
            {
                d = dicWhitePiece;
            }
            else
            {
                d = dicBlackPiece;
            }
            foreach (Piece piece in d.Values)
            {
                piece.SetPassingLeft(false);
                piece.SetPassingRight(false);
            }
        

    }

        private void SetPassing()
        {
            int positionY = lstPlayer[0].LastPosition[0];
            int positionX = lstPlayer[0].LastPosition[1];
            if (positionX - 1 >= 0 && positionX + 1 < tabPiece.Length)
            {
                Piece Adj = tabPiece[positionY][positionX - 1];
                if (Adj != null)
                {
                    if (Adj.GetType() == typeof(Pawn))
                    {
                        Pawn PawnAdj = (Pawn)Adj;
                        PawnAdj.IsPassingRight = true;
                    }
                }
                Adj = tabPiece[positionY][positionX + 1];
                if (Adj != null)
                {
                    if (Adj.GetType() == typeof(Pawn))
                    {
                        Pawn PawnAdj = (Pawn)Adj;
                        PawnAdj.IsPassginLeft = true;
                    }
                }
            }
        }

        /// <summary>
        /// test si le pion reçu en paramètre se trouve sur la dernière ligne
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool isPawnLastLine(Pawn p)
        {
            if(p.PositionY==0||p.PositionY==tabPiece.Length-1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// créer une copie du roi de la couleur de joueur actif, si la pièce que l'on reçoit en paramètre est la même que la copie du roi que nous venons de faire nous modifions la position du roi cloner
        /// ensuite la méthode IsCheck(Game game) est appelée et revoit si le roi est en échec ou non 
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public bool isKingCheck(Piece piece)
        {
            King k = null;
            if (lstPlayer[0].Color == strColor1)
            {
                k = (King)dicWhitePiece["KingWhite"].Clone();
                if (k.GetType() == piece.GetType())
                {
                    k.PositionX = piece.PositionX;
                    k.PositionY = piece.PositionY;
                }
            }
            else
            {
                k = (King)dicBlackPiece["KingBlack"].Clone();
                if (k.GetType() == piece.GetType())
                {
                    k.PositionX = piece.PositionX;
                    k.PositionY = piece.PositionY;
                }
            }
            
            return k.IsCheck(this);
        }

        /// <summary>
        /// supprime du dictionnaire correspondant la pièce reçu en paramètre
        /// </summary>
        /// <param name="piece"></param>
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

        /// <summary>
        /// transform un pion en une autre pièce reçu en paramètre
        /// </summary>
        /// <param name="type"></param>
        public void ChangePawn(Type type)
        {
            iPawnChange++;
            Piece p = lstPlayer[0].LastPiece;
            String strRemove = "";
            Piece newPiece = null;
            if (type == typeof(Queen))
            {
                newPiece = new Queen(p.Color);
            }
            if (type == typeof(Rook))
            {
                newPiece = new Rook(p.Color);
            }
            if (type == typeof(Bishop))
            {
                newPiece = new Bishop(p.Color);
            }
            if (type == typeof(Knights))
            {
                newPiece = new Knights(p.Color);
            }
            newPiece.PositionX = p.PositionX;
            newPiece.PositionY = p.PositionY;
            Affichage.SetPicture(newPiece);


            iPawnChange += 2;
            if (p.Color == strColor1)
            {
                foreach (string s in dicWhitePiece.Keys)
                {
                    if (p == dicWhitePiece[s])
                    {
                        strRemove = s;
                    }
                }
                dicWhitePiece.Remove(strRemove);
                
                dicWhitePiece.Add(type.Name + iPawnChange.ToString() + p.Color, newPiece);
                
            }
            else
            {
                foreach (string s in dicBlackPiece.Keys)
                {
                    if (p == dicBlackPiece[s])
                    {
                        strRemove = s;
                    }
                }
                dicBlackPiece.Remove(strRemove);
                dicBlackPiece.Add(type.Name + iPawnChange.ToString() + p.Color, newPiece);
            }
            iPawnChange -= 2;

            RemplissageTablePiece();
        }
   
        /// <summary>
        /// méthode vérifiant si il y a échec et math
        /// </summary>
        /// <param name="Color"></param>
        /// <returns></returns>
        public bool IsCheckMath(string Color)
        {
            Game CopyGame = this.Clone();
            if(Color==strColor1)
            {
                foreach(Piece p in CopyGame.dicWhitePiece.Values)
                {
                    p.SetPossibleMoves(this);
                    CopyGame.NoCheck(p);
                    if(p.Move.Count!=0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Piece p in CopyGame.dicBlackPiece.Values)
                {
                    p.SetPossibleMoves(this);
                    CopyGame.NoCheck(p);
                    if (p.Move.Count != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //propriété

        /// <summary>
        /// renvoie le dictionnaire de pièces blanches 
        /// </summary>
        public Dictionary<string,Piece> DicWhitePiece
        {
            get
            {
                return dicWhitePiece;
            }
        }

        /// <summary>
        /// renvoie le dictionnaire de pièces noires
        /// </summary>
        public Dictionary<String,Piece> DicBlackPiece
        {
            get
            {
                return dicBlackPiece;
            }
        }

        /// <summary>
        /// renvoie la liste des joueurs
        /// </summary>
        public List<Player> Players
        {
            get
            {
                return lstPlayer;
            }
        }

        /// <summary>
        /// renvoie le tableau de pièce
        /// </summary>
        public Piece[][] TabPiece
        {
            get
            {
                return tabPiece;
            }
        }

        /// <summary>
        /// renvoi la couleur 1
        /// </summary>
        public string Color1
        {
            get
            {
                return strColor1;
            }
        }

        /// <summary>
        /// renvoie la couleur deux
        /// </summary>
        public string Color2
        {
            get
            {
                return strColor2;
            }
        }


    }
}
