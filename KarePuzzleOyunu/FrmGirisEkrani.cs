using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KarePuzzleOyunu
{
    public partial class FrmGirisEkrani : Form
    {
        public FrmGirisEkrani()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAdSoyad.Text == "")
            {
                MessageBox.Show("Adınızı Soyadınızı Giriniz", "Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                FrmOyunEkrani frmOyunEkrani = new FrmOyunEkrani(txtAdSoyad.Text);
                frmOyunEkrani.Show();
                this.Hide();
            }
        }
    }
}
