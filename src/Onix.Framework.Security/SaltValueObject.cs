namespace Onix.Framework.Security
{
    public class SaltValueObject
    {
        public string Valor { get; private set; }
        public static readonly int SaltLengthInBytes = 16;

        public SaltValueObject(string salt)
        {
            Valor = salt;
        }
        public SaltValueObject()
        {
            Valor = EncryptionHelper.GenerateRandomSecret(SaltLengthInBytes);
        }
    }
}