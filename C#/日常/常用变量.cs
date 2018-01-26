//MallBaseController
CustomerBindInfo.CustomerID.Value:微信用户 会员信息
CustomerBindInfo.CustomerID.HasValue:判断是否有会员信息的值;  hasValue

//Json解码
SaleSuccessMessage request = Newtonsoft.Json.JsonConvert.DeserializeObject<SaleSuccessMessage>(requestDataJson);

//订单号
string orderNumber = CompanySetInfo.BossID + System.DateTime.Now.ToString("yyMMddHHmmssfff") + Common.StringHelper.BuildRandomStr(6);
ViewBag.OrderNumber = orderNumber;

//分享
wx.ready(function () {
    var shareData = {
        title: '@shareData.title',//分享标题
        link: '@Html.Raw(shareData.link)',//分享链接 Html.Raw:可以将url链接中的&amp;转为&;
        desc: '@shareData.desc',//分享内容
        imgUrl: '@Html.Raw(shareData.imgUrl)',//分享图片
        success: function () {
            //用户确认分享以后执行的回调函数
        },
        cancel:function(){
            //用户取消分享以后执行的回调函数
        }
    };
    wx.onMenuShareAppMessage(shareData);
    wx.onMenuShareTimeline(shareData);
});

//日期格式化
datetime.ToString("yyyy-MM-dd HH:mm:ss");
Convert.ToDateTime(saleOrders.ReserveDate).ToString("yyyy-MM-dd")//针对datetime?的类型

//输出参数要先赋值
cmd.Parameters.Add("@IsSuccess_Para", MySqlDbType.Int32).Value = 0;
cmd.Parameters["@IsSuccess_Para"].Direction = ParameterDirection.Output;


//数据赋值时,查询出来的数据与model必须一一对应.model可以进行继承.

//获取int的值
isMallStart = systemset.IsMallStart.Value;//后边加Value;

//错误语句写入日志的语句
catch (Exception ex)
{
	ActCommon.Log.WriteLog(this.GetType().FullName + ".P_AddBargainingByBossID_MZ(date:" + System.DateTime.Now + "):error:" + ex.Message + "", "weixin");
    //"MZPai"是写入的文件名称
	return -1000;
    ActCommon.Log.WriteLog(".CreateQRCode(date:" + System.DateTime.Now + "):error:" + ex.Message + "", "weixin");
}

//错误页面
return Redirect("/Mobile/tip?action=para");

//访问限制特性?需要研究
[VisitConfig(IsDenyNoOpenId = true, IsDenyNotCustomer = true)]

//判断可空类型是否有值
HasValue:CustomerBindInfo.CustomerID.HasValue
//session接口方法
System.Web.SessionState.IRequiresSessionState