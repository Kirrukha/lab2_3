using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; // Библиотека для работы с файлами.
using Functions;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class KeyForm : Form
    {
        byte[] mbrData;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        void readmbr()
        {
            SafeFileHandle handle = CreateFile(
            lpFileName: @"\\.\" + Func.FlashDrive,
            dwDesiredAccess: FileAccess.Read,
            dwShareMode: FileShare.ReadWrite,
            lpSecurityAttributes: IntPtr.Zero,
            dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
            dwFlagsAndAttributes: FileAttributes.Normal,
            hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Read))
            {
                this.Invoke(new Action(() => {
                    txtPas.Text = "";
                }));
                mbrData = new byte[512];

                disk.Read(mbrData, 0, 512);

                for (int i = 384; i < Func.Pas.Length + 384; i++)
                {
                    txtPas.Invoke(new Action(() => { txtPas.Text += (char)mbrData[i]; }));

                }

            }
        }

        Func b;
        public KeyForm()
        {
            b = new Func();
            InitializeComponent();
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            /*  
                2) Шифрование принятого пароля "data = txtPas.Text".
                3) Проверка зашифрованного пароля и пароляна флешке(3 попытки).
                Если Введёный == Сохранённый пароли сопадают:
                4) Генерация НОВОГО ключа.
                5) Шифрование пароля.
                6) Запись в файл. 
                7) Запись на флешку.
                8) Передача ключа в конструктор формы "LabelKeyForm1".
                9) Открытие формы "LabelKeyForm1".
                Иначе 
                4) Сообщение об ошибке(еще 2 попытки).
                5) Открытие формы "LogForm" для ввода пароля.
            */

            string EncodePas; // Новый сгенерированный пароль.
            string EnterPas; // Введёный (Зашифрованный) пароль.

            if (txtKey.Text == "") // Если поле для ввода клча пустое. 
            {
                MessageBox.Show("Поле ключа не может быть пустым!");
            }

            else
            { 
                
                // (2).
                EnterPas = b.Encode(Func.Pas, txtKey.Text);

                // (3).
                if (txtPas.Text == EnterPas) // Проверка Введённого и  Сохранённого пароля.
                {

                    // (4).
                    Func.Key = b.GenKey();

                    // (5).
                    EncodePas = b.Encode(Func.Pas, Func.Key);

                    // (6).
                    b.WriteInFile(EncodePas);

                    SafeFileHandle handle = CreateFile(
                    lpFileName: @"\\.\" + Func.FlashDrive,
                    dwDesiredAccess: FileAccess.Read,
                    dwShareMode: FileShare.ReadWrite,
                    lpSecurityAttributes: IntPtr.Zero,
                    dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
                    dwFlagsAndAttributes: FileAttributes.Normal,
                    hTemplateFile: IntPtr.Zero);

                    using (FileStream disk = new FileStream(handle, FileAccess.Read))
                    {
                        mbrData = new byte[512];
                        disk.Read(mbrData, 0, 512);
                    }

                    handle = CreateFile(
                    lpFileName: @"\\.\" + Func.FlashDrive,
                    dwDesiredAccess: FileAccess.Write,
                    dwShareMode: FileShare.ReadWrite,
                    lpSecurityAttributes: IntPtr.Zero,
                    dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
                    dwFlagsAndAttributes: FileAttributes.Normal,
                    hTemplateFile: IntPtr.Zero);

                    using (FileStream disk = new FileStream(handle, FileAccess.Write))
                    {

                        for (int i = 0; i < EncodePas.Length; i++)
                            mbrData[384 + i] = (byte)EncodePas[i];

                        disk.Write(mbrData, 0, 512);
                    }

                    // (7).
                    LabelKeyForm1 f = new LabelKeyForm1();

                    // (8).
                    this.Hide();
                    f.ShowDialog();
                    this.Close();

                }
                else
                {
                    // (4).
                    MessageBox.Show(String.Format("Неправильный пароль или ключ!\nОсталось попыток: {0}", Func.PopytkaNum));
                    Func.PopytkaNum--;

                    if (Func.PopytkaNum < 0)
                    {
                        MessageBox.Show("Попытки закончились!");
                        Application.Exit();
                    }

                    LogForm f = new LogForm();

                    // (5).
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
            }
        }

        private void KeyForm_Load(object sender, EventArgs e)
        {
            Thread potok = new Thread(readmbr);
            potok.Start();
        }
    }
}
