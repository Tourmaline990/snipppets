from db import cursor

def get_lga_result_by_id(lga_id):
    query = """
       SELECT SUM(party_score) as score,announced_pu_results.party_abbreviation
        FROM announced_pu_results
          JOIN polling_unit
            ON announced_pu_results.polling_unit_uniqueid = polling_unit.uniqueid
            WHERE polling_unit.lga_id = %s
         GROUP BY announced_pu_results.party_abbreviation;
         """
    query_param = (lga_id,)
    cursor.execute(query,query_param)
    return cursor.fetchall()