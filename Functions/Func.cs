using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; // Библиотека для работы с файлами.

namespace Functions
{

    public class Func
    {

        public static string Key { get; set; }
        public static string FlashDrive { get; set; }
        public static string Pas { get; set; }
        public static int PopytkaNum { get; set; } = 2;

        const string Path = "\\test.txt";

        char[] characters = new char[] { 'A', 'B', 'C', 'D', 'I', 'F', 'G', 'H', 'I', 'J',
                                                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
        // Генерация ключа.
        public string GenKey()
        {
            int length = 5; // Длина пароля.
            Random rand = new Random();

            string Key = "";
            ;
            for(int i = 0; i < length; i++)
                Key += characters[rand.Next(0, characters.Length)];

            return Key;
        }

        // Шифрование.
        public string Encode(string input, string key)
        {

            input = input.ToUpper();
            key = key.ToUpper();

            string EncodeWord = "";

            int keyIndex = 0;

            for (int i = 0; i < input.Length; i++)
            {
                int c = (Array.IndexOf(characters, input[i]) +
                    Array.IndexOf(characters, key[keyIndex])) % characters.Length;

                EncodeWord = EncodeWord + characters[c];

                keyIndex++;

                if ((keyIndex + 1) == key.Length)
                    keyIndex = 0;

            }
            return EncodeWord;
        }

        // Запись в файл.
        public void  WriteInFile(string EncodePas)
        {
            FileStream file = new FileStream(Path, FileMode.Create);
            StreamWriter Writer = new StreamWriter(file);
            Writer.WriteLine("{0}", EncodePas);
            Writer.Close();
        }

        // Считывание из файла.
        public string ReadFromFile()
        {
            String[] UserData;
            // Для считывания зашифрованного пароля из файла.
            FileStream file = new FileStream(Path, FileMode.Open);
            StreamReader Reader = new StreamReader(file);
            UserData = Reader.ReadLine().Split(' ');
            Reader.Close();

            return UserData[0];
        }
    }
}
