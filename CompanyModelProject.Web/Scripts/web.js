﻿function mit() {

    var name1 = /[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*/;
    var plones = /^1[3|4|5|8][0-9]\d{4,8}$/;
    var name = $("#myname").val();

    var plone = $("#mebli").val();
    var main = $("#main").val();
    if (!name1.test(name)) {
        alert("您的姓名有误，请重新输入");
        return;
    } else if (name == "---请输入您的姓名---") {
        alert("请输入您的姓名");
        return;
    } else if (!plones.test(plone)) {
        alert("您的电话有误，请重新输入");
        return;
    }
    else if (main == "请提出您的要求") {
        alert("请填写您的需求");
        return;
    }
    else {
        $.get('/Home/AddMessage?date=' + new Date(), { plone: plone, name: name, main: main }, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "/Home/Index";
            }
            else { alert(data.message); }
        });
    }
}

function getTeacher()
{
    var html='';
    $.get('/Home/getTeacherHandler?date=' + new Date(), {}, function (data) {
        if (data.code == 0) {
            var obj = eval(data.data);
            for (var i = 0; i < obj.length; i++) {
                html += '<div class="taer_img2"><a href="' + obj[i].HtmlUrl + '"><img src="' + obj[i].pic + '" alt="' + obj[i].Name + '" width="82px" height="109px" /></a><div>' + obj[i].Name + ' </div> </div>';
            }
            $("#teacher").html(html);     
        }
        else { alert(data.message); }
    });
}


function getstudy() {
    var html = '';
    $.get('/Home/getstudyHandler?date=' + new Date(), {}, function (data) {
        if (data.code == 0) {
            var obj = eval(data.data);
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].Name.length>21) {
                    obj[i].Name = obj[i].Name.toString().substring(0, 21);
                }
                html += '<li><span class="fll1"> <a href="' + obj[i].HtmlUrl + '"> <span class="tear_t_dot" style="margin-right:2px;"></span> ' + obj[i].Name + '</a></span></li>';
            }
            $("#studyul").html(html);
            }
        else { alert(data.message); } 
        } 
    );
}

function getFriendLink() { 
    $.get('/Home/getFriendLinkHandler?date=' + new Date(), {}, function (data) {
        if (data.code == 0) {
            var html = ' <div style="float: left;margin-top:8px;color: #fff;">友情链接：</div>';
            var obj = eval(data.data);
            for (var i = 0; i < obj.length; i++) {
                html += ' <div class="friendlinks"><a href="' + obj[i].fromUrl + '" target="_blank"> ' + obj[i].Name + '</a></div>'; 
            }
            $("#friendlinks").html(html);
        } 
    }
    );
} 