using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace KarePuzzleOyunu
{
    public partial class FrmOyunEkrani : Form
    {
        public FrmOyunEkrani(string nameSurname)
        {
            InitializeComponent();
            lblNameSurname.Text = nameSurname;
        }

        private void FrmOyunEkrani_Load(object sender, EventArgs e)
        {
            lblPuan.Text = puan.ToString();
            btnKaristir.Enabled = false;



            //Skoru ekrana yazma//
            string[] lines = File.ReadAllLines(@"C:\\Users\\ylmzo\\Desktop\\KarePuzzleOyunu-master\\KarePuzzleOyunu\\enyuksekskor.txt");
            if (lines.Length > 0)
                label8.Text = lines.Max();
        }

        LinkedList<Image> ListOfObjects = new LinkedList<Image>();
        LinkedList<Button> ListOfObjects2 = new LinkedList<Button>();

        bool debug = false;
        Image[] imgarray = new Image[16];
        Button[] buttons = new Button[16];

        private void btnGorselEkle_Click(object sender, EventArgs e)
        {
            resimSec();
            btnKaristir.Enabled = true;
        }

        private void btnKaristir_Click(object sender, EventArgs e)
        {
            parcalariKaristir();
        }

        public void resimSec()
        {
            String imageLocation = "";

            Random rnd = new Random();

            int[] intArray = new int[16];

            buttons = new Button[] { button1, button5, button9,  button13,
                                     button2, button6, button10, button14,
                                     button3, button7, button11, button15,
                                     button4, button8, button12, button16 };

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "|*.jpg||*.png||*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    var image = Image.FromFile(imageLocation);
                    image = resizeImage(image, new Size(500, 500)); //Görsel boyutunu varsayılan olarak 500x500 olarak ayarlar
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            var index = i * 4 + j;
                            imgarray[index] = new Bitmap(125, 125);
                            var graphics = Graphics.FromImage(imgarray[index]);
                            graphics.DrawImage(image, new Rectangle(0, 0, 125, 125), new Rectangle(i * 125, j * 125, 125, 125), GraphicsUnit.Pixel);
                            graphics.Dispose();

                            button1.BackgroundImage = imgarray[0];
                            button2.BackgroundImage = imgarray[4];
                            button3.BackgroundImage = imgarray[8];
                            button4.BackgroundImage = imgarray[12];
                            button5.BackgroundImage = imgarray[1];
                            button6.BackgroundImage = imgarray[5];
                            button7.BackgroundImage = imgarray[9];
                            button8.BackgroundImage = imgarray[13];
                            button9.BackgroundImage = imgarray[2];
                            button10.BackgroundImage = imgarray[6];
                            button11.BackgroundImage = imgarray[10];
                            button12.BackgroundImage = imgarray[14];
                            button13.BackgroundImage = imgarray[3];
                            button14.BackgroundImage = imgarray[7];
                            button15.BackgroundImage = imgarray[11];
                            button16.BackgroundImage = imgarray[15];
                        }
                    }

                    ListOfObjects = new LinkedList<Image>(imgarray);

                    ListOfObjects.Find(image);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir sorunla karşılaşıldı!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public void parcalariKaristir()
        {

            timer1.Start();
            timer1.Interval = 1;

            buttons = new Button[] { button1, button5, button9,  button13,
                                     button2, button6, button10, button14,
                                     button3, button7, button11, button15,
                                     button4, button8, button12, button16 };

            ListOfObjects2 = new LinkedList<Button>(buttons);

            foreach (var item in ListOfObjects2)
            {
                item.Click += new EventHandler(this.ButtonArray_click);
            }

            var karisik_sirali_sayilar = Enumerable.Range(0, 16).OrderBy(g => Guid.NewGuid()).Take(16).ToArray();
            int karisik_sirali_dizi_indexi = 0;

            foreach (var item in ListOfObjects2)
            {
                var sira = karisik_sirali_sayilar[karisik_sirali_dizi_indexi];
                item.BackgroundImage = imgarray[sira];
                if (!debug)
                {
                    item.Text = "";
                }
                karisik_sirali_dizi_indexi += 1;
            }

            string dosya_yolu = "C:\\Users\\ylmzo\\Desktop\\KarePuzzleOyunu-master\\KarePuzzleOyunu\\enyuksekskor.txt";
            if (!File.Exists(dosya_yolu))
            {
                File.Create(dosya_yolu).Close();

            }

        }

        Button btn1;
        Button btn2;
        int puan = 0;
        int btn_sayac = 0;
        private void ButtonArray_click(object sender, EventArgs e)
        {
            btn_sayac += 1;
            Button btn = ((Button)sender);

            var imgNode = ListOfObjects.First;
            var btnNode = ListOfObjects2.First;

            if (btn_sayac == 1)
            {
                btn1 = btn;
                btn.BackColor = Color.Red;
            }
            else if (btn_sayac == 2)
            {
                btn2 = btn;
                btn_sayac = 0;
                var temp_img = btn2.BackgroundImage;
                btn2.BackgroundImage = btn1.BackgroundImage;
                btn1.BackgroundImage = temp_img;

                checkButton();
            }
        }

        private void checkButton()
        {
            int sayac = 0;
            var imgNode = ListOfObjects.First;
            var btnNode = ListOfObjects2.First;

            for (int i = 0; i < 16; i++)
            {
                if (imgNode.Value == btnNode.Value.BackgroundImage)
                {
                    btnNode.Value.Enabled = false;
                    imgNode = imgNode.Next;
                    btnNode = btnNode.Next;
                    sayac++;
                    puan = puan + 10;
                    lblPuan.Text = puan.ToString();
                }
                else
                {
                    puan = puan - 10;
                    lblPuan.Text = puan.ToString();
                    break;
                }
            }
            if (sayac == 16)
            {
                timer1.Stop();


                StreamWriter Yaz = new StreamWriter("C:\\Users\\ylmzo\\Desktop\\KarePuzzleOyunu-master\\KarePuzzleOyunu\\enyuksekskor.txt");
                Yaz.WriteLine(label2.Text);
                Yaz.Close();
                
                MessageBox.Show("Tebrikler Oyun Bitti Puanınız: " + lblPuan.Text);
                

                

            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        int salise = 0;
        int saniye = 0;
        int dakika = 0;
        int saat = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            salise++;
            if (salise == 100)
            {
                salise = 0;
                saniye++;
            }

            if (saniye == 60)
            {
                saniye = 0;
                dakika++;
            }


            if (dakika == 60)
            {
                dakika = 0;
                saat++;
            }

            lblSalise.Text = salise.ToString();
            lblSaniye.Text = saniye.ToString();
            lblDakika.Text = dakika.ToString();
            lblSaat.Text = saat.ToString();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
