
namespace DigitallySign
{


    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            #if (false)
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
            #endif
            // DigitallySign.ProviderFactory.CreateNewKey();
            // DigitallySign.EncryptDecrypt.Test();
            // DigitallySign.XmlSigner.Test();
            DigitallySign.TextSigner.Test();

            // DigitallySign.ManageKeys.ExportToPEM(null);
            System.Console.ReadKey();
        }


    }


}
