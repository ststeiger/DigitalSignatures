
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
            } // Next i 

            return baBuffer;
        } // End Function HexStringToByteArray


        public static string ByteArrayToHexString(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            byte b;

            for(int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx) 
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx]=(char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            } // Next bx, cx 

            return new string(c);
        } // End Function ByteArrayToHexString


    } // End Class Helper 


} // End Namespace DigitallySign 
