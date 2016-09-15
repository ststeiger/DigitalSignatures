
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
                    throw new System.Exception("No valid cert was found");
                
                // Hash the data
                using (System.Security.Cryptography.HashAlgorithm sha1 = new System.Security.Cryptography.SHA1Managed())
                {
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
                    byte[] hash = sha1.ComputeHash(data);


                    // AES RijndaelManaged
                    // Sign the hash
                    return csp.SignHash(hash, System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1"));
                } // End using sha1 

            } // End Using csp 

        } // End Function Sign 


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
                } // End Using sha1 

            } // End Using csp 

        } // End Function Verify 


        public static void Test()
        {
            // Usage sample
            try
            {
                string jose = @"{""alg"":""none""}";
                string bar = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jose));
                System.Console.WriteLine(bar);
                jose = System.Web.HttpUtility.UrlEncode(bar);
                System.Console.WriteLine(jose);



                string foo = System.Web.HttpUtility.UrlDecode("eyJhbGciOiJub25lIn0");
                System.Console.WriteLine(foo);


                // Sign text
                byte[] signature = Sign("Test");



                string sig = Helper.ByteArrayToHexString(signature);
                System.Console.WriteLine(sig);


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
        } // End Sub Test 


    } // End Class TextSigner 


} // End Namespace DigitallySign 
