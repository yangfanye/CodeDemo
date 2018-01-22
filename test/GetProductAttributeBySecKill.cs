/// <summary>
        /// 获取商品属性_秒杀
        /// </summary>
        /// <param name="bossId"></param>
        /// <param name="strProductWhere"></param>
        /// <returns></returns>
        public static AMP_ProductAttribute GetProductAttributeBySecKill(string bossId, string productToken, string attributeId, string strWhere, int type)
        {
            List<V_Model.SOP_SecKill> secKillList = new List<V_Model.SOP_SecKill>();
            string Where = " AND Status = " + type;
            secKillList = SOP_SecKillBLL.GetSecKillList(bossId, Where);
            AMP_ProductAttribute seckillProductList = new AMP_ProductAttribute();
            seckillProductList = dal.GetProductAttribute(bossId, productToken, attributeId, strWhere);
            List<V_Model.SOP_SecKillDetail> secKillDetailinfos = new List<V_Model.SOP_SecKillDetail>();
            secKillDetailinfos = SOP_SecKillDetailBLL.GetSecKillDetailInfos(bossId, secKillList[0].SecID);//后期需要优化,支持多活动
            if (secKillDetailinfos != null && secKillDetailinfos.Count > 0)
            {
                if (seckillProductList != null)
                {
                    foreach (var Item in secKillDetailinfos)
                    {
                        if (seckillProductList.Pid == Item.PID)
                        {
                            seckillProductList.ListPrice = Item.SeckillPrice;
                            seckillProductList.OrderTypeCode = 1; //标注为秒杀订单;
                        }
                    }
                }
            }
            return seckillProductList;
        }