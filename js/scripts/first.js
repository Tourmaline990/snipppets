const numbers = [3, 7, 12, 5, 9];
if (numbers.length != 0){
  
    const randommIndex = Math.floor(Math.random() *  numbers.length);
     const num = numbers[randommIndex];
    console.log(randommIndex)
     console.log(num)
}
else{
    console.log("Empty Array ")
}


// Prompt 4
const sunrise = 1705910400;
const timeZone =  3600;

const date =  new Date(sunrise * 3600);
console.log(date.toLocaleTimeString());

