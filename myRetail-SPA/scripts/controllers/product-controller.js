
app.controller("ProductCtrl", function ($scope, productService) {
    $scope.Products = [];
    $scope.product = null;
    $scope.editMode = false;

    //get product
    $scope.get = function () {
        $scope.product = this.product;
        $("#viewModal").modal('show');
    };

    // initialize your products data
    (function () {
        productService.getproductsList().success(function (data) {
            $scope.Products = data;
        }).error(function (data) {
            $scope.error = "An Error has occured while Loading products! " + data.ExceptionMessage;
        });
    })();

    // add product
    $scope.add = function () {
        var currentproduct = this.product;
        if (currentproduct != null && currentproduct.name != null && currentproduct.sku && currentproduct.last_updated
            && currentproduct.category && currentproduct.price) {
            productService.addproduct(currentproduct).success(function (data) {
                $scope.addMode = false;
                currentproduct = data;
                $scope.Products.push(currentproduct);

                //reset form
                $scope.product = null;
                // $scope.addproductform.$setPristine(); //for form reset
                $scope.error = null;
                $('#productModal').modal('hide');
            }).error(function (data) {
                $scope.error = "An Error has occured while Adding product! " + data.ExceptionMessage;
                $('#productModal').modal('hide');
            });
        }
    };

    //edit product
    $scope.edit = function () {
        $scope.product = this.product;
        $scope.editMode = true;

        $("#productModal").modal('show');
    };

    //update product
    $scope.update = function () {
        var currentproduct = this.product;
        productService.updateproduct(currentproduct).success(function (data) {
            currentproduct.editMode = false;

            $('#productModal').modal('hide');
        }).error(function (data) {
            $('#productModal').modal('hide');
            $scope.error = "An Error has occured while Updating product! " + data.ExceptionMessage;
        });
    };

    // delete product
    $scope.delete = function () {
        currentproduct = $scope.product;
        productService.deleteproduct(currentproduct).success(function (data) {
            $('#confirmModal').modal('hide');
            $scope.Products.splice($scope.index, 1);
        }).error(function (data) {
            $scope.error = "An Error has occured while Deleting product! " + data.ExceptionMessage;

            $('#confirmModal').modal('hide');
        });
    };

    //Model popup events
    $scope.showadd = function () {
        $scope.product = null;
        $scope.editMode = false;

        $("#productModal").modal('show');
    };

    $scope.showedit = function () {
        $('#productModal').modal('show');
    };

    $scope.showconfirm = function (data, idx) {
        $scope.product = data;
        $scope.index = idx;
        $("#confirmModal").modal('show');
    };

    $scope.cancel = function () {
        $scope.product = null;
        $("#productModal").modal('hide');
    };

}).constant("utility",
    {
        baseAddress: "http://localhost:53143/api/"
    });