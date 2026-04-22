using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TAKSİT_TAKİP
{
    public partial class Ana_Sayfa : Form
    {
        public Ana_Sayfa()
        {
            InitializeComponent();
        }

        private void taksitYatırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Hide();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
