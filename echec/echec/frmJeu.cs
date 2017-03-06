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
        public frmJeu()
        {
            InitializeComponent();
        }
        public frmJeu(string nomJoueur1, string nomJoueur2)
            :this()
        {

        }
    }
}
