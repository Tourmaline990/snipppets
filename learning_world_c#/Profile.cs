public class Profile
{
    private string _name;
    private string _accountId;

    public Profile(string name, string id)
    {
        _name  = ValidateInput(name);
        _accountId = ValidateInput(id);
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public string GetprofileId()
    {
        return _accountId;
    }
    public string GetProfileName()
    {
        return _name;
    }
}