angular.module("app", [])

    .directive("required", function () {
        return {
            restrict: "E",
            templateUrl: "Required.html",
            link: function (scope, element, attrs) {
                
            }
        }
    })

    .directive("buttons", function () {
        return {
            restrict: "E",
            template: '<div class="{{clas}}"><a href="{{link}}" class="{{color}}">{{text}}</a></div>',
            scope: {
                text: "@label",
                link: "@link",
                color: "@color",
                clas: "@clas"
            }
        }
    })