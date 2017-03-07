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


            tlpAffichage = new TableLayoutPanel();
            tlpAffichage.Location = new Point(0, 0);
            tlpAffichage.AutoScroll = true;
            tlpAffichage.AutoSize = true;
            Controls.Add(tlpAffichage);
            GenerationJeu(jeu);

            
        }
        public frmJeu(string nomJoueur1, string nomJoueur2)
            :this()
        {
           
        }
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GenerationJeu(Jeu jeu)
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

                    int y = i + j;
                    string s = jeu.Piece[y].Image;
                    AffichagePiece(s, pct);
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
            
        }

    }
}
