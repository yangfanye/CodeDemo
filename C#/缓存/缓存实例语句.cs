
string CacheKey = "BargainingShareNum_Value$" + BossID + "$" + BarID;//给缓存定义Key值;

object cacheValueObj = Common.MemCachedHelper.GetCache(CacheKey);//获取缓存
if (cacheValueObj != null)
{
	try
	{
		int ShareNum = Convert.ToInt32(cacheValueObj);//查看人数+1
		if (ShareNum % switchPara == 0)//当被缓存参数整除时
		{
			ShareNum = Convert.ToInt32(cacheValueObj) + 1;
			retInt = dal.UpdataBargainingInfo(BossID, BarID, ShareNum, 2);//进行数据库的更新
			Common.MemCachedHelper.DeleteCache(CacheKey);//删除缓存
			retInt = 5;
		}
		else
		{
			ShareNum = Convert.ToInt32(cacheValueObj) + 1;
			Common.MemCachedHelper.AddCache(CacheKey, ShareNum, TimeSpan.FromDays(cacheDay));//更新缓存
			retInt = 5;
		}
	}
	catch (Exception ex)
	{
		ActCommon.Log.WriteLog("UpdataBarShareNum_更新缓存:date(" + System.DateTime.Now.ToString() + "),error:" + ex.Message.ToString(), "Vxin");
		retInt = -5;
	}