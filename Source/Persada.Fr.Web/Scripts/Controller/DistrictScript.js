$(function () {
    $('#COUNTRY_ID').change(function () {
        var idSelected = $(this).find('option:selected').attr('value');
        var path = pathWeb + "/City/GetProvinceListByCountryId/";
        var response = $.xResponse(path, { countryId: idSelected });

        var markup;
        for (var x = 0; x < response.length; x++) {
            var responseText = response[x].Text;
            var responseValue = response[x].Value;
            if (x == 0) {
                responseText = "Select Province :"
            }
            markup += "<option value=" + responseValue + ">" + responseText + "</option>";
        }
        $("#PROVINCE_ID").html(markup).show();
        $("#CITY_ID").html("").show();
    });

    $('#PROVINCE_ID').change(function () {
        var idSelected = $(this).find('option:selected').attr('value');
        var path = pathWeb + "/District/GetCityListByProvinceId/";
        var response = $.xResponse(path, { provinceId: idSelected });

        var markup;
        for (var x = 0; x < response.length; x++) {
            var responseText = response[x].Text;
            var responseValue = response[x].Value;
            if (x == 0) {
                responseText = "Select City :"
            }
            markup += "<option value=" + responseValue + ">" + responseText + "</option>";
        }
        $("#CITY_ID").html(markup).show();
    });
});