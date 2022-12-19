using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EsDnevnik2._0
{
    public partial class Osoba : Form
    {
        int broj_sloga = 0;
        DataTable tabela;
        public Osoba()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM osoba", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }

        private void TxtLoad()
        {
            if (tabela.Rows.Count == 0)
            {
                txt_id.Text = "N/A";
                txt_ime.Text = "N/A";
                txt_prezime.Text = "N/A";
                txt_adresa.Text = "N/A";
                txt_jmbg.Text = "N/A";
                txt_email.Text = "N/A";
                txt_pass.Text = "N/A";
                txt_uloga.Text = "N/A";
                btn_delete.Enabled = false;
            }
            else
            {
                txt_id.Text = tabela.Rows[broj_sloga]["id"].ToString();
                txt_ime.Text = tabela.Rows[broj_sloga]["ime"].ToString();
                txt_prezime.Text = tabela.Rows[broj_sloga]["prezime"].ToString();
                txt_adresa.Text = tabela.Rows[broj_sloga]["adresa"].ToString();
                txt_jmbg.Text = tabela.Rows[broj_sloga]["jmbg"].ToString();
                txt_email.Text = tabela.Rows[broj_sloga]["email"].ToString();
                txt_pass.Text = tabela.Rows[broj_sloga]["pass"].ToString();
                txt_uloga.Text = tabela.Rows[broj_sloga]["uloga"].ToString();
                btn_delete.Enabled = true;
            }
            if(broj_sloga == 0)
            {
                btn_first.Enabled = false;
                btn_prev.Enabled = false;
            }
            else
            {
                btn_first.Enabled = true;
                btn_prev.Enabled = true;
            }
            if(broj_sloga == tabela.Rows.Count - 1)
            {
                btn_last.Enabled = false;
                btn_next.Enabled = false;
            }
            else
            {
                btn_last.Enabled = true;
                btn_next.Enabled = true;
            }
        }
        private void Osoba_Load(object sender, EventArgs e)
        {
            Load_Data();
            TxtLoad();
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            broj_sloga = 0;
            TxtLoad();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            broj_sloga--;
            TxtLoad();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            broj_sloga++;
            TxtLoad();
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            broj_sloga = tabela.Rows.Count - 1;
            TxtLoad();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder naredba_ins = new StringBuilder("INSERT INTO osoba (ime, prezime, adresa, jmbg, email, pass, uloga) VALUES('");
            naredba_ins.Append(txt_ime.Text + "', '");
            naredba_ins.Append(txt_prezime.Text + "', '");
            naredba_ins.Append(txt_adresa.Text + "', '");
            naredba_ins.Append(txt_jmbg.Text + "', '");
            naredba_ins.Append(txt_email.Text + "', '");
            naredba_ins.Append(txt_pass.Text + "', '");
            naredba_ins.Append(txt_uloga.Text + "')");
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba_ins.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch(Exception Greška)
            {
                MessageBox.Show(Greška.Message);
            }
            Load_Data();
            broj_sloga = tabela.Rows.Count - 1;
            TxtLoad();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            StringBuilder naredba_upd = new StringBuilder("UPDATE osoba SET ");
            naredba_upd.Append("ime = '" + txt_ime.Text + "', ");
            naredba_upd.Append("prezime = '" + txt_prezime.Text + "', ");
            naredba_upd.Append("adresa = '" + txt_adresa.Text + "', ");
            naredba_upd.Append("jmbg = '" + txt_jmbg.Text + "', ");
            naredba_upd.Append("email = '" + txt_email.Text + "', ");
            naredba_upd.Append("pass = '" + txt_pass.Text + "', ");
            naredba_upd.Append("uloga = '" + txt_uloga.Text + "'");
            naredba_upd.Append("WHERE id = " + txt_id.Text);
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba_upd.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greška)
            {
                MessageBox.Show(Greška.Message);
            }
            Load_Data();
            TxtLoad();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string naredba_del = "DELETE FROM osoba WHERE id = " + txt_id.Text;
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba_del, veza);
            Boolean brisan = false;
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                brisan = true;
            }
            catch (Exception Greška)
            {
                MessageBox.Show(Greška.Message);
            }
            if (brisan)
            {
                Load_Data();
                if (broj_sloga > 0) broj_sloga--;
                TxtLoad();
            }
            
        }
    }
}
