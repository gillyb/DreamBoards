
$(function() {

	$('.create-board-link').click(function() {
		window.location.href = '/canvas-page';
	});

	$('.pin').click(function() {
		window.location.href = '/canvas-page?boardId=' + $(this).data('board-id');
	});

});