using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Klub1B
{

    public partial class Form1 : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kukysek\source\repos\Klub1B\Klub1B\Database1.mdf;Integrated Security=True");
            cn.Open();
            GetAllMembersRecord();
        }

        private void GetAllMembersRecord()
        {
            cmd = new SqlCommand("Select * from tblMembers", cn);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                SaveInfo();
                ClearInitials();
            }
            else
            {
                MessageBox.Show("ID nemůže být prázdné.");
                txtID.Focus();
            }

            GetAllMembersRecord();
        }

        protected void SaveInfo()
        {
            string QUERY = "INSERT INTO tblMembers" +
            "(Id, Name, Points, City)" +
            "VALUES (@Id, @Name, @Points, @City)";

            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@Id", txtID.Text);
            CMD.Parameters.AddWithValue("@Name", txtName.Text);
            CMD.Parameters.AddWithValue("@Points", txtPoints.Text);
            CMD.Parameters.AddWithValue("@City", txtCity.Text);
            CMD.ExecuteNonQuery();
        }

        protected void ClearInitials()
        {
            txtID.Clear();
            txtName.Clear();
            txtPoints.Clear();
            txtCity.Clear();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from tblMembers where ID =" + txtID.Text, cn);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            ClearInitials();
        }

    }
}
