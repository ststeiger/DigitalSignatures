
using System;
using System.Security.Cryptography;


namespace DocumentSigner
{


    public class EncryptDecrypt
    {
        public EncryptDecrypt()
        { } // End Constructor 


        public static RSACryptoServiceProvider CreateProvider()
        {
            string publicPrivateKeyXML = "";
            string publicOnlyKeyXML = "";

            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
                {
                    Flags = CspProviderFlags.UseMachineKeyStore
                });
            rSACryptoServiceProvider.FromXmlString(publicPrivateKeyXML);

            return rSACryptoServiceProvider;
        } // End Function CreateProvider 


        public static string ReadPlainFile()
        {
            string fn = @"/root/Projects/DocumentSigner/DocumentSigner/TestFile.txt";
            return System.IO.File.ReadAllText(fn, System.Text.Encoding.UTF8);
        } // End Function ReadPlainFile 


        public static string ReadCyperFile()
        {
            string fn = @"/root/Projects/DocumentSigner/DocumentSigner/TestFileEnc1.txt";
            fn = "/root/Projects/DocumentSigner/DocumentSigner/TestFileEnc.txt";

            return System.IO.File.ReadAllText(fn, System.Text.Encoding.UTF8).Replace("-", "");
        } // End Function ReadCyperFile 


        public static void WriteCyperFile(string contents)
        {
            string fn = @"/root/Projects/DocumentSigner/DocumentSigner/TestFileEnc.txt";
            System.IO.File.WriteAllText(fn, contents, System.Text.Encoding.UTF8);
        } // End Sub WriteCyperFile 


        public static void EncryptFile()
        {
            bool bSomeSetting = false;
            string inputText = ReadPlainFile();

            using (RSACryptoServiceProvider rsa = CreateProvider())
            {
                byte[] inputTextBytes = System.Text.Encoding.UTF8.GetBytes(inputText);
                byte[] enc = rsa.Encrypt(inputTextBytes, bSomeSetting);

                string hexValue = System.BitConverter.ToString(enc);
                WriteCyperFile(hexValue);
            } // End Using rsa 

        } // End Sub EncryptFile 


        public static void DecryptFile()
        {
            string plainText = null;
            bool bSomeSetting = false;

            using (RSACryptoServiceProvider rsa = CreateProvider())
            {
                string readHexValue = ReadCyperFile();
                byte[] enc2 = HexStringToByteArray(readHexValue);
                byte[] dec = rsa.Decrypt(enc2, bSomeSetting);

                plainText = System.Text.Encoding.UTF8.GetString(dec);
                // System.Windows.Forms.Clipboard.SetText("text to add to clipbäöüÄÖÜ");
                // System.Windows.Forms.Clipboard.SetText("abc", System.Windows.Forms.TextDataFormat.Text);
                // System.Windows.Forms.MessageBox.Show(plainText, "capt", System.Windows.Forms.MessageBoxButtons.OK);
            } // End Using rsa 

            System.Console.WriteLine(plainText);
        } // End Sub DecryptFile 


        public static void Test()
        {
            EncryptFile();
            DecryptFile();
        } // End Sub Test 


        public static void EncryptDecryptFile()
        {
            bool bSomeSetting = false;
            string inputText = ReadPlainFile();

            using (RSACryptoServiceProvider rsa = CreateProvider())
            {
                byte[] inputTextBytes = System.Text.Encoding.UTF8.GetBytes(inputText);
                byte[] enc = rsa.Encrypt(inputTextBytes, bSomeSetting);

                string hexValue = System.BitConverter.ToString(enc);
                WriteCyperFile(hexValue);


                string readHexValue = ReadCyperFile();

                byte[] enc2 = HexStringToByteArray(readHexValue);

                byte[] dec = rsa.Decrypt(enc2, bSomeSetting);
                string plainText = System.Text.Encoding.UTF8.GetString(dec);
                System.Console.WriteLine(plainText);
                // System.Windows.Forms.Clipboard.SetText("text to add to clipbäöüÄÖÜ");
                // System.Windows.Forms.Clipboard.SetText("abc", System.Windows.Forms.TextDataFormat.Text);
                System.Windows.Forms.MessageBox.Show(plainText, "capt", System.Windows.Forms.MessageBoxButtons.OK);
            } // End Using rsa 

        } // End Sub Test 


        public static byte[] HexStringToByteArray(string strHexString)
        {
            int iNumberOfChars = strHexString.Length;
            byte[] baBuffer = new byte[iNumberOfChars / 2];
            for (int i = 0; i <= iNumberOfChars - 1; i += 2)
            {
                baBuffer[i / 2] = System.Convert.ToByte(strHexString.Substring(i, 2), 16);
            }
            return baBuffer;
        } // End Function HexStringToByteArray


    }


} 
