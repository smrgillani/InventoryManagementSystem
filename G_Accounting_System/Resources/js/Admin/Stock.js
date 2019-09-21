$(function () {
    var url = window.location.pathname.toString().toLowerCase() + window.location.search.toString().toLowerCase();
    if ((window.location.pathname.toString().toLowerCase()) == "/stock/index") {
        GetAllItemsStockList(null);
    } else if (url.includes("/item/profile/")) {
        var Item_id = window.location.pathname.toString().toLowerCase().split("/profile/")[1];
        ProfileViewItems(url);
        if (ItemsTransactionList != null) {
            ItemsTransactionList.destroy();
        }
        ItemsTransactionSO(Item_id);
    }
})

function GetAllItemsStockList(parameter) {

    var token = $('[name=__RequestVerificationToken]').val();

    ItemsStockList = $('#Items_Stocks_list').DataTable({
        "bServerSide": true,
        "sAjaxSource": "/Stock/GetAllItemsStockList/",
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            oSettings.jqXHR = $.ajax({
                "dataType": 'json',
                "type": "POST",
                "url": sSource,
                "data": { __RequestVerificationToken: token, "Search": JSON.stringify({ Option: parameter, Draw: aoData[0].value, PageStart: aoData[3].value, PageLength: aoData[4].value, Search: aoData[20].value }) },
                "success": fnCallback
            });
        },
        responsive: {
            details: false
        },
        "columns": [
            {
                data: function (data, type, dataToSet) {
                    return "<a href='/item/profile/" + data.Item_id + "'>" + data.Item_Name + "</a>";

                }
            },
            { data: "Quantity_Sold" },
            { data: "Physical_Quantity" }
            
        ]
    });
}