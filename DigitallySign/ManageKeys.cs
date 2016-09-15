
namespace DigitallySign
{


    public class ManageKeys
    {


        public ManageKeys()
        { }


        /// <summary>
        /// Export a certificate to a PEM format string
        /// </summary>
        /// <param name="cert">The certificate to export</param>
        /// <returns>A PEM encoded string</returns>
        public static string ExportToPEM(System.Security.Cryptography.X509Certificates.X509Certificate cert)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();            

            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(
                System.Convert.ToBase64String(
                 cert.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Cert)
                ,System.Base64FormattingOptions.InsertLineBreaks)
            );
            builder.AppendLine("-----END CERTIFICATE-----");

            return builder.ToString();
        } // End Function ExportToPEM 


        /// Following function will initialize a instance of RSACryptoServiceProvider
        /// based on a already saved machine key container.
        public static System.Security.Cryptography.RSACryptoServiceProvider GetSavedKeyFromContainer(string ContainerName)
        {
            System.Security.Cryptography.CspParameters cp = new System.Security.Cryptography.CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = System.Security.Cryptography.CspProviderFlags.UseMachineKeyStore;

            System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cp);

            return rsa;
        } // End Function GetSavedKeyFromContainer 


        ///Following function will delete the specified MachineKeyContainer
        public static void DeleteSavedKeyFromContainer(string ContainerName)
        {
            System.Security.Cryptography.CspParameters cp = new System.Security.Cryptography.CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = System.Security.Cryptography.CspProviderFlags.UseMachineKeyStore;

            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(cp))
            {
                rsa.PersistKeyInCsp = false;
                rsa.Clear();
            } // End Using rsa 

        } // End Sub DeleteSavedKeyFromContainer 


    } // End Class ManageKeys


} // End Namespace DigitallySign 
