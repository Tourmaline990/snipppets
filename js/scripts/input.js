while(true){
  const userName = prompt("Enter a name: ");
   if(userName.trim !== ""){
    console.log("valid:",userName)
    }
    else{
    console.log("invalid name")
    }
    const age  = prompt("Enter your age:")
    const numAge = Number(age);
    if(!NaN && numAge <= 120 && numAge >= 1 ){
      console.log("Valid:",numAge);
    }
    else{
    console.log("Invalid:",numAge);
    }
   const income = prompt("Enter income total ")
   const floatIncome = parseFloat(income);
    if (floatIncome > 0){
    console.log("Positive income total")
    }
    else{
        console.log("Negative income")
    }
    break;
}

function ValidateName(name) {
  if (typeof(name) === "string" && name.trim !== "") {
    return name
  }
  else{
    console.log("Name cannot be empty")
  }

  
}
function ValidateAge(age) {
  const val_age = Number(age)
  if(Number.isFinite(val_age) && val_age > 0 && val_age <= 120){
    return val_age;
  }
  else{
    console.log("invalid value for age")
  }
}
function validateIncome(income) {
  const val_income = parseFloat(income)
  if(val_income > 0){
    console.log(`$${val_income}`)
  }
  else{
    console.log("Negative Income")
  }
}