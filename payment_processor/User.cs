public class User
{
    private string _userId;
    private string _name;
    private bool _accountStatus;
    
   
   public User(string userId,string name,bool acc_status)
    {
        _userId = userId;
        _name = name;
        _accountStatus = acc_status;
    }


    public string GetId()
    {
     return _userId;
    }
   public void SetId(string new_id)
    {
        _userId = new_id;
    }
   public void Set_accountStatus(bool status)
    {
        _accountStatus = status;
    }
    public bool Get_Account_Status()
    {
        return _accountStatus;
    }
    public string Get_name()
    {
        return _name;
    }
    
}

