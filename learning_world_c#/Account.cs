using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class Account
{
    private string _name;
    private string _email;
    private string _accountId = null!;
    private AccountStatus _accountStatus;

    public Account(string name,string email)
    {
        _name = name;
        _email = email;
        _accountStatus = AccountStatus._drafted;
    }
    public void Register()
    {
        if (_accountStatus != AccountStatus._drafted)
        {
            throw new InvalidOperationException("Account is not drafted.");
        }
        ValidateInput(_email);
        ValidateInput(_name);
        _accountId = GenerateId();
        _accountStatus = AccountStatus._registered;
    }
    private string GenerateId()
    {
        string learner_id;
        Random random = new Random();
        List<string> letters = new List<string> {"b","j","K","l","m","n","y","o","p","h","t","w","v","s","d","q","r","a","c","z","f","g"};
        List<int> int_indexes = new List<int>();
        List<string> index_value = new List<string>();

        for (int i = 0; int_indexes.Count < 5; i++)
        {
            int value = random.Next(letters.Count);
            int_indexes.Add(value);
        }
        foreach (int item in int_indexes)
        {
            index_value.Add(letters[item]);
        }
        learner_id = _name;
        foreach (string item in index_value)
        {
            
            learner_id += item;
        }
        return learner_id;
    }
    public string GetName()
    {
        return _name;
    }
    public void SwitchId()
    {
        _accountId = GenerateId();
    }
    public string GetAccountId()
    {
        if (_accountStatus != AccountStatus._registered)
        {
            throw new InvalidOperationException("Account is not registered.");
        }
        return _accountId;
    } 
    public virtual string Display()
    {
        return $">>> Name: {_name} | Email: {_email} | {_accountId}";
    }
    public void Delete()
    {
        if (_accountStatus == AccountStatus._deleted)
        {
            throw new InvalidOperationException("Already Deleted.");
        }
        _accountStatus = AccountStatus._deleted;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public AccountStatus GetAccountStatus()
    {
        return _accountStatus;
    }
    //
}

