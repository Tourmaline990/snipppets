from db import cursor

def get_all_lga():
    query = """
      SELECT lga_id,lga_name 
      FROM lga;
    """
    cursor.execute(query)
    return cursor.fetchall()

def get_lga_name(lga_id):
    query = """
       SELECT lga_name
       FROM lga
       WHERE lga_id = %s
   """
    query_param = (lga_id,)
    cursor.execute(query,query_param)
    return cursor.fetchone()