
def cart_actions():
    actions = ["Add to Cart","View Cart","Remove item from cart","Exit"]
    for i in range(len(actions)):
        a = actions[i]
        print(f"{i + 1}.  {a}")


# def display_cart_items(storage,system):
#     if storage:
#      cart = [item.copy() for item in storage]
#      count = 0
#      total = 0
#      for item in cart:
#        count += 1
#        p_id = item["product_id"]
#        p_name = get_product_name(p_id,system)
#        price = get_product_price(p_id,system)
#        print(f"{count}. {p_id} >> {p_name} - {item["quantity"]}")
#        total += item["quantity"] * price
#        print()
#      print(f"Total: ${total:.2f}")
#     else:
#         raise ValueError("Cart is Empty")
    

# def delete_cart_item(storage,index):
#     product_index = index - 1
#     if len(storage) > product_index:
#       storage.pop(product_index)
#       print("........")
#       print("item removed.")
#     else:
#         raise IndexError("Number entered is greater than items in cart")
    


# def add_to_cart(storage,id,quantity):
#     cart = {}
#     cart["product_id"] = id
#     cart["quantity"] = quantity
#     storage.append(cart)