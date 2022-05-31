using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace DES_Encryption
{
    public partial class Form1 : Form
    {
        File file = new File();

        public Form1()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)      // Užšifruoti
        {
            try
            {
                DES des = new DES();
                tBoxEncData.Text = des.EncryptData(textBox1.Text, tBoxKeyEncrypt.Text, cbCBCMode.Checked);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)     // Dešifruoti
        {
            try 
            {
                DES des = new DES();
                tBoxDcpData.Text = des.DecryptData(textBox2.Text, tBoxKeyDecrypt.Text,cbCBCMode.Checked);
            }
            catch (Exception)
            {
                MessageBox.Show("Parinktas neteisingas šifravimo režimas");
            }
        }

        private void cbCBCMode_CheckedChanged(object sender, EventArgs e)
        {
            if(cbCBCMode.Checked) { cbECBMode.Checked = false; };
        }

        private void cbECBMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbECBMode.Checked) { cbCBCMode.Checked = false; };
        }

        private void button3_Click(object sender, EventArgs e)  //0139
        {           

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Pasirinkite tekstini faila modifikavimui",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = openFileDialog1.FileName;
                pbFile.Visible = true;
                tbFileText.Text = file.ReadFile(tbPath.Text);
            }
        }

        private void bFileEncrypt_Click(object sender, EventArgs e)
        {
            if (tbFileText.Text == "")
            {
                MessageBox.Show("Tekstinis failas yra tuščias!");
            }
            else
            {
                try 
                {
                    DES des = new DES();
                    file.WriteFile(des.EncryptData(tbFileText.Text, tFileKey.Text, cbCBCMode.Checked), tbPath.Text);
                    tbFileText.Text = file.ReadFile(tbPath.Text);
                    MessageBox.Show("Tekstas esantis faile uzsifruotas!");
                }
                catch (Exception)
                {
                    MessageBox.Show("Parinktas neteisingas šifravimo režimas");
                }
            }
        }

        private void bFileDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                DES des = new DES();
                file.WriteFile(des.DecryptData(tbFileText.Text, tFileKey.Text, cbCBCMode.Checked), tbPath.Text);
                tbFileText.Text = file.ReadFile(tbPath.Text);
                MessageBox.Show("Tekstas dešifruotas!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pbFile_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
