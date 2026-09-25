from db import cursor

def get_polling_unit_result_by_id(polling_unit_id):
    query = """ 
     SELECT polling_unit.uniqueid,polling_unit.polling_unit_name,announced_pu_results.party_abbreviation,announced_pu_results.party_score
         FROM polling_unit
               JOIN announced_pu_results
                   ON polling_unit.uniqueid = announced_pu_results.polling_unit_uniqueid
              WHERE polling_unit.uniqueid = %s;
   """
    query_params = (polling_unit_id,)
    cursor.execute(query,query_params)
    return cursor.fetchall()

def create_polling_unit(params):
    query = """
       INSERT INTO polling_unit 
        (polling_unit_id,ward_id,lga_id)
        VALUES(%s,%s,%s)
     """
    query_params = params
    cursor.execute(query,query_params)
    return cursor.lastrowid

def create_polling_unit_results(params):
    query = """
      INSERT INTO `announced_pu_results` 
      (polling_unit_uniqueid,party_abbreviation,party_score,entered_by_user,
        date_entered,user_ip_address) VALUES (%s,%s,%s,%s,%s,%s)
    """
    cursor.executemany(query,params)
    return cursor.rowcount