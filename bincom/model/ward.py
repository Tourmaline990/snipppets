from db import cursor

def get_wards():
    query = """
      SELECT uniqueid, ward_name
          FROM ward;
    """
    cursor.execute(query)
    return cursor.fetchall()

def get_ward_id_by_pk(ward_pk):
    query = """
       SELECT ward_id FROM ward
         WHERE uniqueid = %s
     """
    query_params = (ward_pk,)
    cursor.execute(query,query_params)
    return cursor.fetchone()

def validate_ward(ward_pk):
    query = """
       SELECT lga_id,ward_id 
       FROM ward
       WHERE uniqueid = %s;
     """
    query_param = (ward_pk,)
    cursor.execute(query,query_param)
    return cursor.fetchone()