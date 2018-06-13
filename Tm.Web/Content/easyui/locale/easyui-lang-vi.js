if ($.fn.pagination){
	$.fn.pagination.defaults.beforePageText = 'Trang';
	$.fn.pagination.defaults.afterPageText = 'của {pages}';
	$.fn.pagination.defaults.displayMsg = 'Hiển thị {from} đến {to} trong {total} mục';
}
if ($.fn.datagrid){
	$.fn.datagrid.defaults.loadMsg = ' đang xử lý...';
}
if ($.fn.treegrid && $.fn.datagrid){
	$.fn.treegrid.defaults.loadMsg = $.fn.datagrid.defaults.loadMsg;
}
if ($.messager){
	$.messager.defaults.ok = 'Ok';
	$.messager.defaults.cancel = 'Thoát';
}
$.map(['validatebox','textbox','passwordbox','filebox','searchbox',
		'combo','combobox','combogrid','combotree',
		'datebox','datetimebox','numberbox',
		'spinner','numberspinner','timespinner','datetimespinner'], function(plugin){
	if ($.fn[plugin]){
		$.fn[plugin].defaults.missingMessage = 'Thông tin bắt buộc.';
	}
});
if ($.fn.validatebox){
	$.fn.validatebox.defaults.rules.email.message = 'Xin mời nhập vào địa chỉ email.';
	$.fn.validatebox.defaults.rules.url.message = 'Xin mời nhập một URL.';
	$.fn.validatebox.defaults.rules.length.message = 'Hãy nhập vào gía trị trong khoảng từ {0} đến {1}.';
	$.fn.validatebox.defaults.rules.remote.message = 'Sửa lại nội dung này.';
}
if ($.fn.calendar){
	$.fn.calendar.defaults.weeks = ['S','M','T','W','T','F','S'];
	$.fn.calendar.defaults.months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
}
if ($.fn.datebox){
	$.fn.datebox.defaults.currentText = 'Hôm nay';
	$.fn.datebox.defaults.closeText = 'Đóng';
	$.fn.datebox.defaults.okText = 'Ok';
}
if ($.fn.datetimebox && $.fn.datebox){
	$.extend($.fn.datetimebox.defaults,{
		currentText: $.fn.datebox.defaults.currentText,
		closeText: $.fn.datebox.defaults.closeText,
		okText: $.fn.datebox.defaults.okText
	});
}
