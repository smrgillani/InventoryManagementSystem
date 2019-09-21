function CountriesDropdown() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.CountryDrodown').empty();
    $('.CountryDrodown').append('<option value="0" selected="selected">Select Country</option>');

    $.ajax({
        url: '/Dropdown/CountriesDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".CountryDrodown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".CountryDrodown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".CountryDrodown").val("0");


    })
        .always(function () {
            
        });
}

function CitiesDropdown(Country_id) {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.CityDropdown').empty();
    $('.CityDropdown').append('<option value="0" selected="selected">Select City</option>');

    $.ajax({
        url: '/Dropdown/CitiesDropdown',
        data: { __RequestVerificationToken: token, Country_id },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $("CityDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".CityDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".CityDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownCategories() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.ItemCategoryDropdown').empty();
    $('.ItemCategoryDropdown').append('<option value="0" selected="selected">Select Category</option>');

    $.ajax({
        url: '/Dropdown/CategoriesDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".ItemCategoryDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $('.ItemCategoryDropdown')
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".ItemCategoryDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownBrands() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.ItemBrandDropdown').empty();
    $('.ItemBrandDropdown').append('<option value="0" selected="selected">Select Brand</option>');

    $.ajax({
        url: '/Dropdown/BrandsDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".ItemBrandDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".ItemBrandDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".itemBrandDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownManufacturers() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.ItemManufacturerDropdown').empty();
    $('.ItemManufacturerDropdown').append('<option value="0" selected="selected">Select Manufacturer</option>');

    $.ajax({
        url: '/Dropdown/ManufacturersDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".ItemManufacturerDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".ItemManufacturerDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".ItemManufacturerDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownUnits() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.ItemUnitDropdown').empty();
    $('.ItemUnitDropdown').append('<option value="0" selected="selected">Select Unit</option>');

    $.ajax({
        url: '/Dropdown/UnitsDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".ItemUnitDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".ItemUnitDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".ItemUnitDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownVendors() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.VendorDropdown').empty();
    $('.VendorDropdown').append('<option value="0" selected="selected">Select Vendor</option>');

    $.ajax({
        url: '/Dropdown/VendorsDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".VendorDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".VendorDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".VendorDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownCompanies() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.CompanyDropdown').empty();
    $('.CompanyDropdown').append('<option value="0" selected="selected">Select Company</option>');

    $.ajax({
        url: '/Dropdown/CompaniesDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".CompanyDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".CompanyDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".CompanyDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownCustomers() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.CustomerDropdown').empty();
    $('.CustomerDropdown').append('<option value="0" selected="selected">Select Customer</option>');

    $.ajax({
        url: '/Dropdown/CustomersDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".CustomerDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".CustomerDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".CustomerDropdown").val("0");


    })
        .always(function () {

        });
}

function RolesDropdown() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.RolesDropdown').empty();
    $('.RolesDropdown').append('<option value="0" selected="selected">Select Role</option>');

    $.ajax({
        url: '/Dropdown/RolesDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".RolesDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".RolesDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".RolesDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownItems() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.ItemsDropdown').empty();
    $('.ItemsDropdown').append('<option value="0" selected="selected">Select Item</option>');

    $.ajax({
        url: '/Dropdown/ItemsDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".ItemsDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".ItemsDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".ItemsDropdown").val("0");


    })
        .always(function () {

        });
}

function PaymentModesDropdown() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.PaymentModeDropdown').empty();
    $('.PaymentModeDropdown').append('<option value="0" selected="selected">Select Payment Mode</option>');

    $.ajax({
        url: '/Dropdown/PaymentModesDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".PaymentModeDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".PaymentModeDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".PaymentModeDropdown").val("0");


    })
        .always(function () {

        });
}

function DropDownUsers() {
    var token = $('[name=__RequestVerificationToken]').val();
    $('.UsersDropdown').empty();
    $('.UsersDropdown').append('<option value="0" selected="selected">Select User</option>');

    $.ajax({
        url: '/Dropdown/UsersDropdown',
        data: { __RequestVerificationToken: token },
        type: "POST",
        async: false,
        dataType: "json",
        ContentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        var options = $(".UsersDropdown");
        var rowCount = $response.length;

        for (var i = 0; i < rowCount; i++) {
            $(".UsersDropdown")
                .append($('<option>', { value: $response[i].id })
                    .text($response[i].name));

        }
        $(".UsersDropdown").val("0");


    })
        .always(function () {

        });
}
