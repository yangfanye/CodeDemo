/// <summary>
        /// 获取商品信息_秒杀
        /// </summary>
        /// <param name="bossId"></param>
        /// <param name="strProductWhere"></param>
        /// <returns></returns>
        public static AMP_Products GetProductInfoBySecKill(string bossId, string productToken, int attributId, int type)
        {
            List<V_Model.SOP_SecKill> secKillList = new List<V_Model.SOP_SecKill>();
            string strWhere = " AND Status = " + type;
            secKillList = SOP_SecKillBLL.GetSecKillList(bossId, strWhere);
            AMP_Products seckillProductList = new AMP_Products();
            seckillProductList = dal.GetProductInfo(bossId, productToken, attributId);
            List<V_Model.SOP_SecKillDetail> secKillDetailinfos = new List<V_Model.SOP_SecKillDetail>();
            secKillDetailinfos = SOP_SecKillDetailBLL.GetSecKillDetailInfos(bossId, secKillList[0].SecID);//后期需要优化,支持多活动
            if (secKillDetailinfos != null && secKillDetailinfos.Count > 0)
            {
                if (seckillProductList != null)
                {
                    foreach (var Item in secKillDetailinfos)
                    {
                        if (seckillProductList.PID == Item.PID)
                        {
                            seckillProductList.ProductAttriteList[0].ListPrice = Item.SeckillPrice;
                            seckillProductList.ListPrice = Item.SeckillPrice;
                            seckillProductList.LimitSaleNum = Item.SeckillSaleNum;
                            seckillProductList.LimitPurchaseNum = Item.SeckillPurchaseNum;
                            seckillProductList.LimitSaleStartTime = secKillList[0].SecKillStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                            seckillProductList.LimitSaleEndTime = secKillList[0].SecKillEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                            seckillProductList.SecID = Item.SecID;
                            seckillProductList.SecKillName = secKillList[0].SecKillName;
                        }
                    }
                }
            }
            return seckillProductList;
        }