using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModuleApp
{
    public partial class Form1 : Form
    {
        private char[] alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();   //инициализация алфавита

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            string encrypted = EncryptText(textBoxInput.Text, textBoxKey.Text);

            textBoxOutput.Text = encrypted;
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            string decrypted = DecryptText(textBoxInput.Text, textBoxKey.Text);

            textBoxOutput.Text = decrypted;
        }

        private string EncryptText(string text, string key)
        {
            string fullKey = GetFullKey(text, key);

            char[] encrypted = text.ToCharArray();
            int i_key = 0;
            for (int i = 0; i < text.Length; i++)   // цикл шифровки
            {
                char symbol = text[i];
                if (Char.IsLetter(symbol) && alphabet.Contains(symbol))
                {
                    int offset = Char.IsUpper(symbol) ? 0 : 33;
                    int letterIndex = (GetNInAlphabet(text[i]) + GetNInAlphabet(fullKey[i_key])) % 33 + offset;
                    encrypted[i] = alphabet[letterIndex];
                    i_key++;
                } 
            }

            return new String(encrypted);
        }

        private string DecryptText(string text, string key)
        {
            string fullKey = GetFullKey(text, key);

            char[] decrypted = text.ToCharArray();
            int i_key = 0;
            for (int i = 0; i < text.Length; i++)   // цикл расшифровки
            {
                char symbol = text[i];
                if (Char.IsLetter(symbol) && alphabet.Contains(symbol)){
                    int offset = Char.IsUpper(symbol) ? 0 : 33;
                    int letterIndex = (GetNInAlphabet(text[i]) + 33 - GetNInAlphabet(fullKey[i_key])) % 33 + offset;
                    decrypted[i] = alphabet[letterIndex];
                    i_key++;
                } 
            }

            return new String(decrypted);
        }

        private int GetNInAlphabet(char letter) // Вычисление индекса буквы в алфавите
        {
            int letterIndex = Array.IndexOf(alphabet, letter);
            return Char.IsUpper(letter) ? letterIndex : letterIndex - 33;
        }

        private string GetFullKey(string text, string key) 
        {
            var onlyLetters = new String(text.Where(Char.IsLetter).ToArray());                        // берем из строки text (введенный текст) только буквы
            var fullKeyLength = (int)Math.Ceiling((float)(onlyLetters.Length) / key.Length);          // вычисляем длину ключа для всего текста
            var fullKey = string.Concat(Enumerable.Repeat(key, fullKeyLength));                       // полный ключ для текста (пока что с остатком)
            fullKey = fullKey.Substring(0, fullKey.Length - (fullKey.Length - onlyLetters.Length));   // полный ключ для текста (убираем излишек)

            return fullKey;
        }
    }
}
