from src.order_processor.helper import string_validation,int_validation
import pytest
from unittest.mock import patch

def test_string_validation():
    # assert two arguments
    with patch('builtins.input', return_value="aliya"):
      result =  string_validation("","Enter a name: ")
    assert result == "aliya"

    # assert three arguments with a list for default
    with patch('builtins.input',return_value = "login"):
        list_result = string_validation("login","Login or signup",["login","signup"])
    assert list_result == "login"
  
    # assert email
    with patch("builtins.input",return_value = "agbetoba@gmail.com"):
        email_result = string_validation("agbetoba@gmail.com","Enter a valid email address","email")
    assert email_result == "agbetoba@gmail.com"

    # assert simple string
    with patch("builtins.input",return_value = "done"):
        string_result = string_validation("done","Enter Done","done")
    assert string_result == "done"

def test_int_validation():
    # assert two arguments
    with patch("builtins.input",return_value = 1):
        num_result = int_validation(1,"Enter num")
    assert num_result == 1

    # assert three arguments
    with patch("builtins.input",return_value = 2 ):
        num_three_arg = int_validation(2,"choose one",[1,2])
    assert num_three_arg == 2