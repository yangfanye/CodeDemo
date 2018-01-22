

//微商城后台按钮规则
<input type="submit" value="查询" class="btn send_button" /> //功能按钮：查询，新建，尺寸较大
 <a class="btn send_button" href="/Marketing/BargainingAdd" style="display:inline-block;width:90px;text-align:center;">新建砍价</a>
 
<span><a class="btn edit_btn" href="/Marketing/SecKillEdit?SecID=@Convert.ToInt32(SecKillItem.SecID)">编辑</a></span> //信息按钮,编辑,
<span><input type="button" class="btn send_button" value="发布" onclick='ReleaseSecKill(@SecKillItem.SecID)' /></span>//功能按钮，发布 尺寸较小
<span><input type="button" class="btn del_btn" value="终止" onclick='CloseSecKill(@SecKillItem.SecID)' /></span> //删除警告按钮 删除 终止





