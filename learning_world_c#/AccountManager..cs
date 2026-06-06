using System.Globalization;

public class AccountManager
{
    private List<Account> _accounts = new List<Account>();
    private List<DeletedAccount> _deletedAccounts = new List<DeletedAccount>();
    private List<Learner> _learners = new List<Learner>();

    public Learner GetLearner(Account account)
    {
       Account? acc = _accounts.Find(A => A.GetLearningId() == account.GetLearningId());
        if (acc == null)
        {   
          _accounts.Add(account);
        }
        else
        {
            throw new ArgumentException(nameof(account), "Learning id already exists, create a new profile.");
        }
        Learner learner = new Learner(account.GetLearningId());
        _learners.Add(learner);
        return learner;
    }
    public void DisplayAccounts()
    {
        int num = 1;
        foreach (Account acc in _accounts)
        {
            Console.WriteLine($"{num}. {acc.Display()}");
            num ++;
        }
    }
    public void ViewDeletedAccounts()
    {
        int num = 1;
        foreach (DeletedAccount acc in _deletedAccounts)
        {
            Console.Write(num  );
            acc.DisplayInfo();
            num ++;
        }
    }
    public void RemoveLearner(string LearnerId,string reason = "")
    {
        if (string.IsNullOrEmpty(LearnerId))
        {
            throw new ArgumentNullException(nameof(LearnerId),"Learner id provided is empty.");
        }
        Account? account = _accounts.Find(A => A.GetLearningId() == LearnerId);
        Learner? learner = _learners.Find(L => L.GetLearnerId() == LearnerId);
        if(account == null || learner == null)
        {
            throw new ArgumentNullException(nameof(LearnerId), "Invalid Id, Not found");
        }
        if (!string.IsNullOrWhiteSpace(reason))
        {
            _deletedAccounts.Add(new DeletedAccount(account,learner,reason));
        }
        else
        {
           _deletedAccounts.Add(new DeletedAccount(account,learner)); 
        }
        _accounts.Remove(account);
       _learners.Remove(learner);

    }
    public bool VerifyLearner(string learnerId)
    {
        if (string.IsNullOrEmpty(learnerId))
        {
            throw new ArgumentNullException(nameof(learnerId),"Learner id provided is empty.");
        }
       Learner? l =  _learners.Find(L => L.GetLearnerId() == learnerId);
        if (l != null)
        {
            return true;
        }
        return false;
        
    }
    ///
}