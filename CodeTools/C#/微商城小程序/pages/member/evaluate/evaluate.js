/**
 *
 * 配套视频教程请移步微信->小程序->灵动云课堂
 * 关注订阅号【huangxiujie85】，第一时间收到教程推送
 *
 * @link http://blog.it577.net
 * @author 黄秀杰
 */

const AV = require('../../../utils/av-weapp.js')
var that;
Page({
    data: {
        images: [],
        imageWidth: getApp().screenWidth / 4 - 10
    },
    onLoad: function(options) {
        that = this;
        // 取出商品id
        var objectId = options.objectId;
        // 存在当前页面data中，以提交评价表使用
        that.setData({
            objectId: objectId
        });
        console.log(objectId);
    },
    getContent: function(e) {
        that.setData({
            content: e.detail.value
        });
    },
    chooseImage: function() {
        // 选择图片
        wx.chooseImage({
            sizeType: ['compressed'],
            sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
            success: function(res) {
                // 返回选定照片的本地文件路径列表，tempFilePath可以作为img标签的src属性显示图片
                var tempFilePaths = res.tempFilePaths;
                console.log(tempFilePaths);
                that.setData({
                    images: that.data.images.concat(tempFilePaths)
                });
            }
        })
    },
    previewImage: function() {
        // 预览图集
        wx.previewImage({
            urls: that.data.images
        });
    },
    submit: function() {
        // 提交图片，事先遍历图集数组
        var promises = that.data.images.map(function(tempFilePath) {
            return new AV.File('file-name', {
                blob: {
                    uri: tempFilePath,
                },
            }).save();
        });
        Promise.all(promises).then(
            function(files) {
                // Promise返回待上传图片数组
                var uploadedImages = files.map(function(file) {
                    return file.url();
                });
                // 提交数据到网络
                var evaluate = new AV.Object('Evaluate');
                // 设置图组
                evaluate.set('images', uploadedImages);
                // 设置用户提交的描述
                evaluate.set('content', that.data.content);
                // 设置当前用户
                evaluate.set('user', AV.User.current());
                // 关联商品
                evaluate.set('goods', AV.Object.createWithoutData('Goods', that.data.objectId));
                // 保存到LeanCloud数据表
                evaluate.save().then(function() {
                    // 弹出提示
                    wx.showToast({
                        title: '评价成功',
                        success: function() {
                            // wx.navigateBack();
                        }
                    });
                }, function(err) {
                    console.log(err);
                });
            }
        );
    },
    delete: function(e) {
        var index = e.currentTarget.dataset.index;
        var images = that.data.images;
        images.splice(index, 1);
        that.setData({
            images: images
        });
    }
})