using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface IMailTokenRepository
{
    Task AddToken(MailToken mailToken);

    Task<MailToken> FindMailToken(string token, MailToken.MailTokenType type);

    Task RemoveToken(MailToken mailToken);
}