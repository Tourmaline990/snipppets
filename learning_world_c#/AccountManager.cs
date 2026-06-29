using System.Globalization;
using System.Net.Http.Headers;

public class AccountManager
{
    private List<Account> _accounts = new List<Account>();
    private List<Learner> _learners = new List<Learner>();
    private List<Instructor> _instructors = new List<Instructor>();

    public Profile Register(Account account)
    {
        if (account is InstructorAuditAccount auditAccount)
        {
             if (auditAccount.GetInstructorStatus() != InstructorStatus._certified)
             {
                throw new InvalidOperationException("Cannot register an uncertified instructor.");
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
       return GetProfile(account.GetAccountId())!;
    }
    private Profile? GetProfile(string accId)
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
    public void DisplayAccounts()
    {
        int num = 1;
        foreach (Account acc in _accounts)
        {
            if (acc.GetAccountStatus() != AccountStatus._deleted)
            {
                if (acc is InstructorAuditAccount auditAccount)
                {
                    Console.WriteLine($"{num}. {auditAccount.Display()}");
                }
                else
                {
                    Console.WriteLine($"{num}. {acc.Display()}"); 
                }
                num ++;
            }
        }
    }
    public void DeactivateAccount(string AccountId,ForumManager manager)
    {
       Account account =  GetAccount(AccountId);
       Profile profile =  GetAccountProfile(AccountId)!;
        switch (profile)
        {
            case Learner learner:
               learner.DeleteAccount(manager);
               account.Delete();
               break;
            case Instructor instructor:
                if (instructor.GetInstructorActiveStatus() == InstructorActiveStatus._active)
                {
                    Instructor? instructor1 = _instructors.Find(I => I.GetInstructorActiveStatus() == InstructorActiveStatus._available);
                    if (instructor1 == null)
                    {
                        throw new InvalidOperationException("Instructor Deactivation failed! No available instructor to fill");
                    }
                    instructor1.AllocateSession(instructor.GetCourseSession(),manager.GetForum(instructor.GetCourseSession().GetSessionId()));
                    instructor.GetCourseSession().SetInstructor(instructor);
                }
              instructor.Revoke();
                switch (account)
                {
                    case InstructorAuditAccount instructorAudit:
                      instructorAudit.Revoke();
                      instructorAudit.Delete();
                      break;
                }
                break;
        }
    }
    public Type VerifyAccount(string accountId)
    {
         return GetAccountProfile(accountId)!.GetType();
    }
    public Account GetAccount(string accId)
    {
        if (string.IsNullOrEmpty(accId))
        {
            throw new ArgumentNullException(nameof(accId),"Learner id provided is empty.");
        }
        Account? account =  _accounts.Find(A => A.GetAccountId() == accId);
        if (account == null)
        {
            throw new NullReferenceException("Not found");
        }
        return account;
    }
    public void ViewDeletedAccounts()
    {
       List<Account>? accounts =  _accounts.FindAll(A => A.GetAccountStatus() == AccountStatus._deleted);
       foreach(Account item in accounts)
        {
           Console.WriteLine($">>>> {item.Display()}"); 
        }
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