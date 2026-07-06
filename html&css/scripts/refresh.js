
const ham_btn = document.querySelector("#ham-btn");
const navigation = document.querySelector("#nav");

ham_btn.addEventListener("click",() =>{
  const istrue = ham_btn.getAttribute("aria-expanded") === "true";
  ham_btn.setAttribute("aria-expanded",!istrue)
  ham_btn.classList.toggle("show");
  navigation.classList.toggle("show");
})

const AllImages = [
  "images/flower_small.webp",
  "images/hero_large.webp",
  "images/picnic.webp",
  "images/salt_lake.webp",
  "images/horse_large.webp"
]
const imageContainer = document.querySelector("#imageContainer");
let index = 0;
const increaseBtn = document.querySelector("#increase");
const decreaseBtn = document.querySelector("#decrease");


increaseBtn.addEventListener("click", () => {
  index++;
  if(index >= AllImages.length){
     index = 0;
  }
  imageContainer.innerHTML = "";
  img = document.createElement("img");
  img.src = AllImages[index];
  img.alt = "Image display";
  img.loading = "lazy";
  imageContainer.append(img);
})

decreaseBtn.addEventListener("click",()=>{
  index--;
  if(index <= 0){
     index = 0;
  }
  imageContainer.innerHTML = "";
  img = document.createElement("img");
  img.src = AllImages[index];
  img.alt = "Image display";
  img.loading = "lazy";
  imageContainer.append(img);
})