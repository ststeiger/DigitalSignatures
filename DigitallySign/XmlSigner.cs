
using System.Security.Cryptography.Xml; // Requires Reference to System.Security


namespace DigitallySign
{


    public class XmlSigner
    {


        public static void SignDocument(string inFileName, string outFileName, System.Security.Cryptography.AsymmetricAlgorithm key)
        {
            // Load an XML file into the XmlDocument object.
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.XmlResolver = null;
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(inFileName);


            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = key;

            // Create a reference to be signed.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            Reference reference = new Reference();
            reference.Uri = "";
            reference.AddTransform(env);

            signedXml.AddReference(reference);
            signedXml.ComputeSignature();
            System.Xml.XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            xmlDoc.Save(outFileName);
        } // End Sub SignDocument 


        // Verify the signature of an XML file against an asymetric 
        // algorithm and return the result.
        public static bool VerifyXmlFile(string fileName, System.Security.Cryptography.AsymmetricAlgorithm key)
        {
            // Create a new XML document.
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.XmlResolver = null;
            xmlDocument.PreserveWhitespace = true;

            // Load the passed XML file into the document. 
            xmlDocument.Load(fileName);

            // Create a new SignedXml object and pass it the XML document class.
            SignedXml signedXml = new SignedXml(xmlDocument);

            // Find the "Signature" node and create a new XmlNodeList object.
            System.Xml.XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");
            if (nodeList == null)
                throw new System.IO.InvalidDataException("The XML-file provided does not contain a Signature tag...");

            // Load the signature node.
            signedXml.LoadXml((System.Xml.XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(key);
        } // End Function VerifyXmlFile 


        public static void Test()
        {
            using (System.Security.Cryptography.RSACryptoServiceProvider rsaKey = ProviderFactory.CreateProvider())
            {
                SignDocument("Test.xml", "Test1.xml", rsaKey);
                System.Console.WriteLine("XML file signed.");

                try
                {
                    // Verify the signature of the signed XML.
                    System.Console.WriteLine("Verifying signature...");
                    bool result = VerifyXmlFile("Test1.xml", rsaKey);

                    // Display the results of the signature verification to the console.
                    if (result)
                    {
                        System.Console.WriteLine("The XML signature is valid.");
                    }
                    else
                    {
                        System.Console.WriteLine("The XML signature is not valid.");
                    }
                }
                catch (System.Security.Cryptography.CryptographicException e)
                {
                    System.Console.WriteLine(e.Message);
                }

            } // End Using rsaKey 

            System.Console.WriteLine("Finished testing XML-signature");
        } // End Sub Test


    } // End Class XmlSigner 


} // End Namespace DigitallySign 
