using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public frmJeu()
        {
            InitializeComponent();
        }
        public frmJeu(string nomJoueur1, string nomJoueur2)
            :this()
        {
            tlpAffichage = new TableLayoutPanel();
            tlpAffichage.Location = new Point(0, 0);
            tlpAffichage.AutoScroll = true;
            tlpAffichage.AutoSize = true;
            Controls.Add(tlpAffichage);
            GenerationJeu();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GenerationJeu()
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
                }
            }

        }
    }
}
