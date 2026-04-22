public class Employee 
{
    private string _name;
    private int _age;
    private decimal _salary;

    public Employee(string name,int age, decimal salary)
    {
         _name = ValidateName(name);
         _age = validateAge(age);
         _salary = ValidateSalary(salary);
    }
    private string ValidateName(string name)
    {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be Empty");
            }
        return name;
     }
     
        
    private int validateAge(int age)
    {
        
           if(age <= 0 || age > 120)
            {
                throw new ArgumentOutOfRangeException(nameof(age),"Age should be between 1 and 120, Age will be set to a -1 default");
            }
            return age;
    }
        

    
    private decimal ValidateSalary(decimal salary)
    {
            if (salary <= 0m )
            {
                throw new  ArgumentOutOfRangeException(nameof(salary),"salary cannot be negative, will default to -1");
            }
            return salary;
      }
        
    
    public string EmployeeDetails()
    {
        return  $"Employee Detail\nName: {_name}\n Age: {_age}\n Salary: ${_salary}";
    }

}