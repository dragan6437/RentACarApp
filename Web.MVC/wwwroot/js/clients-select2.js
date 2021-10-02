$(document).ready(function () {
    $("#clientList").select2({
        placeholder: "Search client",
        allowClear: true,
        ajax: {
            url: "/Clients/SearchClient",
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
                            text: `${item.fullName} (${item.email})`
                        };
                    }),
                };
            }
        }
    });
});