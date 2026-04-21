
def AcceptList(list):
    if len(list) != 0:
      sum = 0
      count = 0
      for i in list:
        count += 1
        sum += i
      average =  sum/ count
      return average
    elif len(list) == 0:
       print("None")


numbers = [3, 7, 12, 5, 9]
num = []
test = AcceptList(numbers)
print(test)
    