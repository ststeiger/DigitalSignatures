
namespace DigitallySign
{


    public class TextSigner 
    {

        
        public static byte[] Sign(string text)
        {
            // Find the certificate we’ll use to sign
            using (System.Security.Cryptography.RSACryptoServiceProvider csp = ProviderFactory.CreateProvider())
            {
                if (csp == null)
                {
                    throw new System.Exception("No valid cert was found");
                }

                // Hash the data
                using (System.Security.Cryptography.HashAlgorithm sha1 = new System.Security.Cryptography.SHA1Managed())
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
                    byte[] hash = sha1.ComputeHash(data);

                    // Sign the hash
                    return csp.SignHash(hash, System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"));
                }
            }
        }


        public static bool Verify(string text, byte[] signature)
        {
            // Get its associated CSP and public key
            using (System.Security.Cryptography.RSACryptoServiceProvider csp = ProviderFactory.CreateProvider())
            {

                // Hash the data
                using (System.Security.Cryptography.HashAlgorithm sha1 = new System.Security.Cryptography.SHA1Managed())
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
                    byte[] hash = sha1.ComputeHash(data);

                    // Verify the signature with the hash
                    return csp.VerifyHash(hash, System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"), signature);
                }
            }
        }


        public static void Test()
        {
            // Usage sample
            try
            {
                // Sign text
                byte[] signature = Sign("Test");

                // Verify signature. Testcert.cer corresponds to "cn=my cert subject"
                if (Verify("Test", signature))
                {
                    System.Console.WriteLine("Signature verified");
                }
                else
                {
                    System.Console.WriteLine("ERROR: Signature not valid!");
                }

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("EXCEPTION: " + ex.Message);
            }

            System.Console.ReadKey();
        }


    }


}
