CREATE PROCEDURE P_AddMergeBuySignUp_Vxin
(
 BossId_Para VARCHAR(6),
 MerID_Para INT,
 OrderID_Para INT,
 PID_Para INT,
 CustomerID_Para INT,
 DealPrice_Para DECIMAL(8,2),
 IsLeader_Para INT,
 Satus_Para INT,
 OUT IsSuccess_Para INT 
)
top:BEGIN
	-- --------------------
	-- 新增拼团情况表   
	-- auhtor:yf 
	-- MODIFY:2017-12-7,新建
	-- --------------------
DECLARE err INT DEFAULT 0;

SET @BossId_Para = BossId_Para;

SET @MerID_Para = MerID_Para;

SET @OrderID_Para = OrderID_Para;

SET @PID_Para = PID_Para;

SET @CustomerID_Para = CustomerID_Para;

SET @DealPrice_Para = DealPrice_Para;

SET @IsLeader_Para = IsLeader_Para;

SET @Satus_Para = Satus_Para;

-- 保存订单信息
SET @Sql_Para = CONCAT(
	'INSERT INTO MergeBuySignUp(
  `BossID`,`MerID`,`OrderID`,`PID`,`CustomerID`,`DealPrice`,`IsLeader`,`Status`,`AddTime`,`UpDateTime`
	) VALUES(
  ? ,? ,? ,? ,? ,? ,? ,? ,NOW(),NOW()  
	)'
);
PREPARE SqlCmd
FROM
	@Sql_Para;

SET @Satus_Para = Satus_Para;
EXECUTE SqlCmd USING @BossId_Para ,@MerID_Para ,@OrderID_Para ,@PID_Para ,@CustomerID_Para ,@DealPrice_Para,@IsLeader_Para,@Satus_Para;

DEALLOCATE PREPARE SqlCmd;


IF err = 1 THEN
SET IsSuccess_Para=-20;
ELSE
SET IsSuccess_Para=5;
END IF;

END;



CREATE PROCEDURE P_GetMergeBuySignUpLastCount_Vxin
(
 BossID_Para VARCHAR(6),
 MerID_Para INT,
 ID_Para INT,
 OUT IsSuccess_Para INT 
)
top:BEGIN
	-- --------------------
	-- 获取拼团剩余人数   
	-- auhtor:yf 
	-- MODIFY:2017-12-11,新建
	-- --------------------
SET @BossId_Para = BossId_Para;

SET @MerID_Para = MerID_Para;

SET @ID_Para = ID_Para;

SET @IsSuccess_Para=0;

SELECT ID FROM SOP_MergeBuySignUp WHERE MerID = @MerID_Para AND BossID = @BossId_Para and IsLeader = 1 and fid=0 AND ID = @ID_Para;
IF FOUND_ROWS() = 0 THEN
	SET IsSuccess_Para=-7; -- 参数不正确,不存在相关数据
	LEAVE TOP;
END IF;

SET @MerPeopleNum = 0;
SELECT MerPeopleNum INTO @MerPeopleNum FROM SOP_MergeBuy  WHERE BossID =  @BossId_Para AND MerID =  @MerID_Para;
IF @MerPeopleNum<=0 THEN
	SET IsSuccess_Para=-6; -- 拼团数据有误;
	LEAVE TOP;
END IF;	

SET @LastCount = 0;
SELECT COUNT(ID) INTO @LastCount  FROM SOP_MergeBuySignUp WHERE MerID = @MerID_Para AND BossID = @BossId_Para AND fid=@ID_Para;

SET @ISSuccess_Para =  @MerPeopleNum - 1 - @LastCount;
SET IsSuccess_Para=@ISSuccess_Para;

END;



CREATE PROCEDURE P_GetMergeBuySignUpList_Vxin
(
 BossID_Para VARCHAR(6),
 MerID_Para INT,
 ID_Para INT
)
top:BEGIN
	-- --------------------
	-- 获取拼团活动拼单的列表 
	-- auhtor:yf 
	-- MODIFY:2017-12-15,新建
	-- --------------------
SET @BossId_Para = BossId_Para;

SET @MerID_Para = MerID_Para;

SET @ID_Para = ID_Para;


-- 保存订单信息
SET @Sql_Para = CONCAT('SELECT ID,Fid,BossID,MerID,OrderID,PID,CustomerID,DealPrice,IsLeader,Status,PayStatus,AddTime,UpDateTime FROM SOP_MergeBuySignUp 
WHERE fid=0 AND BossID = ? AND MerID = ? AND ID = ?
union all
SELECT ID,Fid,BossID,MerID,OrderID,PID,CustomerID,DealPrice,IsLeader,Status,PayStatus,AddTime,UpDateTime FROM SOP_MergeBuySignUp 
WHERE  BossID = ? AND MerID = ? AND fid=? 
'
);
PREPARE SqlCmd
FROM
	@Sql_Para;

EXECUTE SqlCmd USING @BossId_Para ,@MerID_Para ,@ID_Para ,@BossId_Para ,@MerID_Para ,@ID_Para;

DEALLOCATE PREPARE SqlCmd;

END;

CREATE PROCEDURE P_GetMergeBuySignUPListByMerID_MZ
(
	BossID_Para VARCHAR(6),
 	MerID_Para INT
)
top:BEGIN
	-- --------------------
	-- 获取拼团活动情况的列表 
	-- auhtor:yf 
	-- MODIFY:2017-12-15,新建
	-- CALL P_GetMergeBuySignUPListByMerID_MZ(900003,3);
	-- --------------------
DECLARE TabExCode_Para varchar(10);
DECLARE TableName_Para varchar(50);
SET @BossId_Para = BossId_Para;

SET @MerID_Para = MerID_Para;


-- 赋值
SET TabExCode_Para=GetTableExtension(BossID_Para,2);
-- 分表名称
SET TableName_Para = CONCAT('AMP_SaleOrders_',TabExCode_Para);

SET @sql_Para=CONCAT('select (case When merSginUP.Fid > 0 then merSginUP.Fid ELSE merSginUP.ID END)as ID,merSginUP.Fid,merSginUP.BossID,merSginUP.MerID,merSginUP.OrderID,merSginUP.PID,merSginUP.CustomerID,
merSginUP.DealPrice,merSginUP.IsLeader,merSginUP.Status,merSginUP.PayStatus,merSginUP.AddTime,merSginUP.UpDateTime,sOrder.OrderNumber,
sOrder.Addressee,sOrder.Tel,sOrder.Address,sOrder.OrderProgress
FROM SOP_MergeBuySignUp as merSginUP INNER JOIN ',TableName_Para,' as sOrder ON merSginUP.BossID = sOrder.BossID AND merSginUP.OrderID = sOrder.OrderID 
WHERE merSginUP.MerID = ? and merSginUP.BossID = ? ORDER BY ID ');
PREPARE SqlCmd FROM @Sql_Para;
EXECUTE SqlCmd USING @MerID_Para,@BossId_Para;
DEALLOCATE PREPARE SqlCmd;
END;