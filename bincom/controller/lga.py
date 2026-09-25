from model.lga import get_all_lga
from flask import render_template

def lga_dropdown():
    lga = get_all_lga()
    return render_template("select.html",lga=lga)