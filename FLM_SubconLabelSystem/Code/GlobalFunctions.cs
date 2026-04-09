using System;
using System.Security.Cryptography;
using System.Text;

public class GlobalFunctions
{
    private const string SecretKey = "N882A66A56299823AA026E8GARR1B527WDYY3A6EA2FF14F8DB70AB2829B69DDF";

    public static string Encrypt(string plaintext)
    {
        using (var aes = Aes.Create())
        using (var sha256 = SHA256.Create())
        {
            try
            {
                aes.GenerateIV();
                aes.Key = sha256.ComputeHash(Encoding.ASCII.GetBytes(SecretKey));
                aes.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] buffer = Encoding.ASCII.GetBytes(plaintext);

                return Convert.ToBase64String(aes.IV) + Convert.ToBase64String(encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public static string Decrypt(string ciphertext)
    {
        using (var aes = Aes.Create())
        using (var sha256 = SHA256.Create())
        {
            try
            {
                string[] delim = new string[] { "==" };
                string[] ivct = ciphertext.Split(delim, StringSplitOptions.None);
                string iv = ivct[0] + "==";
                ciphertext = ivct.Length == 3 ? ivct[1] + "==" : ivct[1];

                aes.Key = sha256.ComputeHash(Encoding.ASCII.GetBytes(SecretKey));
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] buffer = Convert.FromBase64String(ciphertext);

                return Encoding.ASCII.GetString(decryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public static string RetrieveIV(string ciphertext)
    {
        using (var aes = Aes.Create())
        using (var sha256 = SHA256.Create())
        {
            try
            {
                string[] delim = new string[] { "==" };
                string[] ivct = ciphertext.Split(delim, StringSplitOptions.None);
                string iv = ivct[0] + "==";
                ciphertext = ivct.Length == 3 ? ivct[1] + "==" : ivct[1];

                aes.Key = sha256.ComputeHash(Encoding.ASCII.GetBytes(SecretKey));
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] buffer = Convert.FromBase64String(ciphertext);

                // Decryption performed to validate, but only IV is returned
                Encoding.ASCII.GetString(decryptor.TransformFinalBlock(buffer, 0, buffer.Length));

                return iv;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public static string EncryptIV(string plaintext, string iv)
    {
        using (var aes = Aes.Create())
        using (var sha256 = SHA256.Create())
        {
            try
            {
                aes.Key = sha256.ComputeHash(Encoding.ASCII.GetBytes(SecretKey));
                aes.IV = Convert.FromBase64String(iv);
                aes.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] buffer = Encoding.ASCII.GetBytes(plaintext);

                return Convert.ToBase64String(aes.IV) + Convert.ToBase64String(encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}