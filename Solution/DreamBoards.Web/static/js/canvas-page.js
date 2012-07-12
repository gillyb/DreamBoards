
$(function() {

	var hostDomain = function() {
		return $('body').data('host-domain');
	};
	var readOnlyMode = function() {
		return $('.canvas').data('read-only');
	};

	// bind 'select category' drop down to api
	$('#category-dropdown').change(function() {
		// TODO: add a nice preloader here...
		$.ajax({
			url: '/-/platform/get-products-for-category',
			type: 'POST',
			data: { categoryId: $(this).val() },
			success: function(data) {
				$('.items-container').html('');
				$(data).each(function(index, element) {
					$('.items-container').append(wrapItemForToolBox(element.productId, element.imageUrl));
				});
				makeToolboxDraggable();
			},
			error: function() {
				// TODO: display some nice error message
			}
		});
	});

	var wrapItemForToolBox = function(productId, imageUrl) {
		var item = $('<div/>').addClass('thumbnail-container').data('product-id', productId)
			.append($('<img>').attr('src', imageUrl)
					.addClass('thumbnail')
					.error(function() {
						$(this).parents('.thumbnail-container').remove();
					}));
		return item;
	};

	var makeCanvasDroppable = function() {
		if (readOnlyMode()) return;
		$('.canvas').droppable({
			tolerance: 'fit',
			drop: function(event, ui) {
				if (ui.draggable.parents('.canvas').length > 0) return;
				ui.draggable
					.clone()
					.data('product-id', ui.draggable.data('product-id'))
					.appendTo($(this))
					.css(ui.position)
					.css('position', 'absolute')
					.draggable({
						helper: 'original'
					})
					.find('.thumbnail').transparent().resizable();

				if ($('.teaser', '.canvas').length > 0)
					$('.teaser', '.canvas').remove();
			}
		});
	};
	var makeToolboxDraggable = function() {
		if (readOnlyMode()) return;
		$('.items-container .thumbnail-container').draggable({
			revert: 'invalid',
			helper: 'clone'
		});
	};

	var loadExistingCanvasImages = function() {
		if (typeof boardItems == 'undefined') return;
		if (boardItems.length == 0) return;

		$('.teaser', '.canvas').remove();
		$(boardItems).each(function(index, item) {
			var newItem = wrapItemForToolBox(item.ProductId, item.ImageUrl);
			newItem
				.appendTo('.canvas')
				.css({
					position: 'absolute',
					top: item.PosY,
					left: item.PosX
				});
			if (!readOnlyMode()) {
				newItem.draggable({
					helper: 'original'
				});
			}
			newItem.find('.thumbnail')
				.css({
					width: item.Width,
					height: item.Height
				})
				.attr('src', item.ImageUrl);
			if (!readOnlyMode())
				newItem.resizable();
		});
	};

	var loadProductsFromBoard = function() {
		if (!readOnlyMode()) return;
		var productIds = [];
		$('.canvas .thumbnail-container').each(function(index, element) {
			var elementProductId = $(element).data('product-id');
			if (typeof elementProductId != 'undefined' && elementProductId != 0)
				productIds.push(elementProductId);
		});
		if (productIds.length == 0) return;
		$.ajax({
			url: '/-/platform/get-products',
			type: 'POST',
			data: JSON.stringify({ productIds: productIds }),
			dataType: 'json',
			contentType: 'application/json; charset=utf-8',
			success: function(products) {
				$(products).each(function(pIndex, pProduct) {
					$('<div/>').addClass('product-thumbnail float')
						.append($('<img>').attr('src', pProduct.imageUrl))
						.appendTo($('.items-container'))
						.click(function() {
							window.open(hostDomain() + pProduct.productUrl, '_blank');
						});
				});
			},
			error: function(ex) {
				debugger;
			}
		});
	};

	makeCanvasDroppable();
	makeToolboxDraggable();
	loadExistingCanvasImages();
	loadProductsFromBoard();

	if (readOnlyMode()) {
		$('.action-link.save').hide();
		$('.action-link.save-as-image').hide();
	}

});