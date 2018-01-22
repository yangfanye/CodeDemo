 /// <summary>
        /// 获取单个商品详情_秒杀
        /// </summary>
        /// <param name="proid"></param>
        /// <param name="bossid"></param>
        /// <param name="type">是获取状态5的活动还是获取状态3的活动</param>
        /// <returns></returns>
        public static AMP_Products GetSingleProductBySecKill(string pid, string bossid, int type)
        {
            List<V_Model.SOP_SecKill> secKillList = new List<V_Model.SOP_SecKill>();
            string strWhere = " AND Status = " + type;
            secKillList = SOP_SecKillBLL.GetSecKillList(bossid, strWhere);
            AMP_Products seckillProductList = new AMP_Products();
            seckillProductList = dal.GetSingleProduct(pid, bossid);
            List<V_Model.SOP_SecKillDetail> secKillDetailinfos = new List<V_Model.SOP_SecKillDetail>();
            secKillDetailinfos = SOP_SecKillDetailBLL.GetSecKillDetailInfos(bossid, secKillList[0].SecID);//后期需要优化,支持多活动
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
                            seckillProductList.LimitSaleStartTime = secKillList[0].SecKillStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                            seckillProductList.LimitSaleEndTime = secKillList[0].SecKillEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                            seckillProductList.LimitPurchaseNum = Item.SeckillPurchaseNum;
                            seckillProductList.LimitPurchaseStartTime = secKillList[0].SecKillStartTime.ToString("yyyy-MM-dd HH:mm:ss");
                            seckillProductList.LimitPurchaseEndTime = secKillList[0].SecKillEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                            seckillProductList.SecID = Item.SecID;
                            seckillProductList.SecKillName = secKillList[0].SecKillName;
                        }
                    }
                }
            }
            return seckillProductList;
        }