
function showSuccessMessage(message) {
	$('.user-message').html(message).addClass('success-message').show();
	setTimeout(function() {
		$('.success-message').fadeOut('slow', function() {
			$(this).removeClass('success-message');
		});
	}, 2000);
}

function showErrorMessage(message) {
	$('.user-message').html(message).addClass('error-message').show();
	setTimeout(function() {
		$('.error-message').fadeOut('slow', function() {
			$(this).removeClass('error-message');
		});
	}, 2000);
}