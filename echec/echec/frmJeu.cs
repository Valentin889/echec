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

namespace echec
{
    public partial class frmJeu : Form
    {
        private TableLayoutPanel tlpDisplay;
        private int iColonne;
        private int iLigne;
        private Game Game;
        private List<String> pictureParts;
        private string strActifColor;
        private bool bIsEchec;

        public frmJeu()
        {
            InitializeComponent();
            Game = new Game(this);
            Game.Creationpiece();
            Game.CreationJoueur(new Human(Game.Color1), new Human(Game.Color2));
            Game.PositionPiece();
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
            PlacementParts();
            strActifColor = Game.Color1;
        }
        public frmJeu(string nomJoueur1, string nomJoueur2)
            :this()
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
            
            for(int i=0; i<iColonne;i++)
            {
                for(int j=0; j<iLigne;j++)
                {
                    PictureBox pct = new PictureBox();
                    
                    pct.Size = new Size(this.Size.Width / 9, this.Size.Height / 9);
                    tlpDisplay.Controls.Add(pct);

                    pct.Tag = i.ToString() +"/" + j.ToString();

                    pct.Click += new System.EventHandler(PictureBox_click);
                }
            }

        }
        private void LoadColor()
        {
            bool bColor = true;
            for(int i=0; i<tlpDisplay.ColumnCount;i++)
            {
                bColor = !bColor;
                for(int j=0; j<tlpDisplay.RowCount;j++)
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
            for (int i=0; i<Game.TabPiece.Length*Game.TabPiece[0].Length;i++)
            {
                
                if(Game.TabPiece[x][y]!=null)
                {
                    PartsDiaplay(Game.TabPiece[x][y].Image, (PictureBox)tlpDisplay.Controls[i]);
                }
                else
                {
                    ResetImage((PictureBox)tlpDisplay.Controls[i]);
                }
                y++;
                if(y%8==0)
                {
                    y = 0;
                    x++;
                }
            }
        }
        private void LoadPicture()
        {
            string path = "ressource";
            foreach (string sFileName in  Directory.GetFiles(path))
            {
                if (Path.GetExtension(sFileName) == ".png")
                {
                    pictureParts.Add(Path.GetFileName(sFileName));
                }
            }


        }
        private void PartsDiaplay(string strPiece, PictureBox pct)
        {
            FileStream fs = new FileStream(@"ressource\"+strPiece, FileMode.Open);
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
           
            foreach(Piece p in Game.ListPieces)
            {
                int index = 0;
                if(p.Color==Game.Color2)
                {
                    index += 6;
                }

               switch(p.ToString())
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
                p.Image = pictureParts[index];
            }
        }
        private void TurnGameAround()
        {
            Game.TournGameAround();
            PlacementParts();
        }
        private void PictureBox_click(object sender, EventArgs e)
        {
            PictureBox pct = (PictureBox)sender;

            string s = pct.Tag.ToString();
            string[] t = s.Split('/');
            if (pct.BackColor == Color.Green)
            {
                bIsEchec = false;

                int[] Coup = new int[2];
                Coup[0] = Convert.ToInt32(t[0]);
                Coup[1] = Convert.ToInt32(t[1]);

                Game.Players[0].DernierPosition = Coup;
                Game.Play();
                PlacementParts();
                LoadColor();
                Game.NextPlayer();
                TurnGameAround();
                if(strActifColor==Game.Color1)
                {
                    strActifColor = Game.Color2;
                }
                else
                {
                    strActifColor = Game.Color1;
                }
            }
            else
            {
                LoadColor();
                List<String> Deplacement = new List<string>();
                Piece p = Game.TabPiece[Convert.ToInt32(t[0])][Convert.ToInt32(t[1])];
                if (p!=null)
                {

                    if(strActifColor == p.Color)
                    {
                         Game.MovePiece(t);
                         ShowTraveling();
                    }
                    else
                    {
                        LoadColor();
                    }

                }
                else
                {
                    Game.Players[0].DernierePiece = null;
                }
            }
        }
        private void ShowTraveling()
        {
            foreach(string s in Game.Players[0].DernierePiece.Move)
            {
                string[] t = s.Split('/');

                int colonne = Convert.ToInt32(t[0]);
                int ligne = Convert.ToInt32(t[1]);

                int numeroListe = colonne * 8 + ligne;

                PictureBox pct = (PictureBox)tlpDisplay.Controls[numeroListe];
                pct.BackColor = Color.Green;

            }
        }


    }
}
