from flask import Flask,request
from controller.polling_unit import polling_unit as polling_unit_controller
from controller.lga import lga_dropdown as lga_controller
from controller.lga_result import lga_result as result_controller
from controller.new import new_poll as new_poll_controller
from controller.success import success as success_controller


app = Flask(__name__)

# 
@app.route("/polling_unit/<polling_unit_id>")
def polling_unit_route(polling_unit_id):
  return polling_unit_controller(polling_unit_id)

@app.route("/lga")
def lga_route():
   return lga_controller()

@app.route("/lga_result",methods=["POST"])
def lga_result_route():
   lga_id = request.form["lga"]
   print(lga_id)
   return result_controller(lga_id)

@app.route("/new_poll")
def new_poll_route():
   return new_poll_controller()

@app.route("/success",methods=["POST"])
def success_route():
   return success_controller(request)


if __name__ == "__main__":
    app.run(debug=True)