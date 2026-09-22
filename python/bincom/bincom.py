
import requests
from bs4 import BeautifulSoup;
import psycopg
import os
import random
from dotenv import load_dotenv


load_dotenv()

ALL_COLORS = []
WEEK_DAYS = []
COLOR_OCCURRENCE = []
url = "https://drive.google.com/file/d/1nf9WMDjZWIUnlnKyz7qomEYDdtWfW1Uf/view"
file_id = url.split("/d/")[1].split("/")[0]
download_url = f"https://drive.google.com/uc?export=download&id={file_id}"

response = requests.get(download_url)

soup = BeautifulSoup(response.text,"html.parser")

table = soup.find("table")
if not table: 
    print("no table found")
else: 
    all_rows = table.find_all("tr")

    for row in all_rows: 
           data_td = row.find_all("td")
           for i in data_td:
             i = i.get_text(strip=True)
             items = i.split(",")
             if(len(items) == 1):
                WEEK_DAYS.append([i.strip() for i in items])
             else:
               ALL_COLORS.append([i.strip() for i in items])
# print("all colors:", ALL_COLORS)
# print("days of the week:", WEEK_DAYS)


for color in ALL_COLORS:
    lower_cased = [i.lower() for i in color]
    occured = []
    for index in lower_cased: 
        num = lower_cased.count(index)
        if(len(COLOR_OCCURRENCE) != 0):
            seen = None
            for i in COLOR_OCCURRENCE:
                  if(index in i and index not in occured):
                      i[index] += num
                      seen = True
                      occured.append(index)
                      break
            if(seen != True and index not in occured):
                COLOR_OCCURRENCE.append({index:num})
                occured.append(index)
        else:
            COLOR_OCCURRENCE.append({index:num})
            occured.append(index)
       
# print("color occurence", COLOR_OCCURRENCE)

def calc_mean(value_sum,total_count):
    return value_sum/total_count
def calc_mode(num_array):
    num = num_array[0]
    for i in num_array:
        if(i >= num):
            num = i
    return num

# KEY FEATURES

# 1. Which color of shirt is the mean color?
# The term 'mean color' is ambigous. I can't tell its meaning in this context, because colors are categorical values
# answer: 
count_values = [value for item in COLOR_OCCURRENCE for value in item.values()]
total_count = sum(count_values)
for i in COLOR_OCCURRENCE:
    for key,value in i.items():
     print(f"Color:{key} - Mean {calc_mean(value,total_count):.2f}")

# 2. Which color is mostly worn throughout the week?
values = [value  for i in COLOR_OCCURRENCE for key,value in i.items()]
print("values;",values)
mode = calc_mode(values)
mode_color = "".join([k for item in COLOR_OCCURRENCE for k,v in item.items() if v == mode ])
print("Mostly worn throughout the week;",f"{mode_color} - {mode} times")

#3. Which color is the median?
# Again, since colors are categorical, i am calculating the median 
# from their frequency, and the color assoiated with the frequency is displayed
length = len(COLOR_OCCURRENCE)
divided = length % 2
median = None
if divided == 1:
   median = COLOR_OCCURRENCE[len(COLOR_OCCURRENCE) + 1/2] 
else:
   position = int(len(COLOR_OCCURRENCE) / 2)
   last = position + 1
   total = (values[position] + values[last]) / 2
   print("Median Frequency",total)
   print("No color has exactly the median frequency")

# Save the colours and their frequencies in postgresql database
# db_url = os.getenv("dburl")
# try:
#     with psycopg.connect(db_url) as connection:
#         with connection.cursor() as cursor:
#            create = 'CREATE TABLE bincom_colors(color_id SERIAL PRIMARY KEY,color VARCHAR(50) NOT NULL,' \
#            'frequency INT NOT NULL)'
#            insert_query = 'INSERT INTO bincom_colors (color, frequency) VALUES(%s,%s)'

#            cursor.execute(create)
#            for occ in COLOR_OCCURRENCE:
#                for key, value in occ.items():
#                    cursor.execute(insert_query,(key,value))
#            print("done")

        
# except Exception as e:
#      print(e)


# Write a program that generates random 4 digits number of 0s and 1s and convert the generated number to base 10.
random_val = []
while len(random_val) != 4:
    random_val.append(random.randint(0,1))
print(random_val)
string_val = "".join([str(num) for num in random_val])
base_ten = int(string_val,2)
print(f"{string_val} base 2 == {base_ten} base 10")

# Write a program to sum the first 50 fibonacci sequence.
fib_sequence = []
def calc(a,b):
    c = a + b
    fib_sequence.append(a)
    a = b
    b = c
    while len(fib_sequence) != 50:
        calc(a,b)
calc(0,1)
fib_sequence_sum = sum(fib_sequence)
print("first fifty fibonacci sequence sum;",fib_sequence_sum)


# BONUS Get the variance of the colors
main_mean = calc_mean(sum(values),len(values))
sum_total = 0
for x in values:
   sum_total += (main_mean - x) ** 2
variance = sum_total/len(values)
print("variance;", f"{variance:.2f}")