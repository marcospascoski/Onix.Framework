using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Onix.Framework.Security
{
    public static class EncryptionHelper
    {
        public static string SHA256Hash(string rawData)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string SHA512Hash(string input)
        {
            byte[] message = Encoding.UTF8.GetBytes(input);
            byte[] hashValue = SHA512.HashData(message);
            var hexNumber = new StringBuilder();
            foreach (byte x in hashValue)
            {
                hexNumber.AppendFormat("{0:x2}", x);
            }
            return hexNumber.ToString();
        }

        public static bool SlowEquals(string a, string b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            byte[] aBytes = Convert.FromBase64String(a);
            byte[] bBytes = Convert.FromBase64String(b);

            int diff = aBytes.Length ^ bBytes.Length;
            for (int i = 0; i < aBytes.Length && i < bBytes.Length; i++)
            {
                diff |= aBytes[i] ^ bBytes[i];
            }

            return diff == 0;
        }
        public static string Encrypt(string value, EEncryptionType encryptionType)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return encryptionType switch
            {
                EEncryptionType.SHA256 => SHA256Hash(value),
                EEncryptionType.SHA512 => SHA512Hash(value),
                _ => value,
            };
        }

        public static int CharactersCount(EEncryptionType encryptionType)
        {
            return encryptionType switch
            {
                EEncryptionType.SHA256 => 64, // SHA256 gera um hash de 64 caracteres hexadecimais
                EEncryptionType.SHA512 => 128, // SHA512 gera um hash de 128 caracteres hexadecimais
                EEncryptionType.AES => 64, // AES-256 usa uma chave de 64 caracteres hexadecimais
                _ => 0, // Tipo não suportado
            };
        }

        public static string GenerateRandomSecret(int keySizeInBytes)
        {
            byte[] secretBytes = new byte[keySizeInBytes];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(secretBytes);

            return Convert.ToBase64String(secretBytes);
        }

        public static string GenerateRandomSecret(EEncryptionType encryptionType)
        {
            int tamanhoBytes = CharactersCount(encryptionType) / 2;
            if (tamanhoBytes == 0)
            {
                return null;
            }

            byte[] secretBytes = new byte[tamanhoBytes];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(secretBytes);

            return Convert.ToBase64String(secretBytes);
        }

        public static string EncryptAES(string texto, string chaveMestra)
        {
            if (string.IsNullOrWhiteSpace(texto) || string.IsNullOrWhiteSpace(chaveMestra))
            {
                return null;
            }

            using var aes = Aes.Create();
            aes.GenerateIV();

            using (var pbkdf2 = new Rfc2898DeriveBytes(chaveMestra, aes.IV, 10000, HashAlgorithmName.SHA256))
            {
                aes.Key = pbkdf2.GetBytes(32); // 256 bits
            }

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new System.IO.MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new System.IO.StreamWriter(cs))
            {
                sw.Write(texto);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptAES(string textoCriptografado, string chaveMestra)
        {
            if (string.IsNullOrWhiteSpace(textoCriptografado) || string.IsNullOrWhiteSpace(chaveMestra))
            {
                return null;
            }

            var textoBytes = Convert.FromBase64String(textoCriptografado);

            using var aes = Aes.Create();
            aes.IV = [.. textoBytes.Take(16)];

            using var pbkdf2 = new Rfc2898DeriveBytes(chaveMestra, aes.IV, 10000, HashAlgorithmName.SHA256);
            aes.Key = pbkdf2.GetBytes(32); // 256 bits

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new System.IO.MemoryStream(textoBytes, 16, textoBytes.Length - 16);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new System.IO.StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}