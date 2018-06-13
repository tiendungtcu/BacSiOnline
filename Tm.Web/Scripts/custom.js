//
// Dinh dang lai calendar
// Việt hóa hiển thị ngày tháng năm cho calendar
//
var tuan = ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"];
var thang = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"];
// document.onready
//-------------------------------------------------------------------
//// add tabs to center panel when user click on a tree node/////////
//-----------------------------------------------------------------------
function open1(plugin, url) {
    if ($('#tt').tabs('exists', plugin)) {
        $('#tt').tabs('select', plugin);
    } else {
        $('#tt').tabs('add', {
            title: plugin,
            href: url,
            closable: true,
            iconCls: 'icon-form',
            bodyCls: 'content-doc'
            // Không dùng extractor vì dữ liệu trả về là partial view đã tối ưu
            /*,
            extractor:function(data){
                data = $.fn.panel.defaults.extractor(data);
                var tmp = $('<div></div>').html(data);
                data = tmp.find('#content').html();
                tmp.remove();
                return data;
            } */
        });
    }
}
//---------------------------------------------------------------------------
// định dạng ngày tháng năm cho datebox
// Format date dd-mm-yyyy
//---------------------------------------------------------------------
function dformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return (d < 10 ? ('0' + d) : d) + '/' + (m < 10 ? ('0' + m) : m) + '/' + y;
}
//---------------------------------------------------------------------
// Parse date in datebox
//------------------------------------------------------------------
function dparser(s) {
    if (!s) return new Date();
    var ss = (s.split('/'));
    var y = parseInt(ss[2], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[0], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}
//-----------------------------------------------------------------
// Gửi yêu cầu server kết xuất thông tin và trả về file excel
//--------------------------------------------------------------
function Export(url) {
    var date = $('#vdate').datebox('getValue');
    var namsinh = $('#drpNamSinh').val();
    $.post(url, { ngay: date }, function (data) {
        if (data.message == 'success') {
            window.open(data.url);
        }
    });
}
//-----------------------------------------------------------------------
// đăng xuất 
//------------------------------------------------------------------------
function LogOff() {
    $('#logoutForm').submit();
}
//----------------------------------------------------------------------
// Xem thông tin người dùng
//----------------------------------------------------------------------
function ShowUserInfo(url) {

    $('#dlgUserInfo').dialog('open');
    $('#dlgUserInfo').dialog('refresh', url);
    $('#OldPassword').textbox('setValue', '');
    $('#NewPassword').textbox('setValue', '');
    $('#ConfirmPassword').textbox('setValue', '');
}
//--------------------------------------------------------------------------
//change password
//--------------------------------------------------------------------------
function ChangePassword() {
    var oldpass = $('#OldPassword').textbox('getValue');
    var newpass = $('#NewPassword').textbox('getValue');
    var confirmpass = $('#ConfirmPassword').textbox('getValue');

    $('#fmChangePassword').form('submit', {
        onSubmit: function () {
            if ($(this).form('validate')) {
                if (newpass != confirmpass) {
                    $.messager.alert('Thông báo', 'Mật khẩu mới và xác nhận mật khẩu không trùng nhau.', 'info');
                    console.log(2);
                    return false;
                }
                else {
                    console.log(1);
                    $('#btnChangePassword').linkbutton('disable');
                    return true;
                }
            }
        },
        success: function (result) {
            $('#btnChangePassword').linkbutton('enable');
            var result = eval('(' + result + ')');
            if (result.message == 'error') {
                $.messager.alert('Lỗi', result.status, 'error');
            } else {
                $('#dlgUserInfo').dialog('close');
                $.messager.show({
                    title: 'Thông báo',
                    msg: "Mật khẩu đã được thay đổi!"
                });
            }
        }
    });


}

//--------------------------------------------------------------------------
//save user info
//--------------------------------------------------------------------------
function SaveUserInfo() {
    $('#fmUserInfo').form('submit', {
        onSubmit: function () {
            if ($(this).form('validate')) {
                $('#btnSaveUserInfo').linkbutton('disable');
                return true;
            }
            return false;
        },
        success: function (result) {
            $('#btnSaveUserInfo').linkbutton('enable');
            var result = eval('(' + result + ')');
            if (result.message == 'error') {
                $.messager.alert('Lỗi', 'Quân số không khớp, xin kiểm tra lại!', 'error');
            } else {
                $('#dlgUserInfo').dialog('close');
                $.messager.show({
                    title: 'Thông báo',
                    msg: "Đã thực hiện thành công <br/> Cần thoát ra và đăng nhập lại để cập nhật"
                });
            }
        }
    });
}

//-------------------------------------------------------------------------------
// kiem tra thong tin form nhat ky huan luyen truoc khi submit
//------------------------------------------------------------------------------
function fNkcheck() {
    if ($('#fmHl').form('validate')) {
        $('#fmHl').submit();
    } else
        $.messager.alert('Lỗi', 'Thông tin chưa đầy đủ, xin kiểm tra lại', 'error');
}
//------------------------------------------------------------------------------
// Tính phân loại lần kiểm tra
//---------------------------------------------------------------------------
function calkt() {
    var tongso = Number($("#qscm").numberbox('getValue')) +
               Number($("#qsvm").numberbox('getValue'));
    $("#qstong").numberbox('setValue', tongso);
}


