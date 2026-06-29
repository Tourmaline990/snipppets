const ham_btn = document.querySelector("#ham-btn");
const forBtn = document.querySelector(".forBtn");
const navigation = document.querySelector("#nav");
const closeBtn = document.querySelector("#closeBtn")
const overlay = document.querySelector(".overlay")

ham_btn.addEventListener("click",() =>{
  const istrue = ham_btn.getAttribute("aria-expanded") === "true";
  ham_btn.setAttribute("aria-expanded",!istrue)
  forBtn.classList.toggle("show");
  ham_btn.classList.toggle("show");
  overlay.classList.toggle("show");
})
closeBtn.addEventListener("click",CloseMenu)
overlay.addEventListener("click",CloseMenu)

function CloseMenu() {
    overlay.classList.remove("show")
    forBtn.classList.remove("show")
    ham_btn.classList.remove("show")
}