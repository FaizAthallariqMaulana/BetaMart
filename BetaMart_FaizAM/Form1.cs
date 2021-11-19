using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaMart_FaizAM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'betaMart_FaizAMDataSet.BetaMart_FaizAM' table. You can move, or remove it, as needed.
            this.betaMart_FaizAMTableAdapter.Fill(this.betaMart_FaizAMDataSet.BetaMart_FaizAM);
            betaMartFaizAMBindingSource.DataSource = this.betaMart_FaizAMDataSet.BetaMart_FaizAM;

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                    dataGridView.DataSource = betaMartFaizAMBindingSource;
                else
                {
                    var query = from o in this.betaMart_FaizAMDataSet.BetaMart_FaizAM
                                where o.id_barang.ToString() == txtSearch.Text|| o.kode_barang.ToString() == txtSearch.Text || o.nama_barang == txtSearch.Text || o.stok_barang.ToString() == txtSearch.Text || o.harga_barang.ToString() == txtSearch.Text
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    betaMartFaizAMBindingSource.RemoveCurrent();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
               try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "PNG|*.png", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betaMartFaizAMBindingSource.ResetBindings(false);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtKodeBarang.Focus();
                this.betaMart_FaizAMDataSet.BetaMart_FaizAM.AddBetaMart_FaizAMRow(this.betaMart_FaizAMDataSet.BetaMart_FaizAM.NewBetaMart_FaizAMRow());
                betaMartFaizAMBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betaMartFaizAMBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtKodeBarang.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            betaMartFaizAMBindingSource.ResetBindings(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                betaMartFaizAMBindingSource.EndEdit();
                betaMart_FaizAMTableAdapter.Update(this.betaMart_FaizAMDataSet.BetaMart_FaizAM);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betaMartFaizAMBindingSource.ResetBindings(false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    betaMartFaizAMBindingSource.RemoveCurrent();
        }
    }
}
