using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Angela
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void myShowToolTip(TextBox tB, byte[] arr)
        {
            string hexValues = BitConverter.ToString(arr);
            String hexvv = null;
            int a = 0;
            foreach (char c in hexValues)
            {
                a = Convert.ToInt32(c);
                hexvv += Convert.ToString(a, 2);
            }
            toolTip_HEX.SetToolTip(tB, hexvv);
        }
        byte[] myXOR(byte[] arr_text, byte[] arr_key)
        {
            int len_text = arr_text.Length;
            int len_key = arr_key.Length;
            byte[] arr_cipher = new byte[len_text];
            for (int i = 0; i < len_text; i++)
            {
                byte p = arr_text[i];
                byte k = arr_key[i % len_key]; // mod
                byte c = (byte)(p ^ k); // XOR

                arr_cipher[i] = c;
            }
            return arr_cipher;
        }
        string myCipher(TextBox tb_text, TextBox tb_Key, TextBox tb_cipher, string cipher = "")
        {
            string text = tb_text.Text;
            byte[] arr_text;
            if (cipher == "") arr_text = Encoding.UTF8.GetBytes(text);
            else arr_text = Encoding.UTF8.GetBytes(cipher);
            myShowToolTip(tb_text, arr_text); // Створити підказку

            string key = tb_Key.Text;
            byte[] arr_key = Encoding.UTF8.GetBytes(key);
            myShowToolTip(tb_Key, arr_key); // Створити підказку

            byte[] arr_cipher = myXOR(arr_text, arr_key);

            //string cipher = BitConverter.ToString(arr_cipher).Replace(&quot;-&quot;, &quot;&quot;);
            cipher = Encoding.UTF8.GetString(arr_cipher);
            tb_cipher.Text = cipher;
            myShowToolTip(tb_cipher, arr_cipher); // Створити підказку

            return cipher;
        }
        private void button_XOR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Key_IN.Text))
            {
                textBox_P_OUT.Text = textBox_P_IN.Text;
                textBox_C_OUT.Text = textBox_P_IN.Text;
                textbox_C_IN.Text = textBox_P_IN.Text;
                return;
            }
            string cipher = myCipher(textBox_P_IN, textBox_Key_IN, textbox_C_IN); //зашифрування
            textBox_P_OUT.Text = textbox_C_IN.Text;
            textBox_Key_OUT.Text = textBox_Key_IN.Text;
            myCipher(textBox_C_OUT, textBox_Key_OUT, textBox_C_OUT, cipher); //розшифрування
        }
        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_P_IN.Text = "";
            textBox_Key_IN.Text = "";
            textbox_C_IN.Text = "";

            textBox_P_OUT.Text = "";
            textBox_Key_OUT.Text = "";
            textBox_C_OUT.Text = "";
        }

        private void textbox_C_IN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

