
using System.Security.Cryptography;
using System.Text;

namespace ToDoList.EfCore
{
    public class Helper
    {
        public static string ConnectionString { get; set; }
        public static string SymetricSecurityKey { get; set; }


        public static void LoadConfig(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("ToDoContext");
            SymetricSecurityKey = "4e9f5a0824554525bbf35490d8da48f2";
        }


        //Decrypt Password

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                // Set the key and IV
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            // Read the decrypted bytes from the stream and return as a string
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }


        //Encrypt Password
        public static string EncryptString(string key, string plainText) 
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                // Create a decryptor to perform the stream transform
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }

                        array = ms.ToArray();   
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

    }
}
