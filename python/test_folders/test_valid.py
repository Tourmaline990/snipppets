from src.valid import Employee
from pytest import approx
import pytest

def test_validate_name():
   employee = Employee("Aaliya",18,100)

   # assert valid name
   assert employee._validate_name("Aaliya") == "Aaliya"

   # assert invalid name 
   with pytest.raises(ValueError) as int_as_name:
      employee._validate_name(123)
      
   assert "Name cannot be empty" == str(int_as_name.value)

   # assert empty name 
   with pytest.raises(ValueError) as empty_name:
      employee._validate_name("")
   assert "Name cannot be empty" == str(empty_name.value)

def test_validate_age():
   # assert valid  age
   employee = Employee("Aaliya",18,100)
   assert employee._validate_age(54) == 54

   # assert invalid age
   with pytest.raises(ValueError) as  invalid_age:
      employee._validate_age(154)
   assert "Age must be between 1 and 120" in str(invalid_age.value)

   # assert string for age
   with pytest.raises(ValueError) as string_age:
      employee._validate_age("123")
   assert "Age must be between 1 and 120" in str(string_age.value)

def test_validate_income():
   # assert valid income
   employee = Employee("Aaliya",18,100)
   assert employee._validate_income(100) == 100
   assert employee._validate_income(100.07) == approx(100.07)

   # assert invalid income(lesser or equal to 0)
   with pytest.raises(IndexError) as invalid_income:
      employee._validate_income(-5)
   assert "Income  cannot be negative" in str(invalid_income.value)
   
   # assert income = 0
   with pytest.raises(IndexError) as zero:
      employee._validate_income(0)
   assert "Income  cannot be negative" in str(zero.value)

def test_employee_details():
   employee = Employee("Aaliya",18,1000)
   assert employee.Employeedetails() == "Employee Informations: Name: Aaliya Age: 18 Income: $1000.00"
   