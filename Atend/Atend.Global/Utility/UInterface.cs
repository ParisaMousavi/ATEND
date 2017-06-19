using System;
using System.Collections;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Win32;

namespace Atend.Global.Utility
{
    public class UInterface
    {
        public string GetInterface()
        {
            string password = "";
            ManagementObjectSearcher searcherProcess = new ManagementObjectSearcher("select * from Win32_Processor");
            //ManagementObjectSearcher searcherHDD = new ManagementObjectSearcher("select * from Win32_PhysicalMedia"); --> shareHDD["SerialNumber"]
            ManagementObjectSearcher searcherHDD = new ManagementObjectSearcher("select * from Win32_DiskDrive");
            try
            {
                string pro = string.Empty;
                string ser = string.Empty;
                foreach (ManagementObject sharePro in searcherProcess.Get())
                {
                    pro = sharePro["ProcessorId"].ToString();
                    break;
                }
                foreach (ManagementObject shareHDD in searcherHDD.Get())
                {
                    ser = shareHDD["Signature"].ToString();
                    break;
                }
                string strNames = "";
                using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"Software\Autodesk\AutoCAD\R17.2\ACAD-7001:409\Applications\ATEND", false))
                {
                    if (Key != null)
                    {
                        strNames = Key.GetValue("VERSION").ToString();
                    }
                }
                strNames = strNames.Substring(6, 7).Replace(".", "");
                DateTime dt = DateTime.Now;
                password = string.Format("{0}{1}{2}{3}", pro, ser, string.Format("{0}{1:00}{2:00}", dt.Year, dt.Month, dt.Day), strNames);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't Get Data Because Of The Followeing Error \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return password;
        }

        public string EnInterface(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        public string DeInterface(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                // Start decrypting.
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return plainText;
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
    }
}
