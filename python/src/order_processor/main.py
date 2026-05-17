from products import Product
from helper import intro_message,Display_Menu,string_validation,int_validation
from cart import cart_actions
from database import Persistence


   
def main(): 
    try:
       data = Persistence()
       system =  data.load("company")
    except FileNotFoundError as f: print(f"Error! {f}")

    system.add_product(Product("p001","laptop",130,20 ))
    system.add_product(Product("p002","smartglasses",240,20))
    system.add_product(Product("p003","phone",110,23))
    system.add_product(Product("p004","Tablets",250,40))
    system.add_product(Product("p005","SmartWatch",350,90))
    data.save("company",system)
    
    current_user = ""
    status = ""
    choice = None
  
    intro_message()
    status = string_validation(status,"Login or Signup",["login","signup"])
    if status == "login":
      user_id = string_validation(current_user,"Enter your user_id to proceed: ")
      try:
        user = system.login(user_id)
      except ValueError as v: print(v) 
      current_user = user
      print()
    elif status == "signup":
      print()
      name = string_validation("","Enter a preffered username: ")
      email = string_validation("","Enter a valid email address: ","email")
      try:
        user = system.signup(name,email)
        current_user = user
        print(f"Save your User-Id: {current_user.get_user_id()}")
        data.save("company",system)
      except ValueError as v: print(f"Error! {v}")

    while choice != 6:
      print()
      Display_Menu()
      choice = int_validation(choice,"Select an action: ")
      if choice == 1:
        print()
        system.display_products()
      elif choice == 2:
        action = None
        while action !=  4:
          print()
          cart_actions()
          print()
          action = int_validation(action,"Select an action or enter 4 to quit")
          if action == 1:
            end = ''
            while end != "done":
             try:
                product_id =  input("Add product to cart (Enter product ID eg: P001): ").lower()
                product = system.product_lookup(product_id)
             except(ValueError,IndexError) as err:
                print("Please enter a valid product ID")
                product_id = input("Add product to cart (Enter product ID eg: P001): ").lower()
                product = system.product_lookup(product_id)
             product_quantity  = int_validation(None,"Enter Quantity")
             while product_quantity > product.get_stock_quantity():
                print(f"Quantity requested cannot be fufilled. {product.get_stock_quantity()} items left")
                product_quantity = int(input("Enter a lesser quantity: "))
             current_user.add_to_cart(product,product_quantity)
             end = input("Enter 'done' to leave cart OR click enter to continue: ").lower()
             data.save("company",system)
           
          elif action == 2:
              print()
              try:
               current_user.view_cart()
              except ValueError as vr: print(vr)

          elif action == 3:
              remove_index = int_validation(None,"Enter a number to delete: ")
              try:
                current_user.delete_item_from_cart(remove_index)
              except IndexError as I: print(f"Error! {I}")
              data.save("company",system)                                       
      elif choice == 3: 
          try:
              order_id = current_user.checkout()
              print(f"Order Id - {order_id}")
              data.save("company",system)
          except ValueError as val: print(f"Error! {val}")           
      elif choice == 4:
            print("Enter 1 to view all orders OR click 2 to filter by ORDER_ID")
            viewing_type = int_validation(None,"Enter 1 or 2",[1,2])
            if viewing_type == 1:
             try:
              current_user.view_all_orders()
             except ValueError as inderr: print(f"Error! {inderr}")   
            else:
             order_id = string_validation("","Enter Order Id:")
             try:
                order = current_user.search_order(order_id)
                order.display()
             except IndexError as err: print(f"Error! {err}")            
      elif choice == 5:
          print("Proceed with caution")
          print("Do you want to proceed with order cancellation? (yes/no)")
          proceed = string_validation("","Enter yes OR no",["yes","no"])
          if proceed == "yes":
              order_id = string_validation("","Enter your order_id to proceed")
              try:
                current_user.delete_checkout(order_id)
                data.save("company",system)
              except (IndexError,FileNotFoundError,ValueError) as error:
                 print(error)
          else:
            print("Cancellation Invalidated.")
                
if __name__ == "__main__":
    main()