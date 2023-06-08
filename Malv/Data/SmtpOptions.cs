namespace Malv.Data;

public class SmtpOptions
{
    public string Client { get; set; }
    public int Port { get; set; }
    public string FromMail { get; set; }
    public string FromPassword { get; set; }
}