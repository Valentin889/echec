using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
namespace echec
{
    public partial class frmGame : Form
    {
        // déclaration variable
        private TableLayoutPanel tlpDisplay;
        private int iColonne;
        private int iLigne;
        private Game game;
        private List<String> pictureParts;
        private string strActifColor;
        private bool bTurnedGame;
        private bool bIsGameTurned;
        private Piece[][] DisplayBoardGame;
        private string strNamePlayer1;
        private string strNamePlayer2;

        //contructeur

       /// <summary>
       /// contructeur par défaut initialise les composant
       /// </summary>
        public frmGame()
        {
            InitializeComponent();
            game = new Game(this);
            game.CreatPiece();
            game.CreatPlayer(new Human(game.Color1), new Human(game.Color2));
            game.PositionPiece();
            pictureParts = new List<string>();

            LoadPicture();
            AddPicturePerPiece();

            tlpDisplay = new TableLayoutPanel();
            tlpDisplay.Location = new Point(0, 0);
            tlpDisplay.AutoScroll = true;
            tlpDisplay.AutoSize = true;
            Controls.Add(tlpDisplay);
            LoadBoardGame();
            LoadColor();
            DisplayBoardGame = game.Clone().TabPiece;
            TurnGame();
            PlacementParts();
            strActifColor = game.Color1;
            bIsGameTurned = false;
        }

        /// <summary>
        /// contructeur paramétré, reçoit les noms des joueurs les inscrits en mémoire, reçoit un bool définissant si le jeu tourne à chaque déplacement ou non et appelle le constructeur par défaut
        /// </summary>
        /// <param name="NamePlayer1"></param>
        /// <param name="NamePlayer2"></param>
        public frmGame(string NamePlayer1, string NamePlayer2, bool GameTurn)
            : this()
        {
            strNamePlayer1 = NamePlayer1;
            strNamePlayer2 = NamePlayer2;
            bTurnedGame = GameTurn;
        }

        //méthode

        /// <summary>
        /// bouton permettant de quiter le jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// méthode permettant de charger le plateau de jeu
        /// </summary>
        private void LoadBoardGame()
        {
            iColonne = 8;
            iLigne = 8;

            tlpDisplay.ColumnStyles.Clear();
            tlpDisplay.Controls.Clear();
            tlpDisplay.ColumnCount = iColonne;
            tlpDisplay.RowCount = iLigne;

            for (int i = 0; i < iColonne; i++)
            {
                for (int j = 0; j < iLigne; j++)
                {
                    PictureBox pct = new PictureBox();

                    pct.Size = new Size(this.Size.Width / 9, this.Size.Height / 9);
                    tlpDisplay.Controls.Add(pct);

                    pct.Tag = i.ToString() + "/" + j.ToString();

                    pct.Click += new System.EventHandler(PictureBox_click);
                }
            }

        }

        /// <summary>
        /// méthode permettant de colorer les case en noir ou en blanc
        /// </summary>
        private void LoadColor()
        {
            bool bColor = true;
            for (int i = 0; i < tlpDisplay.ColumnCount; i++)
            {
                bColor = !bColor;
                for (int j = 0; j < tlpDisplay.RowCount; j++)
                {
                    int indice = i * 8 + j;
                    PictureBox pct = (PictureBox)tlpDisplay.Controls[indice];

                    if (bColor)
                    {
                        pct.BackColor = Color.Black;
                    }
                    else
                    {
                        pct.BackColor = Color.White;
                    }
                    bColor = !bColor;
                }
            }


        }

        /// <summary>
        /// méthode chargent les immage de chaque pièce sur la bonne case
        /// </summary>
        private void PlacementParts()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < DisplayBoardGame.Length * DisplayBoardGame[0].Length; i++)
            {

                if (DisplayBoardGame[x][y] != null)
                {
                    PartsDiaplay(DisplayBoardGame[x][y].Picture, (PictureBox)tlpDisplay.Controls[i]);
                }
                else
                {
                    ResetImage((PictureBox)tlpDisplay.Controls[i]);
                }
                y++;
                if (y % 8 == 0)
                {
                    y = 0;
                    x++;
                }
            }
        }

        /// <summary>
        /// méthode permettant de tourner le jeu
        /// </summary>
        public void TurnGame()
        {
            if (bTurnedGame)
            {
                Piece[][] tempHorizontal = new Piece[DisplayBoardGame.Length][];
                Piece[][] tempVertical = new Piece[DisplayBoardGame.Length][];
                for (int i = 0; i < tempHorizontal.Length; i++)
                {
                    tempHorizontal[i] = new Piece[DisplayBoardGame[i].Length];
                    tempVertical[i] = new Piece[DisplayBoardGame[i].Length];
                }

                for (int i = 0; i < DisplayBoardGame.Length; i++)
                {
                    for (int j = 0; j < DisplayBoardGame[i].Length; j++)
                    {
                        tempHorizontal[i][j] = DisplayBoardGame[i][DisplayBoardGame[i].Length - j - 1];

                    }
                }
                for (int i = 0; i < tempHorizontal.Length; i++)
                {
                    for (int j = 0; j < tempHorizontal[i].Length; j++)
                    {
                        tempVertical[i][j] = tempHorizontal[tempHorizontal.Length - i - 1][j];
                    }
                }
                DisplayBoardGame = tempVertical;
                bIsGameTurned = !bIsGameTurned;
            }
        }

        /// <summary>
        /// méthode qui chargent les immages contenu dans un dossier ressource et ajoutes les chemins des ces immages dans une liste de string
        /// </summary>
        private void LoadPicture()
        {
            string path = "ressource";
            foreach (string sFileName in Directory.GetFiles(path))
            {
                if (Path.GetExtension(sFileName) == ".png")
                {
                    pictureParts.Add(Path.GetFileName(sFileName));
                }
            }


        }

        /// <summary>
        /// reçoit le nom d'un pièce et une picturebox, charge l'image de la pièce dans cette picturebox
        /// </summary>
        /// <param name="strPiece"></param>
        /// <param name="pct"></param>
        private void PartsDiaplay(string strPiece, PictureBox pct)
        {
            FileStream fs = new FileStream(@"ressource\" + strPiece, FileMode.Open);
            pct.Image = Image.FromStream(fs);
            fs.Close();
            pct.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        /// <summary>
        /// reçoit une pictureBox en paramète et supprime son image
        /// </summary>
        /// <param name="pct"></param>
        private void ResetImage(PictureBox pct)
        {
            pct.Image = null;
        }

        /// <summary>
        /// appelle pour chaque pièce la méthode set picture pièce
        /// </summary>
        private void AddPicturePerPiece()
        {
            foreach (string s in game.DicWhitePiece.Keys)
            {
                SetPicture(game.DicWhitePiece[s]);
            }

            foreach (string s in game.DicBlackPiece.Keys)
            {
                SetPicture(game.DicBlackPiece[s]);
            }
        }
        
        /// <summary>
        /// reçoit une pièce et lui charge son image
        /// </summary>
        /// <param name="p"></param>
        public void SetPicture(Piece p)
        {
            int index = 0;
            if (p.Color == game.Color2)
            {
                index += 6;
            }

            switch (p.GetType().FullName)
            {
                case "echec.Knights":
                    index += 1;
                    break;
                case "echec.Bishop":
                    index += 2;
                    break;
                case "echec.Queen":
                    index += 3;
                    break;
                case "echec.King":
                    index += 4;
                    break;
                case "echec.Pawn":
                    index += 5;
                    break;
            }
            p.Picture = pictureParts[index];
        }

        /// <summary>
        /// quand l'utilisateur clic sur une picture box si aucune pièce n'est dessus rien ne se passe, si il presse sur une pièce adverse rien ne se passe, si il pressque sur une de ses pièces les déplacements s'affichent
        /// si il presse sur une des picturebox afficher en vert la pièces séléctionner plus tôt se déplace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_click(object sender, EventArgs e)
        {
            PictureBox pct = (PictureBox)sender;

            string s = pct.Tag.ToString();
            string[] t = s.Split('/');
            if (bIsGameTurned)
            {
                int[] Coup = new int[2];
                Coup[0] = Convert.ToInt32(t[0]);
                Coup[1] = Convert.ToInt32(t[1]);
                Coup[0] = DisplayBoardGame.Length - 1 - Coup[0];
                Coup[1] = DisplayBoardGame[Coup[0]].Length - 1 - Coup[1];

                t[0] = Convert.ToString(Coup[0]);
                t[1]=Convert.ToString(Coup[1]);


            }
            if (pct.BackColor == Color.Green)
            {
                int[] move = new int[2];
                move[0] = Convert.ToInt32(t[0]);
                move[1] = Convert.ToInt32(t[1]);

                game.Players[0].LastPosition = move;
                if (bIsGameTurned)
                {
                    TurnGame();
                    PlayDisplayMove();
                }
                else
                {
                    PlayDisplayMove();
                    TurnGame();
                }
                game.Play();
                LoadColor();
                PlacementParts();
                if (game.Players[0].LastPiece.GetType() == typeof(Pawn))
                 {
                     if (game.isPawnLastLine((Pawn)game.Players[0].LastPiece))
                    {
                        CustomMsgBox msg = new CustomMsgBox();
                        msg.ShowDialog();
                        Type Result = msg.Return;
                        game.ChangePawn(Result);

                        int[] MoveDisplay = new int[2];
                        MoveDisplay[0] = move[0];
                        MoveDisplay[1] = move[1];
                        if (bIsGameTurned)
                        {
                            MoveDisplay[0] = DisplayBoardGame.Length - 1 - MoveDisplay[0];
                            MoveDisplay[1] = DisplayBoardGame[move[0]].Length - 1 - MoveDisplay[1];
                        }
                        DisplayBoardGame[MoveDisplay[0]][MoveDisplay[1]] = game.TabPiece[move[0]][move[1]];
                        PlacementParts();
                    }
                }

                if (strActifColor == game.Color1)
                {
                    strActifColor = game.Color2;
                }
                else
                {
                    strActifColor = game.Color1;
                }
                if(game.IsCheckMath(strActifColor))
                {
                    MessageBox.Show("échec et math");
                }
                game.NextPlayer();


            }
            else if(pct.BackColor==Color.Orange)
            {
                int[] Coup = new int[2];
                Coup[0] = Convert.ToInt32(t[0]);
                Coup[1] = Convert.ToInt32(t[1]);
                game.Players[0].LastPosition = Coup;
                game.DoRock(strActifColor);
                PlacementParts();
                LoadColor();
                game.NextPlayer();
                if (strActifColor == game.Color1)
                {
                    strActifColor = game.Color2;
                }
                else
                {
                    strActifColor = game.Color1;
                }
            }
            else
            {
                LoadColor();
                Piece p = game.TabPiece[Convert.ToInt32(t[0])][Convert.ToInt32(t[1])];
                if (p != null)
                {
                    if (strActifColor == p.Color)
                    {
                        game.SetMovePiece(p);

                        List<String> Move = p.Move;
                        List<String> specialMove = new List<string>();
                        if(p.GetType()==typeof(King))
                        {
                            King k = (King)p;
                            specialMove = k.Specialmove;
                        }
                        if (bIsGameTurned)
                        {
                            Move = TurnedList(Move);
                            if(p.GetType()==typeof(King))
                            {
                                specialMove = TurnedList(specialMove);
                            }
                        }
                        ShowTraveling(Move, Color.Green);

                        if(p.GetType()==typeof(King))
                        {
                            Game copiGame = game.Clone();
                            if (copiGame.IsSmallRock(p.Color)||copiGame.IsBigRock(p.Color))
                            {
                                King k = (King)p;
                                ShowTraveling(specialMove, Color.Orange);
                            }
                        }

                    }
                    else
                    {
                        LoadColor();
                    }

                }
                else
                {
                    game.Players[0].LastPiece = null;
                }
            }
        }

        /// <summary>
        /// joue le coup sur le plateau que les utilisateurs voient
        /// </summary>
        public void PlayDisplayMove()
        {
            Piece p = game.Players[0].LastPiece.Clone();
            DisplayBoardGame[p.PositionY][p.PositionX] = null;
            p.SetPosition(game.Players[0].LastPosition);
            DisplayBoardGame[p.PositionY][p.PositionX] = p;
        }

        public void RemoveFromDisplay(int y,int x)
        {
            DisplayBoardGame[y][x]=null;
        }

        /// <summary>
        /// reçoit une liste ^contenant des coordonnées de jeu etinverse leurs ordre
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        private List<String> TurnedList(List<String> lst)
        {
            List<String> Return = new List<string>();

            foreach(string s in lst)
            {
                string[] t = s.Split('/');
                int colonne = Convert.ToInt32(t[0]);
                int ligne = Convert.ToInt32(t[1]);

                colonne = DisplayBoardGame.Length - colonne-1;
                ligne = DisplayBoardGame[colonne].Length - ligne-1;

                Return.Add(Convert.ToString(colonne)+"/"+Convert.ToString(ligne));
            }


            return Return;

        }

        /// <summary>
        /// reçoit une liste de coordonnées ainsi qu'une couleur, défini le fond de chaque picturebox aux coordonnées reçu de la couleur reçu
        /// </summary>
        /// <param name="move"></param>
        /// <param name="c"></param>
        private void ShowTraveling(List<String> move, Color c)
        {
            foreach (string s in move)
            {
                string[] t = s.Split('/');

                int colonne = Convert.ToInt32(t[0]);
                int ligne = Convert.ToInt32(t[1]);

                int numeroListe = colonne * 8 + ligne;

                PictureBox pct = (PictureBox)tlpDisplay.Controls[numeroListe];
                pct.BackColor = c;

            }
        }

        //propriété
        public bool IsGameTurned
        {
            get
            {
                return bIsGameTurned;
            }
        }
    }
}
