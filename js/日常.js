//简单判断手机号的正则
var mobileCheckPars = /^1\d{10}$/;
//encodeURIComponent(orderRemark):加密;
//JSON.stringify(jsonData):json转字符串
var jsonData = { orderNumber: orderNumber, couponId: couponId, addressId: addressId, orderRemark: encodeURIComponent(orderRemark), productInfos: [] };
var jsonDataStr = JSON.stringify(jsonData);

//对本地数据库存储缓存数据
if(window.sessionStorage){
    sessionStorage.setItem('b-data-list',$("#sc").html());
}
var datalist = sessionStorage.getItem('b-data-list');