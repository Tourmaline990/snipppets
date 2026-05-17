import random
import datetime
import copy

class Order:
    def __init__(self,items,order_id = None,date = None,total = None):
        self.items = self.get_cart(items)
        self.order_id = order_id if order_id is not None else self.generate_order_id()
        self.date = date if date is not None else self.present_time()
        self.total = total if total is not None else self.calculate_total()
        
    def get_order_id(self):
       return self.order_id
    
    def generate_order_id(self):
        id_s = ["a","b","c","d","e","f","1","2","3","4","65","z","12","g","k","t","c","w","l","0","m","x","v","r","h","i","u","ed","es","ng"]
        constituent = random.sample(id_s,k=4)
        name = "order"
        for i in constituent:
         name += i
        return  name 
    
    def present_time(self):
      present_date = datetime.datetime.now()
      formatted = present_date.strftime("%Y-%m-%d %H:%M:%S")
      return formatted

    def get_cart(self,cart):
        cart = copy.deepcopy(cart)
        if not cart:
          raise ValueError("Cart is Empty")
        return cart 
    
    def calculate_total(self):
       total = 0
       for product, qty in self.items:
            total += product.get_product_price() * qty
       return total
    
    def checkout(self):
        for product, qty in self.items:
           if qty > product.get_stock_quantity():
              raise ValueError("Insufficient stock.")
        for product, qty in self.items:
           product.reduce_stock_quantity(qty)
        print("Order Created Successfully.")
    
    def display(self):
       print()
       print("===== Order Details ========= ")
       print(f"ORDER ID - {self.order_id}")
       print() 
       print("     -- purchase --    ")
       for product, qty in self.items:
          print(f"{product.get_product_name()}  X  {qty}")
       print()
       print(f"TOTAL ORDER AMOUNT - ${self.total:.2f}")
       print()
       print(f"==== Date Ordered: {self.date} =========")

    def to_json_dict(self):
       return{
          "order_id": self.order_id,
          "items": [[product.to_json_dict(),qty] for product,qty in self.items],
          "date": self.date,
          "total": self.total
       }

           









# from helper import present_time,Generate_id
# from products import merge_same_products
# #from storage import file_persistence
# # from products import get_product_price,get_product_name,get_stock_quantity,update_stock_quantity,validate_product_id

# def get_order_by_user(system,user_id):
#     return system["Users"][user_id]["orders"]

# def get_all_orders(system):
#     return system["Orders"]

# def add_new_order(system,new_order,user_id):
#     system["Users"][user_id]["orders"].append(new_order)
#     system["Orders"].append(new_order)

# def get_order_details(system,order_id,user_id):
#     seen = None
#     for i in get_order_by_user(system,user_id):
#         if order_id == i["order_id"]:
#             seen = i 
#         return seen
#     raise IndexError("Order Id not found")

# def checkout(storage,system,user_id):
#     print()
#     if storage:
#         total_price = 0
#         cust_order = {}
#         now = present_time()
#         for i in storage:
#           product_id = i["product_id"]
#           if validate_product_id(product_id,system):
#             new_quantity = get_stock_quantity(product_id,system) - i["quantity"]
#             update_stock_quantity(system,product_id,new_quantity)
#             cost = get_product_price(product_id,system) * i["quantity"]
#             total_price += cost
#         cust_order["user_id"] = user_id
#         order_id = Generate_id("order")
#         cust_order["order_id"] = order_id
#         cust_order["total"] = total_price
#         cust_order["items"] = merge_same_products([i.copy() for i in storage])
#         cust_order["date"] = now
#         add_new_order(system,cust_order,user_id)
#         print(">>  ......")
#         print("Order Created Successfully.")
#         print(f"ORDER ID  - {order_id}")
#         file_persistence("company",system)
#         storage.clear() 
#     else:
#        raise ValueError("Cart is Empty")
    
# def view_orders(system,user_id,order_id=None):
#     if order_id != None:
#       order = get_order_details(system,order_id,user_id)
#       if order:
#             Display_Order_Details(order,system)
#     else:
#       for i in get_all_orders(system):
#           Display_Order_Details(i,system)

# def Display_Order_Details(order_object,system):
#    print()
#    print("===== Order Details ========= ")
#    print(f"USER ID - {order_object["user_id"]}")
#    print(f"ORDER ID - {order_object["order_id"]}")
#    print()
#    print("     -- purchase --    ")
#    for item in order_object["items"]:
#        pr_id = item["product_id"]
#        qty = item["quantity"]
#        sum_per_item = get_product_price(pr_id,system) * qty
#        print(f"{get_product_name(pr_id,system)}  X  {qty} = ${sum_per_item:.2f}")
#    print()
#    print(f"TOTAL ORDER AMOUNT - ${order_object["total"]:.2f}")
#    print()
#    print(f"==== Date Ordered: {order_object["date"]} =========")

# def cancel_checkout(user_id,order_id,system):
#      print("Analyzing user status...")
#      order =  get_order_details(system,order_id,user_id)
#      if order:
#         print("User request in progress...")
#         for i in order["items"]:
#            product_id = i["product_id"]
#            qty = i["quantity"]
#            available_stock = get_stock_quantity(product_id,system) + qty
#            update_stock_quantity(system,product_id,available_stock)
#         get_order_by_user(system,user_id).remove(order)
#         get_all_orders(system).remove(order)
#         print("Order Cancelled")
       
