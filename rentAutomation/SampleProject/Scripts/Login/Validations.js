
window.onload = Validateform;

function Validateform() {
    //alert("Testing........");
   
}

function Register() {
    alert("Testing........");

}



function CheckExistence() {
    //alert("INside validation");
    var un = $("#User").val();
    var pwd = $("#password").val();
    if (un == "") {
       
        alert("Please Enter UserName");
        $("#User").focus();
        return false;
    }
    else if (pwd == "") {
        alert("Please Enter password");
        $("#password").focus();
        return false;
    }
    //else {
    //    alert("xscecr");
    //    $.ajax({
    //        type: "POST",
    //        contentType:"json",
    //        url: "Login/CheckUSer",
    //        data: {
    //            'user': un,
    //            'password': pwd

    //        },
    //        success: function (data) {
    //            if (data[0] == "1") {
    //                window.location.href = "../Customer/ViewCustomer";
                    
    //            }
    //            else {
    //                alert("Please Enter Valid Credentials");
    //                window.location.href = "../Login/Login";
    //                return false;
                    
    //            }






    //        }
    //    });
    //}
}


//function ValidateFields() {

//    var un = $("#User").val();
//    var pwd = $("#password").val();

//    if (un == "") {
//        alert("Please Enter UserName");
//        $("#User").focus();
//        return false;
//    }
//    else if (pwd == "") {
//        alert("Please Enter password");
//        $("#password").focus();
//        return false;
//    }
   
//                    window.location.href = "../Customer/ViewCustomer";
    
//}




   


