using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            if (txtAdSoyad.Text == "") //Oyuncu adını girmeden oyuna geçemez
            {
                MessageBox.Show("Adınızı Soyadınızı Giriniz", "Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                FrmOyunEkrani frmOyunEkrani = new FrmOyunEkrani(txtAdSoyad.Text); //Girdiği adını soyadını oyun ekranına yolluyoruz
                frmOyunEkrani.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Oyunu kapatır
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] //Program üzerindeki panelin hareketini sağlana kodlar
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
