
namespace DigitallySign
{


    public class Helper
    {


        public Helper()
        { }


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
