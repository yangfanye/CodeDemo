/**
 *
 * 配套视频教程请移步微信->小程序->灵动云课堂
 * 关注订阅号【huangxiujie85】，第一时间收到教程推送
 *
 * @link http://blog.it577.net
 * @author 黄秀杰
 */

const AV = require('../../../utils/av-weapp.js')
var app = getApp()
var that;
Page({
	onLoad: function () {
		that = this;
	},
	orderManage: function () {
		wx.navigateTo({
			url: '../order/order'
		});
	}
})