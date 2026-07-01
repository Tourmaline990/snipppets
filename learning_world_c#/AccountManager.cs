using System.Globalization;
using System.Net.Http.Headers;
using System.Numerics;

public class AccountManager
{
    private List<Account> _accounts = new List<Account>();
    private List<Learner> _learners = new List<Learner>();
    private List<Instructor> _instructors = new List<Instructor>();
    private readonly EventDispatcher _eventDispatcher;
    public AccountManager(EventDispatcher dispatcher)
    {
        _eventDispatcher = dispatcher;
    }

    public Profile Register(Account account)
    {
        if (account is InstructorAuditAccount auditAccount)
        {
             if (auditAccount.GetInstructorStatus() != InstructorStatus._certified)
             {
                throw new InvalidOperationException("Cannot register an uncertified instructor.");
             }
        }
        else
        {
            if (account.GetAccountStatus() != AccountStatus._drafted)
            {
                throw new InvalidOperationException("Failed! Account is not drafted.");
            }
        }
       account.Register();
       Account? acc = _accounts.Find(A => A.GetAccountId() == account.GetAccountId());
       while (acc != null)
       {
          account.SwitchId();
          acc = _accounts.Find(A => A.GetAccountId() == account.GetAccountId());
       }
       _accounts.Add(account); 
       return CreateProfile(account.GetAccountId())!;
    }
    private Profile? CreateProfile(string accId)
    {
        Account account = GetAccount(accId);
        switch (account)
        {
            case InstructorAuditAccount auditAccount:
              Instructor instructor =  new Instructor(auditAccount.GetName(),auditAccount.GetAccountId());
              _instructors.Add(instructor);
              return instructor;
            case Account acc:
              Learner learner =  new Learner(acc.GetAccountId(),acc.GetName());
              _learners.Add(learner);
              return learner;
        }
        return null;
    }
    public List<Account> GetActiveAcccounts()
    {
        return _accounts.FindAll(A => A.GetAccountStatus() == AccountStatus._registered);
    }
    public List<Account> GetAllDeletedAccount()
    {
        return _accounts.FindAll(A => A.GetAccountStatus() == AccountStatus._deleted);
    }
    public Instructor GetAvailableInstructor()
    {
        return _instructors.Find(I => I.GetInstructorActiveStatus() == InstructorActiveStatus._available)!;
    }
    public Learner? GetLearner(string accId)
    {
        accId = Utility.ValidateString(accId);
        Type type = VerifyAccount(accId);
        if (type != typeof(Learner))
        {
            throw new InvalidOperationException("Not a learner.");
        }
        Profile profile =  GetAccountProfile(accId)!;
        if (profile is Learner learner)
        {
            return learner;
        }
        return null;
    }   
    public Instructor? GetInstructor(string accId)
    {
        accId = Utility.ValidateString(accId);
        Type type = VerifyAccount(accId);
        if (type != typeof(Instructor))
        {
            throw new InvalidOperationException("Not an instructor.");
        }
        Profile profile =  GetAccountProfile(accId)!;
        if (profile is Instructor instructor)
        {
            return instructor;
        }
        return null;
    }
    public void DeactivateAccount(string AccountId,ForumManager manager)
    {
       AccountId = Utility.ValidateString(AccountId);
       Account account =  GetAccount(AccountId);
       Profile profile =  GetAccountProfile(AccountId)!;
        switch (profile)
        {
            case Learner learner:
               learner.DeleteAccount(manager);
               account.Delete();
               break;
            case Instructor instructor:
                switch (account)
                {
                    case InstructorAuditAccount instructorAudit:
                      instructorAudit.Revoke();
                      break;
                }
               _eventDispatcher.Dispatch(new InstructorDeactivatedEvent(instructor.GetprofileId(),instructor.GetInstructorActiveStatus(),DateTime.UtcNow));
                instructor.Revoke();
                break; 
        }
    }
    public Type VerifyAccount(string accountId)
    {
         return GetAccountProfile(accountId)!.GetType();
    }
    public Account GetAccount(string accId)
    {
        accId = Utility.ValidateString(accId);
        Account? account =  _accounts.Find(A => A.GetAccountId() == accId);
        if (account == null)
        {
            throw new NullReferenceException("Not found");
        }
        return account;
    }
    public Profile? GetAccountProfile(string accountId)
    {
        Account account = GetAccount(accountId);
        switch (account)
        {
            case InstructorAuditAccount auditAccount: 
              Instructor? instructor =  _instructors.Find(I => I.GetprofileId() == accountId && I.GetInstructorActiveStatus() != InstructorActiveStatus.revoked);
                if (instructor == null)
                {
                    throw new NullReferenceException("No results matches query");
                }
                return instructor;
            case Account account1:
               Learner? learner =  _learners.Find(L => L.GetprofileId() == accountId && L.GetLearnerStatus() == LearnerStatus._live);
                if (learner == null)
                {
                    throw new NullReferenceException("No results matches query");
                }
                return learner;
        }
        return null;
    } 
}