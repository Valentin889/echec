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
    public partial class CustomMsgBox : Form
    {
        public CustomMsgBox()
        {
            InitializeComponent();
        }

        private void btnQueen_Click(object sender, EventArgs e)
        {
            Return = typeof(Queen);
            this.Close();
        }

        private void btnRook_Click(object sender, EventArgs e)
        {
            Return = typeof(Rook);
            this.Close();
        }

        private void btnBishop_Click(object sender, EventArgs e)
        {
            Return = typeof(Bishop);
            this.Close();
        }

        private void btnKnight_Click(object sender, EventArgs e)
        {
            Return = typeof(Knights);
            this.Close();
        }
        public Type Return { get; private set; }
    }
}
