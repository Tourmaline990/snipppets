import random
import datetime
import re
   
def main():   
        user_id_store = []
        storage = []
        total_price = 0
        system = {
          "products": {},
           "Users": {},
            "Orders": []
        }

        system["products"]["P001"] = {"name": "laptop", "id" : "A112","price": 130,"stock": 10}
        system["products"]["P002"] = {"name": "smartglasses","id":"A113","price": 239, "stock": 15}
        system["products"]["P003"] = {"name": "phone","id":"A114","price":108,"stock": 23}


        choice = None
        menu = ["View Products","Register user","Place Order","View Orders","Exit"]
        while choice != 5:
            for i in range(len(menu)):
                m = menu[i]
                print(f"{i + 1}.  {m}")
            try:
             choice = int(input("Select a choice: ")) 
            except ValueError as e:
               print(f"Error! {e}")
               choice = int(input("Select a choice: "))             
            if choice == 1:
                print()
                Display_Products(system["products"])
                end = ''
                while end != "done":
                    cart = {}
                    cart_id =  input("Add product to cart (Enter product name): ").lower()
                    valid_product_names = [p["name"].lower() for p in system["products"].values()]
                    while cart_id not in valid_product_names:
                       print("Please enter a valid product name")
                       cart_id =  input("Add product to cart (Enter product name): ").lower()
                    cart["product_name"] = cart_id
                    cart_quantity  = int(input("Enter Quantity: "))
                    cart["quantity"] = cart_quantity
                    storage.append(cart)
                    end = input("Enter 'done' to leave cart OR click enter to continue: ").lower()

            elif choice == 2:
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
                user_id = Register_User(name,email,system["Users"],user_id_store)
                print(f"Authorize your transactions with your USER ID '{user_id}'")

            elif choice == 3:
                print()
                present_date = datetime.datetime.now()
                formatted = present_date.strftime("%Y-%m-%d %H:%M:%S")
                try:
                 user_id = ""
                 while user_id.strip() == "" or not(isinstance(user_id,str)):
                     user_id = input("Enter your user_id to proceed: ")
                except ValueError as e:
                   print(f"Error! {e}")
                try:
                 if validate_user_id(user_id,user_id_store):
                    total_price = 0
                    cust_order = {}
                    for x in range(len(storage)):
                        order_item_name = storage[x]["product_name"]
                        for key,value in system["products"].items():
                            if order_item_name == value["name"]:
                                stock_quantity = value["stock"]
                                while storage[x]["quantity"] > stock_quantity:
                                    print(f"'{storage[x]["quantity"]}' '{order_item_name}' Quantity requested cannot be fufilled at the moment, {stock_quantity} items in stock. ")
                                    changed_item_quantity = int(input("Enter a lesser quantity: "))
                                    storage[x]["quantity"] = changed_item_quantity
                                new_quantity = stock_quantity - storage[x]["quantity"]
                                value["stock"] = new_quantity
                                cost = value["price"] * storage[x]["quantity"]
                                total_price += cost
                                break
                        cust_order["total"] = total_price
                        cust_order["items"] = list(storage)
                        cust_order["user_id"] = user_id
                        cust_order["order_id"] = Generate_id("order")
                        cust_order["date"] = f"{formatted}"
                        system["Orders"].append(cust_order)
                        system["Users"][user_id]["orders"].append(cust_order)
                    print(">>  ......")
                    print("Order Created.")
                except ValueError as e:
                    print(f"Error! {e}")
            elif choice == 4:
              try:
                  user_id = input("Enter your user_id to proceed: ")
              except ValueError as e:
                while user_id.strip() == "" or not(isinstance(user_id,str)):
                      print(f"Error! {e}")
                      user_id = input("Enter your user_id to proceed: ")
              try:
               if validate_user_id(user_id,user_id_store):
                   for index in range(len(system["Users"][user_id]["orders"])): # order is an array, the array's index
                       index_dict = system["Users"][user_id]["orders"][index] # a dict holding the order's details
                       print()
                       print("===== Order Details ========= ")
                       print(f"USER ID - {index_dict["user_id"]}") # {system["Users"][user_id]["orders"][index]
                       print(f"ORDER ID - {index_dict["order_id"]}")
                       print()
                       print("     -- purchase --    ")
                       for i in index_dict["items"]:
                             name = i["product_name"]
                             qty = i["quantity"]
                             found = None
                             for j in system["products"].values():
                                 if name == j["name"]:
                                     found = j
                                     break
                             if found:
                              sum_per_item = j["price"] * qty
                             print(f"{name}  X  {qty} = ${sum_per_item:.2f}")        
                       print()
                       print(f"TOTAL ORDER AMOUNT - ${index_dict["total"]:.2f}")
                       print()
                       print(f"==== Date Ordered: {index_dict["date"]} =========")
               storage.clear()
              except ValueError as e:
                    print(f"Error! {e}")     
                    print()
               
            
                
            

def Display_Products(product_dict):
    for key, value in product_dict.items():
        print(f">> {value["name"]} | Cost: ${value["price"]:.2f} | Items left: {value["stock"]} ")
        print()

def Register_User(name,email,dict,store):
    u_i = Generate_id(name)
    dict[u_i] = {
        "name": name,
        "email":email,
        "orders": []
    }
    store.append(u_i)
    return u_i


def Generate_id(recepient):
    id_s = ["a","b","c","d","e","f","1","2","3","4","65","z","12","g","k","t","c","w","l","0","m","x","v","r","h","i","u"]
    constituent = random.sample(id_s,k=4)
    for i in constituent:
        recepient += i
    return  recepient 

def validate_user_id(id,id_storage):
    if id in id_storage:
        return True
    raise ValueError("Invalid user,create an ID via option 2 ")


if __name__ == "__main__":
    main()