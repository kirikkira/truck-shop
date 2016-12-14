(function update(maker, id) {
    $http({
        method: "GET",
        url: '@Url.Action("_MakersInCars")',
        date: {maker : maker},
        succes: function(data) {
            $(id).replaceWith(data);
        }
    });
});