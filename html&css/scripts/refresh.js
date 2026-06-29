const ham_btn = document.querySelector("#ham-btn");
const navigation = document.querySelector("#nav");

ham_btn.addEventListener("click",() =>{
  const istrue = ham_btn.getAttribute("aria-expanded") === "true";
  ham_btn.setAttribute("aria-expanded",!istrue)
  ham_btn.classList.toggle("show");
  navigation.classList.toggle("show");
})