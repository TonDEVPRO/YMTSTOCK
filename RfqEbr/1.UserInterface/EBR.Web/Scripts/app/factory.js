myApp.factory('focus', ['$timeout', '$window', function ($timeout, $window) {
	return function (id) {
		$timeout(function () {
			var element = $window.document.getElementById(id);
			if (element)
				element.focus();
		});
	};
}]);

myApp.factory('select', ['$timeout', '$window', function ($timeout, $window) {
	return function (id) {
		$timeout(function () {
			var element = $window.document.getElementById(id);
			if (element)
				element.select();
		});
	};
}]);
