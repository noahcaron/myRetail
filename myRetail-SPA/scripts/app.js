var app;

(function () {
    app = angular.module("ProductApp", []);

    //global service
    app.constant("utility",
        {
            baseAddress: "http://localhost:53143/api/"
        });
})();


