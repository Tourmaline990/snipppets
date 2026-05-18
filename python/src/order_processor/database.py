import json
import os
from System import System
from products import Product
from user import User
from order import Order

class Persistence():
    def __init__(self):
        pass

    def save(self,filename,system):
        if not isinstance(system,System):
           raise ValueError(" Not 'System' datatype.")
        system = system.to_json_dict()
        with open(filename,"w") as file:
          json.dump(system,file,indent=4)

    def load(self,filename):
     try:
        if os.path.exists(filename):
           system = System()
           with open(filename,"r") as file:
              system_in_dictionary = json.load(file)
              for key,value in system_in_dictionary["Products"].items():
                 new_product = Product(value["id"],value["name"],value["price"],value["stock"])
                 system.add_product(new_product)
              for key,value in system_in_dictionary["Users"].items():
                 user_orders = [Order([(system.product_lookup(product),qty) for product, qty in order["items"]], order["order_id"],order["date"],order["total"] )for order in value["orders"]]
                 name = value["name"]
                 email = value["email"]
                 user_id = value["user_id"]
                 new_user = User(name,email,user_orders,[(system.product_lookup(product),qty) for product, qty in value["cart"]],user_id)
                 system.add_user(new_user)
              return system
        print(f"File '{filename}' does not exist yet. A new system has been created.")
        return System()
     except(json.JSONDecodeError) as j:
        print(j)
                 
                 
                 
