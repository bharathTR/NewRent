
var url = null;

$(document).ready(function () {
    $('#TxtTCKrosolve_Date').datepicker({});

    $('#TicketViewTable').DataTable({


        "bServerSide": true,
        "sAjaxSource": "/Ticket/TicketViewGrid",
        "bProcessing": true,
        "deferRender": true,
        "rowReorder": {
            "selector": 'td:nth-child(2)'
        },
        "responsive": true,
        "aoColumns": [
            {
                "sName": "TicketID",
                "bVisible":false
            },
            {
                "sName": "TicketNo"
            },
            {
                "sName": "Type"},
            { "sName": "Description" },
            { "sName": "Raised Date" },
            {
                "sName": "Status"},

        ]
    });

    $("#btnTicketNew").click(function () {
        //var id = $('#TxtRaisedLoginID').val();
        var Type = $('#TxtTicketType option:selected').text();
        var Desc = $('#TxtTicketDesc').val();
      


        if (Type === "") {
            alert("Select Service Type");
            $('#TxtTicketType').focus();
            return false;
        }
        if (Desc === "") {
            alert("Enter the Description");
            $('#TxtTicketDesc').focus();
            return false;
        }


        $.ajax({
            url: "/Ticket/TicketCreateNew",
            type: "Post",
            data: {'Type': Type, 'Desc': Desc},
            success: function (response) {
                
                if (response === 1) {

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
                }
                

            },
            error: function (response) {
                alert(response);
            }
        });


    });

    $('#TicketViewTableOwner').DataTable({


        "bServerSide": true,
        "sAjaxSource": "/Ticket/TicketViewGridOwner",
        "bProcessing": true,
        "deferRender": true,
        "rowReorder": {
            "selector": 'td:nth-child(2)'
        },
        "responsive": true,
        "aoColumns": [
            {
                "sName": "TicketID",
                "mRender": function (oObj) {
                    
                    return "<a href='javascript:DoLoadTicketDetails(" + oObj + ")'><img src='../images/Editicon.png'/></a>";

                }


            },
            {
                "sName": "TicketNo"
            },
            
        { "sName": "FIRSTNAME" },
        { "sName": "LASTNAME" },
        { "sName": "MOBILENO" },
        { "sName": "H_Number" },
        { "sName": "H_BLOCK" },
        { "sName": "Type" },
        { "sName": "Description" },
        { "sName": "Raised Date" },
        { "sName": "Status" },
        ]
    });


    $("#btnTCKStatus").click(function () {
        debugger;
        var Ticketid = $('#TxtTicketid').val();
        var time = $('#TxtSlot option:selected').text();
        var response = $('#TxtTCKResponse').val();
        var Expectedrosolvedate = $('#TxtTCKrosolve_Date').val();
        var progress = $('#TxtProgress option:selected').text();
        //alert(Ticketid);


        if (time === "" || time === "Select") {
            swal("Here's a message!")
            $('#TxtSlot').focus();
            return false;
        }
        if (response === "") {
            swal("Please Enter the Response")
            $('#TxtTCKResponse').focus();
            return false;
        }
        if (Expectedrosolvedate === "") {
            swal("Please Choose the Expected Resolving Date")
            $('#TxtTCKrosolve_Date').focus();
            return false;
        }
         if (progress === ""||progress==="Select") {
             swal("Please Select the Progress Status")
            $('#TxtProgress').focus();
            return false;
        }


        $.ajax({
            url: "../Ticket/TicketStatusUpdate",
            type: "Post",
            data: { 'Ticketid': Ticketid, 'time': time, 'response': response, 'Expectedrosolvedate': Expectedrosolvedate,'progress':progress},
            success: function (response) {

                if (response === 1) {
                    swal({
                        title: "Success",
                        text: "Ticket Status Has been Updated",
                        icon: "success",
                        buttons: true,
                        dangerMode: false,
                    })
                        .then((value) => {
                            if (value)
                                window.location.href = '/Ticket/OwnerTCKView';
                        });                 
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
    if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode !== 32) {

        return false;
    }
    return true;
}

function DoLoadTicketDetails(data) {
    //urlTicket is specified in _ChildLayout
    
    $.post(urlTicket, {
        id: data,

    },
        function () {
            window.location.href = "../Ticket/bindTicketDetails";
            
        });
}








