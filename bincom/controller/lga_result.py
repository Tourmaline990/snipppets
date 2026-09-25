from flask import render_template
from model.lga_result import  get_lga_result_by_id
from model.lga import get_lga_name

def lga_result(lga_id):
    result = get_lga_result_by_id(lga_id)
    lga_name = get_lga_name(lga_id)
    if len(result) == 0:
        result = [{
            "party_abbreviation": "No match found",
             "score": f"- {lga_name["lga_name"]}"
        }]
    context = {
        "result": result,
        "lga_name": lga_name
    }
    return render_template("result.html",**context)
