myApp.directive('selectOnClick', function () {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			element.on('click', function () {
				this.select();
			});
		}
	};
});

myApp.directive('ngEnter', function () {
	return function (scope, element, attrs) {
		element.bind('keydown keypress', function (event) {
			var code = event.keyCode || event.which;
			if (code === 13) {
				scope.$apply(function () {
					scope.$eval(attrs.ngEnter);
				});
				event.preventDefault();
			}
		});
	};
});


myApp.directive('focus', ['$timeout', function ($timeout) {
	return {
		restrict: 'A',
		link: function (scope, element) {
			$timeout(function () {
				element[0].focus();
			});
		}
	};
}]);

myApp.directive('labelDefect', function () {
	return {
		restrict: 'E',
		template: '<label class="label-defect" ng-class="(defect.qty > 0)? \'defect-danger\': \'defect-info\'">\
						<span class="defect-name">{{defect.defectCode}}</span>\
						<span class="defect-qty">{{defect.qty}}</span>\
					</label>'
	};
});

myApp.directive('integerOnly', function () {
	return {
		require: 'ngModel',
		restrict: 'A',
		link: function (scope, element, attr, ctrl) {
			function inputValue(val) {
				if (val) {
					var digits = val.replace(/[^0-9]/g, '');

					if (digits !== val) {
						ctrl.$setViewValue(digits);
						ctrl.$render();
					}
					return parseInt(digits, 10);
				}
				return undefined;
			}
			ctrl.$parsers.push(inputValue);
		}
	};
});
myApp.directive('onFinishRender', function ($timeout) {
	return {
		restrict: 'A',
		link: function (scope, element, attr) {
			if (scope.$last === true) {
				$timeout(function () {
					scope.$emit('ngRepeatFinished');
				});
			}
		}
	};
});
myApp.directive('decimalOnly', function () {
	return {
		require: 'ngModel',
		restrict: 'A',
		link: function (scope, element, attr, ctrl) {
			function inputValue(val) {
				if (val) {
					var digits = val.replace(/[^0-9.]/g, '');

					if (digits !== val) {
						ctrl.$setViewValue(digits);
						ctrl.$render();
					}
					return parseFloat(digits);
				}
				return undefined;
			}
			ctrl.$parsers.push(inputValue);
		}
	};
});

myApp.directive('activeLink', function () {
	return {
		link: function (scope, element) {
			element.find('.nav a').on('click', function () {
				angular.element(this)
					.parent().siblings('.active')
					.removeClass('active');
				angular.element(this)
					.parent()
					.addClass('active');
			});
		}
	};
});
myApp.directive("datetimepicker", ['$filter', function ($filter) {
	return {
		restrict: "EA",
		replace: true,
		scope: {
			options: "=",
			css: "="
		},
		template: '<input readonly type="text" style="cursor: pointer;" class="form-control" />',
		link: function (scope, elem, attr) {
			scope.options.onChangeDateTime = function (dp, i) {
				scope.ngModel = dp;
			}
			$(elem).datetimepicker(scope.options);
			$(elem).css(scope.options.css);

			scope.$watch("ngModel ", function () {
				var format = scope.options.angularFormat ? scope.options.angularFormat : "yyyy-MM-dd";
				scope.ngModel = $filter("date")(scope.ngModel, format);
			});
		}
	}
}]);
myApp.directive('fileModel', ['$parse', function ($parse) {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			var model = $parse(attrs.fileModel);
			var modelSetter = model.assign;

			element.bind('change', function () {
				scope.$apply(function () {
					modelSetter(scope, element[0].files[0]);
				});
			});
		}
	};
}]);