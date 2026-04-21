def Employee(name,age,income):
    result = ""
    if name != "" and age >=1 and age <= 120 and income > 0:
     result = f"Name: {name}, Age: {age}, Income: ${income:.2f}"
    else:
      result = "Invalid data format"
    return result

name = "aliya"
Age = 18
income = 1000
print(Employee(name,Age,income))

class Employee: 
  def __init__(self,name,age,income):
    self.name = self._validate_name(name)
    self.age = self._validate_age(age)
    self.income = self._validate_income(income)
  
  def _validate_name(self,name):
       if isinstance(name,str) and name.strip() != "":
        return name
       raise ValueError("Name cannot be empty")
  def _validate_age(self,age):
      if isinstance(age,int) and age <= 120 and age > 0:
        return age
      raise ValueError("Age must be between 1 and 120 ")
  def _validate_income(self,income):
      if isinstance(income,(float,int)) and income > 0:
        return income
      raise  IndexError("Income  cannot be negative")
       
    
  def Employeedetails(self):
    return f"Employee Informations: Name: {self.name} Age: {self.age} Income: ${self.income:.2f}"


Employee1 = Employee("Aliya",18,1000.00)
Employee1.Employeedetails()

try:
    Employee2 = Employee("Aliya",18,-5)
except IndexError:
   print("Invalid assignment to constructor")

