using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; 
using Functions;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;


namespace WindowsFormsApp1
{
    public partial class LogForm : Form
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

        private Func b;
        public LogForm()
        {
            b = new Func();
            InitializeComponent();
            search_external_drives(comboBox1);
            comboBox1.SelectedIndex = 0;
        }

        // Поиск внешних накопителей.
        private void search_external_drives(ComboBox input) 
        {
            string mydrive;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && (d.DriveType == DriveType.Removable))
                {
                    mydrive = d.Name;
                    input.Items.Add(mydrive);
                }
            }
            if (input.Items.Count == 0)
            {
                input.Items.Add("Внешние носители отсутствуют");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Func.FlashDrive = comboBox1.Text.Remove(2);
            Func.Pas = txtPas.Text;

            // Файл НЕ существует.
            if (!(File.Exists("\\test.txt")))
            {
                /*  
                    1) Генерация ключа.
                    2) Шифрование пароля.
                    3) Запись в  файл.
                    4) Запись на флешку.
                    5) Открытие формы "LabelKeyForm1".
                */
                string EncodePas;

                // (1).
                Func.Key = b.GenKey();

                // (2).
                EncodePas = b.Encode(txtPas.Text, Func.Key);

                // (3).
                b.WriteInFile(EncodePas);

                // (4).
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

                // (5).   
                this.Hide();
                LabelKeyForm1 f = new LabelKeyForm1();
                f.ShowDialog();
                this.Close();
                
            }
            // Файл существует.
            else
            {
                /*  
                    1) Передача пароля в конструктор формы "KeyForm".
                    2) Открытие формы "KeyForm" для ввода ключа.
                */

                // (1).
                KeyForm f = new KeyForm();

                // (2).
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            
        }

    }
}
