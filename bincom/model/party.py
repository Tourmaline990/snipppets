from db import cursor

def get_all_party():
    query = """
       SELECT partyname
         FROM party;
     """
    cursor.execute(query)
    return cursor.fetchall()