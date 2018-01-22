//初始化索引列表
    mui(".mui-col-xs-3").on('tap', "#content31", function () {
        var height = window.screen.height - 51;; //获取元素的高度
        var indexed = $("#list .mui-indexed-list-bar").children("a").length; // 获取元素的个数
        var margin = (height - indexed * 22) / indexed ; //获取每个元素之间的空隙
        $("#list .mui-indexed-list-bar").children("a").css('padding-bottom', margin/2); //设置元素的css样式
        $("#list .mui-indexed-list-bar").children("a").css('padding-top', margin/2); //设置元素的css样式
    })
	//向上滑动
    mui("#list").on("swipeup", "#brandArea", function () {
        $("#brandArea").css("transform", "translateY(0)");
    })
    //索引列表移动
    function GotoDiv(id) {       
        $("#brandArea").css("transform", "translateY(0)"); //元素的位移改为0
        var t = $(id).offset().top - 51; //每个元素与顶部的差值 - 搜索栏的高度;
        $("#brandArea").css("transform", "translateY(-" + t + "px)"); //改变元素的位移
		$("#showLetter span").html(id.innerText); //显示字母
        $("#showLetter").show().delay(500).hide(0); //显示之后隐藏
    }