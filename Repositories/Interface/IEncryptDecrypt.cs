namespace auction.Repositories.Interface
{
    public interface IEncryptDecrypt
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}
