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
        private frmGame Display;
        private Piece[][] tabPiece;
        private int iPawnChange;
        private int iConterEqualGame;

        //constructeur
        public Game(frmGame form)
        {
            iNumerberPiece = 32;
            iNumberPiecePerColor = iNumerberPiece / 2;
            lstPlayer = new List<Player>();
            dicWhitePiece = new Dictionary<string, Piece>();
            dicBlackPiece = new Dictionary<string, Piece>();

            
            Display = form;
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
            string Color = strColor2;

            dicBlackPiece.Add("Rook1" + Color, new Rook(Color));
            dicBlackPiece.Add("Knights1" + Color, new Knights(Color));
            dicBlackPiece.Add("Bishop1" + Color, new Bishop(Color));
            dicBlackPiece.Add("Queen" + Color, new Queen(Color));
            dicBlackPiece.Add("King" + Color, new King(Color));
            dicBlackPiece.Add("Bishop2" + Color, new Bishop(Color));
            dicBlackPiece.Add("Knights2" + Color, new Knights(Color));
            dicBlackPiece.Add("Rook2" + Color, new Rook(Color));

            for (int i =1; i<9;i++)
            {
                dicBlackPiece.Add("Pawn" + i + Color, new Pawn(Color));
            }
            Color = strColor1;

            for (int i = 1; i < 9; i++)
            {
                dicWhitePiece.Add("Pawn" + i + Color, new Pawn(Color));
            }
            dicWhitePiece.Add("Rook1" + Color, new Rook(Color));
            dicWhitePiece.Add("Knights1" + Color, new Knights(Color));
            dicWhitePiece.Add("Bishop1" + Color, new Bishop(Color));
            dicWhitePiece.Add("Queen" + Color, new Queen(Color));
            dicWhitePiece.Add("King" + Color, new King(Color));
            dicWhitePiece.Add("Bishop2" + Color, new Bishop(Color));
            dicWhitePiece.Add("Knights2" + Color, new Knights(Color));
            dicWhitePiece.Add("Rook2" + Color, new Rook(Color));







        }

        /// <summary>
        /// ajoute dans la liste des joueurs deux joueurs reçu en paramètre
        /// </summary>
        /// <param name="Player1"></param>
        /// <param name="Player2"></param>
        public void CreatPlayer(Player Player1, Player Player2)
        {
            lstPlayer.Add(Player1);
            lstPlayer.Add(Player2);
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
            FillTablePiece();
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
        public void FillTablePiece()
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
        }

        public void AskMovePlayer()
        {
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

                copyGame.Players[0].LastPosition = Coup;

                if(copyGame.tabPiece[colonne][ligne]!=null)
                {
                    copyGame.CatchUpPiece(copyGame.tabPiece[colonne][ligne]);
                }
                copyGame.tabPiece[pieceClone.PositionY][pieceClone.PositionX] = null;
                pieceClone.SetPosition(copyGame.Players[0].LastPosition);
                copyGame.tabPiece[pieceClone.PositionY][pieceClone.PositionX] = pieceClone;
                copyGame.lstPlayer[0].LastPiece = pieceClone;

                if(copyGame.IsKingCheck(copyGame.lstPlayer[0].LastPiece.Color))
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
            Game clone =new Game(Display);
            clone.iNumerberPiece = this.iNumerberPiece;
            clone.iNumberPiecePerColor = this.iNumberPiecePerColor;
            
            foreach(Player p in this.lstPlayer)
            {
                clone.lstPlayer.Add(p.Clone());
            }

            foreach(string s in dicBlackPiece.Keys)
            {
                Piece p = dicBlackPiece[s].Clone();
                clone.dicBlackPiece.Add(s, p);
                clone.tabPiece[p.PositionY][p.PositionX] = p;
            }
            foreach (string s in dicWhitePiece.Keys)
            {
                Piece p = dicWhitePiece[s].Clone();
                clone.dicWhitePiece.Add(s, p);
                clone.tabPiece[p.PositionY][p.PositionX] = p;
            }
            clone.FillTablePiece();
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
                DoBigRock(color);
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
                Display.PlayDisplayMove();
                Play();

                lstPlayer[0].LastPiece = tabPiece[7][4];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 6;
                Display.PlayDisplayMove();
                Play();
            }
            else
            {
                lstPlayer[0].LastPiece = tabPiece[0][7];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 5;
                if(Display.IsGameTurned)
                {
                    Display.TurnGame();
                }
                Display.PlayDisplayMove();
                Play();


                lstPlayer[0].LastPiece = tabPiece[0][4];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 6;
                Display.PlayDisplayMove();
                Play();
            }
        }

        /// <summary>
        /// écécute un grand rock de la couleur reçu
        /// </summary>
        /// <param name="color"></param>
        private void DoBigRock(string color)
        {
            if (color == strColor1)
            {
                lstPlayer[0].LastPiece = tabPiece[7][0];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 3;
                Display.PlayDisplayMove();
                Play();

                lstPlayer[0].LastPiece = tabPiece[7][4];
                lstPlayer[0].LastPosition[0] = 7;
                lstPlayer[0].LastPosition[1] = 2;
                Display.PlayDisplayMove();
                Play();
            }
            else
            {
                lstPlayer[0].LastPiece = tabPiece[0][0];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 3;
                if (Display.IsGameTurned)
                {
                    Display.TurnGame();
                }
                Display.PlayDisplayMove();
                Play();

                lstPlayer[0].LastPiece = tabPiece[0][4];
                lstPlayer[0].LastPosition[0] = 0;
                lstPlayer[0].LastPosition[1] = 2;
                Display.PlayDisplayMove();
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
                if (tabPiece[7][7] == null)
                {
                    return false;
                }
                if (tabPiece[7][7].GetType() != typeof(Rook))
                {
                    return false;
                }
                if (tabPiece[7][7].IsAlreadyMove)
                {
                    return false;
                }
                    
                if (tabPiece[7][6] != null || tabPiece[7][5] != null)
                {
                    return false;
                }
                if (tabPiece[7][4] == null)
                {
                    return false;
                }
                if (tabPiece[7][4].GetType()!=typeof(King))
                {
                    return false;
                }
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
            else
            {

                if (tabPiece[0][7] == null)
                {
                    return false;
                }
                if (tabPiece[0][7].GetType()!=typeof(Rook))
                {
                    return false;
                }
                if(tabPiece[0][7].IsAlreadyMove)
                {
                    return false;
                }
                if (tabPiece[0][6] != null || tabPiece[0][5] != null)
                {
                    return false;
                }
                if (tabPiece[0][4] == null)
                {
                    return false;
                }
                if (tabPiece[0][4].GetType()!=typeof(King))
                {
                    return false;
                }
                King k = (King)tabPiece[0][4].Clone();
                if (k.IsAlreadyMove)
                {
                    return false;
                }
                k.PositionX = 5;
                if (k.IsCheck(this))
                {
                    return false;
                }
                k.PositionX = 6;
                if (k.IsCheck(this))
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
            int NewY = lstPlayer[0].LastPosition[0];
            int NewX = lstPlayer[0].LastPosition[1];
            //si un pion se déplace de deux cases il regarde à gauche et à droite si il y a un pion advserse, si tel est le cas il lui initialise un passing a true
            if (p.GetType() == typeof(Pawn) && !p.IsAlreadyMove)
            {
                if (p.Color == strColor1)
                {
                    if (lstPlayer[0].LastPosition[0] == 4)
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
            if (p.GetType() == typeof(Pawn))
            {
                // série de test pour savion si le pion qui vient de se déplacer effectue une prise en passant

                if (p.PositionY + 1 == NewY && p.PositionX + 1 == NewX)
                {
                    IsPawnBlackUsedPassing(NewY, NewX);
                }
                if (p.PositionY + 1 == NewY && p.PositionX - 1 == NewX)
                {
                    IsPawnBlackUsedPassing(NewY, NewX);
                }
                if (p.PositionY - 1 == NewY && p.PositionX + 1 == NewX)
                {
                    IsPawnWhiteUsedPassing(NewY, NewX);
                }
                if (p.PositionY - 1 == NewY && p.PositionX - 1 == NewX)
                {
                    IsPawnWhiteUsedPassing(NewY, NewX);
                }
            }
            if (tabPiece[NewY][NewX] != null)
            {
                CatchUpPiece(tabPiece[NewY][NewX]);
            }

            tabPiece[p.PositionY][p.PositionX] = null;
            p.SetPosition(lstPlayer[0].LastPosition);
            tabPiece[p.PositionY][p.PositionX] = p;
            p.IsAlreadyMove = true;

            //pour chaque pièce adverse défini un passing à faux
            Dictionary<string, Piece> d;
            if (p.Color == strColor1)
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


        /// <summary>
        /// test si il y a un passing à gauche et à droite du dernier pion jouer et si tel est le cas défini une variable à true pour le pion concerné
        /// </summary>
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
                        Adj.SetPassingRight(true);
                    }
                }
                Adj = tabPiece[positionY][positionX + 1];
                if (Adj != null)
                {
                    if (Adj.GetType() == typeof(Pawn))
                    {
                       Adj.SetPassingLeft(true);
                    }
                }
            }
        }

        /// <summary>
        /// est appelé lorsqu'on déplace un pion blanc pour savoir si il est entrain de faire du passing, si tel est le cas la méthode CatchupPiece est appelée pour gerer la capture du pion pris en passant   
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        private void IsPawnWhiteUsedPassing(int Y, int X)
        {
            if (tabPiece[Y][X] == null)
            {
                CatchUpPiece(tabPiece[Y + 1][X]);
                Display.RemoveFromDisplay(Y + 1, X);
            }
        }

        /// <summary>
        /// est appelé lorsqu'on déplace un pion noir pour savoir si il est entrain de faire du passing, si tel est le cas la méthode CatchupPiece est appelée pour gerer la capture du pion pris en passant   
        /// </summary>
        /// <param name="Y"></param>
        /// <param name="X"></param>
        private void IsPawnBlackUsedPassing(int Y, int X)
        {
            if(tabPiece[Y][X]==null)
            {
                CatchUpPiece(tabPiece[Y - 1][X]);
                Display.RemoveFromDisplay(Y-1, X);
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
        public bool IsKingCheck(string color)
        {
            King k = null;
            Piece piece = lstPlayer[0].LastPiece;
            Dictionary<string, Piece> dTemp;
            string Key;
            if (color == strColor1)
            {
                dTemp = dicWhitePiece;
                Key = "KingWhite";
            }
            else
            {
                dTemp = dicBlackPiece;
                Key = "KingBlack";
                
            }
            k = (King)dTemp[Key].Clone();
            if (k.GetType() == piece.GetType())
            {
                k.PositionX = piece.PositionX;
                k.PositionY = piece.PositionY;
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
            tabPiece[piece.PositionY][piece.PositionX] = null;
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
            Display.SetPicture(newPiece);


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

            FillTablePiece();
        }
   
        /// <summary>
        /// méthode vérifiant si il y a match nul
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsDraw(string color)
        {
            if (iConterEqualGame==3)
            {
                return true;
            }
            if(IsPossibleMove(color))
            {
                return true;
            }
            return false;
        }

        private bool IsPossibleMove(string color)
        {
            Game CopyGame = this.Clone();
            if (color == strColor1)
            {
                foreach (Piece p in CopyGame.DicWhitePiece.Values)
                {
                    p.SetPossibleMoves(this);
                    CopyGame.NoCheck(p);
                    if (p.Move.Count != 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (Piece p in CopyGame.DicBlackPiece.Values)
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
        /// <summary>
        /// méthode véifiant si il y a échec et math
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsCheckmate(string color )
        {
            if(IsKingCheck(color)&&IsPossibleMove(color))
            {
                return true;
            }
            return false;
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
            set
            {
                dicWhitePiece = value;
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
            set
            {
                dicBlackPiece = value;
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
        public int ConterEqualGame
        {
            get
            {
                return iConterEqualGame;
            }
            set
            {
                iConterEqualGame = value;
            }
        }

        public Piece[][] TabPiece
        {
            get
            {
                return tabPiece;
            }
            set
            {
                tabPiece = value;
            }
        }
    }
}
