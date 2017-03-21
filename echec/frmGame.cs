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
        private TableLayoutPanel tlpDisplay;
        private int iColonne;
        private int iLigne;
        private Game game;
        private List<String> pictureParts;
        private string strActifColor;
        private bool bTurnedGame;
        private bool bIsGameTurned;
        private Piece[][] DisplayBoardGame;



        public frmGame()
        {
            InitializeComponent();
            game = new Game(this);
            game.Creationpiece();
            game.CreationJoueur(new Human(game.Color1), new Human(game.Color2));
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
            bTurnedGame = false;
            bIsGameTurned = false;
        }
        public frmGame(string nomJoueur1, string nomJoueur2)
            : this()
        {

        }
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
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

        private void PartsDiaplay(string strPiece, PictureBox pct)
        {
            FileStream fs = new FileStream(@"ressource\" + strPiece, FileMode.Open);
            pct.Image = Image.FromStream(fs);
            fs.Close();
            pct.SizeMode = PictureBoxSizeMode.CenterImage;
        }
        private void ResetImage(PictureBox pct)
        {
            pct.Image = null;
        }
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
        private void SetPicture(Piece p)
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
                int[] Coup = new int[2];
                Coup[0] = Convert.ToInt32(t[0]);
                Coup[1] = Convert.ToInt32(t[1]);

                
                game.Players[0].LastPosition = Coup;

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
                game.NextPlayer();
                PlacementParts();
                if (strActifColor == game.Color1)
                {
                    strActifColor = game.Color2;
                }
                else
                {
                    strActifColor = game.Color1;
                }
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
                        game.SetMovePiece(t);

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
        public void PlayDisplayMove()
        {
            Piece p = game.Clone().Players[0].LastPiece.Clone();
            DisplayBoardGame[p.PositionY][p.PositionX] = null;
            p.PositionY = game.Players[0].LastPosition[0];
            p.PositionX = game.Players[0].LastPosition[1];
            DisplayBoardGame[p.PositionY][p.PositionX] = p;
        }
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
        private void btnTurnGame_Click(object sender, EventArgs e)
        {
            bTurnedGame = true;
        }

        public bool IsGameTurned
        {
            get
            {
                return bIsGameTurned;
            }
        }
    }
}
