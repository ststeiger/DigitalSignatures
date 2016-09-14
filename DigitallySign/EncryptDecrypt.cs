
namespace DigitallySign
{


    public class EncryptDecrypt
    {


        public EncryptDecrypt()
        { } // End Constructor 


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

            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = ProviderFactory.CreateProvider())
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

            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = ProviderFactory.CreateProvider())
            {
                string readHexValue = ReadCyperFile();
                byte[] enc2 = Helper.HexStringToByteArray(readHexValue);
                byte[] dec = rsa.Decrypt(enc2, bSomeSetting);

                plainText = System.Text.Encoding.UTF8.GetString(dec);
                // System.Windows.Forms.Clipboard.SetText("text to add to clipbäöüÄÖÜ");
                // System.Windows.Forms.Clipboard.SetText("abc", System.Windows.Forms.TextDataFormat.Text);
                // System.Windows.Forms.MessageBox.Show(plainText, "capt", System.Windows.Forms.MessageBoxButtons.OK);
            } // End Using rsa 

            System.Console.WriteLine(plainText);
        } // End Sub DecryptFile 


        public static void EncryptDecryptFile()
        {
            bool bSomeSetting = false;
            string inputText = ReadPlainFile();

            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = ProviderFactory.CreateProvider())
            {
                byte[] inputTextBytes = System.Text.Encoding.UTF8.GetBytes(inputText);
                byte[] enc = rsa.Encrypt(inputTextBytes, bSomeSetting);

                string hexValue = System.BitConverter.ToString(enc);
                WriteCyperFile(hexValue);

                string readHexValue = ReadCyperFile();
                byte[] enc2 = Helper.HexStringToByteArray(readHexValue);
                byte[] dec = rsa.Decrypt(enc2, bSomeSetting);
                string plainText = System.Text.Encoding.UTF8.GetString(dec);
                System.Console.WriteLine(plainText);
                // System.Windows.Forms.Clipboard.SetText("text to add to clipbäöüÄÖÜ");
                // System.Windows.Forms.Clipboard.SetText("abc", System.Windows.Forms.TextDataFormat.Text);
                System.Windows.Forms.MessageBox.Show(plainText, "capt", System.Windows.Forms.MessageBoxButtons.OK);
            } // End Using rsa 

        } // End Sub Test 


        public static void Test()
        {
            EncryptFile();
            DecryptFile();
        } // End Sub Test 


    } // End Class EncryptDecrypt


} // End Namespace DigitallySign
