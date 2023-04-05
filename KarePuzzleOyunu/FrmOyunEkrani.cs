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

namespace KarePuzzleOyunu
{
    public partial class FrmOyunEkrani : Form
    {
        public FrmOyunEkrani(string nameSurname)
        {
            InitializeComponent();
            lblNameSurname.Text = nameSurname; //Oyuncu ad-soyad bilgisini başlangıç ekranından parametreyle alıp labela bastırıyoruz
        }

        private void FrmOyunEkrani_Load(object sender, EventArgs e)
        {
            lblPuan.Text = puan.ToString(); //Oyun açıldığınında puan olayı başlar
            btnKaristir.Enabled = false;   //Karıştır butonu oyun ilk açıldığında pasif durur
        }

        LinkedList<Image> ListOfObjects = new LinkedList<Image>();   //Görsellerin orijinal sırasını içerisinde tutacak bağlı liste
        LinkedList<Button> ListOfObjects2 = new LinkedList<Button>(); // Karıştırma işlemi sonrası karışık görselleri içinde tutacak bağlı liste

        bool debug = false;
        Image[] imgarray = new Image[16]; //Orijinal görsel sırasını tutan dizi
        Button[] buttons = new Button[16]; // Karışık görsel sırasını tutan dizi

        private void btnGorselEkle_Click(object sender, EventArgs e)
        {
            resimSec(); //Görsel seçme ve listeye ekleme işlemlerini yapan metot
            btnKaristir.Enabled = true; //Karıştır butonu görsel ekledikten sonra aktif olur
        }

        private void btnKaristir_Click(object sender, EventArgs e)
        {
            parcalariKaristir(); //Görselleri karıştıran metot
        }

        public void resimSec()
        {
            String imageLocation = ""; //Yüklenen görsel yolunu tutacak değişkenin tanımlanması

            Random rnd = new Random();

            int[] intArray = new int[16];

            buttons = new Button[] { button1, button5, button9,  button13,
                                     button2, button6, button10, button14,
                                     button3, button7, button11, button15,
                                     button4, button8, button12, button16 }; //Orijinal görselleri içinde tutmak için butonları dizi içine attık

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();    //Görsel seçimi penceresi
                dialog.Filter = "|*.jpg||*.png||*.*";           //Seçilecek içierik türleri filtresi

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName; //Seçilen Görsel Adı

                    var image = Image.FromFile(imageLocation); //Seçilen Görsel uzantısı
                    image = resizeImage(image, new Size(500, 500)); //Görsel boyutunu varsayılan olarak 500x500 olarak ayarlar
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            //Döngü içinde sırayla her bir resmi 125*125 boyutlarına indirip aynı boyuttaki buton içerisine arkaplan resmi olarak ekleme yapıyor
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

                    ListOfObjects = new LinkedList<Image>(imgarray); //Dizi içerisindeki görselleri bağlı liste içerisine ekler

                    ListOfObjects.Find(image);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir sorunla karşılaşıldı!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //Görsek yükleme sırasında hata olursa hata mesajı verir
            }
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size)); //Yüklenen görseli istenen boyutlara indirgeyen bir metot
        }

        public void parcalariKaristir()
        {
            buttons = new Button[] { button1, button5, button9,  button13,
                                     button2, button6, button10, button14,
                                     button3, button7, button11, button15,
                                     button4, button8, button12, button16 };
            //Karıştırılan görselleri tekrar buton içerisine eklemek için dizi tanımlanması

            ListOfObjects2 = new LinkedList<Button>(buttons); //Diziden bağlı liste oluşturduk

            foreach (var item in ListOfObjects2)
            {
                item.Click += new EventHandler(this.ButtonArray_click); //Bütün butonlara click özelliği verdik
            }

            var rasgele_sayi = Enumerable.Range(0, 16).OrderBy(g => Guid.NewGuid()).Take(16).ToArray(); //16ya kadar 16 adet rastgele sayı üretimi
            int rasgele_sayi_sayisi = 0;

            foreach (var item in ListOfObjects2)
            {
                var sira = rasgele_sayi[rasgele_sayi_sayisi]; //Rastgele sayı oluşturulması
                item.BackgroundImage = imgarray[sira]; //Butona görselin basımı
                if (!debug)
                {
                    item.Text = "";      //Buton arkası yazılan silinir
                    item.Enabled = true; //Görsel karıştırıldıktan sonra her buton aktif
                }
                rasgele_sayi_sayisi += 1;
            }
        }

        Button btn1;
        Button btn2;
        int puan = 0;
        int btn_sayac = 0;
        private void ButtonArray_click(object sender, EventArgs e)
        {
            btn_sayac += 1;
            Button btn = ((Button)sender); //Basılan butonu alır

            var imgNode = ListOfObjects.First; //Orijinal görsel düğümü
            var btnNode = ListOfObjects2.First; //Karıştırılan görsel düğümü

            if (btn_sayac == 1) //İlk basılan butonu bir değişken içerisine atar
            {
                btn1 = btn;
                btn.BackColor = Color.Red;
            }
            else if (btn_sayac == 2) //İkinci basılan butonu da bir değişken içine attıktan sonra iki buton üzerindeki görselleri yer değştirir ve buton kontrolü için farklı bir metotda gider
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
            var imgNode = ListOfObjects.First; //Orijinal görsel düğümü
            var btnNode = ListOfObjects2.First; //Karıştırılan görsel düğümü

            for (int i = 0; i < 16; i++)
            {
                if (imgNode.Value == btnNode.Value.BackgroundImage) //Bütün butonların arka plan resmini tek tek kontrol eder doğruluk varsa sonrakine geçer yoksa ömrünü tamamlar
                {
                    btnNode.Value.Enabled = false;
                    imgNode = imgNode.Next;
                    btnNode = btnNode.Next;
                    sayac++;
                    puan = puan + 10; //Doğruya puan artırımı
                    lblPuan.Text = puan.ToString();
                }
                else
                {
                    puan = puan - 10; //Yanlışsa puan azaltımı
                    lblPuan.Text = puan.ToString();
                    break;
                }
            }
            if (sayac == 16)
            {
                MessageBox.Show("Tebrikler Oyun Bitti Puanınız: " + lblPuan.Text); //For döngüsü başarılı şekilde sonuçlanırsa oyun bitti demektir
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] //Program üzerindeki panelin hareketini sağlana kodlar
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Kapama butonu
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FrmGirisEkrani frmGirisEkrani = new FrmGirisEkrani();
            frmGirisEkrani.Show();
            this.Hide();
        }
    }
}
