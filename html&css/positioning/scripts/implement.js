const hamBtn  = document.querySelector(".ham-btn")
const nav = document.querySelector(".nav")
const brandLogo = document.querySelector(".brand-logo")
const icon = document.querySelector("header img")

hamBtn.addEventListener("click",()=>{
    const isExpanded = hamBtn.getAttribute("aria-expanded") === "true"
    hamBtn.setAttribute("aria-expanded",!isExpanded)
    hamBtn.classList.toggle("active");
    nav.classList.toggle("active");
})

window.addEventListener("scroll", () =>{
    if(window.scrollY >= 17){
        brandLogo.classList.add("switch")
        setTimeout(()=>{
            icon.classList.add("switch")
            //set timeout here so the animation completes before icon (image) shows
        },1000)
    }
    else{
        setTimeout(()=>{
            icon.classList.remove("switch")
        },1000)
        brandLogo.classList.remove("switch")
    }
})