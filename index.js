var addbtn = document.getElementById('add-btn');
var cancelbtn = document.getElementById('cancel-btn');
var submitbtn = document.getElementById('submit-btn');

var modal = document.getElementById('modal');

addbtn.addEventListener("click", function(){
	modal.classList.toggle("active");
	modal.calssList.toggle("scrolling");
})

cancelbtn.addEventListener("click", function(){
	modal.classList.toggle("active");
})

submitbtn.addEventListener("click", function(){
	//function 
})

