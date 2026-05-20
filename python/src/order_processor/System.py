
from products import Product
from user import User
from order import Order

class System:

    def __init__(self):
        self.products = {}
        self.users = {}
        
    def product_lookup(self,product_id):
        if not isinstance(product_id,str) or product_id.strip() == "":
            raise ValueError("Invalid Product Id")
        if not product_id in self.products:
            raise IndexError("Product Id not found")
        product = self.products[product_id]
        return product
    
    def add_product(self,product):
      if not isinstance(product,Product):
          raise ValueError("'Product' object required.")
      self.products[product.get_product_id()] = product

    def add_user(self,user:User):
        if not isinstance(user,User):
          raise ValueError("'User' object required.")
        self.users[user.get_user_id()] = user

    def display_products(self):
        for product in self.products.values():
            print(product.display_product_details())

    def reduce_stocks(self,cart):
       for product,qty in cart:
           if qty > product.get_stock_quantity():
              raise ValueError("Insufficient stock.")
       for product, qty in cart:
           product.reduce_stock_quantity(qty)

    def login(self,user_id):
        if not isinstance(user_id,str) or user_id.strip()  == "" :
            raise ValueError("Invalid User Information.")
        if not user_id in self.users:
            raise ValueError("Invalid User Id")
        print("Login successful.")
        return self.users[user_id]
    
    def signup(self,name,email):
        if not (isinstance(email,str) and isinstance(name,str)) or (name.strip() == "" and email.strip() == ""):
            raise ValueError("Provide Valid Information (name and email)")
        new_user = User(name,email)
        self.add_user(new_user)
        return new_user
    
    def place_order(self,user):
        if not user.cart:
            raise ValueError("Cart is Empty.")
        self.reduce_stocks(user.cart)
        order = Order(user.cart)
        user.orders.append(order)
        user.cart.clear()
        print("Checkout Successful.")
        return order.get_order_id()
    
    def stock_return(self,cart):
        if not cart:
         raise ValueError("No order")
        for item in cart:
            product = self.product_lookup(item["product_id"])
            product.increase_stock_quantity(item["qty"])

    
    def to_json_dict(self):
        return{
            "Products": {key: value.to_json_dict() for key,value in self.products.items()},
            "Users": {key: value.to_json_dict() for key,value in self.users.items()}
        }
       
      
    





        
