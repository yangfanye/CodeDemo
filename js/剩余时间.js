//活动倒计时
var setBargainingEndTime = function () {
	var endTime = document.getElementById('hidBarEndTime').value;
	var endTime = getDate(endTime);
	var year = endTime.getFullYear();
	var month = endTime.getMonth();
	var day = endTime.getDate();
	var hour = endTime.getHours();
	var minute = endTime.getMinutes();
	var second = endTime.getSeconds();
	leftTimer(year, month, day,hour, minute, second);
}
//日期格式化
function getDate(strDate) {
	var date = eval('new Date(' + strDate.replace(/\d+(?=-[^-]+$)/,
		function (a) { return parseInt(a, 10) - 1; }).match(/\d+/g) + ')');
	return date;
}
function leftTimer(year, month, day, hour, minute, second) {
	var leftTime = (new Date(year, month -1, day, hour, minute, second)) - (new Date()); //计算剩余的毫秒数 
	var days = parseInt(leftTime / 1000 / 60 / 60 / 24, 10); //计算剩余的天数 
	var hours = parseInt(leftTime / 1000 / 60 / 60 % 24, 10); //计算剩余的小时 
	var minutes = parseInt(leftTime / 1000 / 60 % 60, 10);//计算剩余的分钟 
	var seconds = parseInt(leftTime / 1000 % 60, 10);//计算剩余的秒数 
	days = checkTime(days);
	hours = checkTime(hours);
	minutes = checkTime(minutes);
	seconds = checkTime(seconds);
	document.getElementById("Day").innerHTML = days; 
	document.getElementById("hour").innerHTML = hours;
	document.getElementById("minutes").innerHTML = minutes;
	document.getElementById("seconds").innerHTML = seconds;
}
function checkTime(i) { //将0-9的数字前面加上0，例1变为01 
	if (i < 10) {
		i = "0" + i;
	}
	return i;
} 