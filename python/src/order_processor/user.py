import random
from products import Product


class User:
    def __init__(self,name,email,orders=None,cart=None,user_id=None):
        self.name = name
        self.email = email
        self.user_id = user_id if user_id is not None else self.generate_id()
        self.orders = orders if orders is not None else []
        self.cart = cart if cart is not None else []
        
    def get_user_name(self):
       return  self.name
    
    def generate_id(self):
       id_s = ["a","b","c","d","e","f","1","2","3","4","65","z","12","g","k","t","c","w","l","0","m","x","v","r","h","i","u","ed","es","ng"]
       constituent = random.sample(id_s,k=4)
       name = self.get_user_name()
       for i in constituent:
         name += i
       return  name 
    
    def get_user_id(self):
       return self.user_id
    
    def add_to_cart(self,product,qty):
       if isinstance(qty,int) and qty > 0 and isinstance(product,Product):
          self.cart.append((product,qty))
          return
       raise ValueError("Invalid quantity")

    def delete_item_from_cart(self,item_index:int):
      if isinstance(item_index,int) and item_index > 0 and len(self.cart) > item_index - 1:
         self.cart.pop(item_index-1) 
         print("Item Removed.") 
         return
      raise IndexError("Invalid Index")
    
    def checkout(self,system):
        return system.place_order(self)
    
    def view_cart(self):
       if not self.cart:
          raise ValueError("Cart Is Empty.")
       total = 0
       num = 1
       for product, qty in self.cart:
          print(f"{num}. {product.get_product_id()} -- {product.get_product_name()}  X {qty} ")
          total += product.get_product_price() * qty
          num += 1
       print(f"Total: ${total:.2f}")   

    def view_all_orders(self):
       if not self.orders:
         raise ValueError("User has no completed Order.")
       for order in self.orders:
          order.display()

    def search_order(self,order_id):
        if not isinstance(order_id,str) or order_id.strip()  == "" or order_id == None:
            raise ValueError("Invalid Order - Id Format.")
        for order in self.orders:
           if order.order_id == order_id:
              return order 
        raise IndexError("Invalid Order Id.")
        
    def delete_checkout(self,order_id,system):
        if not isinstance(order_id,str) or order_id.strip()  == "" or order_id == None:
            raise ValueError("Invalid Order - Id Format.")
        print("Processing User Request")
        orderIds = [order.order_id for order in self.orders]
        if not order_id in orderIds:
           raise ValueError("Invalid order id.")
        del_item_index = orderIds.index(order_id)
        order = self.orders[del_item_index]
        system.stock_return(order.items)
        self.orders.pop(del_item_index)
        print("Checkout cancelled.")
      
           
    def to_json_dict(self):
       return {
          "name": self.name,
          "email": self.email,
          "orders": [order.to_json_dict() for order in self.orders],
          "user_id": self.user_id,
          "cart": [[product.get_product_id(),qty] for product,qty in self.cart]
       }
    
    














# from helper import Generate_id
# # from storage import file_persistence


# def validate_user_id(userid,system_users):
#     if userid in system_users:
#         return True
#     raise ValueError("Invalid user,create an ID via option 2 ")
        
# def Register_User(name,email,users):
#     user_id = Generate_id(name)
#     users[user_id] = {
#         "name": name,
#         "email":email,
#         "orders": []
#     }
#     return user_id

# def get_user_name(user_id,system):
#     return system["Users"][user_id]["name"].capitalize()

# def login(curent_user,system):
#   if validate_user_id(curent_user,system["Users"]):
#      print(f"Welcome back {get_user_name(curent_user,system)}")

# def signup(name,email,system):
#     user_id = Register_User(name,email,system["Users"])
#     print(f"Authorize your transactions with your USER ID '{user_id}'")
#     file_persistence("company",system)
#     return user_id
