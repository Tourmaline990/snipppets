
from flask import render_template
from model.polling_unit import create_polling_unit,create_polling_unit_results
from model.ward import get_ward_id_by_pk,validate_ward
from model.party import get_all_party
from datetime import datetime

def success(request):
   form = request.form
   name = form["username"]
   date = datetime.now()
   formatted = date.strftime("%Y-%m-%d %H:%M:%S")
   ip = form["ip"]
   polling_unit_id = form["p_unit_id"]
   lga_id = form["lga_id"]
   ward_pk = form["ward"]
   result = []
   party = get_all_party()
   ward_id = get_ward_id_by_pk(ward_pk)
   params = (polling_unit_id,ward_id["ward_id"],lga_id,)
   validate = validate_ward(ward_pk)
   if validate == None or validate["lga_id"] != int(lga_id) or validate["ward_id"] != ward_id["ward_id"]:
      context = {
         "title": "An error occured",
         "desc":"ward not in LGA"
      }
      return render_template("success.html",**context)  
   polling_unit_unique_id = create_polling_unit(params)
   for row in party:
      party_name = row["partyname"]
      score = form[party_name]
      if party_name == "LABOUR":
         party_name = "LABO"
      result.append(
         (polling_unit_unique_id,party_name,score,name,formatted,ip)
      )
   polling_unit_result_pk = create_polling_unit_results(result)
   if polling_unit_result_pk != None:
      context = {
         "title": "success",
         "desc": "Polling unit created and updated"
      }
      return render_template("success.html",**context)

