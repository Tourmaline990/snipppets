import random
   
def main():   

        user_id_store = []
        storage = []
        total_price = 0
        system = {
          "products": {},
           "Users": {},
            "Orders": {}
        }

        system["products"]["P001"] = {"name": "laptop", "id" : "A112","price": 130,"stock": 10}
        system["products"]["P002"] = {"name": "smartglasses","id":"A113","price": 239, "stock": 15}
        system["products"]["P003"] = {"name": "phone","id":"A114","price":108,"stock": 23}


        choice = 0
        menu = ["View Products","Register user","Place Order","View Orders","Exit"]
        while choice != 5:
            for i in range(len(menu)):
                m = menu[i]
                print(f"{i + 1}.  {m}")
            choice = int(input("Select a choice: "))
            if choice == 1:
                print()
                Display_Products(system["products"])
                end = ''
                while end != "done":
                    cart_id =  input("Add product to cart (Enter product name): ").lower()
                    cart_quantity  = int(input("Enter Quantity: "))
                    cart = {}
                    cart["product_id"] = cart_id
                    cart["quantity"] = cart_quantity
                    storage.append(cart)
                    end = input("Enter 'done' to leave cart OR click enter to continue: ").lower()

            elif choice == 2:
                print()
                name = input("Enter your username:  ")
                email = input("Enter a valid email address: ")
                user_id = Register_User(name,email,system["Users"],user_id_store)
                print(f"Authorize your transactions with your USER ID '{user_id}'")

            elif choice == 3:
                print()
                user_id = input("Enter your user_id to proceed: ")
                try:
                 if validate_user_id(user_id,user_id_store):
                    for x in range(len(storage)):
                        order_item_name = storage[x]["product_id"]
                        for key,value in system["products"].items():
                            if order_item_name == value["name"]:
                                reduce_quantity = value["stock"]
                                new_quantity = reduce_quantity - storage[x]["quantity"]
                                value["stock"] = new_quantity
                                cost = value["price"] * storage[x]["quantity"]
                                total_price += cost
                                break
                    cust_order = {}
                    cust_order["total"] = total_price
                    cust_order["items"] = storage
                    cust_order["user_id"] = user_id
                    cust_order["order_id"] = Generate_id("order")
                    system["Orders"] = cust_order
                    system["Users"]["orders"] = cust_order
                    print(">>  ......")
                    print("Order Created.")
                except ValueError as e:
                    print(f"Error! {e}")
            elif choice == 4:
                user_id = input("Enter your user_id to proceed: ")
                try:
                  if validate_user_id(user_id,user_id_store):
                   print(system["Users"]["orders"])
                   print()
                   print("===== Order Details ========= ")
                   print(f"USER ID - {system["Users"]["orders"]["user_id"]}")
                   print(f"ORDER ID - {system["Users"]["orders"]["order_id"]}")
                   print("== purchase ==")
                   for item in range(len(system["Users"]["orders"]["items"])):
                       items = system["Users"]["orders"]["items"][item]
                       print(f"{items["product_id"]}  X  {items["quantity"]}")
                  print(f"TOTAL ORDER AMOUNT - ${system["Users"]["orders"]["total"]:.2f}")
                except ValueError as e:
                    print(f"Error! {e}")     
                print()


def Display_Products(product_dict):
    for key, value in product_dict.items():
        print(f">> {value["name"]} | Cost: ${value["price"]:.2f} | Items left: {value["stock"]} ")
        print()

def Register_User(name,email,dict,store):
    dict["name"] = name
    dict["email"] = email
    dict["orders"] = []
    u_i = Generate_id(name)
    store.append(u_i)
    return u_i


def Generate_id(recepient):
    id_s = ["a","b","c","d","e","f","1","2","3","4","65","z","12","g","k","t","c","w","l","0","m","x","v","r","h"]
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