const temperatures = [23,17,29,31,19];


function computation(numberArray){
  const arrIsNum = numberArray.every(el => typeof el === "number")
  if(Array.isArray(numberArray)&& arrIsNum && numberArray.length !== 0 )
    {
  const data = numberArray.reduce(function(accumulator,currentValue){
    accumulator.total += currentValue
    if(currentValue < accumulator.min){
        accumulator.min = currentValue
      }
    if(currentValue > accumulator.max){
      accumulator.max = currentValue
       }
     
    return accumulator
  },{total:0, min:numberArray[0], max:numberArray[0]})
   data.avg = data.total/numberArray.length
   return data
  }
  else{
    throw new Error("Invalid Arguments(array of numbers required)"); 
  }
}

console.log(computation(temperatures))
const fakeout = [0,"q","b",1]
console.log("Hello world")
console.log(computation(fakeout))