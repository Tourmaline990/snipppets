class Product:
   def __init__(self,product_id,name,price,stock):
     self.name = name
     self.product_id = product_id
     self.price  = price
     self.stock = stock
     
   def get_product_name(self):
      return self.name
   def get_product_id(self):
      return self.product_id
   def get_product_price(self): 
      return self.price
   def get_stock_quantity(self):
      return self.stock
   def increase_stock_quantity(self,new_qty:int):
      if not(isinstance(new_qty,int)) or new_qty <= 0:
         raise ValueError("quantity should be greater than Zero,and an int datatype.")
      self.stock += new_qty  
   def reduce_stock_quantity(self,qty:int):
      if not(isinstance(qty,int)) or qty <= 0:
         raise ValueError("quantity should be greater than Zero,and an int datatype.")
      if qty > self.stock:
         raise ValueError("Quantity exceeds available stock")
      self.stock -= qty
      
   def display_product_details(self):
      return f"{self.product_id} >>> {self.name} | Cost: {self.price} | Items Left: {self.stock}"
   
   def to_json_dict(self):
      return{
         "name": self.name,
         "id": self.product_id,
         "price": self.price,
         "stock": self.stock
      }
   
    

    
   

# def merge_same_products(storage):
#  merged = {}
#  for item in storage:
#     p_id  = item["product_id"]
#     qty = item["quantity"]
    
#     if p_id in merged:
#        merged[p_id]["quantity"] += qty
#     else:
#        merged[p_id] = item.copy()

#  return list(merged.values())

# def Display_Products(product_dict):
#     for product in product_dict.values():
#        print(product.display_product_details())

# def validate_product_id(product_id,system):
#    if product_id not in system["products"]:
#        return False
#    return True

