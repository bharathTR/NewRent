
var url = null;

$(document).ready(function () {


    $('#TicketViewTable').DataTable({


        "bServerSide": true,
        "sAjaxSource": "/Ticket/TicketViewGrid",
        "bProcessing": true,
        "deferRender": true,
        "aoColumns": [
            { "sName": "TicketID" },
            {
                "sName": "Type",
                "render": function (data, type, row) {

                    if (type === 'display') {
                        if (data == 0) {
                            return '<label>All Services</label>'
                            // return '<input type="text" value="Pending" class="editor-active">';
                        }
                        else if (data == 1)
                        {
                            return '<label>Plumbing Service</label>'
                        }
                        else if (data == 2) {
                            return '<label>Electric Service</label>'
                        }
                        else {
                            return '<label>Others</label>'
                            //return '<input type="text" value="Others" class="editor-active">';
                        }
                    }
                    return data;
                },
                className: "dt-body-center"

            },
            { "sName": "Description" },
            { "sName": "Raised Date" },
            {
                "sName": "Status",
                "render": function (data, type, row) {

                    if (type === 'display') {
                        if (data == 0) {
                            return '<label>Pending</label>'
                           // return '<input type="text" value="Pending" class="editor-active">';
                        }
                        else {
                            return '<input type="text" value="Completed" class="editor-active">';
                        }
                    }
                    return data;
                },
                className: "dt-body-center"
            },

        ]
    });

    $("#btnTicketNew").click(function () {
        //var id = $('#TxtRaisedLoginID').val();
        var Type = $('#TxtTicketType').val();
        var Desc = $('#TxtTicketDesc').val();
      


        if (Type == "") {
            alert("Select Service Type");
            $('#TxtTicketType').focus();
            return false;
        }
        if (Desc == "") {
            alert("Enter the Description");
            $('#TxtTicketDesc').focus();
            return false;
        }


        $.ajax({
            url: "/Ticket/TicketCreateNew",
            type: "Post",
            data: {'Type': Type, 'Desc': Desc},
            success: function (response) {
                
                if (response == 1) {

                    swal({
                        title: "Success",
                        text: "New Query Has been Raised",
                        icon: "success",
                        buttons: true,
                        dangerMode: false,
                    })
                        .then((value) => {
                            if (value) 
                            window.location.href = '/Ticket/TicketView';
                        });

                    //swal({
                    //    title: "",
                    //    text: "New Query Has been Raised",
                    //    icon: "success",

                    //});
                    //setInterval(function () { window.location.href = '/Ticket/TicketView' }, 2000);
                    ////alert("New Ticket Has been Raised");
                    
                }
                

            },
            error: function (response) {
                alert(response);
            }
        });


    });



});

function ValidateAlpha(evt) {

    evt = (evt) ? evt : window.event;
    var keyCode = (evt.which) ? evt.which : evt.keyCode;
    if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode != 32) {

        return false;
    }
    return true;
}








