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
    public partial class Ocena : Form
    {
        SqlConnection veza = Konekcija.Connect();
        DataTable dt_ocena;

        public Ocena()
        {
            InitializeComponent();
        }

        private void cmb_godinaPopulate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from skolska_godina", veza);
            DataTable dt_godina = new DataTable();
            adapter.Fill(dt_godina);
            cmb_godina.DataSource = dt_godina;
            cmb_godina.ValueMember = "id";
            cmb_godina.DisplayMember = "naziv";
            cmb_godina.SelectedValue = 2;
        }

        private void cmb_profesorPopulate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select distinct osoba.id as id, ime + ' ' + prezime as naziv from osoba join raspodela on osoba.id = nastavnik_id where godina_id = " + cmb_godina.SelectedValue.ToString(), veza);
            DataTable dt_profesor = new DataTable();
            adapter.Fill(dt_profesor);
            cmb_profesor.DataSource = dt_profesor;
            cmb_profesor.ValueMember = "id";
            cmb_profesor.DisplayMember = "naziv";
            cmb_profesor.SelectedValue = -1;
        }

        private void cmb_predmetPopulate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select distinct predmet.id as id, naziv from predmet join raspodela on predmet.id = predmet_id where godina_id = " + cmb_godina.SelectedValue.ToString() + "and nastavnik_id = " + cmb_profesor.SelectedValue.ToString(), veza);
            DataTable dt_predmet = new DataTable();
            adapter.Fill(dt_predmet);
            cmb_predmet.DataSource = dt_predmet;
            cmb_predmet.ValueMember = "id";
            cmb_predmet.DisplayMember = "naziv";
            cmb_predmet.SelectedValue = -1;
        }

        private void cmb_odeljenjePopulate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select distinct odeljenje.id as id, str(razred) + '-' + indeks as naziv from odeljenje join raspodela on odeljenje.id = odeljenje_id where raspodela.godina_id = " + cmb_godina.SelectedValue.ToString() + "and nastavnik_id = " + cmb_profesor.SelectedValue.ToString() + "and predmet_id = " + cmb_predmet.SelectedValue.ToString(), veza);
            DataTable dt_odeljenje = new DataTable();
            adapter.Fill(dt_odeljenje);
            cmb_odeljenje.DataSource = dt_odeljenje;
            cmb_odeljenje.ValueMember = "id";
            cmb_odeljenje.DisplayMember = "naziv";
            cmb_odeljenje.SelectedValue = -1;
        }

        private void cmb_ucenikPopulate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select osoba.id as id, ime + ' ' + prezime as naziv from osoba join upisnica on osoba.id = osoba_id where upisnica.odeljenje_id = " + cmb_odeljenje.SelectedValue.ToString(), veza);
            DataTable dt_ucenik = new DataTable();
            adapter.Fill(dt_ucenik);
            cmb_ucenik.DataSource = dt_ucenik;
            cmb_ucenik.ValueMember = "id";
            cmb_ucenik.DisplayMember = "naziv";
            cmb_ucenik.SelectedValue = -1;
        }

        private void Ocena_Load(object sender, EventArgs e)
        {
            cmb_godinaPopulate();
            cmb_predmet.Enabled = false;
            cmb_ocena.Items.Add(1);
            cmb_ocena.Items.Add(2);
            cmb_ocena.Items.Add(3);
            cmb_ocena.Items.Add(4);
            cmb_ocena.Items.Add(5);
            cmb_odeljenje.Enabled = false;
            cmb_ucenik.Enabled = false;
            cmb_ocena.Enabled = false;
            cmb_profesorPopulate();
        }

        private void cmb_godina_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_godina.IsHandleCreated && cmb_godina.Focused)
            {
                cmb_profesorPopulate();
            }
        }

        private void cmb_profesor_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_profesor.IsHandleCreated && cmb_profesor.Focused)
            {
                cmb_predmetPopulate();
                cmb_predmet.Enabled = true;

                cmb_odeljenje.SelectedIndex = -1;
                cmb_odeljenje.Enabled = false;

                cmb_ucenik.SelectedIndex = -1;
                cmb_ucenik.Enabled = false;

                cmb_ocena.SelectedIndex = -1;
                cmb_ocena.Enabled = false;

                dt_ocena = new DataTable();
                dataGridView1.DataSource = dt_ocena;
            }
        }

        private void cmb_predmet_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_predmet.IsHandleCreated && cmb_predmet.Focused)
            {
                cmb_odeljenjePopulate();
                cmb_odeljenje.Enabled = true;

                cmb_ucenik.SelectedIndex = -1;
                cmb_ucenik.Enabled = false;

                cmb_ocena.SelectedIndex = -1;
                cmb_ocena.Enabled = false;

                dt_ocena = new DataTable();
                dataGridView1.DataSource = dt_ocena;
            }
        }

        private void cmb_odeljenje_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_odeljenje.IsHandleCreated && cmb_odeljenje.Focused)
            {
                cmb_ucenikPopulate();
                cmb_ucenik.Enabled = true;
                GridPopulate();
                uceoceidSet(0);

                cmb_ocena.Enabled = true;

            }
        }

        public void GridPopulate()
        {
            StringBuilder command = new StringBuilder("select ocena.id as id, ime + ' ' + prezime as naziv, ocena, ucenik_id, datum from osoba ");
            command.Append("join ocena on osoba.id = ucenik_id ");
            command.Append("join raspodela on raspodela_id = raspodela.id ");
            command.Append("where raspodela_id = ");
            command.Append("(select id from raspodela ");
            command.Append("where godina_id = " + cmb_godina.SelectedValue.ToString());
            command.Append(" and nastavnik_id = " + cmb_profesor.SelectedValue.ToString());
            command.Append(" and predmet_id = " + cmb_predmet.SelectedValue.ToString());
            command.Append(" and odeljenje_id = " + cmb_odeljenje.SelectedValue.ToString() + ")");

            SqlDataAdapter adapter = new SqlDataAdapter(command.ToString(), veza);
            dt_ocena = new DataTable();
            adapter.Fill(dt_ocena);
            dataGridView1.DataSource = dt_ocena;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns["ucenik_id"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                uceoceidSet(e.RowIndex);
            }
        }

        public void uceoceidSet(int broj_sloga)
        {
            cmb_ucenik.SelectedValue = dt_ocena.Rows[broj_sloga]["ucenik_id"];
            cmb_ocena.SelectedItem = dt_ocena.Rows[broj_sloga]["ocena"];
            txt_id.Text = dt_ocena.Rows[broj_sloga]["id"].ToString();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder command = new StringBuilder("Select id from raspodela ");
            command.Append("where godina_id = " + cmb_godina.SelectedValue.ToString());
            command.Append(" and nastavnik_id = " + cmb_profesor.SelectedValue.ToString());
            command.Append(" and predmet_id = " + cmb_predmet.SelectedValue.ToString());
            command.Append(" and odeljenje_id = " + cmb_odeljenje.SelectedValue.ToString());
            // textBox2.Text = command.ToString();
            SqlCommand order = new SqlCommand(command.ToString(), veza);
            int id_raspodela = 0;
            try
            {
                veza.Open();
                id_raspodela = (int) order.ExecuteScalar();
                veza.Close();
            }
            catch(Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }

            if(id_raspodela > 0)
            {
                StringBuilder command2 = new StringBuilder("insert into ocena (datum, raspodela_id, ucenik_id, ocena) values( '");
                DateTime datumcic = datum.Value;
                command2.Append(datumcic.ToString("yyyy-MM-dd") + "', '");
                command2.Append(id_raspodela.ToString() + "', '");
                command2.Append(cmb_ucenik.SelectedValue.ToString() + "', '");
                command2.Append(cmb_ocena.SelectedItem.ToString() + "')");
                order = new SqlCommand(command2.ToString(), veza);
                try
                {
                    veza.Open();
                    order.ExecuteNonQuery();
                    veza.Close();
                }
                catch (Exception Greska)
                {
                    MessageBox.Show(Greska.Message);
                }
            }

            GridPopulate();

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txt_id.Text) > 0)
            {
                DateTime datumce = datum.Value;
                StringBuilder build = new StringBuilder("update ocena set ");
                build.Append(" ucenik_id = '" + cmb_ucenik.SelectedValue.ToString() + "', ");
                build.Append(" ocena = '" + cmb_ocena.SelectedItem.ToString() + "', ");
                build.Append(" datum = '" + datumce.ToString("yyyy-MM-dd") + "' ");
                build.Append("where id = " + txt_id.Text);
                SqlCommand order2 = new SqlCommand(build.ToString(), veza);
                try
                {
                    veza.Open();
                    order2.ExecuteNonQuery();
                    veza.Close();
                }
                catch (Exception Greska)
                {
                    MessageBox.Show(Greska.Message);
                }
            }
            GridPopulate();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_id.Text) > 0)
            {
                string delete = "delete from ocena where id = " + txt_id.Text;
                SqlCommand komanda= new SqlCommand(delete.ToString(), veza);
                try
                {
                    veza.Open();
                    komanda.ExecuteNonQuery();
                    veza.Close();
                    GridPopulate();
                    uceoceidSet(0);
                }
                catch (Exception Greska)
                {
                    MessageBox.Show(Greska.Message);
                }
            }
        }
    }
}
