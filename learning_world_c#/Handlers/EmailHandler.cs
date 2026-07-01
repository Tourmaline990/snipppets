public class EmailHandler
{
    private readonly AccountManager _accountManager;
    public EmailHandler(AccountManager accountManager)
    {
       _accountManager = accountManager; 
    }
    public void Handle(InstructorDeactivatedEvent deactivatedEvent)
    {
        Account account = _accountManager.GetAccount(deactivatedEvent.GetprofileId());
        string email = account.GetEmail();
        // user should be mailed.
    }
}