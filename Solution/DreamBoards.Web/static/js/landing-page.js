
$(function() {

	$('.create-board').click(function() {
		$.ajax({
			url: '/-/canvas/new',
			type: 'POST',
			data: JSON.stringify({ name:$('#board-name').val(), description:$('#board-description').val() }),
			contentType: 'application/json; charset=utf-8',
			success: function() {

			},
			error: function() {

			}
		});
	});

});