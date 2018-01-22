public static IList<AMP_Products> GetTeamProListBySecKill(string bossid, string teamCode, string teamWhere, int type = 5, int intSortFiled = 0, string strSortOrder = "desc")
        {
            List<V_Model.SOP_SecKill> secKillList = new List<V_Model.SOP_SecKill>();
            string strWhere = " AND Status = " + type;
            secKillList = SOP_SecKillBLL.GetSecKillList(bossid, strWhere);

            IList<AMP_Products> seckillProductList = new List<V_Model.SOP.AMP_Products>();
            seckillProductList = dal.GetTeamProList(bossid, teamCode, teamWhere, intSortFiled, strSortOrder);
            List<V_Model.SOP_SecKillDetail> secKillDetailinfos = new List<V_Model.SOP_SecKillDetail>();
            secKillDetailinfos = SOP_SecKillDetailBLL.GetSecKillDetailInfos(bossid, secKillList[0].SecID);//后期需要优化,支持多活动
            if (secKillDetailinfos != null && secKillDetailinfos.Count > 0)
            {
                if (seckillProductList != null && seckillProductList.Count > 0)
                {
                    foreach (var item in seckillProductList)
                    {
                        foreach (var childItem in secKillDetailinfos)
                        {
                            if (item.PID == childItem.PID)
                            {
                                item.ProductAttriteList[0].ListPrice = childItem.SeckillPrice;
                                item.ListPrice = childItem.SeckillPrice;
                                item.LimitSaleEndTime = secKillList[0].SecKillEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                                item.SecID = childItem.SecID;
                            }
                        }
                    }
                }
            }
            return seckillProductList;
        }