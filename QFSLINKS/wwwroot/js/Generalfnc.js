var OptionsAjax = {
        url: '',
        type: 'GET',
        contentType: 'application/json',
        data: ''
    };
function AddRow(f1, f2, nn)
{
    var filter = $('#SearchString').val(); //OK
    var pages = $('#pages').val();
    var page = nn;
    var currentFilter = f2;
    var searchString = f1;
    var sortOrder;
    var tt = '<form action="/Home/AddUser?sortOrder=' + sortOrder + '&=currentFilter' + currentFilter + '&searchString=' + searchString + '&Page=' + page + '" method="get">';
    $('#UserTable > tbody:first-child').append('<form action="./Home/AddUser?sortOrder=' + sortOrder + '&=currentFilter' + currentFilter + '&searchString=' + searchString + '&Page=' + page + '" method="get">' +
        '<tr>' +
    '<td>' +
        '<input id="txtusername" type="textbox" value="" placeholder = "e.g aranglew"/>' +
    '</td>'+
    '<td>' +
        '<input id="txtdata" type="textbox" value="" />' +
    '</td>'+
    '<td>' +
        '<input id="txtlocation" type="textbox" value="" />' +
    '</td>'+
    '<td>' +
        '<input id="txtdivision" type="textbox" value="" />' +
    '</td>'+
    '<td>' +
        '<input id="txtuseremail" type="textbox" value=""/> ' +
    '</td>'+
    '<td>' +
        '<input id="txtvimvisible" type="checkbox" />' +
    '</td>'+
    '<td>' +
        '<input id="txtvimdelegate" type="checkbox"  />' +
    '</td>'+
    '<td>' +
        '<input id="txtvimaccess" type="checkbox"  />' +
    '</td>' +
    '<td>' +        
        '<input title="save new user" class="button" value="Save" type="Submit"/>' +
        '</form>'+
    '</td>' +
'</tr>');
}
function showhide(id) {
    var e = document.getElementById(id);
    e.style.display = e.style.display === 'block' ? 'none' : 'block';
}
function ShowUserDetails(operation, button) {
    var modal = $('#myModal');
    if (operation === 2) { //Create new user
        modal.find('.modal-title').text('New User ' );
        //Setting the field editable and show the save button
        $("#modaluser").prop('disabled', false);
        $("#modaluser2").prop('disabled', false);
        $("#savebutton").show();
        $("#CopyUserid").hide();
        $("#CopyUserform").hide(); 
        GetDivisionList("");
        $('#username').val('');
        $('#userdata').val('');
        $('#division').val('');
        $('#location').val('');
        $('#topicid').val('');
        $('#topicuserid').val('');
        $('#email').val('');
        $('#initials').val('');
        $('#phone').val('');
        $('#Access').val('');
        $('#Accessa').val('');
        $('#vimsaccess').val('');
        $('#vimsdelegate').prop("checked", false);
        $('#vimsvisible').prop("checked", false);

    }
    else { //Show User Details
        var recipient = button.dataset.user;
        GetDetailbyUser(recipient, function (result) {                               
            if (operation === 1) { //Edit User
                modal.find('.modal-title').text('Editing user: ' + result[0].Data);
                //Setting the field editable and show the save button
                $("#modaluser").prop('disabled', false);
                $("#modaluser2").prop('disabled', false);
                $("#savebutton").show();
                $("#CopyUserid").show(); 
                $("#CopyUserform").show();
                GetUserList("", result[0].Division);
            }
            if (operation === 0) { //Show user details
                modal.find('.modal-title').text('Details for: ' + result[0].Data);
                $("#modaluser").prop('disabled', true);
                $("#modaluser2").prop('disabled', true);
                $("#savebutton").hide();
                $("#CopyUserid").hide();                
                $("#CopyUserform").hide(); 
            }
            GetDivisionList(result[0].Division);
            $('#username').val(result[0].UserName);
            $('#userdata').val(result[0].Data);
            $('#division').val(result[0].Division);
            $('#location').val(result[0].Location);
            $('#topicid').val(result[0].TopicID);
            $('#topicuserid').val(result[0].TopicUserID);
            $('#email').val(result[0].UserEmail);
            $('#initials').val(result[0].UserInitials);
            $('#phone').val(result[0].UserPhone);
            $('#Access').val(result[0].Access);
            $('#Accessa').val(result[0].AccessA);
            $('#vimsaccess').val(result[0].VimsAccess);
            $('#vimsdelegate').prop("checked", result[0].VimsDelegate);
            $('#vimsvisible').prop("checked", result[0].VimsVisible);
        });
    }
    //})
}
function SaveUser() {
    var userDTO = {
        username: $('#username').val(),
        data: $('#userdata').val(),
        division: $('#division').val(),
        location: $('#location').val(),
        TopicID : $('#topicid').val(),
        TopicUserID: $('#topicuserid').val(),
        UserEmail: $('#email').val(),
        UserInitials: $('#initials').val(),
        UserPhone: $('#phone').val(),
        Access: $('#Access').val(),
        Accessa:$('#Accessa').val(),
        vimsaccess:$('#vimsaccess').val(),
        vimsdelegate: $('#vimsdelegate').prop('checked'),
        vimsvisible: $('#vimsvisible').prop('checked')
    };

    if (userDTO.TopicUserID === "" || userDTO.TopicUserID === "") {
        //Insert New user
        Newuser(userDTO);
    }
    else {
        EditUser(userDTO);
    }
}
function GetDivisionList(CurrentDivision) {
    $.ajax({
        url: './Home/GetDivisionList/',
        type: "GET",
        dataType: "json",
        contentType: "application/json;",
        success: function (resultD) {

            var options = "";
            if (CurrentDivision.length < 1) {
                options += '<option value ="Select a Division" disabled selected> Select Division </option>';
            }
            var existdivision = false;
            for (var i = 0; i < resultD.length; i++) {
                if (resultD[i].DivisionName === CurrentDivision) {
                    options += '<option value ="' + resultD[i].DivisionName + '" selected>' + resultD[i].DivisionName + '</option>';
                    existdivision = true;
                }
                else {
                    options += '<option value ="' + resultD[i].DivisionName + '">' + resultD[i].DivisionName + '</option>';
                }
            }
            $("#division").empty();
            $("#division").append(options);
            //if (!existdivision) $("#division").val("empty");
        }
    });
}
function GetUserList(CurrentUser, CurrentDivision) {
    $.ajax({
        url: './Home/GetUserList/',
        type: "GET",
        data: {division: CurrentDivision},
        dataType: "json",
        contentType: "application/json;",
        success: function (resultD) {

            var options = "";
            var existuser = false;
            for (var i = 0; i < resultD.length; i++) {
                if (resultD[i].UserName === CurrentUser) {
                    //options += '<option value ="' + resultD[i] + '" selected>' + resultD[i] + '</option>';
                    existuser = true;
                }
                else {
                    options += '<option value ="' + resultD[i] + '">' + resultD[i] + '</option>';
                }
            }
            $("#CopyTousername").empty();
            $("#CopyTousername").append(options);
            if (!existuser) $("#CopyTousername").val("empty");
        }
    });
}
function Newuser(userDTO) {
    userDTO.TopicUserID = 1;
    userDTO.TopicID = 0;
    $.ajax({
        url: './Home/AddUserAjax/',
        type: "POST",
        contentType: "application/json;",
        data: JSON.stringify({ New_user: userDTO }),
        success: function (result) {
            $('#myModal').modal('hide');
            $('#SearchString').val(userDTO.UserName);
            window.location.reload();
        },
        error: function () {
            alert("Error saving user data");
        }
    });

}
function GetDetailbyUser(recipient, myhandler) {
    $.ajax({
        url: './Home/DetailsbyUser',
        //async: false,
        type: "GET",
        dataType: "json",
        contentType: "application/json;",
        data: { "UserName": recipient },
        success: function (resultData) {
            myhandler(resultData);
        },
        error: function(){
            alert("Error getting user data");
        }
    });
}
function EditUser(userDTO) {
    $.ajax({
        url: './Home/EditUser/',
        type: "POST",
        contentType: "application/json;",
        data: JSON.stringify({ Edit_user: userDTO }),
        success: function (result) {
            $('#myModal').modal('hide');
            window.location.reload();
        },
        error: function () {
            alert("Error saving user data");
        }
    });
}
function GetNextRow(result) {
    var exceptionList = ['TID','Topic','Access','username','Detail'];
    var $tr = $('<tr/>');
    for (var propt in result) {        
        if (jQuery.inArray(propt, exceptionList) < 0) {
            var $input = $("<td/>")
                .attr("width",2)
                .append($("<input/>", { "id": propt + i, "value": result[propt] }));
            $tr.append($input);
        }
    }   
    //console.log($tr.html());
    return $tr.html();
}
function GetMenuOptionsbyUser(recipient, result) {
    $.ajax({
        url: './Home/Details/',
        //async: false,
        type: "GET",
        dataType: "json",
        contentType: "application/json;",
        data: { "SearchParam": recipient, "CurrentPage": 1, "CurrentFilter": recipient },
        success: function (resultData) {
            result(resultData);
        },
        error: function () {
            alert("Error getting user data");
        }
    });
}
function SaveMenuOption() {
    var userOptionDTO = {
        TopicID: $('#TopicIDOpt').val(),
        Topic: $('#TopicOpt').val(),
        Description: $('#DescriptionOpt').val(),
        Type: $('#TypeOpt').val(),
        sortOrder: $('#sortOrderOpt').val(),
        Location: $('#LocationOpt').val(),
        Data: $('#DataOpt').val(),
        Format: $('#FormatOpt').val(),
        Detail: $('#DetailOpt').val(),
        Access: $('#AccessOpt').val()
    };

    if (userOptionDTO.TopicUserID === 0 || userOptionDTO.TopicUserID === null) {
        //Insert New user Option
        NewuserOption(userOptionDTO);
    }
    else {
        EditUserOption(userOptionDTO);
    }
}
function NewUserLink(operation, button) {
    var modal = $('#NewUserLink');
    var CurrentUser = button.dataset.user;
    if (operation === "Add") { //Create new user Link
        modal.find('.modal-title').text('New Link to user:  ->' + CurrentUser);
        $("#modalOption1").prop('disabled', false);
        $("#savebutton").show();
        $("#CurrentUser").val(CurrentUser);
        GetAllLinks();
    }
}
function GetAllLinks() {    
    $.ajax({
        url: './GetAllLinks',
        //async: false,
        type: "GET",
        dataType: "json",
        contentType: "application/json;",
        success: function (resultD) {
            var options = "";            
            for (var i = 0; i < resultD.length; i++) {
                options += '<option value ="' + resultD[i].Value+ '">' + resultD[i].Text + '</option>';
            }
            $("#NewLink").empty();
            $("#NewLink").append(options);
        }
    });

}
function SaveUserLink() {

    var DTO_menubyuser = {
        TopicId: parseInt($('#NewLink').val()),
        Description: $('#Description').val(),
        Username: $('#CurrentUser').val()
    };
    $.ajax({
        url: './Home/AddUserLinkAjax/',
        type: "POST",
        contentType: "application/json;",
        data: JSON.stringify({ DTO_Link: DTO_menubyuser })
    })
    .done(function (result) {
        $('#NewUserLink').modal('hide');
        window.location.reload();
    })
    .fail(function (response, textStatus, errorThrown) {
        alert("Error saving user data");
        //alert(response.responseText);
        //alert(errorThrown);
        $('#NewUserLink').modal('hide');
        window.location.reload();
    });
}
function CopyUser() {
    //Get Current User
    //Get New User
    //Call Api to copy all links from one user to the other.
    var CurrentUser = $('#username').val();
    var CopyTouser = $('#CopyTousername option:selected').text();
    if (CopyTouser) {
        $.ajax({
            url: './Home/CopyUserLinks/',
            type: "POST",
            contentType: "application/json;",
            data: JSON.stringify({ currentuser: CurrentUser, copyTo: CopyTouser }),
            success: function (result) {
                $('#myModal').modal('hide');
                window.location.reload();
            },
            error: function () {
                alert("Error saving user data");
            }
        });
    }
    else //confirm('You must select a user to copy this user links');
    {
        $("#LinksWarning").prop("display", "block");
        $("#LinksWarning").show();
    }
}
function ShowOptionDetail(operation, button) {
    var modal = $('#OptionModal');
    var TuserId = button.dataset.tuid;
    var username = button.dataset.user;
    GetLinkById(TuserId, username, function (result) {
    if (operation === 2) { //Delete Option
        //TODO
        modal.find('.modal-title').text('Editing Option: ' + result[0].TopicUserID + ' For user:' + username);
        $("#savebuttonO").text("YES");
        $("#CloseBO").text("NO");
        $("#DivDeleteText").show();        
    }
    else { //Show Option Details  
        $("#DivDeleteText").hide();
        if (operation === 1) { //Edit Option
            modal.find('.modal-title').text('Editing Option: ->' + result[0].TopicUserID + ' For user:' + username);
            //Setting the field editable and show the save button
            $("#modaloptionColumn1").prop('disabled', true);
            $("#modaloptionColumn2").prop('disabled', false);
            $("#savebuttonO").show();
        }
        if (operation === 0) { //Show Option details
            modal.find('.modal-title').text('Details for Option: ' + result[0].TopicUserID+' - Username:'+username);
            $("#modaloptionColumn1").prop('disabled', true);
            $("#modaloptionColumn1").prop('disabled', true);
            $("#savebuttonO").hide();
        }
    }
    //$("DivDeleteText").hide();
    $('#Type').val(result[0].Type);
    $('#Description').val(result[0].Description);
    $('#Location').val(result[0].Location);
    $('#Data').val(result[0].Data);
    $('#Format').val(result[0].Format);
    $('#TopicUserId').val(result[0].TopicUserID);
    $('#TopicId').val(result[0].TopicId);
    $('#SortOrder').val(result[0].SortOrder);
    $('#Datau').val(result[0].DataU);
    $('#VimsVisible').prop("checked", result[0].VimsVisible);               
    });
    }
function GetLinkById(TopicuserId, username,myfunction) {
$.ajax({
    url: './GetLinkById',
    type: "GET",
    dataType: "json",
    contentType: "application/json;",
    data: { "TopicUserId":TopicuserId, "UserName": username },
    success: function (result) {
        myfunction(result);
    },
    error: function () {
        alert("Error getting option data");
    }
});
}
function SaveUserOption() {
    var DTOmenubyuser = {
        TopicUserID: parseInt($('#TopicUserId').val()),
        TopicId: parseInt($('#TopicId').val()),
        Username: $('#CurrentUser').val(),
        SortOrder: $('#SortOrder').val(),
        DataU: $('#Datau').val()
    };
    OptionsAjax.type = 'POST';
    OptionsAjax.data = DTOmenubyuser;    
    // The text button changed to Yes when the option is delete user link
    if ($("#savebuttonO").text() === "YES") {OptionsAjax.url = './Home/DeleteUserLink/';}
    else { OptionsAjax.url = './Home/EditUserLink/'; } //Edit User Link
    AjaxRequest(OptionsAjax);
    $('#OptionModal').modal('hide');
    window.location.reload();
}
function AjaxRequest(Options) {
    $.ajax({
        url: Options.url,
        type: Options.type,
        contentType: Options.contentType,
        data: JSON.stringify({ data: Options.data }),
        success: function (result) {
        },
        error: function (response, textStatus, errorThrown) {
            alert(response.statusText);
        }
    });

}
function AjaxRequest(Options,myfunction) {
    $.ajax({
        url: Options.url,
        type: Options.type,
        contentType: Options.contentType,
        data: JSON.stringify({ data: Options.data }),
        success: function (result) {
            myfunction(result);
        },        
        error: function (response, textStatus, errorThrown) {
            alert(response.statusText);
        }
    });    
    
}

///****************************************************************************************************
/* Begin Options for Menu Item Links*/

function LinkFunctions(operation, button) {
    var modal = $('#LinkOptionsModal');
    $("#modalfunctionsLinkC1").prop('disabled', false);
    $("#modalfunctionsLinkC2").prop('disabled', false);
    $("#saveLinkbutton").show();
    if (operation === 2) { //Create new Link
        modal.find('.modal-title').text('New Link ');
        //Setting the field editable and show the save button
        $('#topicid').val('');
        $('#sortorder').val('');
        $('#format').val('');
        $('#detail').val('');
        $('#type').val('');
        $('#description').val('');
        $('#data').val('');
        $('#location').val('');
    }
    else { //Show Link Details
        var ThisTopic = button.dataset.topic;
        GetLinkDetails(ThisTopic, function (result) {
            if (operation === 1) { //Edit Link
                modal.find('.modal-title').text('Editing Link: ' + result[0].Description);
            }
            if (operation === 0) { //Show Link  details
                modal.find('.modal-title').text('Details for: ' + result[0].Description);
                $("#modalfunctionsLinkC1").prop('disabled', true);
                $("#modalfunctionsLinkC2").prop('disabled', true);
                $("#saveLinkbutton").hide();
            }
            $('#tid').val(result[0].TID);
            $('#topicid').val(result[0].TopicID);
            $('#sortorder').val(result[0].SortOrder.toString());
            $('#format').val(result[0].Format);
            $('#detail').val(result[0].Detail);
            $('#type').val(result[0].Type);
            $('#description').val(result[0].Description);
            $('#data').val(result[0].Data);
            $('#location').val(result[0].Location);
        });
    }
}
function GetLinkDetails(ThisTopic,myfunction) {
    
    var DTOMenuLink = {
        TID: parseInt(ThisTopic),
        TopicID: parseInt($('#topicid').val()),        
        Type: $('#type').val(),
        SortOrder: $('#sortorder').val(),
        Description: $('#description').val(),
        Data: $('#data').val(),
        Location: $('#location').val(),
        Format: $('#format').val(),
        Detail: $('#detail').val()
    };
    OptionsAjax.url = './GetSingleLink';
    OptionsAjax.type = 'POST';
    OptionsAjax.data = DTOMenuLink;
    AjaxRequest(OptionsAjax, myfunction);
}
function SaveLinkChanges() {
    //Save the data for the user links.
    var DTOMenuLink = {
        TID: parseInt($('#tid').val()),
        TopicID: parseInt($('#topicid').val()),
        Type: $('#type').val(),
        SortOrder: $('#sortorder').val(),
        Description: $('#description').val(),
        Data: $('#data').val(),
        Location: $('#location').val(),
        Format: $('#format').val(),
        Detail: $('#detail').val()
    };

    if (DTOMenuLink.TID === "" || DTOMenuLink.TopicID === "") {
        //Insert New user
        //Newuser(userDTO);
        Alert('New');
    }
    else {
        //EditDataLink(DTOMenuLink);
        OptionsAjax.url = './EditDataLink';
        OptionsAjax.type = 'POST';
        OptionsAjax.data = DTOMenuLink;
        AjaxRequest(OptionsAjax);
        $('#LinkOptionsModal').modal('hide');
        window.location.reload();
    }
}
/* End Options for Menu Item Links*/
