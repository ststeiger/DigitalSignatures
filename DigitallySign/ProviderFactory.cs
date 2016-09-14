﻿
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


namespace DigitallySign
{


    class ProviderFactory
    {


        // if (Verify("Test", signature, @"C:\testcert.cer"))
        public static RSACryptoServiceProvider FromCertPath(string certPath)
        {
            // Load the certificate we’ll use to verify the signature from a file
            X509Certificate2 cert = new X509Certificate2(certPath);

            // Note:
            // If we want to use the client cert in an ASP.NET app, we may use something like this instead:
            // X509Certificate2 cert = new X509Certificate2(Request.ClientCertificate.Certificate);

            // Get its associated CSP and public key
            return (RSACryptoServiceProvider)cert.PublicKey.Key;
        }


        // FromStore("cn=my cert subject");
        public static RSACryptoServiceProvider FromStore(string certSubject)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);

            foreach (X509Certificate2 cert in my.Certificates)
            {
                if (cert.Subject.Contains(certSubject))
                {
                    // We found it.
                    // Get its associated CSP and private key
                    return (RSACryptoServiceProvider)cert.PrivateKey;
                } // End if (cert.Subject.Contains(certSubject)) 
            }

            throw new System.Exception("No valid cert was found");
        }


        public static RSACryptoServiceProvider CreateProvider()
        {
            string publicPrivateKeyXML = "<RSAKeyValue><Modulus>jwC1EyNgHkh3Q3J3ITmh6EkbsTSKJuuCYsg9UsaYA9+Trwlp4v37VVc3b2jTsUHaEcG1IYGQbQBu/IIxiDlmDFiQPpN8UrhLz0ZQ9SzWRONSzRC97DR08epl4JtO86uAWYR9+iEnxIaeiRG6i32sjZYaiqwBuvNzli94Wtz7yQDNlH6FmdkMp0n9Hg8MslRXbRbINXhW/nJ4zOggmRLQfOzc2ZxyARgmXvpmxPxaaawtBj919VHFishyU0u7CbzpQ3J5dldV1d+FTICZN5AveF4qM4tzWMBZdCubdcKiMnIGNgBO/mUb/SbWwlu4OuXG8vOr5aqYIuaWUKBFEyK3pQ==</Modulus><Exponent>AQAB</Exponent><P>uSoRvVLPX8EdlsYbCcVTgjqpP0e/UsXeBRXTMjxaHLDYgvgSUADhAd5ECscsNdb5sLUy18xSd9pVLFRMr7lqfstiY1tJDyLrP+54TON4KUhFkV1ntf5R1bGzvxlY3po3vUz+2a8fLq4F9ennaT6/NKyn4d7WH+qmr6oSbrgGLgU=</P><Q>xbWTfXb9UIGLN/j/V+TVukMSP8GmSXj+tKnrr5XjdwB4yRyf6krw4ntIU/wnS6in5TRy5R3Wdy8ixGCB9zghQnjShhCKprD2nx4f74BzpL62Y6qI+lsH17AZAov0Olpe3MPGFo+F6UXqlRQPEAfh2Wxv6hiCcfucP7gEy5Tc9SE=</Q><DP>JneN7eX5POxSqFMJpPMAkUp8hK/0GE8Q+793+7S8B7/ZiwPcUhCMriWtvwt3rMu3XbWXFWvWKh4KmcX9lHgRnrvD+d4qBGH9u29gQKD1AqaIBVYBSLbH63waWnX6l2w0bjhDrZeLA9iVVmw8bgniESBZVDxGAaVu8YmEgMnsRr0=</DP><DQ>etVV/hRIS5VAdpUHp4bv1ppHIz9f3bQDoyES4fMg8FVltaVIIVtQD5YCmNNHYrU1Iq0UWQ7RqRiq5BEFjh/cYh0IxuxOCERX5QHlW3qV3pvyWzefhNO7qqCo2TE0mnB9EXG8h1XCH+0lUlu1BAOxqNC7M1jo6oIlUF029XjWUqE=</DQ><InverseQ>aZwOBDDsp0tZSs2p418syfQwxyWUJ/kdu4D08x+LtI0Fd31LzHbD1Ogs6aBlAFf8wPFE4mNsHDrkjujLKoEmnwt8SEMSIXKz2EuDG4E7wKgTT3w0Dy3Vydo4Zh6kGJF+bQ2DP3LhwoHZBt06CPeFvsUBOnM/nKn9ICJeKHwyfkQ=</InverseQ><D>Cgxtgj9ESUx5SPLZqSrbaYRS9FRHTZh99y1alcmhCUtOyDLGpIM0A9lW6ra4gruoXwK4FMx9wWhm5B/NQDpxpSDHXgZPavaf/nF9Tdp34gEBTVSbATvnEyVlQZpYOr93Nj3Hpmm/BCHGMRve+l9QiTseJAFrNl+rZHHIfhtfe/kTZu+Klhelmb1JEgtqRe7Ve91JGoDka4L9GP0oIqD9nnxmI1gmpaUeuDKfpbzfeoQCU1RVUDyZ5MirRbTvUmCIjs3Hed2pmDTigkTJ2mYHd4gESXXyCYVa8Qgw5mOOoHCd5viG3hLYQGzMIoGkzKr+5vF63kxtkMRFKNIra3vbQQ==</D></RSAKeyValue>";
            string publicOnlyKeyXML = "<RSAKeyValue><Modulus>jwC1EyNgHkh3Q3J3ITmh6EkbsTSKJuuCYsg9UsaYA9+Trwlp4v37VVc3b2jTsUHaEcG1IYGQbQBu/IIxiDlmDFiQPpN8UrhLz0ZQ9SzWRONSzRC97DR08epl4JtO86uAWYR9+iEnxIaeiRG6i32sjZYaiqwBuvNzli94Wtz7yQDNlH6FmdkMp0n9Hg8MslRXbRbINXhW/nJ4zOggmRLQfOzc2ZxyARgmXvpmxPxaaawtBj919VHFishyU0u7CbzpQ3J5dldV1d+FTICZN5AveF4qM4tzWMBZdCubdcKiMnIGNgBO/mUb/SbWwlu4OuXG8vOr5aqYIuaWUKBFEyK3pQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            });

            rSACryptoServiceProvider.FromXmlString(publicPrivateKeyXML);
            return rSACryptoServiceProvider;
        } // End Function CreateProvider 


    }


}
