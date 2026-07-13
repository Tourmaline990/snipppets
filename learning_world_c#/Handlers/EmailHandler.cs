public class EmailHandler
{
    private readonly AccountManager _accountManager;
    public EmailHandler(AccountManager accountManager)
    {
       _accountManager = accountManager; 
    }
    public void Handle(Event evt)
    {
        switch (evt)
        {
            case InstructorDeactivatedEvent instructorDeactivated:
               string email = _accountManager.GetAccount(instructorDeactivated.GetprofileId()).GetEmail();
               // send email
               break;
            case LearnerDeactivatedEvent learnerDeactivated:
               email = _accountManager.GetAccount(learnerDeactivated.GetLearnerId()).GetEmail();
              break;
            case LearnerRegisteredEvent learnerRegistered:
              email = _accountManager.GetAccount(learnerRegistered.GetId()).GetEmail();
              break; 
            case InstructorRegisteredEvent instructorRegistered:
              email = _accountManager.GetAccount(instructorRegistered.GetId()).GetEmail();
              break;
        }
    }
} 