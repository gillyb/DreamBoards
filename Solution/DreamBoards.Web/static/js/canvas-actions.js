
$(function() {

	$('.action-link.save').click(function() {
		var id = $('.canvas').data('board-id');

		$.ajax({
			url: '/-/canvas/save',
			type: 'POST',
			dataType: 'json',
			data: JSON.stringify({ boardId: id, boardItems: getItemsArray(id) }),
			contentType: 'application/json; charset=utf-8',
			success: function(data) {
				debugger;
			},
			error: function(xhr) {
				debugger;
			}
		});
	});

	$('.action-link.save-as-image').click(function() {
		var id = $('.canvas').data('board-id');
		$.ajax({
			url: '/-/canvas/save-as-image',
			type: 'POST',
			data: JSON.stringify({ boardItems: getItemsArray(id) }),
			contentType: 'application/json; charset=utf-8',
			success: function() {
				debugger;
			},
			error: function() {
				debugger;
			}
		});
	});

	var getItemsArray = function(boardId) {
		var items = [];
		$('.canvas .thumbnail-container').each(function(index, element) {
			var elem = $(element);
			var itemImage = elem.find('.thumbnail');
			items.push({
				boardId: boardId,
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
		return items;
	};

});