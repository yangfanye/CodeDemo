CREATE PROCEDURE P_AddPreSale_MZ
(
 BossId_Para VARCHAR (6),
 PreSaleName_Para VARCHAR (100),
 PosterURL_Para VARCHAR (200),
 PreSaleStartTime_Para datetime,
 PreSaleEndTime_Para datetime,
 ProductDes_Para VARCHAR (1000),
 PreSaleRule_Para VARCHAR (1000),
 Hotline_Para VARCHAR (1000),
 PreStoreList_Para VARCHAR (500),
 PreQRCode_Para VARCHAR (255),
 PreShortURL_Para VARCHAR (255),
 WxinQRCode_Para VARCHAR (255),
 OperatorID_Para INT,
 IPAddress_Para VARCHAR (20),
 OUT IsSuccess_Para INT,
 OUT PreID_Para INT 
)
BEGIN
	-- --------------------
	-- 新增预售活动信息   
	-- auhtor:yf 
	-- MODIFY:2017-10-31,新建
	-- --------------------
DECLARE err INT DEFAULT 0;

SET @BossId_Para = BossId_Para;

SET @PreSaleName_Para = PreSaleName_Para;

SET @PosterURL_Para = PosterURL_Para;

SET @PreSaleStartTime_Para = PreSaleStartTime_Para;

SET @PreSaleEndTime_Para = PreSaleEndTime_Para;

SET @ProductDes_Para = ProductDes_Para;

SET @PreSaleRule_Para = PreSaleRule_Para;

SET @Hotline_Para = Hotline_Para;

SET @PreStoreList_Para = PreStoreList_Para;

SET @PreQRCode_Para = PreQRCode_Para;

SET @PreShortURL_Para = PreShortURL_Para;

SET @WxinQRCode_Para = WxinQRCode_Para;

SET @OperatorID_Para = OperatorID_Para;

SET @IPAddress_Para = IPAddress_Para;

-- 保存订单信息
SET @Sql_Para = CONCAT(
	'INSERT INTO SOP_PreSale(
  `BossID`,`PreSaleName` ,`PosterURL` ,`PreSaleStartTime` ,`PreSaleEndTime` ,`ProductDes` ,`PreSaleRule` ,`Hotline` ,
	`Status`,`PreStoreList`,`PreQRCode`,	`PreShortURL`,`WxinQRCode`,`AddTime`,`UpdateTime`,`OperatorID`,`IPAddress`
	) VALUES(
  ? ,? ,? ,? ,? ,? ,? ,? ,
	0 ,? ,? ,? ,? ,NOW() ,NOW() ,? ,?  
	)'
);
PREPARE SqlCmd
FROM
	@Sql_Para;

EXECUTE SqlCmd USING @BossId_Para ,@PreSaleName_Para ,@PosterURL_Para ,@PreSaleStartTime_Para ,@PreSaleEndTime_Para ,@ProductDes_Para,@PreSaleRule_Para,@Hotline_Para,@PreStoreList_Para,@PreQRCode_Para,@PreShortURL_Para,@WxinQRCode_Para,@OperatorID_Para,@IPAddress_Para;

DEALLOCATE PREPARE SqlCmd;


IF err = 1 THEN

SET IsSuccess_Para =- 20;

-- 订单信息保存不了
SET PreID_Para = 0;

ELSE

SET IsSuccess_Para = 5;
-- 订单信息成功保存
SET PreID_Para = LAST_INSERT_ID();

END IF;


END;






CREATE PROCEDURE P_AddPreSaleDetail_MZ
(
 BossId_Para VARCHAR (6),
 PreID_Para INT,
 PID_Para int,
 PreSalePrice_Para DECIMAL(18,2),
 PreSaleNum_Para INT,
 PreSalePurchaseNum_Para INT,
 OUT IsSuccess_Para INT,
 OUT ID_Para INT
)
BEGIN
-- --------------------
-- 新增预售活动信息   
-- auhtor:yf 
-- MODIFY:2017-10-31,
-- Description:	P_AddPreSaleDetail_MZ
-- --------------------
DECLARE err INT DEFAULT 0;
SET IsSuccess_Para=5;
SET ID_Para = 0;

SET @BossId_Para = BossId_Para;
SET @PreID_Para = PreID_Para;
SET @PID_Para = PID_Para;
SET @PreSalePrice_Para = PreSalePrice_Para;
SET @PreSaleNum_Para = PreSaleNum_Para;
SET @PreSalePurchaseNum_Para = PreSalePurchaseNum_Para;


-- 保存订单信息
SET @Sql_Para = CONCAT(
	'INSERT INTO SOP_PreSaleDetail(
  `BossID`,`PreID` ,`PID` ,`PreSalePrice` ,`PreSaleNum` ,`PreSalePurchaseNum` ,`AddTime` ,`UpDateTime`
	) VALUES(
  ? ,? ,? ,? ,? ,? ,NOW() ,NOW()
	)'
);
PREPARE SqlCmd
FROM
	@Sql_Para;

EXECUTE SqlCmd USING @BossId_Para ,@PreID_Para ,@PID_Para ,@PreSalePrice_Para ,@PreSaleNum_Para,@PreSalePurchaseNum_Para;

DEALLOCATE PREPARE SqlCmd;


IF err=1 THEN
		SET IsSuccess_Para=-20; -- 订单信息保存不了
		SET ID_Para = 0;
	ELSE
		SET IsSuccess_Para=5;
	  SET ID_Para = LAST_INSERT_ID();
	END IF;

END;




CREATE PROCEDURE P_SubmitEditPreSale_MZ
(
 PreID_Para INT,
 BossId_Para VARCHAR (6),
 PreSaleName_Para VARCHAR (100),
 PosterURL_Para VARCHAR (200),
 PreSaleStartTime_Para datetime,
 PreSaleEndTime_Para datetime,
 ProductDes_Para VARCHAR (1000),
 PreSaleRule_Para VARCHAR (1000),
 Hotline_Para VARCHAR (1000),
 PreStoreList_Para VARCHAR (500),
 PreQRCode_Para VARCHAR (255),
 PreShortURL_Para VARCHAR (255),
 WxinQRCode_Para VARCHAR (255),
 OperatorID_Para INT,
 IPAddress_Para VARCHAR (20),
 OUT IsSuccess_Para INT
)
top:BEGIN
-- =============================================
-- Author:yf
-- Create date: 2017-10-31
-- Description:	更新预售活动记录 P_SubmitEditPreSale_MZ
-- =============================================
-- 声明变量
DECLARE TableName_Para varchar(50);

-- 表名称
SET TableName_Para = 'SOP_PreSale';

set @count_para=1;
  set @strSQL=concat('SELECT count(1) INTO @count_para FROM ',TableName_Para,' WHERE PreID=''',PreID_Para,''' AND BossID=''',BossID_Para,''' AND status=0 ');

  prepare SqlCmd from @strSQL;
  execute SqlCmd; 
  DEALLOCATE PREPARE SqlCmd;
 
if @count_para=0 then
    set IsSuccess_Para=-6;  -- 该活动不存在
    leave top;
end if;

if @count_para=1 then

set @strSQL=concat('update ',TableName_Para,' set PreSaleName=''',PreSaleName_Para,''',PosterURL=''',PosterURL_Para,''',PreSaleStartTime=''',PreSaleStartTime_Para,
''',PreSaleEndTime=''',PreSaleEndTime_Para,''',ProductDes=''',ProductDes_Para,''',PreSaleRule=''',PreSaleRule_Para,''',Hotline=''',Hotline_Para,
''',PreStoreList=''',PreStoreList_Para,''',PreQRCode=''',PreQRCode_Para,''',PreShortURL=''',PreShortURL_Para,''',WxinQRCode=''',WxinQRCode_Para,
''',UpdateTime=Now(),OperatorID=''',OperatorID_Para,''',IPAddress=''',IPAddress_Para,''' WHERE  PreID=''',PreID_Para,''' AND BossID=''',BossID_Para,''' ');

prepare SqlCmd from @strSQL;
execute SqlCmd; 
DEALLOCATE PREPARE SqlCmd;
  
end if;

set IsSuccess_Para=5;  -- 编辑成功

END;




CREATE PROCEDURE P_SubmitEditPreSaleDetail_MZ
(
 ID_Para INT,
 BossId_Para VARCHAR (6),
 PreID_Para INT,
 PID_Para int,
 PreSalePrice_Para DECIMAL(18,2),
 PreSaleNum_Para INT,
 PreSalePurchaseNum_Para INT,
 OUT IsSuccess_Para INT
)
top:begin
-- =============================================
-- Author:yf
-- Create date: 2017-10-31
-- Description:	更新预售活动明细记录
-- =============================================
-- 声明变量
DECLARE TableName_Para varchar(50);

-- 表名称
SET TableName_Para = 'SOP_PreSaleDetail';

set @count_para=1;
  set @strSQL=concat('SELECT count(1) INTO @count_para FROM ',TableName_Para,' WHERE PreID=''',PreID_Para,''' AND BossID=''',BossID_Para,''' AND ID=',ID_Para);

  prepare SqlCmd from @strSQL;
  execute SqlCmd; 
  DEALLOCATE PREPARE SqlCmd;
 
if @count_para=0 then
    set IsSuccess_Para=-6;  -- 该活动不存在
    leave top;
end if;

if @count_para=1 then

set @strSQL=concat('update ',TableName_Para,' set PreSalePrice=''',PreSalePrice_Para,''',PreSaleNum=''',PreSaleNum_Para,''',PreSalePurchaseNum=''',PreSalePurchaseNum_Para,
''',UpdateTime=Now() WHERE PreID=''',PreID_Para,''' AND BossID=''',BossID_Para,''' AND ID=',ID_Para);

 prepare SqlCmd from @strSQL;
 execute SqlCmd; 
 DEALLOCATE PREPARE SqlCmd;
  
end if;

set IsSuccess_Para=5;  -- 编辑成功

end;




CREATE PROCEDURE P_GetPreSaleByPage
(
 BossID_Para varchar(20),
 PageIndex_Para INT,
 PageSize_Para INT,
 Order_Para VARCHAR(100),
 Where_Para VARCHAR(2000),
 OUT TotalCount_Para INT
)
top:BEGIN
-- ----------------------------------
-- AUTHORS:yf
-- CREATE Time:2017-10-31
-- DES:预售分页获取活动数据
-- ---------------------------------

		-- 获取总数量
		SET @Sql_Para=CONCAT('SELECT COUNT(0) INTO @TotalCount_Para FROM SOP_PreSale as aTab WHERE ',Where_Para) ;
		PREPARE SqlCmd FROM @Sql_Para;
		EXECUTE SqlCmd;
		DEALLOCATE PREPARE SqlCmd;

		SET TotalCount_Para=@TotalCount_Para;
		-- 分页获取数据
		SET @Sql_Para=CONCAT('SELECT PreID,BossID,PreSaleName,PosterURL,PreSaleStartTime,PreSaleEndTime,ProductDes,PreSaleRule,Hotline,Status,PreStoreList,PreQRCode,PreShortURL,WxinQRCode,AddTime,UpdateTime,OperatorID,IPAddress FROM SOP_PreSale AS aTab WHERE ',Where_Para,' ORDER BY ',Order_Para,' LIMIT ',PageIndex_Para*PageSize_Para-PageSize_Para,',',PageSize_Para);

		PREPARE SqlCmd FROM @Sql_Para;
		EXECUTE SqlCmd;
		DEALLOCATE PREPARE SqlCmd;

END





CREATE PROCEDURE P_GetPreSaleByBarID
(
 PreID_Para INT,
 BossID_para Varchar(20)
)
top:BEGIN
-- ----------------------------------
-- AUTHORS:yf
-- CREATE Time:2017-10-31
-- DES:获取单个的活动记录
-- P_GetPreSaleByBarID
-- CALL P_GetPreSaleByBarID(3,'900003');
-- --------------------------------

		-- 获取数据
		SET @Sql_Para=CONCAT('SELECT  PreID,BossID,PreSaleName,PosterURL,PreSaleStartTime,PreSaleEndTime,ProductDes,PreSaleRule,Hotline,Status,PreStoreList,PreQRCode,PreShortURL,WxinQRCode,AddTime,UpdateTime,OperatorID,IPAddress FROM SOP_PreSale WHERE PreID = ',PreID_Para,' AND BossID = \'',BossID_para,'\'');

		PREPARE SqlCmd FROM @Sql_Para;
		EXECUTE SqlCmd;
		DEALLOCATE PREPARE SqlCmd;

END




CREATE PROCEDURE P_GetPreSaleList
(
 StrWhere_Para Varchar(200),
 BossID_para Varchar(20)
)
top:BEGIN
-- ----------------------------------
-- AUTHORS:yf
-- CREATE Time:2017-10-31
-- DES:获取活动记录
-- P_GetSecKillList
-- --------------------------------

		-- 获取数据
		SET @Sql_Para=CONCAT('SELECT PreID,BossID,PreSaleName,PosterURL,PreSaleStartTime,PreSaleEndTime,ProductDes,PreSaleRule,Hotline,Status,PreStoreList,PreQRCode,PreShortURL,WxinQRCode,AddTime,UpdateTime,OperatorID,IPAddress FROM SOP_PreSale WHERE BossID = \'',BossID_para,'\'',StrWhere_Para);

		PREPARE SqlCmd FROM @Sql_Para;
		EXECUTE SqlCmd;
		DEALLOCATE PREPARE SqlCmd;

END


CREATE PROCEDURE P_GetPreSaleDetailInfosByPreID
(
 PreID_Para INT,
 BossID_Para Varchar(20)
)
top:BEGIN
-- ----------------------------------
-- AUTHORS:yf
-- CREATE Time:2015-08-13
-- DES:获取单个的活动记录
-- P_GetSecKillDetailInfosByBarID
-- CALL P_GetSecKillDetailInfosByBarID
-- --------------------------------
		DECLARE TabExCode_Para varchar(10);
    DECLARE TableNum_Para int;
    DECLARE TableName_Para varchar(50);
    DECLARE DataBaseName varchar(30);

    -- 赋值
		SET TabExCode_Para=GetTableExtension(BossID_Para,2);
		-- 分表名称
  	SET TableName_Para = CONCAT('AMP_Products_',TabExCode_Para);

		SET @Sql_Para=CONCAT('SELECT b.ProductName,b.Standard,b.UnitPrice,b.ListPrice,b.Amount,a.ID,a.BossID,a.PreID,a.PID,a.PreSalePrice,a.PreSaleNum,a.PreSalePurchaseNum,a.AddTime,a.UpDateTime FROM SOP_PreSaleDetail AS a LEFT JOIN ',TableName_Para,' AS b ON a.BossID = b.BossID AND a.PID = b.PID WHERE a.PreID = ',PreID_Para,' AND a.BossID = \'',BossID_Para,'\'');

		PREPARE SqlCmd FROM @Sql_Para;
		EXECUTE SqlCmd;
		DEALLOCATE PREPARE SqlCmd;

END



CREATE PROCEDURE P_GetSaleSumBuyNum_MZ
(
 BossId_Para VARCHAR (6),
 PID_Para int,
 SalePrice_Para DECIMAL(18,2),
 StartDate_Para datetime,
 EndTime_Para datetime,
 OUT buyNum_Para INT
)
top:begin
-- =============================================
-- Author:yf
-- Create date: 2017-10-31
-- Description:	获取销售订单购买汇总数据
-- =============================================
-- 声明变量
DECLARE TabExCode2_Para VARCHAR(10);
DECLARE TableName_Para varchar(50);

SET TabExCode2_Para = GetTableExtension(BossID_Para,2);
SET @result_Para = 0;
set buyNum_Para =-5;

set @strSQL=concat('SELECT SUM(s2.BuyNum) INTO @result_Para FROM AMP_SaleOrders_',TabExCode2_Para,' AS s1 INNER JOIN AMP_SaleOrderDetail_',TabExCode2_Para,' AS s2 ON s1.OrderID = s2.OrderID
  WHERE s1.OrderProgress > 2000 AND s2.BossID = ''',BossId_Para,''' AND s2.PID = ',PID_Para,' AND s2.ListPrice = ',SalePrice_Para,'
AND s2.AddTime >=''',StartDate_Para,''' AND s2.AddTime <= ''',EndTime_Para,'''');

 prepare SqlCmd from @strSQL;
 execute SqlCmd; 
 DEALLOCATE PREPARE SqlCmd;
  


set buyNum_Para=@result_Para;  -- 编辑成功

end


CREATE PROCEDURE P_GetSaleOrderInfo_MZ
(
 BossId_Para VARCHAR (6),
 PID_Para int,
 SalePrice_Para DECIMAL(18,2),
 StartDate_Para datetime,
 EndTime_Para datetime,
 OrderTypeCode_Para int,
 IsSuccess_Para int
)
top:begin
-- =============================================
-- Author:yf
-- Create date: 2017-10-31
-- Description:	获取订单汇总数据
-- =============================================
-- 声明变量
DECLARE TabExCode2_Para VARCHAR(10);
DECLARE TableName_Para varchar(50);

SET TabExCode2_Para = GetTableExtension(BossID_Para,2);
set IsSuccess_Para= -5;

set @strSQL=concat('SELECT s1.OrderNumber s1.Addressee,s1.Tel,s1.Address FROM AMP_SaleOrders_',TabExCode2_Para,' AS s1 INNER JOIN AMP_SaleOrderDetail_',TabExCode2_Para,' AS s2 ON s1.OrderID = s2.OrderID
  WHERE s1.OrderProgress > 2000 AND s2.BossID = ''',BossId_Para,''' AND s2.PID = ',PID_Para,' AND s2.ListPrice = ',SalePrice_Para,'
AND s2.AddTime >=''',StartDate_Para,''' AND s2.AddTime <= ''',EndTime_Para,''' AND s1.OrderTypeCode = ''',OrderTypeCode_Para ,'''');

 prepare SqlCmd from @strSQL;
 execute SqlCmd; 
 DEALLOCATE PREPARE SqlCmd;
  


set IsSuccess_Para= 5;  -- 编辑成功

end


-- 订单信息增加新的字段
DROP PROCEDURE IF EXISTS useCursor;  -- 如果存在这个过程就删除
CREATE PROCEDURE useCursor()  -- 新建过程
  BEGIN  -- 开始
    DECLARE oneAddr varchar(40) default ''; -- 单独一个表
    DECLARE fsum DECIMAL(20,4) DEFAULT 0; #合计的变量,所有变量一定要定义一个初始值,否则在计算中null会导致整个结果是null
      DECLARE v_cut DECIMAL(20,4);
    -- 遍历数据结束标志
    DECLARE done INT DEFAULT FALSE; 
    DECLARE curl CURSOR FOR select table_name from information_schema.tables where table_schema='CosmeticRetail_SOP' and table_name like 'AMP_SaleOrders\___'; -- 定义游标 注意%是字符串,"_"是单个字符 "\"是转义字符
    -- 将结束标志绑定到游标
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
     OPEN curl; -- 开启游标
    -- 开始循环
    read_loop: LOOP
      FETCH curl INTO oneAddr; -- 循环游标,并赋值给oneAddr

         -- 声明结束的时候
      IF done THEN 
       LEAVE read_loop; -- 结束循环
      END IF;
         SET @cut = v_cut; -- 由于直接定义的变量无法存储较大的实数;因此在这里做了一个转换
         SET @cut = 0; -- 临时变量,每次循环都清零;
         SET @sql = CONCAT('alter table ',oneAddr,' add OrderTypeCode int DEFAULT 0;'); 
         PREPARE stmt FROM @sql; 
         EXECUTE stmt;
         SET fsum = fsum + IFNULL(@cut,0); -- 要考虑每次变量的空值问题,一定要加上ifnull.
    END LOOP;
    CLOSE curl; 
    select fsum; 
  END;
call useCursor();




CREATE PROCEDURE P_GetSaleSumBuyNumByCustomerID_Vxin
(
 BossId_Para VARCHAR (6),
 PID_Para int,
 SalePrice_Para DECIMAL(18,2),
 StartDate_Para datetime,
 EndTime_Para datetime,
 OrderTypeCode_Para int,
 CustomerID_Para int,
 OUT buyNum_Para INT
)
top:begin
-- =============================================
-- Author:yf
-- Create date: 2017-10-31
-- Description:	获取销售订单购买汇总数据
-- =============================================
-- 声明变量
DECLARE TabExCode2_Para VARCHAR(10);
DECLARE TableName_Para varchar(50);

SET TabExCode2_Para = GetTableExtension(BossID_Para,2);
SET @result_Para = 0;
set buyNum_Para =-5;

set @strSQL=concat('SELECT SUM(s2.BuyNum) INTO @result_Para FROM AMP_SaleOrders_',TabExCode2_Para,' AS s1 INNER JOIN AMP_SaleOrderDetail_',TabExCode2_Para,' AS s2 ON s1.OrderID = s2.OrderID
  WHERE s1.OrderProgress > 2000 AND s2.BossID = ''',BossId_Para,''' AND s2.PID = ',PID_Para,' AND s2.ListPrice = ',SalePrice_Para,'
AND s2.AddTime >=''',StartDate_Para,''' AND s2.AddTime <= ''',EndTime_Para,''' AND s1.OrderTypeCode = ''',OrderTypeCode_Para ,''' AND s1.CustomerID = ''',CustomerID_Para,'''');

 prepare SqlCmd from @strSQL;
 execute SqlCmd; 
 DEALLOCATE PREPARE SqlCmd;

set buyNum_Para=@result_Para;  -- 编辑成功

end