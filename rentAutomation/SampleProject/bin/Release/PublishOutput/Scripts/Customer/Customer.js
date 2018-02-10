
var url = null;

    $(document).ready(function () {


    $('#myDataTable').DataTable({

        
            "bServerSide": true,
            "sAjaxSource": "/Customer/getCustomerList",
            "bProcessing": true,
            "deferRender": true,
            "aoColumns": [
                {"sName": "id" },
                {"sName": "firstname" },
                {"sName": "lastname" },
                { "sName": "mobileNo" },
                { "sName": "houseNo" },
                { "sName": "blockNo" },
                { "sName": "lastLoginDate" },
                {
                    "sName": "Action",
                    "mRender": function (oObj) {
                        return "<a href='javascript:DoPost(" + oObj + ")'><img src='../images/Editicon.png'/></a>" +
                            "<a href='javascript:DoPostDelete(" + oObj + ")'><img src='../images/del.png'/></a>";
                        
                    }

                },
                ]
        });

      

});

function DoPost(data) {
    //url is specified in _ChildLayout
    $.post(url, {
        id: data,
        
    },
        function () {
            window.location.href = "../Customer/CreateCustomer";
             //$('#GridView').dataTable().fnDraw(); IT WORKS..
        });
}

function DoPostDelete(data) {
    //urldel is specified in _ChildLayout
    if (confirm("Are you sure want to delete ?")) {
        $.post(urldel, {
            id: data,

        },
            function () {
                window.location.href = "../Customer/ViewCustomer";
            });
    }
    return false;
}



    






