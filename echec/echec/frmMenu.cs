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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnJouer_Click(object sender, EventArgs e)
        {
            frmJeu form = new frmJeu(tbxNom1.Text, tbxNom2.Text);
            form.Show();
        }
    }
}
