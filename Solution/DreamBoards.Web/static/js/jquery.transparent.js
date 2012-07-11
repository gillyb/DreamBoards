
(function($) {

	$.fn.transparent = function() {

		var element = this;

		element.addClass('loading')
			.parents('.ui-wrapper')
			.append($('<div/>').addClass('loader'));

		$.ajax({
			url: '/-/images/make-transparent',
			type: 'POST',
			contentType: 'application/json; charset=utf-8',
			data: JSON.stringify({ imageUrl: element.attr('src') }),
			success: function(data) {
				element.attr('src', data);
			},
			error: function() {
				debugger;
			},
			complete: function() {
				element.removeClass('loading')
					.parents('.ui-wrapper')
					.find('.loader')
					.remove();
			}
		});

		return element;

	};

})(jQuery);
