namespace Malv.Models;

public class Error_Res
{
    public IDictionary<string, string[]> Errors { get; }
    public bool HasErrors => Errors.Count > 0;

    public Error_Res()
    {
        Errors = new Dictionary<string, string[]>();
    }

    public Error_Res AddError(string title, params string[] errors)
    {
        Errors.Add(title, errors);
        return this;
    }
}