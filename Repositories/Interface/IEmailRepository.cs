using auction.Models.Domain;
using Org.BouncyCastle.Asn1.Pkcs;

namespace auction.Repositories.Interface
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
