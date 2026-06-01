using System.Globalization;

public class AccountManager
{
    private List<Account> _accounts = new List<Account>();

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
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
    public void RemoveAccount(int acc_index)
    {
        if (acc_index > _accounts.Count || acc_index < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(acc_index),"Invalid Index");
        }
        acc_index = acc_index - 1;
        _accounts.RemoveAt(acc_index);

    }
}