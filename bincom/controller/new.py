from model.lga import get_all_lga
from model.ward import get_wards
from model.party import get_all_party
from flask import render_template

def new_poll():
    lga = get_all_lga()
    ward = get_wards()
    party = get_all_party()
    context = {
        "lga":lga,
        "ward":ward,
        "party":party
    }
    return render_template("new_poll.html",**context)