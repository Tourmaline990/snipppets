import mysql.connector
import os
from dotenv import load_dotenv

load_dotenv()
host = os.getenv("host")
port = os.getenv("port")
user = os.getenv("user")
password = os.getenv("password")
db = os.getenv("db")

try:
   connection = mysql.connector.connect(host=host,port=port,user=user,password=password,database=db)
   cursor = connection.cursor(dictionary=True)
   cursor.execute('SELECT current_time()')
   results = cursor.fetchall()
   print("Database connected at:", results)
except Exception as e:
   print("An error occured;", e)


