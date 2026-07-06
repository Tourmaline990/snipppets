const hamBtn = document.querySelector("#ham-btn");
const closeBtn = document.querySelector("#close-btn");
const navigation = document.querySelector("#navigation");
const copyright = document.querySelector("#copyright");
const lastUpdated = document.querySelector("#last-updated");
const overlay = document.querySelector("#overlay");
const body = document.querySelector("body")
const imagebox = document.querySelector(".imagebox")

hamBtn.addEventListener("click",()=>{
    const istrue = hamBtn.getAttribute("aria-expanded") === "true";
    hamBtn.setAttribute("aria-expanded",!istrue);
    hamBtn.classList.toggle("active")
    navigation.classList.toggle("active");
    overlay.classList.toggle("active");
    body.classList.toggle("active");
    imagebox.classList.toggle("active");
    
})
closeBtn.addEventListener("click",CloseMenu)
overlay.addEventListener("click",CloseMenu)
function CloseMenu() {
    const istrue = hamBtn.getAttribute("aria-expanded") === "true";
    hamBtn.setAttribute("aria-expanded",!istrue);
    hamBtn.classList.remove("active")
    overlay.classList.remove("active");
    navigation.classList.remove("active");
    body.classList.remove("active");
    imagebox.classList.remove("active");
}

const date = new Date();
copyright.innerHTML = `${date.getFullYear()}`;
lastUpdated.innerHTML = document.lastModified;