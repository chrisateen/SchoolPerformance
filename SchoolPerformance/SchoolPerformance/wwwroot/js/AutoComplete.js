function getAutoCompleteData(id,dataSource) {

    $("#" + id).autocomplete({

        source: function (request, response) {

            response($.map(dataSource, function (item) {

                var searchLabel = item.urn + " - " + item.laestab + " - " + item.schname;

                if (searchLabel.toUpperCase().indexOf(request.term.toUpperCase()) != -1) {

                    return {
                        label: searchLabel,
                        value: item.urn
                    }
                }
                else {
                    return null;
                }
            }))

        },
        minLength: 3

    });

}