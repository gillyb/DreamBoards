
$(function() {

	$('.action-link.save').click(function() {
		var items = [];
		$('.canvas .thumbnail-container').each(function(index, element) {
			var elem = $(element);
			var itemImage = elem.find('.thumbnail');
			var item = {
				itemId: 0,
				boardId: 0,
				ProductId: 0,
				CatalogId: 0,
				ImageUrl: itemImage.attr('src'),
				PosX: Math.round(elem.css('left').replace('px', '')),
				PosY: Math.round(elem.css('top').replace('px', '')),
				Width: itemImage.width(),
				Height: itemImage.height(),
				Rotation: 0,
				Layer: 0
			};
			items.push(item);
		});
		// TODO: send this array to some server endpoint to save the dreamboard items
		var data = { boardItems: items };
		$.ajax({
			url: '/-/canvas/save',
			type: 'POST',
			dataType: 'json',
			data: JSON.stringify(data),
			contentType: 'application/json; charset=utf-8',
			success: function(d) {
				debugger;
			},
			error: function(xhr) {
				debugger;
			}
		});
	});

});