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
        private TableLayoutPanel tlpAffichage;
        private int iColonne;
        private int iLigne;
        private Jeu jeu;
        private List<String> imagePiece;

        public frmJeu()
        {
            InitializeComponent();
            jeu = new Jeu(this);
            jeu.Creationpiece();
            jeu.CreationJoueur(new Humain(), new Humain());
            jeu.PositionPiece();
            imagePiece = new List<string>();

            ChargementListImage();
            AjoutImageParPiece();

            tlpAffichage = new TableLayoutPanel();
            tlpAffichage.Location = new Point(0, 0);
            tlpAffichage.AutoScroll = true;
            tlpAffichage.AutoSize = true;
            Controls.Add(tlpAffichage);
            GenerationPlateau();
            PlacementPiece();

        }
        public frmJeu(string nomJoueur1, string nomJoueur2)
            :this()
        {
           
        }
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GenerationPlateau()
        {
            iColonne = 8;
            iLigne = 8;

            tlpAffichage.ColumnStyles.Clear();
            tlpAffichage.Controls.Clear();
            tlpAffichage.ColumnCount = iColonne;
            tlpAffichage.RowCount = iLigne;

            bool bColor = true;
            for(int i=0; i<iColonne;i++)
            {
                bColor = !bColor;
                for(int j=0; j<iLigne;j++)
                {
                    PictureBox pct = new PictureBox();
                    if (bColor)
                    {
                        pct.BackColor = Color.Black;
                    }
                    else
                    {
                        pct.BackColor = Color.White;
                    }
                    bColor = !bColor;
                    pct.Size = new Size(this.Size.Width / 9, this.Size.Height / 9);
                    tlpAffichage.Controls.Add(pct);

                    pct.Tag = i.ToString() + j.ToString();

                    pct.Click += new System.EventHandler(PictureBox_click);
                }
            }

        }

        private void PlacementPiece()
        {
            int x = 0;
            int y = 0;
            for (int i=0; i<jeu.TabPiece.Length*jeu.TabPiece[0].Length;i++)
            {
                
                if(jeu.TabPiece[x][y]!=null)
                {
                    AffichagePiece(jeu.TabPiece[x][y].Image, (PictureBox)tlpAffichage.Controls[i]);
                }
                y++;
                if(y%8==0)
                {
                    y = 0;
                    x++;
                }
            }
        }
        private void ChargementListImage()
        {
            string path = "ressource";
            foreach (string sFileName in  Directory.GetFiles(path))
            {
                if (Path.GetExtension(sFileName) == ".png")
                {
                    imagePiece.Add(Path.GetFileName(sFileName));
                }
            }


        }
        private void AffichagePiece(string strPiece, PictureBox pct)
        {
            FileStream fs = new FileStream(@"ressource\"+strPiece, FileMode.Open);
            pct.Image = Image.FromStream(fs);
            fs.Close();
            pct.SizeMode = PictureBoxSizeMode.CenterImage;
        }
        private void AjoutImageParPiece()
        {
           
            foreach(Piece p in jeu.LstPiece)
            {
                int index = 0;
                if(p.Couleur==jeu.Couleur2)
                {
                    index += 6;
                }

               switch(p.ToString())
                {
                    case "echec.Cavalier":
                        index += 1;
                        break;
                    case "echec.Fou":
                        index += 2;
                        break;
                    case "echec.Reine":
                        index += 3;
                        break;
                    case "echec.Roi":
                        index += 4;
                        break;
                    case "echec.Pion":
                        index += 5;
                        break;
                }
                p.Image = imagePiece[index];
            }
        }
        private void TournePlateau()
        {
            jeu.TournePlateau();
            PlacementPiece();


        }

        private void PictureBox_click(object sender, EventArgs e)
        {
            PictureBox pct = (PictureBox)sender;

            string s = pct.Tag.ToString();

            List<String> Deplacement = jeu.DeplacementPiece(s);

        }


    }
}
