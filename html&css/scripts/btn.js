const HamBtn = document.querySelector(".ham-btn")
const navigation = document.querySelector(".navigation")
const overlay = document.querySelector(".overlay")
const closeBtn = document.querySelector(".closeBtn")
document.querySelector(".ham-btn").addEventListener("click",()=>{
    HamBtn.classList.toggle("show")
    overlay.classList.toggle("show")
    navigation.classList.toggle("show")
})
closeBtn.addEventListener("click",()=>{
    closeMenu()
})
overlay.addEventListener("click",()=>{
    closeMenu()
})
function closeMenu(){
    navigation.classList.remove("show")
    overlay.classList.remove("show")
    HamBtn.classList.remove("show")

}
