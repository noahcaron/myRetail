

app.factory('productService', function ($http, utility) {
    var serviceurl = utility.baseAddress + "products";
    return {
        getproductsList: function () {
            var url = serviceurl;
            return $http.get(url);
        },
        getproduct: function (product) {
            var url = serviceurl + "/" + product.id;
            return $http.get(url);
        },
        addproduct: function (product) {
            var url = serviceurl;
            return $http.post(url, product);
        },
        deleteproduct: function (product) {
            var url = serviceurl + "/" + product.id;
            return $http.delete(url);
        },
        updateproduct: function (product) {
            var url = serviceurl + "/" + product.id;
            return $http.put(url, product);
        }
    };
});
