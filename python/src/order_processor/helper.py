
import re

def Display_Menu():
    menu = ["View Products","Cart","Place Order","View Orders","Cancel Checkout","Exit"]
    for i in range(len(menu)):
        m = menu[i]
        print(f"{i + 1}.  {m}")

def intro_message():
    print("Existing users should 'login' to continue transactions.")
    print("New users are advised to 'sign up' for better experience.")
    print("Enter 'login' OR 'signup' ")

def string_validation(string_to_validate,display_message,truthy_condition: list |str | None = None):
    """truthy_condition must evaluate to true"""
    response = ""
    while string_to_validate.strip() == "" or not (isinstance(string_to_validate,str)):
        print(display_message)
        response = input(">>>  ")
        string_to_validate = response
    if truthy_condition is not None and isinstance(truthy_condition,list):
        if len(truthy_condition) > 1:
          while response not in(truthy_condition):
            print(f"Response should be one of the following: {truthy_condition}")
            response = input(">>>  ")
          return response 
    elif truthy_condition is not None and isinstance(truthy_condition,str): 
        if truthy_condition == "email":
            regex_pattern = r'^[a-z0-9]+[\._]?[a-z0-9]+[@]\w+[.]\w{2,}$'
            while not re.match(regex_pattern,response):
                print(display_message)
                response = input(">>>  ")
            return response
        else:
         while response !=  truthy_condition:
            print(f"{display_message}")
            response = input(">>>  ")
            return response
    return response

def int_validation(num_to_validate,display_msg,truthy:list| None = None):
    """num_to_validate equal none, so the loop executes"""

    response = None
    num_to_validate = None
    while num_to_validate == None or not(isinstance(num_to_validate,int)):
        print(display_msg)
        response = int(input(">>> "))
        num_to_validate = response
    if truthy is not None:
        while response not in truthy:
            response = int(input(">>> "))
        return response
    return response

