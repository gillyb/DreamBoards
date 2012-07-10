
$(function() {
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

	// initialize the drag & drop behaviors
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
						helper: 'original',
						revert: 'invalid'
					})
					.find('.thumbnail').resizable();

			if ($('.teaser', '.canvas').length > 0)
				$('.teaser', '.canvas').remove();
		}
	});
	$('.items-container .thumbnail-container').draggable({
		revert: 'invalid',
		helper: 'clone'
	});
});