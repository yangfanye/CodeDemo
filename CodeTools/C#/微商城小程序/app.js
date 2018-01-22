/**
 *
 * 配套视频教程请移步微信->小程序->灵动云课堂
 * 关注订阅号【huangxiujie85】，第一时间收到教程推送
 *
 * @link http://blog.it577.net
 * @author 黄秀杰
 */

// 初始化AV
const AV = require('./utils/av-weapp.js');
const appId = "7tm1OFlNlmLFukegUhmm4uDU-gzGzoHsz";
const appKey = "XG4FRumQWJ7mNkFIral0ttvj";

AV.init({ 
	appId: appId, 
	appKey: appKey
});

// 授权登录
App({
	onLaunch: function () {
        // auto login via SDK
        var that = this;
        AV.User.loginWithWeapp();
        // 已废弃，已改用云函数实现，详见：http://blog.it577.net/index.php/archives/7/
     //    wx.login({
     //    	success: function(res) {
     //    		if (res.code) {
     //    			that.code = res.code;
	    //       		// 获取openId并缓存
	    //         	wx.request({
	    //         		url: 'https://lendoo.leanapp.cn/index.php/WXPay/getSession',
	    //         		data: {
	    //         			code: res.code,
	    //         		},
	    //         		method: 'POST',
	    //         		header: {
	    //         			'content-type': 'application/x-www-form-urlencoded'
	    //         		},
	    //         		success: function (response) {
	    //         			that.openid = response.data.openid;
			  //           }
			  //       });
	    //         } else {
	    //         	console.log('获取用户登录态失败！' + res.errMsg)
	    //         }
	    //     }
	    // });

		// 设备信息
		wx.getSystemInfo({
			success: function(res) {
				that.screenWidth = res.windowWidth;
				that.screenHeight = res.windowHeight;
				that.pixelRatio = res.pixelRatio;
			}
		});
	}
})
