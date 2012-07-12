
$(function() {

	$('.create-board-link').click(function() {
		window.location.href = '/test';
	});

	$('.pin').click(function() {
		window.location.href = '/test?boardId=' + $(this).data('board-id');
	});

});