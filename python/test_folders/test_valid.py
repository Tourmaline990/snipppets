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

