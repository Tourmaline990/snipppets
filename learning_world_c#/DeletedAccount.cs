public class DeletedAccount
{
    private string _learnerId;
    private Account _account;
    private Learner _learner;
    private string _reason;
    private DateTime _date;
    public DeletedAccount(Account account,Learner learner)
    {
        _account = account;
        _learner = learner;
        _learnerId = SetLearnerId();
        _reason = "Not Stated.";
        _date = DateTime.Now;
        
    }
    public DeletedAccount(Account account,Learner learner,string reason)
    {
        _account = account;
        _learner = learner;
        _learnerId = SetLearnerId();
        _reason = ValidateInput(reason);
        _date = DateTime.Now;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    } 
    private string SetLearnerId()
    {
        string id = _account.GetLearningId();
        if (_learner.GetLearnerId() != id)
        {
            throw new Exception("Wrong Information! Account and learner does not match.");
        }
        return id;
    }
    public string GetLearnerId()
    {
        return _learnerId;
    }
    public Account GetAccount()
    {
        return _account;
    }
    public Learner learner()
    {
        return _learner;
    }
    public string Reason()
    {
        return _reason;
    }
    public void DisplayInfo()
    {
        Console.WriteLine(">>>");
        Console.WriteLine(_learnerId);
        Console.WriteLine(_account.GetName());
        Console.WriteLine(_reason);
        _learner.ViewEnrolledCourses();
    }

}