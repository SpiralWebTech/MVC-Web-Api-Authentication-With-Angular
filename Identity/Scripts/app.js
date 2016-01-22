angular.module("app", []).controller("main", function ($scope, $http) {
    $scope.tokenKey = "accessToken";

    $scope.new = {};
    $scope.roles = ["Admin", "Manager", "Teacher", "Parent", "Student"];
    $scope.new.Role = "Student";

    $scope.register = function () {
        $http.post("/api/Account/Register", $scope.new).success(function (response) {
            $scope.result = "Done!"
        }).error(function (response) {
            $scope.result = response;
        });
    }

    $scope.login = function () {
        $scope.result = "";
        var req = "grant_type=password&username=" + $scope.login.email + "&password=" + $scope.login.password;
        $http.post("/Token", req).success(function (response) {
            $scope.user = response.userName;
            sessionStorage.setItem($scope.tokenKey, response.access_token);
        }).error(function (response) {
            $scope.result = response;
        })
    }

    $scope.logout = function () {
        $http.post("/api/Account/Logout", "", { headers: getAuthHeader() }).success(function (response) {
            $scope.result = response;
            $scope.user = '';
            sessionStorage.removeItem($scope.tokenKey);
        }).error(function (response) {
            $scope.result = response;
        });
    }

    $scope.callAPI = function () {
        $scope.result = "";
                
        $http.get("/api/values", { headers: getAuthHeader() }).success(function (response) {
            $scope.result = response;
        }).error(function (response) {
            $scope.result = response;
        });
    }

    function getAuthHeader() {
        var token = sessionStorage.getItem($scope.tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        return headers;
    }
});