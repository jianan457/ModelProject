function addfristCol() {
    window.location = "ColumnFristAdd";
}

function editcol(v)
{
    window.location = "ColumnModtify/"+v;
}

function delcol(v)
{
    if (confirm("确定要此删栏目吗？")) {
        $.get('/Manager/DelColHandler?date=' + new Date(), { v:v }, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "/Manager/ColumnList";
            }
            else { alert(data.message); }
        });
    } 
}
function delcollist(list)
{
    var v='';
    for (var i = 0; i < list.length; i++) {
        if (list[i]!='') {
            v += list[i] + ',';
        }
    }
   
    $.get('/Manager/DelCollistHandler?date=' + new Date(), { v:v }, function (data) {
        if (data.code == 0) {
            alert(data.message);
            window.location = "/Manager/ColumnList";
        }
        else { alert(data.message); }
    });
}

function addsonCol(v)
{
    window.location = "ColumnSonAdd/" + v;
}

function selectDpList(val, id, value)
{
    var option = '<option value="0">请选择</option>';
    $("#ColumnId").val(val);
    $.get('/News/secColHandler?date=' + new Date(), { val:val }, function (data) {
        if (data.code == 0) {
            //得到data   
            var obj = eval(data.data);
            for (var i = 0; i < obj.length; i++) {
                option += ' <option value="' + obj[i].Id + '">' + obj[i].columnName + '</option>';
            } 
            $("#" + id).html(option);
            $("#" + id + "li").show();
            if (value!='') {
                $("#" + id).val(value);
            }
           
        } 
    }); 
}

function sDpList(val, id) {
    if (id!='') {
        var option = '<option value="-1">请选择</option>';
        $.get('/News/secColHandler?date=' + new Date(), { val: val }, function (data) {
            if (data.code == 0) {
                var obj = eval(data.data);
                for (var i = 0; i < obj.length; i++) {
                    option += ' <option value="' + obj[i].Id + '">' + obj[i].columnName + '</option>';
                }
                $("#" + id).html(option);
                //if (id == "SecCol") {
                //    LoadNewslist(val,0);
                //}
                //if (id == "ThirdCol") {
                //    LoadNewslist(val, 1);
                //} 
            }
            else {
                $("#" + id).html(option);
            }
        });
    }
    //else {
    //    LoadNewslist(val,3);
    //}
}
function search() {
    var key = $("#keyword").val().trim();
    var colid1 = $("#ColumnId").val();
    var colid2 = $("#SecCol").val();
    var colid3 = $("#ThirdCol").val();
    var level = 0;
    var colid = 0; 
    if (key == "" && key == null) {
        alert("请输入搜索关键字");
        return;
    }
    if (colid1 != 0 && colid2 == -1 && colid3==null) {
        colid = colid1; 
    }
    if (colid1 != 0 && colid2 != -1 && (colid3 == null|| colid3==-1)) {
        colid = colid2; level = 1;
    }
    if (colid1 != 0 && colid2 != -1 &&  (colid3 != null && colid3!=-1)) {
        colid = colid3; level = 2;
    }
    window.location.href = "/News/NewsList?page=1&upid=" + colid + "&levl=" + level + "&key=" + key;
}
function  LoadNewslist(val,levl)
{
    ///根据栏目加载新闻
    window.location = "?page=1&upid=" + val + "&levl=" + levl;
}
function setColId(val)
{
    $("#ColumnId").val(val);
}

//上传网站缩略图
function uploadnewsimg() {

    if ($("#newspic").val().length > 0) {
        ajaxFileUpload('newspic');
    }
    else {
        alert("请选择缩略图片");
    }
}
function ajaxFileUpload(pic) {
   
    $.ajaxFileUpload
    (
        {
            url: '/News/UploadHandler', //用于图片文件上传的服务器端请求地址
            //data:{cid:newcid},
            secureuri: false, //一般设置为false
            fileElementId: pic, //文件上传空间的id属性  <input type="file" id="file" name="file" />
            dataType: 'HTML', //返回值类型 一般设置为json
            success: function (data, status)  //服务器成功响应处理函数
            {
                $("#img" + pic).attr("src", data);
                $("#hdimg" + pic).val(data);
                if (typeof (data.error) != 'undefined') {
                    if (data.error != '') {
                        alert(data.error);
                    } else {
                        alert(data.msg);
                    }
                }
            },
            error: function (data, status, e)//服务器响应失败处理函数
            {
                alert(e);
            }
        }
    )
    return false;
}

function editNews(v)
{
    window.location = "NewsModtify/" + v;
}

function delNews(v)
{
    if (confirm("确定要删除信息吗？")) {
        $.get('/News/DelNewsHandler?date=' + new Date(), { v: v }, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "NewsList";
            }
            else { alert(data.message); }
        });
    }
}

function selectall(type)
{
    var checkID = new Array();
    $("#checkboxall").click(function () {
        $("input[name='chk_list']").attr("checked", $(this).attr("checked"));
    });
    $(".click").click(function () {

        $('input[name="chk_list"]:checked').each(function (i) {
            checkID[i] = $(this).val();
        });
        if (checkID == null || checkID == '') {
            alert("您有选择任何选项");
        }
        else {
            $(".tip").fadeIn(200);
        }

    });
    $(".tiptop a").click(function () {
        $(".tip").fadeOut(200);
    });

    $(".sure").click(function () {
        $(".tip").fadeOut(100);
        if (type == 'column') {
            delcollist(checkID);
        }
        if (type=='news') {
            delnewlist(checkID);
        }
        if (type = 'message') {
            delmesslist(checkID);
        }
    });

    $(".cancel").click(function () {
        $(".tip").fadeOut(100);
    });
}
function addNews()
{
    window.location = 'NewsAdd';
}

function delnewlist(list) {
    var v = '';
    for (var i = 0; i < list.length; i++) {
        if (list[i] != '') {
            v += list[i] + ',';
        }
    }
    $.get('/News/DelNewslistHandler?date=' + new Date(), { v: v }, function (data) {
        if (data.code == 0) {
            alert(data.message);
            window.location = "/News/NewsList";
        }
        else { alert(data.message); }
    });
}

function addMess()
{
    window.location = "/Manager/MessageAdd";
}

function delmesslist(list) {
    var v = '';
    for (var i = 0; i < list.length; i++) {
        if (list[i] != '') {
            v += list[i] + ',';
        }
    }
    $.get('/Manager/DelMesslistHandler?date=' + new Date(), { v: v }, function (data) {
        if (data.code == 0) {
            alert(data.message);
            window.location = "/Manager/MessageList";
        }
        else { alert(data.message); }
    });
}

function delMessage(v) {
    if (confirm("确定要此信息吗？")) {
        $.get('/Manager/DelmessHandler?date=' + new Date(), { v: v }, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "/Manager/MessageList";
            }
            else { alert(data.message); }
        });
    }
}

function ToHtml()
{
    var checkID = new Array();
    $('input[name="chk_list"]:checked').each(function (i) {
        checkID[i] = $(this).val();
    });
    if (checkID == null || checkID == '') {
        alert("您有选择任何选项"); 
    }
    else {
        var v = '';
        for (var i = 0; i < checkID.length; i++) {
            if (checkID[i] != '') {
                v += checkID[i] + ',';
            }
        }
        $.get('/News/NewslistToHtmlHandler?date=' + new Date(), { v: v }, function (data) {
            if (data.code == 0) {
                alert(data.message);
                window.location = "/News/NewsList";
            }
            else { alert(data.message); }
        });
    }
}