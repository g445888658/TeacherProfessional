// JavaScript Document
function getTime() {
var date = new Date();
var year = date.getFullYear().toString();
var month = (date.getMonth() + 1);
var day = date.getDate().toString();
var hour = date.getHours().toString() + ":";
var minutes = date.getMinutes().toString();
var seconds = date.getSeconds().toString();
if (minutes.toString() < 10) {
minutes = "0" + minutes;
}
if (seconds < 10) {
seconds = "0" + seconds;
}
var week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", " "];
var weekday = week[7] + week[date.getDay()];
document.getElementById("timeSpan").innerHTML =" 现在是："+ hour + minutes + ":" + seconds;
document.getElementById("timeSpan1").innerHTML = year+"年"+ month +"月"+ day  + "日 "+  weekday;
}
setInterval(getTime, 1000);