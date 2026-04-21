
const hambtn = document.querySelector("#ham-btn");
const overlayBtn = document.querySelector(".overlay-btn");
const navigation = document.querySelector("#navigation");
const overlay = document.querySelector(".overlay")
const body = document.querySelector("body")

hambtn.addEventListener("click",()=>{
  hambtn.classList.toggle("show");
  navigation.classList.toggle("show")
  overlay.classList.toggle("show")
  body.classList.toggle("show")
})
overlay.addEventListener("click",CloseMenu);
overlayBtn.addEventListener("click",CloseMenu)

function CloseMenu(){
   overlay.classList.remove("show")
   navigation.classList.remove("show")
   body.classList.remove("show")
}

// Direct Dom Interaction
document.querySelector(".btn").addEventListener("click",(event)=>{
   event.preventDefault() 
   applyCode()  
})

// validate coupon: extracts text from input, gets previously displayed total, verify coupon, 
// subtracts discount, recompute total and display.
function validateCoupon(coupons,total,DisplayToUser,CouponInput) {
    if(CouponInput.value.trim !== "" && !coupons.every(el=> typeof el === Object)){
      let coupon_code = CouponInput.value.toLowerCase()
      try {
        const searchvalue =  coupons.find(code => code.name === coupon_code)
        if(searchvalue.used !== true){
          let num_total = parseFloat(total.textContent.replace(/[^0-9.-]+/g,""));
          let discounted = num_total - searchvalue.discount
          let couponIndex = coupons.indexOf(searchvalue)
          coupons[couponIndex].used = true
          console.log(coupons)
          total.textContent = `${discounted.toFixed(2)}`
          DisplayToUser.textContent = `Code ${coupon_code} added`
        }
        else{
          DisplayToUser.textContent = `Code ${coupon_code} already used`
        }
      } 
      catch (error) {
        console.log(error)
        DisplayToUser.textContent = `Invalid coupon code`
       }
      }
    else{
      throw new Error("Invalid Arguments; Array:array of object, input cannot be empty ")
    }
}
// selects Dom elements and delegates
function applyCode() {
   const coupon = document.querySelector("#coupon");
   let total = document.querySelector("#total");
   let discount = document.querySelector("#discount");
   try {
      validateCoupon(Storedcoupons,total,discount,coupon)
   } catch (error) {
     console.log(error.message)
   }
   
}

const Storedcoupons = [
  {name:"save10",  discount: 10, used: false},
  {name:"save20", discount: 20,used: false},
  {name:"comeback",discount: 30, used: false}
]



