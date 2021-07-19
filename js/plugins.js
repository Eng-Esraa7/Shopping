/*global $ , j query , alert*/
$(document).ready(function(){
	
	//nice scroll
    $("html").niceScroll();

    //nav active
    $('.nav ul li').click(function () {
        $('.nav ul li').removeClass('active');
        $(this).addClass('active');
    });

//time of carousel
$('.carousel').carousel({
		interval:5000
	});
	//caching scroll top
	var scrollButton=$("#scroll-top");
	$(window).scroll(function()
	{
		if($(this).scrollTop()>=1000)
			scrollButton.show();
		else
		scrollButton.hide();
	});
	
	//click on button to scroll top
		scrollButton.click(function()
		{ 
		$("html,body").animate({scrollTop : 0},600);
	});

	//if add to cart true
	$('.hasAdded').click(function () {
		alert("you Added It before");
	});

	var maincolor = localStorage.getItem("option");

	var noti = document.querySelector('.cartCircle');
	var button = document.querySelectorAll('.btnAdd');
	for (but of button) {
		but.addEventListener('click', (e) => {
			var add = Number(noti.getAttribute('data-count') || 0);
			noti.setAttribute('data-count', add + 1);
			localStorage.setItem('option', add);
			alert("addddd");
		})
	}
});

//show more girl shoes - bags
$("#girl .showMore, #bags .showMore").click(function () {
	$("#girl .hideMore ,#bags .hideMore ").fadeIn(2000).addClass("show").removeClass("hide");
});
//show less girl shoes - bags
$("#girl .showLess, #bags .showLess").click(function () {
	$("#girl .hideMore, #bags .hideMore").fadeOut(2000).addClass("hide").removeClass("show");
});

//show more men shoes -acess
$("#boy .showMore").click(function () {
	$("#boy .hideMore").fadeIn(2000).addClass("show").removeClass("hide");
});
//show less men shoes -acess
$("#boy .showLess").click(function () {
	$("#boy .hideMore").fadeOut(2000).addClass("hide").removeClass("show");
});

//show more mix shoes
$("#mix .showMore").click(function () {
	$("#mix .hideMore").fadeIn(2000).addClass("show").removeClass("hide");
	console.log("click");
});
//show less mix shoes
$("#mix .showLess").click(function () {
	$("#mix .hideMore").fadeOut(2000).addClass("hide").removeClass("show");
});


	
//loading screen
$(window).load(function()
{
	$(".loading .spinner").fadeOut(1000,
	function()
	{
		//show scroll
	$("body").css("overflow","auto");
		$(this).parent().fadeOut(1000,
		function()
		{
			$(this).remove();
});
});
});