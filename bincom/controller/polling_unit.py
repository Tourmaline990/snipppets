from model.polling_unit import get_polling_unit_result_by_id
from flask import render_template

def polling_unit(polling_unit_id):
 polling_info =  get_polling_unit_result_by_id(polling_unit_id)
 if len(polling_info) == 0:
    return f"No match found"
 return render_template("poll.html",info=polling_info)