$(document).ready(function () {
    $("#carList").select2({
        placeholder: "Search available cars",
        allowClear: true,
        ajax: {
            url: "/Cars/SearchCar",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query =
                {
                    searchTerm: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.id,
                            text: `${item.make} ${item.model} (${item.pricePerDay}€)`
                        };
                    }),
                };
            }
        }
    });
});