var addbtn = document.getElementById('add-btn');

addbtn.addEventListener("click", function(){
	var modal = document.getElementById('modal');
	modal.classList.toggle("active");
	modal.calssList.toggle("scrolling");
})