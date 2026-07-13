const countdown = document.querySelector("#countDown")
const start = document.querySelector("#startBtn")
const pause = document.querySelector("#pause");
let time = 20

countdown.innerHTML = `Active!!!!!`;
function forTimer(){
    if(time >= 0){
        countdown.innerHTML = time;
        time--
    }
    else{
        countdown.innerHTML = `completed!`
    }
}
let IntervalId;;

start.addEventListener("click", ()=>{
    IntervalId = setInterval(forTimer,1000);
})

pause.addEventListener("click",()=>{
    if(time === 20){
      countdown.innerHTML = `Not started!!`;
    }
    else{
        clearInterval(IntervalId);
    }
})