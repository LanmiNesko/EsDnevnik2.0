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
    public partial class Raspodela : Form
    {
        int broj_sloga = 0;
        DataTable raspodela;
        public Raspodela()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM raspodela", veza);
            raspodela = new DataTable();
            adapter.Fill(raspodela);
        }

        private void ComboFill()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter;
            DataTable data_table_godina, data_table_nastavnik, data_table_predmet, data_table_odeljenje;

            adapter = new SqlDataAdapter("SELECT * FROM skolska_godina", veza);
            data_table_godina = new DataTable();
            adapter.Fill(data_table_godina);

            adapter = new SqlDataAdapter("SELECT id, ime + prezime as naziv FROM osoba WHERE uloga = 2", veza);
            data_table_nastavnik = new DataTable();
            adapter.Fill(data_table_nastavnik);

            adapter = new SqlDataAdapter("SELECT id, naziv FROM predmet", veza);
            data_table_predmet = new DataTable();
            adapter.Fill(data_table_predmet);

            adapter = new SqlDataAdapter("SELECT id, STR(razred) + '-' + indeks as naziv FROM odeljenje", veza);
            data_table_odeljenje = new DataTable();
            adapter.Fill(data_table_odeljenje);

            cmb_godina.DataSource = data_table_godina;
            cmb_godina.ValueMember = "id";
            cmb_godina.DisplayMember = "naziv";           

            cmb_nastavnik.DataSource = data_table_nastavnik;
            cmb_nastavnik.ValueMember = "id";
            cmb_nastavnik.DisplayMember = "naziv";            

            cmb_predmet.DataSource = data_table_predmet;
            cmb_predmet.ValueMember = "id";
            cmb_predmet.DisplayMember = "naziv";           

            cmb_odeljenje.DataSource = data_table_odeljenje;
            cmb_odeljenje.ValueMember = "id";
            cmb_odeljenje.DisplayMember = "naziv";           

            txt_id.Text = raspodela.Rows[broj_sloga]["id"].ToString();

            if (raspodela.Rows.Count == 0)
            {
                cmb_godina.SelectedValue = -1;
                cmb_nastavnik.SelectedValue = -1;
                cmb_predmet.SelectedValue = -1;
                cmb_odeljenje.SelectedValue = -1;
            }
            else
            {
                cmb_godina.SelectedValue = raspodela.Rows[broj_sloga]["godina_id"];
                cmb_nastavnik.SelectedValue = raspodela.Rows[broj_sloga]["nastavnik_id"];
                cmb_predmet.SelectedValue = raspodela.Rows[broj_sloga]["predmet_id"];
                cmb_odeljenje.SelectedValue = raspodela.Rows[broj_sloga]["odeljenje_id"];
            }
            if (broj_sloga == 0)
            {
                btn_first.Enabled = false;
                btn_prev.Enabled = false;
            }
            else
            {
                btn_first.Enabled = true;
                btn_prev.Enabled = true;
            }
            if (broj_sloga == raspodela.Rows.Count - 1)
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

        private void Raspodela_Load(object sender, EventArgs e)
        {
            Load_Data();
            ComboFill();
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            broj_sloga = 0;
            ComboFill();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            broj_sloga--;
            ComboFill();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            broj_sloga++;
            ComboFill();
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            broj_sloga = raspodela.Rows.Count - 1;
            ComboFill();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder naredba_ins = new StringBuilder("INSERT INTO raspodela (godina_id, nastavnik_id, predmet_id, odeljenje_id) VALUES('");
            naredba_ins.Append(cmb_godina.SelectedValue + "', '");
            naredba_ins.Append(cmb_nastavnik.SelectedValue + "', '");
            naredba_ins.Append(cmb_predmet.SelectedValue + "', '");
            naredba_ins.Append(cmb_odeljenje.SelectedValue + "')");
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba_ins.ToString(), veza);
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
            broj_sloga = raspodela.Rows.Count - 1;
            ComboFill();
        }

        private void btn_delete_Click_1(object sender, EventArgs e)
        {
            string naredba_del = "DELETE FROM raspodela WHERE id = " + txt_id.Text;
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
                ComboFill();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            StringBuilder naredba_upd = new StringBuilder("UPDATE raspodela SET ");
            naredba_upd.Append("godina_id = '" + cmb_godina.SelectedValue + "', ");
            naredba_upd.Append("nastavnik_id = '" + cmb_nastavnik.SelectedValue + "', ");
            naredba_upd.Append("predmet_id = '" + cmb_predmet.SelectedValue + "', ");
            naredba_upd.Append("odeljenje_id = '" + cmb_odeljenje.SelectedValue + "'");
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
            ComboFill();
        }
    }
}
