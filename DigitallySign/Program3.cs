
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml; // Requires Reference to System.Security.
using System.Text;
using System.Xml;




namespace DocumentSigner
{


    class MainClass
    {


        public static void AssignNewKey()
        {
            RSA rsa = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key

            string publicPrivateKeyXML = rsa.ToXmlString(true); // 
            string publicOnlyKeyXML = rsa.ToXmlString(false); // 

            System.Console.WriteLine(publicOnlyKeyXML + publicPrivateKeyXML);

            // do stuff with keys...
        }


        /// Following function will initialize a instance of RSACryptoServiceProvider
        /// based on a already saved machine key container.
        public static RSACryptoServiceProvider GetSavedKeyFromContainer(string ContainerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = CspProviderFlags.UseMachineKeyStore;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);

            return rsa;
        }


        ///Following function will delete the specified MachineKeyContainer
        public static void DeleteSavedKeyFromContainer(string ContainerName)
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = ContainerName;
            cp.Flags = CspProviderFlags.UseMachineKeyStore;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp))
            {
                rsa.PersistKeyInCsp = false;
                rsa.Clear();
            }
        }


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
        }


        public static void Test()
        {
            // AssignNewKey();
            EncryptDecrypt.Test();


            string xmlString = "";

            // GetSavedKeyFromContainer("XML_DSIG_RSA_KEY");
            // DeleteSavedKeyFromContainer("XML_DSIG_RSA_KEY");



            // CspParameters cspParams = new CspParameters();
            // System.Console.WriteLine(cspParams.KeyContainerName);
            // cspParams.KeyContainerName = "XML_DSIG_RSA_KEY";




            // RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);
            RSACryptoServiceProvider rsaKey = CreateProvider();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.XmlResolver = null;

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load("Test.xml");



            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = rsaKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = ""; // entire document



            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);


            signedXml.AddReference(reference);
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            xmlDoc.Save("Test1.xml");
            Console.WriteLine("XML file signed.");


            Check(rsaKey);




            /*

            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
                {
                    Flags = CspProviderFlags.UseMachineKeyStore
                });


            rSACryptoServiceProvider.FromXmlString(xmlString);


            System.Xml.XmlDocument license = null; // License.GetLicense();
            System.Security.Cryptography.Xml.SignedXml signedXml = new System.Security.Cryptography.Xml.SignedXml(license);
            */


            Console.WriteLine("Hello World!");
        }


        public static void Check(RSACryptoServiceProvider Key)
        {
            try
            {
                // Generate a signing key.
                // RSACryptoServiceProvider Key = new RSACryptoServiceProvider();

                // Create an XML file to sign.
                // CreateSomeXml("Example.xml");
                // Console.WriteLine("New XML file created."); 

                // Sign the XML that was just created and save it in a 
                // new file.
                // SignXmlFile("Example.xml", "signedExample.xml", Key);
                // Console.WriteLine("XML file signed."); 

                // Verify the signature of the signed XML.
                Console.WriteLine("Verifying signature...");
                bool result = VerifyXmlFile("Test1.xml", Key);

                // Display the results of the signature verification to 
                // the console.
                if(result)
                {
                    Console.WriteLine("The XML signature is valid.");
                }
                else
                {
                    Console.WriteLine("The XML signature is not valid.");
                }
            }
            catch(CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        // Verify the signature of an XML file against an asymetric 
        // algorithm and return the result.
        public static Boolean VerifyXmlFile(String Name, RSA Key)
        {
            // Create a new XML document.
            XmlDocument xmlDocument = new XmlDocument();

            // Load the passed XML file into the document. 
            xmlDocument.Load(Name);

            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(xmlDocument);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");

            // Load the signature node.
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(Key);
        }















    }
}
