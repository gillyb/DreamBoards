
$(function() {

	$('.action-link.save').click(function() {
		var id = $('.canvas').data('board-id');
		var items = [];
		$('.canvas .thumbnail-container').each(function(index, element) {
			var elem = $(element);
			var itemImage = elem.find('.thumbnail');
			items.push({
				boardId: id,
				ProductId: 0,
				CatalogId: 0,
				ImageUrl: itemImage.attr('src'),
				PosX: Math.round(elem.css('left').replace('px', '')),
				PosY: Math.round(elem.css('top').replace('px', '')),
				Width: itemImage.width(),
				Height: itemImage.height(),
				Rotation: 0,
				Layer: 0
			});
		});
		
		$.ajax({
			url: '/-/canvas/save',
			type: 'POST',
			dataType: 'json',
			data: JSON.stringify({ boardId: id, boardItems: items }),
			contentType: 'application/json; charset=utf-8',
			success: function(data) {
				debugger;
			},
			error: function(xhr) {
				debugger;
			}
		});
	});

});