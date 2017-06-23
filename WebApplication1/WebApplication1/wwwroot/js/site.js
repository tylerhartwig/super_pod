var addbtn = document.getElementById('add-btn');
var cancelbtn = document.getElementById('cancel-btn');
var submitbtn = document.getElementById('submit-btn');
var successAlert = document.getElementById('successAlert');
var closeAlert = document.getElementById('closeAlert');
var modal = document.getElementById('modal');

/*$(document).ready(function () {
	$('.ui.dropdown').dropdown();
});*/

addbtn.addEventListener("click", function () {
	modal.classList.toggle("active");
});

cancelbtn.addEventListener("click", function () {
	modal.classList.toggle("active");
});

submitbtn.addEventListener("click", function () {
	modal.classList.toggle("active");
	successAlert.classList.toggle("hidden");
});

closeAlert.addEventListener("click", function () {
	successAlert.classList.toggle("hidden");
});

/*
$('add-btn').click(function () {
	$('modal').modal('show');
	console.log("HERE");
});*/

$('.dropdown').dropdown({
	label: {
		duration: 20,
	},
	debug: true,
	performance: true,
});

