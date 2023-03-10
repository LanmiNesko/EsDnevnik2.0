using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsDnevnik2._0
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void osobaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Osoba form_osoba = new Osoba();
            form_osoba.Show();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raspodela form_raspodela = new Raspodela();
            form_raspodela.Show();
        }

        private void tabelaUceniciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form_Form1 = new Form1();
            form_Form1.Show();
        }

        private void ocenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ocena frm_ocena = new Ocena();
            frm_ocena.Show();
        }
    }
}
