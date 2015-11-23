function addmanager()
{
    window.location = "Regisiter";
}

function editpwd(v)
{
    window.location = "pwdModtify/" + v;
}
function editUser(v)
{
    window.location = "UserModtify/" + v;
}
function EditPassword(v)
{
    var pwd1 = $("#pwd1").val().trim();
    var pwd2 = $("#pwd2").val().trim();
    if (pwd1 == "") {
        $("#pwd1").focus();
        $("#spanpassword").html("密码不能为空");
        return;
    } 
    else if (pwd1 != "" && pwd1.length < 6) {
        $("#pwd1").focus();
        $("#spanpassword").html("密码长度不能小于6");
        return false;
    }
    else if (pwd1.length > 12) {
        $("#pwd1").focus();
        $("#spanpassword").html("密码长度不能大于12");
        return false;
    }

    else {
        $("#spanpassword").html("");
    }
    if (pwd2 == "") {
        $("#pwd2").focus();
        $("#spanpassword2").html("请确认密码");
        return false;
    }
        //两次输入密码不一致
    else if (pwd2 != "" && pwd2 != pwd1) {
        $("#pwd2").focus();
        $("#spanpassword2").html("两次输入密码不一致");
        return false;
    }
    else {
        $("#spanpassword2").html("");
    }
    $.post("/Account/EditPasswordHandler", { pwd1: pwd1, v: v }, function (data) {
        if (data.code == "0") {
            alert("密码修改成功！"); window.location = "/Account/UserList";

        }
        else {
            alert("密码修改失败,请重试！");
            return;
        }
    });
}
function delUser(v)
{
    if (confirm("确定要删除此用户信息吗？")) {
        $.get('/Account/DelUserHandler?date=' + new Date(), { v: v }, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "/Account/UserList";
            }
            else { alert(data.message); }
        });
    }
}

function addright(userid)
{
    var checkID = new Array();
    var v = '';
    $('input[name="chk_list"]:checked').each(function (i) {
        checkID[i] = $(this).val();
    });
    if (checkID == null || checkID == '') {
        alert("您有选择任何选项");
    }
    else {
        for (var i = 0; i < checkID.length; i++) {
            if (checkID[i] != '') {
                v += checkID[i] + ',';
            }
        }
        $.get('/Account/AddRightsHandler?date=' + new Date(), { userid: userid ,v:v}, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "/Account/UserList";
            }
            else { alert(data.message); }
        });
    } 
}
function getusrofrights(v)
{
    $.get('/Account/getURList?date=' + new Date(), { v: v }, function (data) {
        if (data.code == 0) {
            var s = data.data;  
            var rid = s.split(","); 
            for (var i = 0; i < rid.length; i++) { 
                $("#checkbox_" + rid[i]).attr("checked",true); 
            } 
        } 
    });
}