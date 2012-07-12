
$(function() {

	$('.action-link.save').click(function() {
		var id = $('.canvas').data('board-id');

		showLoader();
		if (typeof id == 'undefined' || id == 0) {
			if ($('#board-name').val().trim() == '') {
				alert('You must enter a name for the board!');
				return;
			}
			$.ajax({
				url: '/-/canvas/new',
				type: 'POST',
				async: false,
				data: JSON.stringify({ name: $('#board-name').val(), description: $('#board-description').val() }),
				contentType: 'application/json; charset=utf-8',
				success: function(boardId) {
					id = boardId;
					$('.canvas').data('board-id', id);
				},
				error: function() {
					hideLoader();
					debugger;
				}
			});
		}

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
			},
			complete: hideLoader
		});
	});

	$('.action-link.save-as-image').click(function() {
		showLoader();
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
			},
			complete: hideLoader
		});
	});

	$('.action-link.brag').click(function() {
		showLoader();
		var boardId = $('.canvas').data('board-id');
		$.ajax({
			url: '/-/canvas/brag',
			type: 'POST',
			data: JSON.stringify({
				boardId: boardId,
				boardTitle: $('#board-name').val(),
				boardItems: getItemsArray(boardId)
			}),
			contentType: 'application/json; charset=utf-8',
			success: function(data) {
				// TODO: display some nice error message
				debugger;
			},
			error: function(ex) {
				debugger;
			},
			complete: hideLoader
		});
	});

	var getItemsArray = function(boardId) {
		var items = [];
		$('.canvas .thumbnail-container').each(function(index, element) {
			var elem = $(element);
			var itemImage = elem.find('.thumbnail');
			items.push({
				boardId: boardId,
				ProductId: elem.data('product-id'),
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

	var showLoader = function() {
		$('.canvas').addClass('loading');
		$('.header .loader').css('visibility', 'visible');
	};
	var hideLoader = function() {
		$('.canvas').removeClass('loading');
		$('.header .loader').css('visibility', 'hidden');
	};

});