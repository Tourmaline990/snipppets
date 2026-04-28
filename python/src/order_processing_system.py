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
        except FileNotFoundError as f:
            print(f)
        choice = None
        while choice != 5:
            Display_Menu()
            try:
             choice = int(input("Select a choice: ")) 
            except ValueError as e:
               print(f"Error! {e}")
               choice = int(input("Select a choice: "))             
            if choice == 1:
                print()
                Display_Products(system["products"])
                try:
                 end = ''
                 while end != "done":
                    product_id =  input("Add product to cart (Enter product ID eg: P001): ").upper()
                    while product_id not in system["products"]:
                        print("Please enter a valid product ID")
                        product_id =  input("Add product to cart (Enter product ID eg: P001): ").upper()
                    product_quantity  = int(input("Enter Quantity: "))
                    while product_quantity > system["products"][product_id]["stock"]:
                       print(f"Quantity requested cannot be fufilled. {system["products"][product_id]["stock"]} items left")
                       product_quantity = int(input("Enter a lesser quantity: "))
                    add_to_cart(storage,product_id,product_quantity)
                    end = input("Enter 'done' to leave cart OR click enter to continue: ").lower()
                except ValueError as e:
                    print(f"Error! {e}")

            elif choice == 2:
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
                except ValueError as e:
                 print(f"Error! {e}")

            elif choice == 3: 
                try:
                  user_id = ""
                  while user_id.strip() == "" or not(isinstance(user_id,str)):
                     user_id = input("Enter your user_id to proceed: ")
                  if validate_user_id(user_id,system["Users"]):
                     checkout(storage,system,user_id)
                except ValueError as e:
                    print(f"Error! {e}")

            elif choice == 4:
              try:
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
              except ValueError as e:
                    print(f"Error! {e}")   
               
                 
def Display_Menu():
    menu = ["View Products","Register user","Place Order","View Orders","Exit"]
    for i in range(len(menu)):
        m = menu[i]
        print(f"{i + 1}.  {m}")

def Display_Products(product_dict):
    for key, value in product_dict.items():
        print(f"{key} >> {value["name"]} | Cost: ${value["price"]:.2f} | Items left: {value["stock"]} ")
        print()
        

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
   if os.path.exists(filename):
     with open(filename,"w") as file:
        json.dump(system,file,indent=4)
   else:
       with open(filename,"w") as write_file:
           json.dump(system,write_file,indent=4)

def load_data(filename,system):
    if os.path.exists(filename):
        try:
            with open(filename,"r") as stored_data:
                data = json.load(stored_data)
                return data
        except json.JSONDecodeError as j:
            print(j)
            data = system
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

def present_time():
   present_date = datetime.datetime.now()
   formatted = present_date.strftime("%Y-%m-%d %H:%M:%S")
   return formatted

def checkout(storage,system,user_id):
    print()
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
    cust_order["items"] = list(storage)
    cust_order["date"] = now
    system["Orders"].append(cust_order)
    system["Users"][user_id]["orders"].append(cust_order)
    print(">>  ......")
    print("Order Created Successfully.")
    print(f"ORDER ID  - {order_id}")
    file_persistence("company",system)
    storage.clear() 
    

def view_orders(system,user_id,order_id=None):
    if order_id != None:
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