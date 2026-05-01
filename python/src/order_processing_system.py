import random
import datetime
import re
import os
import json
   
def main():   
        storage = []
        system = {
          "products": {},
           "Users": {},
            "Orders": []
        }

        system["products"]["P001"] = {"name": "laptop", "id" : "A112","price": 130,"stock": 10}
        system["products"]["P002"] = {"name": "smartglasses","id":"A113","price": 239, "stock": 15}
        system["products"]["P003"] = {"name": "phone","id":"A114","price":108,"stock": 23}

        try: 
           system = load_data("company",system)
           choice = None
           while choice != 7:
              Display_Menu()
              choice = int(input("Select an action: "))             
              if choice == 1:
                print()
                Display_Products(system["products"])
              elif choice == 2:
                action = None
                while  action !=  4:
                  print()
                  cart_actions()
                  print()
                  print("Select an action or enter 4 to quit")
                  action = int(input(">>> "))
                  if action == 1:
                      end = ''
                      while end != "done":
                          product_id =  input("Add product to cart (Enter product ID eg: P001): ").upper()
                          while not validate_product_id(product_id,system):
                            print("Please enter a valid product ID")
                            product_id =  input("Add product to cart (Enter product ID eg: P001): ").upper()
                            validate_product_id(product_id,system)
                          product_quantity  = int(input("Enter Quantity: "))
                          while product_quantity > system["products"][product_id]["stock"]:
                            print(f"Quantity requested cannot be fufilled. {system["products"][product_id]["stock"]} items left")
                            product_quantity = int(input("Enter a lesser quantity: "))
                          add_to_cart(storage,product_id,product_quantity)
                          end = input("Enter 'done' to leave cart OR click enter to continue: ").lower()
                  elif action == 2:
                     display_cart_items(storage,system)
                  elif action == 3:
                      remove_index = None
                      while remove_index == None and not(isinstance(remove_index,int)):
                          remove_index = int(input("Enter the number you will like to delete:  "))
                      delete_cart_item(storage,remove_index)
                           
              elif choice == 3:
                regex_pattern = r'^[a-z0-9]+[\._]?[a-z0-9]+[@]\w+[.]\w{2,}$'
                print()
                name = input("Enter a preffered username:  ")
                email = input("Enter a valid email address: ")
                while not (isinstance(email,str) and re.match(regex_pattern,email)):
                        print("Please enter a valid email address.")
                        email = input("Enter a valid email address: ")
                while not(name.strip() != "" and name != None):
                        print("Name cannot be empty")
                        name = input("Enter a preffered username:  ")
                user_id = Register_User(name,email,system["Users"])
                print(f"Authorize your transactions with your USER ID '{user_id}'")
                file_persistence("company",system)
              elif choice == 4: 
                  user_id = ""
                  while user_id.strip() == "" or not(isinstance(user_id,str)):
                     user_id = input("Enter your user_id to proceed: ")
                  if validate_user_id(user_id,system["Users"]):
                     checkout(storage,system,user_id)
              elif choice == 5:
                  user_id = input("Enter your user_id to proceed: ")
                  while user_id.strip() == "" or not(isinstance(user_id,str)):
                      user_id = input("Enter your user_id to proceed: ")
                  if validate_user_id(user_id,system["Users"]):
                    print("Enter 1 to view all orders OR click 2 to filter by ORDER_ID")
                    viewing_type = None
                    while viewing_type != 1 and viewing_type != 2:
                      viewing_type = int(input(">>> "))
                    if viewing_type == 1:
                      view_orders(system,user_id)
                    else:
                      id = ""
                      while id.strip() == "" or not(isinstance(id,str) ):
                        id = input("Enter Order Id: ")
                      view_orders(system,user_id,id)
              elif choice == 6:
                  print("Proceed with caution")
                  print("Do you want to proceed with order cancellation? (yes/no)")
                  proceed = input(">>>  ").lower()
                  if proceed == "yes":
                    user_id = input("Enter your user_id to proceed: ")
                    while user_id.strip() == "" or not(isinstance(user_id,str)):
                        user_id = input("Enter your user_id to proceed: ")
                    order_id = input("Enter your order_id to proceed with cancellation: ")
                    while order_id.strip() == "" or not(isinstance(order_id,str)):
                        order_id = input("Enter your user_id to proceed: ")
                    cancel_checkout(user_id,order_id,system)
                    file_persistence("company",system)
                  else:
                      print("Cancellation Invalidated.")
        except ValueError as e:
            print(f"Error! {e}")   
        except FileNotFoundError as f:
            print(f"Error! {f}")
        except IndexError as I:
            print(f"Error! {I}")
        except KeyError as k:
            print(f"Error! {k}")
        except TypeError as t:
            print(f"Error! {t}")
        except Exception as ex:
             print(f"Error! {ex}")


def cancel_checkout(user_id,order_id,system):
 if validate_user_id(user_id,system["Users"]):
     print("Analyzing user status...")
     seen = None
     for i in system["Users"][user_id]["orders"]:
          if order_id == i["order_id"]: 
             seen = i  
             print("User request in progress...") 
             system["Users"][user_id]["orders"].remove(seen)
     print("Order Cancelled")         
def validate_product_id(id,system):
   if id not in system["products"]:
       return False
   return True

def display_cart_items(storage,system):
    if storage:
     store = merge_same_products(storage)
     count = 0
     for i in store:
       count += 1
       p_id = i["product_id"]
       p_name = system["products"][p_id]["name"]
       print(f"{count}. {i["product_id"]} >> {p_name} - {i["quantity"]}")
    else:
        raise IndexError("Cart is Empty")
    
def delete_cart_item(storage,index):
    product_index = index - 1
    if len(storage) > product_index:
     storage.pop(product_index)
     print("........")
     print("item removed.")
    else:
        raise IndexError("Number entered is greater than items in cart")
def Display_Menu():
    menu = ["View Products","Cart","Register user","Place Order","View Orders","Cancel Checkout","Exit"]
    for i in range(len(menu)):
        m = menu[i]
        print(f"{i + 1}.  {m}")

def Display_Products(product_dict):
    for key, value in product_dict.items():
        print(f"{key} >> {value["name"]} | Cost: ${value["price"]:.2f} | Items left: {value["stock"]} ")
        print()
        
def cart_actions():
    actions = ["Add to Cart","View Cart","Remove item from cart","Quit"]
    for i in range(len(actions)):
        a = actions[i]
        print(f"{i + 1}.  {a}")

def Register_User(name,email,dict):
    u_i = Generate_id(name)
    dict[u_i] = {
        "name": name,
        "email":email,
        "orders": []
    }
    return u_i


def Generate_id(recepient):
    id_s = ["a","b","c","d","e","f","1","2","3","4","65","z","12","g","k","t","c","w","l","0","m","x","v","r","h","i","u","red","res"]
    constituent = random.sample(id_s,k=4)
    for i in constituent:
        recepient += i
    return  recepient 

def validate_user_id(id,user_dict):
    if id in user_dict:
        return True
    raise ValueError("Invalid user,create an ID via option 2 ")
        

def file_persistence(filename,system):
     with open(filename,"w") as file:
        json.dump(system,file,indent=4)
  

def load_data(filename,system):
    if os.path.exists(filename):
        try:
            with open(filename,"r") as stored_data:
                data = json.load(stored_data)
                return data
        except json.JSONDecodeError as j:
            print(j)
            data = system
            return data
    else:
      raise FileNotFoundError(f"File '{filename}' does not exist")
    
def Display_Order_Details(order_object,system):
   print()
   print("===== Order Details ========= ")
   print(f"USER ID - {order_object["user_id"]}")
   print(f"ORDER ID - {order_object["order_id"]}")
   print()
   print("     -- purchase --    ")
   for i in order_object["items"]:
       id = i["product_id"]
       qty = i["quantity"]
       p_dict = system["products"][id]
       sum_per_item = p_dict["price"] * qty
       print(f"{p_dict["name"]}  X  {qty} = ${sum_per_item:.2f}")
   print()
   print(f"TOTAL ORDER AMOUNT - ${order_object["total"]:.2f}")
   print()
   print(f"==== Date Ordered: {order_object["date"]} =========")
                     

def add_to_cart(storage,id,quantity):
    cart = {}
    cart["product_id"] = id
    cart["quantity"] = quantity
    storage.append(cart)

def merge_same_products(storage):
 storage_copy = [i.copy()  for i in storage ]
 occurence = []
 all_keys = [i["product_id"] for i in storage_copy]
 for p_ids in all_keys:
    total_product_quantity = 0
    for index in range(len(all_keys)):
         if all_keys[index] == p_ids:
             occurence.append(index)
    if len(occurence) > 1:
       for i in occurence:
        quantity = storage_copy[i]["quantity"]
        total_product_quantity += quantity
       first_location = occurence[0]
       product = storage_copy[first_location]
       product["quantity"] = total_product_quantity
       for i in occurence:
           if i != first_location:
               storage_copy.pop(i)
               all_keys.pop(i)  
    occurence.clear()
 return storage_copy
      

def present_time():
   present_date = datetime.datetime.now()
   formatted = present_date.strftime("%Y-%m-%d %H:%M:%S")
   return formatted

def checkout(storage,system,user_id):
    print()
    if storage:
        total_price = 0
        cust_order = {}
        now = present_time()
        for i in storage:
          order_item_id = i["product_id"]
          if order_item_id in system["products"]:
            product_dict = system["products"][order_item_id]
            new_quantity = product_dict["stock"] - i["quantity"]
            product_dict["stock"] = new_quantity
            cost = product_dict["price"] * i["quantity"]
            total_price += cost
        cust_order["user_id"] = user_id
        order_id = Generate_id("order")
        cust_order["order_id"] = order_id
        cust_order["total"] = total_price
        cust_order["items"] = merge_same_products([i.copy() for i in storage])
        cust_order["date"] = now
        system["Orders"].append(cust_order)
        system["Users"][user_id]["orders"].append(cust_order)
        print(">>  ......")
        print("Order Created Successfully.")
        print(f"ORDER ID  - {order_id}")
        file_persistence("company",system)
        storage.clear() 
    else:
       raise ValueError("Cart id Empty")
    

def view_orders(system,user_id,order_id=None):
    if order_id != None:
      seen = None
      for i in system["Users"][user_id]["orders"]:
          if order_id == i["order_id"]: 
             seen = i 
             break
      if seen:
            Display_Order_Details(seen,system)
      else: 
             raise ValueError("Order_Id not found")
    else:
      for i in system["Users"][user_id]["orders"]:
          Display_Order_Details(i,system)

           


if __name__ == "__main__":
    main()