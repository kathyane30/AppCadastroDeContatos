using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastrodeContatos
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=PC03LAB1779\\SQLEXPRESS01;Initial Catalog=CadastrodeContatos; Trusted_Connection=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int id = 0;
        public Form1()
        {
            InitializeComponent();
            ExibirDados();
        }
        private void ExibirDados()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter(" SELECT * FROM Contato", con);
                adapt.Fill(dt);
                dgvCadastrodeContatos.DataSource = dt;
            }
            catch { }

            finally
            {
                con.Close();
            }
        }
        private void LimparDados()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            id = 0;
        }
        private void btn_about_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Adaptado por Kathyane Jailda", "CadastrodeContatos", MessageBoxButtons.OK,
           MessageBoxIcon.Information);
            txtNome.Focus();
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Sair do Programa?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                txtNome.Focus();
            }
        }
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                if (MessageBox.Show("Deseja deletar este registro ?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        cmd = new SqlCommand("DELETE contato WHERE id=@id", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("registro deletado cpm sucesso...!");
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro : " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        ExibirDados();
                        LimparDados();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um registro para deletar");
            }
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtNome.Focus();

        }
        private void dgvCadastrodeContatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(dgvCadastrodeContatos.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtNome.Text = dgvCadastrodeContatos.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEndereco.Text = dgvCadastrodeContatos.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCelular.Text = dgvCadastrodeContatos.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtTelefone.Text = dgvCadastrodeContatos.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmail.Text = dgvCadastrodeContatos.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch { }
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEndereco.Text != "" && txtCelular.Text != "" && txtTelefone.Text != "" && txtEmail.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE contato SET nome=@nome, endereco=@endereco, celular=@celular, telefone=@telefone, email=@email WHERE id=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@celular", txtCelular.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro atualizado com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (txtNome.Text != "" && txtEndereco.Text != "" && txtCelular.Text != "" && txtTelefone.Text != "" && txtEmail.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO Contato(nome,endereco,celular,telefone,email) VALUES(@nome, @endereco, @celular, @telefone, @email)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@celular", txtCelular.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro incluido com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }
    }
   
}
