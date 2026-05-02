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
        
        
        try: 
          system = load_data("company",system)
        except FileNotFoundError as f:
            print(f"Error! {f}")
        status = None
        print("Existing users should 'login' to continue transactions.")
        print("New users are advised to 'sign up' for better experience.")
        print("Enter 'login' OR 'signup' ")
        current_user = ""
        try:
           status = input(">>>  ").lower()
        except ValueError as v:
           print(f"Error! {v}")
        while status != "login" and status != "signup":
           print("Login or Signup")
           status = input(">>>  ").lower()
        if status == "login":
            while current_user.strip() == "" or not(isinstance(current_user,str)):
               current_user = input("Enter your user_id to proceed: ")
            try:
              if validate_user_id(current_user,system["Users"]):
                print()
                print(f"Welcome back {system["Users"][current_user]["name"].capitalize()}")
                print()
            except ValueError as va:
               print(f"Error! {va}")
        elif status == "signup":
            regex_pattern = r'^[a-z0-9]+[\._]?[a-z0-9]+[@]\w+[.]\w{2,}$'
            print()
            try:
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
                current_user = user_id   
            except ValueError as e:
              print(f"Error! {e}")  
        choice = None
        while choice != 6:
            Display_Menu()
            try:
               choice = int(input("Select an action: ")) 
            except ValueError as e:
               print(f"Error! {e}")               
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
                  try:
                    action = int(input(">>> "))
                  except ValueError as e:
                     print(f"Error! {e}")   
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
                      storage = merge_same_products(storage)
                  elif action == 2:
                    try:
                     display_cart_items(storage,system)
                    except IndexError as ind:
                       print(f"Error! {ind}")
                  elif action == 3:
                      remove_index = None
                      while remove_index == None and not(isinstance(remove_index,int)):
                          remove_index = int(input("Enter the number you will like to delete:  "))
                      try:
                        delete_cart_item(storage,remove_index)
                      except IndexError as I:
                        print(f"Error! {I}")                           
            elif choice == 3: 
                try:
                  checkout(storage,system,current_user)
                except IndexError as inder:
                   print(f"Error! {inder}")
                   
            elif choice == 4:
                try:
                    print("Enter 1 to view all orders OR click 2 to filter by ORDER_ID")
                    viewing_type = None
                    while viewing_type != 1 and viewing_type != 2:
                      viewing_type = int(input(">>> "))
                    if viewing_type == 1:
                     try:
                       view_orders(system,current_user)
                     except IndexError as inderr:
                         print(f"Error! {inderr}")
                    else:
                      order_id = ""
                      while order_id.strip() == "" or not(isinstance(order_id,str) ):
                        order_id = input("Enter Order Id: ")
                      try:
                        view_orders(system,current_user,id)
                      except IndexError as err:
                         print(f"Error! {err}")
                except ValueError as e:
                  print(f"Error! {e}") 
            elif choice == 5:
                try:
                  print("Proceed with caution")
                  print("Do you want to proceed with order cancellation? (yes/no)")
                  proceed = input(">>>  ").lower()
                  if proceed == "yes":
                    order_id = input("Enter your order_id to proceed with cancellation: ")
                    while order_id.strip() == "" or not(isinstance(order_id,str)):
                        order_id = input("Enter your user_id to proceed: ")
                    cancel_checkout(current_user,order_id,system)
                    file_persistence("company",system)
                  else:
                      print("Cancellation Invalidated.")
                except ValueError as e:
                 print(f"Error! {e}") 
        
    except Exception as ex:
        print(f"Error! {ex}")


def cancel_checkout(user_id,order_id,system):
 if validate_user_id(user_id,system["Users"]):
     print("Analyzing user status...")
     order =  find_order_id(system,order_id,user_id)
     if order:
        print("User request in progress...")
        for i in order["items"]:
           product_id = i["product_id"]
           qty = i["quantity"]
           available_stock = system["products"][product_id]["stock"]
           recomputed = available_stock + qty
           system["products"][product_id]["stock"] = recomputed

        system["Users"][user_id]["orders"].remove(order)
        system["Orders"].remove(order)
        print("Order Cancelled")
       
def find_order_id(system,order_id,user_id):
    seen = None
    for i in system["Users"][user_id]["orders"]:
        if order_id == i["order_id"]:
            seen = i 
        return seen
    raise IndexError("Order Id not found")

def validate_product_id(id,system):
   if id not in system["products"]:
       return False
   return True

def display_cart_items(storage,system):
    if storage:
     store = [item.copy() for item in storage]
     count = 0
     total = 0
     for i in store:
       count += 1
       p_id = i["product_id"]
       p_name = system["products"][p_id]["name"]
       price = system["products"][p_id]["price"]
       print(f"{count}. {p_id} >> {p_name} - {i["quantity"]}")
       total += i["quantity"] * price
       print()
     print(f"Total: ${total:.2f}")
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
    menu = ["View Products","Cart","Place Order","View Orders","Cancel Checkout","Exit"]
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
    id_s = ["a","b","c","d","e","f","1","2","3","4","65","z","12","g","k","t","c","w","l","0","m","x","v","r","h","i","u","ed","es","ng"]
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
 merged = {}
 for item in storage:
    p_id  = item["product_id"]
    qty = item["quantity"]
    
    if p_id in merged:
       merged[p_id]["quantity"] += qty
    else:
       merged[p_id] = item.copy()


 storage = list(merged.values())
 return storage

      

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
       raise IndexError("Cart is Empty")
    

def view_orders(system,user_id,order_id=None):
    if order_id != None:
      order = find_order_id(system,order_id,user_id)
      if order:
            Display_Order_Details(order,system)
    else:
      for i in system["Users"][user_id]["orders"]:
          Display_Order_Details(i,system)

           


if __name__ == "__main__":
    main()