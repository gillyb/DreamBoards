
$(function() {

	var editMode = function() {
		return $('.canvas').data('edit-only');
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
					$('.items-container').append(wrapImageForToolBox(element.imageUrl));
				});
				makeToolboxDraggable();
			},
			error: function() {
				// TODO: display some nice error message
			}
		});
	});

	var wrapImageForToolBox = function(imageUrl) {
		var item = $('<div/>').addClass('thumbnail-container')
			.append($('<img>').attr('src', imageUrl)
					.addClass('thumbnail')
					.error(function() {
						$(this).parents('.thumbnail-container').remove();
					}));
		return item;
	};

	var makeCanvasDroppable = function() {
		if (editMode()) return;
		$('.canvas').droppable({
			tolerance: 'fit',
			drop: function(event, ui) {
				if (ui.draggable.parents('.canvas').length > 0) return;
				ui.draggable
					.clone()
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
		if (editMode()) return;
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
			var newItem = wrapImageForToolBox(item.ImageUrl);
			newItem
				.appendTo('.canvas')
				.css({
					position: 'absolute',
					top: item.PosY,
					left: item.PosX
				});
			if (!editMode()) {
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
			if (!editMode())
				newItem.resizable();
		});
	};

	makeCanvasDroppable();
	makeToolboxDraggable();
	loadExistingCanvasImages();

	if (editMode()) {
		$('.action-link.save').hide();
		$('.action-link.save-as-image').hide();
	}

});