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
    public partial class Form1 : Form
    {
        SqlConnection veza = Konekcija.Connect();
        DataTable dt_ocena, dt_ocena_j;
        int index;
        public Form1()
        {
            InitializeComponent();
        }

        private void ucenik_populate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, ime+' '+prezime as naziv FROM Osoba WHERE uloga = 1", veza);
            DataTable dt_ucenik = new DataTable();
            adapter.Fill(dt_ucenik);
            comboBox1.DataSource = dt_ucenik;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "naziv";
        }

        private void predmet_populate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Predmet", veza);
            DataTable dt_predmet = new DataTable();
            adapter.Fill(dt_predmet);
            comboBox2.DataSource = dt_predmet;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "naziv";
        }

        private void grid_populate()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ocena join raspodela on raspodela.id = raspodela_id join predmet on predmet.id = predmet_id", veza);
            dt_ocena = new DataTable();
            adapter.Fill(dt_ocena);

            string grid_info = "SELECT osoba.id, ime+' '+prezime as ucenik, naziv as predmet, ocena from ocena join osoba on osoba.id = ucenik_id join raspodela on raspodela_id = raspodela.id join predmet on predmet.id = predmet_id";
            SqlDataAdapter adapter_j = new SqlDataAdapter(grid_info, veza);
            dt_ocena_j = new DataTable();
            adapter_j.Fill(dt_ocena_j);
            dataGridView1.DataSource = dt_ocena_j;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns["id"].Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                index = dataGridView1.CurrentRow.Index;
                comboBox1.SelectedValue = dt_ocena.Rows[index]["ucenik_id"].ToString();
                comboBox2.SelectedValue = dt_ocena.Rows[index]["predmet_id"].ToString();
                textBox1.Text = dt_ocena.Rows[index]["ocena"].ToString();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int final_raspodela;
            //string ucen = comboBox1.SelectedValue.ToString();
            //string pred = comboBox2.SelectedValue.ToString();
            string raspodela_cs = "SELECT raspodela.id as rasp, predmet.id as pred FROM Raspodela JOIN Predmet ON predmet_id = predmet.id";
            SqlDataAdapter adapter_cs = new SqlDataAdapter(raspodela_cs, veza);
            DataTable dt_raspodela = new DataTable();
            adapter_cs.Fill(dt_raspodela);
            int counter = 0;
            while (true)
            {
                if (Convert.ToInt32(dt_raspodela.Rows[counter]["pred"]) == Convert.ToInt32(comboBox2.SelectedValue))
                {
                    final_raspodela = Convert.ToInt32(dt_raspodela.Rows[counter]["rasp"]);
                    break;
                }
                counter++;
            }
            string command = "INSERT INTO OCENA (raspodela_id, ocena, ucenik_id) VALUES (";
            command = command + final_raspodela.ToString() + " , ";
            command = command + textBox1.Text + " , ";
            command = command + comboBox1.SelectedValue.ToString() + " )";

            SqlCommand order = new SqlCommand(command, veza);

            try
            {
                veza.Open();
                order.ExecuteNonQuery();
                veza.Close();
                grid_populate();
            }
            catch(Exception ERROR)
            {
                MessageBox.Show(ERROR.Message);
            }

        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            string command2 = "Delete from ocena where id = " + dt_ocena.Rows[index]["id"].ToString();
            SqlCommand order2 = new SqlCommand(command2, veza);

            try
            {
                veza.Open();
                order2.ExecuteNonQuery();
                veza.Close();
                grid_populate();
            }
            catch (Exception ERROR)
            {
                MessageBox.Show(ERROR.Message);
            }

        }
        private void btn_upd_Click(object sender, EventArgs e)
        {
            string opet_ova_raspodela = "SELECT raspodela.id as rasp, predmet.id as pred FROM Raspodela JOIN Predmet ON predmet_id = predmet.id where predmet_id = " + comboBox2.SelectedValue.ToString();
            SqlCommand order_rasp_pred = new SqlCommand(opet_ova_raspodela, veza);
            veza.Open();
            string value = order_rasp_pred.ExecuteScalar().ToString();
            veza.Close();
            string command3 = "UPDATE ocena SET raspodela_id = '" + value;
            command3 = command3 + "', ocena = " + Convert.ToInt32(textBox1.Text);
            command3 = command3 + ", ucenik_id = '" + comboBox1.SelectedValue.ToString() + "' where id = '";
            command3 = command3 + dt_ocena.Rows[index]["id"].ToString() + "'";

            SqlCommand order3 = new SqlCommand(command3, veza);

            try
            {
                veza.Open();
                order3.ExecuteNonQuery();
                veza.Close();
                grid_populate();
            }
            catch (Exception ERROR)
            {
                MessageBox.Show(ERROR.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string CS = "Data Source = MILAN\\MSSQLSERVER01; Initial Catalog = esdnevnik; Integrated Security = True";
           // veza = new SqlConnection(CS);

            ucenik_populate();
            predmet_populate();
            grid_populate();
        }
    }
}
