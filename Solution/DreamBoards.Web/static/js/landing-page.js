
$(function() {

	$('.create-board-form .submit-button').click(function() {
		$.ajax({
			url: '/-/canvas/new',
			type: 'POST',
			data: JSON.stringify({ name: $('#board-name').val(), description: $('#board-description').val() }),
			contentType: 'application/json; charset=utf-8',
			success: function(data) {
				window.location.href = '/test?boardId=' + data;
			},
			error: function(ex) {
				debugger;
			}
		});
	});

});