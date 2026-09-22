function doFV() {
	// INPUT
	let p = parseFloat(document.getElementById("principle").value);
	let r = parseFloat(document.getElementById("annualrate").value);
	let n = parseInt(document.getElementById("periods").value);
	let y = parseInt(document.getElementById("years").value);
    const message = document.querySelector("#message")
    const api = document.querySelector("#api")
	// PROCESSING

   
    try {
        if(validateNumber(p)){
          const v =  prompt("Enter principle value again: ")
          if(v !== p){
             throw new Error(`Principle value verification failed, please enter again.`);
          }
        }
        let output = computeFutureValue(p, r, n, y);
        document.getElementById("output").innerHTML = `${output.toFixed(2)}`;
    } catch (error) {
        message.innerHTML = `${error.message}`
    }
	// OUTPUT with formatting
}

// computer future value function
// p = principal, r = annual rate, y = number of years, n = periods of year.

function computeFutureValue(p, r, n, y) {
    p = validateNumber(p)
    r = validateNumber(r)
    n = validateNumber(n)
    y = validateNumber(y)
	let er = r / n; // effective rate per period
	let totalperiods = n * y;
	return p * Math.pow(1 + er, totalperiods);
    
}
function validateNumber(value){
    if(isFinite(value) && !isNaN(value) && value !== null){
      return value;
    }
    throw new Error(`Invalid input format.`);
    
    
}

// get and display the current year
document.getElementById("theyear").textContent = new Date().getFullYear();

// const url = https://jsonplaceholder.typicode.com/posts

async function getUser(url,DisplayFn,parentContainer){
    try {
        const data = await fetch(url)
        if(data.ok){
            const response = await data.json();
            DisplayFn(response,parentContainer)
            console.log(response)
        }
        else{
            throw new Error(response.status);
        }
    } catch (error) {
        console.log(error)
        parentContainer.innerHTML = `An error occured.`
    }
    
}
getUser('https://jsonplaceholder.typicode.com/users',DisplayFn,api)

function DisplayFn(data,parent){
    let ul = document.createElement("ul");
    data.forEach(element => {
        let li = document.createElement("li");
        li.innerHTML = `Name: ${element.name}  Email: ${element.email}  Phrase: ${element.company.catchPhrase}`
        ul.appendChild(li)
    });
    parent.append(ul);
}
