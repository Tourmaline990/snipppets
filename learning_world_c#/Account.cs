using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public class Account
{
    private string _name;
    private string _email;
    private string _learning_id;

    public Account(string name,string email)
    {
        _name = ValidateInput(name);
        _email = ValidateInput(email);
        _learning_id = GenerateId();
    }

    public string GenerateId()
    {
        string learner_id = "";
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
    public string GetLearningId()
    {
        return _learning_id;
    }
    public string Display()
    {
        return $">>> Name: {_name} | Email: {_email} | Learner Id: {_learning_id}";
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
}

