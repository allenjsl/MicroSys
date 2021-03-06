-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-21
-- Description:	采购商、供应商、平台用户信息添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_YongHu_CU]
	@YongHuId CHAR(36)
	,@LeiXing INT
	,@GongSiId CHAR(36)
	,@Username NVARCHAR(255)
	,@PasswordMD5 NVARCHAR(255)
	,@JueSeId CHAR(36)
	,@Status INT
	,@BuMenName NVARCHAR(255)
	,@Name NVARCHAR(255)
	,@ZhaoPianFilepath NVARCHAR(255)
	,@ZhiWu NVARCHAR(255)
	,@XingBie INT
	,@ChuShengRiQi DATETIME
	,@ShouJi NVARCHAR(255)
	,@DianHua NVARCHAR(255)
	,@Fax NVARCHAR(255)
	,@Email NVARCHAR(255)
	,@DiZhi NVARCHAR(255)
	,@RuZhiRiQi DATETIME
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@Privs NVARCHAR(MAX)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	SET @FS='C'
	SET @RetCode=0	
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE Username=@Username AND YongHuId<>@YongHuId /*AND LeiXing=@LeiXing*/)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE YongHuId=@YongHuId)
	BEGIN
		SET @FS='U'		
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_YongHu]([YongHuId],[LeiXing],[GongSiId]
			,[Username],[PasswordMd5],[JueSeId]
			,[Status],[BuMenName],[Name]
			,[ZhaoPianFilepath],[ZhiWu],[XingBie]
			,[ChuShengRiQi],[ShouJi],[DianHua]
			,[Fax],[Email],[DiZhi]
			,[RuZhiRiQi],[CaoZuoRenId],[IssueTime]
			,[Privs],[IsDelete])
		VALUES(@YongHuId,@LeiXing,@GongSiId
			,@Username,@PasswordMd5,@JueSeId
			,@Status,@BuMenName,@Name
			,@ZhaoPianFilepath,@ZhiWu,@XingBie
			,@ChuShengRiQi,@ShouJi,@DianHua
			,@Fax,@Email,@DiZhi
			,@RuZhiRiQi,@CaoZuoRenId,@IssueTime
			,@Privs,'0')
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_YongHu] SET [JueSeId]=@JueSeId/*,[Status]=@Status*/
			,[BuMenName]=@BuMenName,[Name]=@Name
			,[ZhaoPianFilepath]=@ZhaoPianFilepath,[ZhiWu]=@ZhiWu,[XingBie]=@XingBie
			,[ChuShengRiQi]=@ChuShengRiQi,[ShouJi]=@ShouJi,[DianHua]=@DianHua
			,[Fax]=@Fax,[Email]=@Email,[DiZhi]=@DiZhi
			,[RuZhiRiQi]=@RuZhiRiQi--,[Privs]=@Privs
			,GongSiId=@GongSiId
		WHERE [YongHuId]=@YongHuId
	END	
	
	IF(@FS='U' AND @PasswordMD5 IS NOT NULL AND LEN(@PasswordMD5)>0)
	BEGIN
		UPDATE [tbl_YongHu] SET [PasswordMD5]=@PasswordMD5
		WHERE [YongHuId]=@YongHuId
	END
	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

GO
CREATE TABLE [dbo].[tbl_DiZhi](
	[DiZhiId] [char](36) NOT NULL,
	[GongSiId] [char](36) NOT NULL,
	[YongHuId] [char](36) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[DiZhi] [nvarchar](255) NULL,
	[ShouJi] [nvarchar](255) NULL,
	[DianHua] [nvarchar](255) NULL,
	[CaoZuoRenId] [char](36) NOT NULL,
	[IssueTime] [datetime] NOT NULL,
	[IsMoRen] [char](1) NOT NULL,
	[IsDelete] [char](1) NOT NULL,
 CONSTRAINT [PK_TBL_DIZHI] PRIMARY KEY CLUSTERED 
(
	[DiZhiId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'DiZhiId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'GongSiId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'YongHuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'DiZhi'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'ShouJi'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'DianHua'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'CaoZuoRenId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'IssueTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否默认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'IsMoRen'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货（发货）地址信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DiZhi'
GO
/****** Object:  Default [DF__tbl_DiZhi__Issue__2D7CBDC4]    Script Date: 05/29/2015 14:44:07 ******/
ALTER TABLE [dbo].[tbl_DiZhi] ADD  CONSTRAINT [DF__tbl_DiZhi__Issue__2D7CBDC4]  DEFAULT (getdate()) FOR [IssueTime]
GO
/****** Object:  Default [DF__tbl_DiZhi__IsMoR__2E70E1FD]    Script Date: 05/29/2015 14:44:07 ******/
ALTER TABLE [dbo].[tbl_DiZhi] ADD  CONSTRAINT [DF__tbl_DiZhi__IsMoR__2E70E1FD]  DEFAULT ('0') FOR [IsMoRen]
GO
/****** Object:  Default [DF__tbl_DiZhi__IsDel__2F650636]    Script Date: 05/29/2015 14:44:07 ******/
ALTER TABLE [dbo].[tbl_DiZhi] ADD  CONSTRAINT [DF__tbl_DiZhi__IsDel__2F650636]  DEFAULT ('0') FOR [IsDelete]
GO

GO
CREATE TABLE [dbo].[tbl_GongSiGuanXi](
	[IdentityId] [int] IDENTITY(1,1) NOT NULL,
	[GongSiId1] [char](36) NOT NULL,
	[GongSiId2] [char](36) NOT NULL,
	[CaoZuoRenId] [char](36) NOT NULL,
	[IssueTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TBL_GONGSIGUANXI] PRIMARY KEY CLUSTERED 
(
	[IdentityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GongSiGuanXi', @level2type=N'COLUMN',@level2name=N'IdentityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司编号1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GongSiGuanXi', @level2type=N'COLUMN',@level2name=N'GongSiId1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司编号2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GongSiGuanXi', @level2type=N'COLUMN',@level2name=N'GongSiId2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GongSiGuanXi', @level2type=N'COLUMN',@level2name=N'CaoZuoRenId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GongSiGuanXi', @level2type=N'COLUMN',@level2name=N'IssueTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司关系' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_GongSiGuanXi'
GO
/****** Object:  Default [DF__tbl_GongS__Issue__324172E1]    Script Date: 05/29/2015 17:06:24 ******/
ALTER TABLE [dbo].[tbl_GongSiGuanXi] ADD  CONSTRAINT [DF__tbl_GongS__Issue__324172E1]  DEFAULT (getdate()) FOR [IssueTime]
GO


ALTER TABLE dbo.tbl_DingDanChanPin ADD
	ChanPinJiaGe1 money NOT NULL CONSTRAINT DF_tbl_DingDanChanPin_ChanPinJiaGe1 DEFAULT 0
GO
DECLARE @v sql_variant 
SET @v = N'产品单价（上次）'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_DingDanChanPin', N'COLUMN', N'ChanPinJiaGe1'
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-24
-- Description:	设置订单报价信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_DingDan_SheZhiBaoJia]
	@DingDanId CHAR(36)
	,@ChanPinXml NVARCHAR(MAX)
	,@RetCode INT OUTPUT
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
AS
BEGIN
	DECLARE @hdoc INT
	DECLARE @JinE MONEY
	DECLARE @GysId CHAR(36)
	DECLARE @CgsId CHAR(36)
	DECLARE @CgdId CHAR(36)
	DECLARE @JiShuQi INT
	DECLARE @ChanPinId CHAR(36)
	DECLARE @ChanPinJiaGe1 MONEY
	DECLARE @TEMP_TABLE TABLE(MingXiId CHAR(36),ShuLiang MONEY,ChanPinJiaGe MONEY,JinE MONEY,GysBaoJiaShuoMing NVARCHAR(MAX),ChanPinId CHAR(36),ChanPinJiaGe1 MONEY,IdentityId INT IDENTITY)
	
	SET @RetCode=0
	
	SELECT @CgdId=CaiGouDanId,@GysId=GysId FROM tbl_DingDan WHERE DingDanId=@DingDanId
	SELECT @CgsId=CgsId  FROM tbl_CaiGouDan WHERE CaiGouDanId=@CgdId

	IF(@ChanPinXml IS NOT NULl AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		INSERT INTO @TEMP_TABLE(MingXiId,ShuLiang,ChanPinJiaGe,JinE,GysBaoJiaShuoMing)
		SELECT MingXiId,ShuLiang,ChanPinJiaGe,JinE,GysBaoJiaShuoMing
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH(MingXiId CHAR(36),ShuLiang MONEY,ChanPinJiaGe MONEY,JinE MONEY,GysBaoJiaShuoMing NVARCHAR(MAX))
		EXEC sp_xml_removedocument @hdoc	
	END
	
	UPDATE @TEMP_TABLE SET ChanPinId=B.ChanPinId
	FROM @TEMP_TABLE AS A INNER JOIN tbl_DingDanChanPin AS B
	ON A.MingXiId=B.MingXiId
	
	SELECT @JiShuQi=COUNT(*) FROM @TEMP_TABLE
	DECLARE @i INT
	SET @i=1
	WHILE(@i<=@JiShuQi)
	BEGIN
		SELECT @ChanPinId=ChanPinId FROM @TEMP_TABLE WHERE IdentityId=@i
		SET @ChanPinJiaGe1=0
		
		SELECT TOP 1 @ChanPinJiaGe1=ChanPinJiaGe FROM tbl_DingDanChanPinJiaGe WHERE GysId=@GysId AND CgsId=@CgsId AND ChanPinId=@ChanPinId ORDER BY IdentityId DESC
	
		SET @ChanPinJiaGe1=ISNULL(@ChanPinJiaGe1,0)		
		
		UPDATE @TEMP_TABLE SET ChanPinJiaGe1=@ChanPinJiaGe1 WHERE IdentityId=@i
		
		SET @i=@i+1
	END
	
	UPDATE tbl_DingDanChanPin SET ShuLiang=B.ShuLiang,ChanPinJiaGe=B.ChanPinJiaGe
		,JinE=B.JinE,GysBaoJiaShuoMing=B.GysBaoJiaShuoMing
		,ChanPinJiaGe1=B.ChanPinJiaGe1
	FROM tbl_DingDanChanPin AS A INNER JOIN @TEMP_TABLE AS B
	ON A.MingXiId=B.MingXiId
	
	INSERT INTO [tbl_DingDanChanPinJiaGe]([MingXiId],[GysId],[CgsId]
		,[ChanPinId],[ChanPinJiaGe],[ChanPinJiaGe1]
		,[IssueTime],[CaoZuoRenId])
	SELECT MingXiId,@GysId,@CgsId
		,ChanPinId,ChanPinJiaGe,ChanPinJiaGe1
		,@IssueTime,@CaoZuoRenId
	FROM @TEMP_TABLE
	
	/*IF(@ChanPinXml IS NOT NULl AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		UPDATE tbl_DingDanChanPin SET ShuLiang=B.ShuLiang,ChanPinJiaGe=B.ChanPinJiaGe
			,JinE=B.JinE,GysBaoJiaShuoMing=B.GysBaoJiaShuoMing
		FROM tbl_DingDanChanPin AS A INNER JOIN(
			SELECT * 
			FROM OPENXML(@hdoc,'/root/info',3)
			WITH(MingXiId CHAR(36),ShuLiang MONEY,ChanPinJiaGe MONEY,JinE MONEY,GysBaoJiaShuoMing NVARCHAR(MAX))
		)B ON A.MingXiId=B.MingXiId
		WHERE A.DingDanId=@DingDanId
		EXEC sp_xml_removedocument @hdoc
	END*/
	
	SELECT @JinE=ISNULL(SUM(JinE),0) FROM tbl_DingDanChanPin WHERE DingDanId=@DingDanId
	
	UPDATE tbl_DingDan SET JinE=@JinE WHERE DingDanId=@DingDanId
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

ALTER TABLE dbo.tbl_CaiGouDan ADD
	ShouHuoDiZhiId char(36) NULL
GO
DECLARE @v sql_variant 
SET @v = N'收获地址编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CaiGouDan', N'COLUMN', N'ShouHuoDiZhiId'
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-23
-- Description:	采购单添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_CaiGouDan_CU]
	@CaiGouDanId CHAR(36)
	,@CgsId CHAR(36)
	,@CaiGouDanName NVARCHAR(255)
	,@MoBanId CHAR(36)
	,@Status INT
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@ShouHuoDiZhi NVARCHAR(255)
	,@ShouHuoRenName NVARCHAR(255)
	,@ShouHuoRenDianHua NVARCHAR(255)
	,@CaiGouBuMen NVARCHAR(255)
	,@DingDanXml NVARCHAR(MAX)
	,@ChanPinXml NVARCHAR(MAX)
	,@CaiGouDanShuoMing NVARCHAR(MAX)
	,@YaoQiuDaoHuoTime DATETIME
	,@ShouHuoDiZhiId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	DECLARE @hdoc INT
	DECLARE @YuanStatus INT
	SET @FS='C'
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_CaiGouDan WHERE CaiGouDanId=@CaiGouDanId AND CgsId=@CgsId)
	BEGIN
		SET @FS='U'
		SELECT @YuanStatus=[Status] FROM tbl_CaiGouDan WHERE CaiGouDanId=@CaiGouDanId
	END
	
	IF(@YuanStatus=1)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_CaiGouDan]([CaiGouDanId],[CgsId],[CaiGouDanHao]
			,[CaiGouDanName],[MoBanId],[Status]
			,[CaoZuoRenId],[IssueTime],[IsDelete]
			,[ShouHuoDiZhi],[ShouHuoRenName],[ShouHuoRenDianHua]
			,[CaiGouBuMen],[FaBuRenId],[FaBuTime]
			,[CaiGouDanShuoMing],[YaoQiuDaoHuoTime],[ShouHuoDiZhiId])
		VALUES(@CaiGouDanId,@CgsId,''
			,@CaiGouDanName,@MoBanId,@Status
			,@CaoZuoRenId,@IssueTime,'0'
			,@ShouHuoDiZhi,@ShouHuoRenName,@ShouHuoRenDianHua
			,@CaiGouBuMen,'',NULL
			,@CaiGouDanShuoMing,@YaoQiuDaoHuoTime,@ShouHuoDiZhiId)
		
		--BM:'CGD'+CONVERT(NVARCHAR(8),@IssueTime,112)+dbo.fn_PadLeft(IdentityId,'0',5)
		--BM:'CGD'+dbo.fn_PadLeft(IdentityId,'0',5)
		UPDATE [tbl_CaiGouDan] SET [CaiGouDanHao]='CGD'+CONVERT(NVARCHAR(8),@IssueTime,112)+dbo.fn_PadLeft(IdentityId,'0',5) WHERE [CaiGouDanId]=@CaiGouDanId
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_CaiGouDan] SET [CaiGouDanName]=@CaiGouDanName,[MoBanId]=@MoBanId
			,[ShouHuoDiZhi]=@ShouHuoDiZhi,[ShouHuoRenName]=@ShouHuoRenName
			,[ShouHuoRenDianHua]=@ShouHuoRenDianHua,[CaiGouBuMen]=@CaiGouBuMen
			,[CaiGouDanShuoMing]=@CaiGouDanShuoMing,[YaoQiuDaoHuoTime]=@YaoQiuDaoHuoTime
			,[ShouHuoDiZhiId]=@ShouHuoDiZhiId
		WHERE CaiGouDanId=@CaiGouDanId
	END
	
	DELETE FROM tbl_DingDanChanPin WHERE CaiGouDanId=@CaiGouDanId
	DELETE FROM tbl_DingDan WHERE CaiGouDanId=@CaiGouDanId
	
	IF(@DingDanXml IS NOT NULL AND LEN(@DingDanXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@DingDanXml
		INSERT INTO [tbl_DingDan]([DingDanId],[CaiGouDanId],[GysId]
			,[GysName],[Status],[JinE]
			,[SongHuoRenName],[SongHuoRenDianHua],[SongHuoTime]
			,[DaoHuoTime],[GysBaoJiaRenId],[GysBaoJiaTime]
			,[CgsQueRenRenId],[CgsQueRenTime],[GysFaHuoRenId]
			,[GysFaHuoTime],[CgsShouHuoRenId],[CgsShouHuoTime]
			,[GysDaoHuoQueRenStatus],[GysDaoHuoQueRenRenId],[GysDaoHuoQueRenTime]
			,[QuXiaoRenId],[QuXiaoTime],[GysFaHuoShuoMing])
		SELECT A.[DingDanId],@CaiGouDanId,A.[GysId]
			,B.Name,A.[Status],A.[JinE]
			,'','',NULL
			,NULL,'',NULL
			,'',NULL,''
			,NULL,'',NULL
			,0,'',NULL
			,'',NULL,''
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([DingDanId] CHAR(36),[GysId] CHAR(36),[Status] INT,[JinE] MONEY) AS A
		INNER JOIN tbl_GongSi AS B ON A.GysId=B.GongSiId
		EXEC sp_xml_removedocument @hdoc
	END
	
	IF(@ChanPinXml IS NOT NULL AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		INSERT INTO [tbl_DingDanChanPin]([MingXiId],[CaiGouDanId],[DingDanId]
			,[ChanPinId],[ChanPinName],[ChanPinGuiGe]
			,[JiLiangDanWei],[ShuLiang],[ChanPinJiaGe]
			,[JinE],[FaHuoShuLiang],[DaoHuoShuLiang]
			,[CgsDaoHuoShuoMing],[GysBaoJiaShuoMing])
		SELECT A.[MingXiId],@CaiGouDanId,A.[DingDanId]
			,A.[ChanPinId],B.Name,B.GuiGe
			,B.JiLiangDanWei,A.[ShuLiang],B.JiaGe2
			,0,0,0
			,'',''
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([MingXiId] CHAR(36),[DingDanId] CHAR(36),[ChanPinId] CHAR(36),[ShuLiang] MONEY) AS A
		INNER JOIN tbl_ChanPin AS B ON A.[ChanPinId]=B.ChanPinId
		EXEC sp_xml_removedocument @hdoc
	END
	
	UPDATE [tbl_DingDanChanPin] SET [JinE]=[ChanPinJiaGe]*[ShuLiang] WHERE [CaiGouDanId]=@CaiGouDanId
	UPDATE tbl_DingDan SET JinE=ISNULL((SELECT SUM(A1.JinE) FROM [tbl_DingDanChanPin] AS A1 WHERE A1.DingDanId=tbl_DingDan.DingDanId),0) WHERE [CaiGouDanId]=@CaiGouDanId	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

ALTER TABLE dbo.tbl_DingDan ADD
	GysSongHuoRenId char(36) NULL
GO
DECLARE @v sql_variant 
SET @v = N'供应商送货人（地址）编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_DingDan', N'COLUMN', N'GysSongHuoRenId'
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-24
-- Description:	设置订单发货信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_DingDan_SheZhiFaHuo]
	@DingDanId CHAR(36)
	,@SongHuoRenName NVARCHAR(36)
	,@SongHuoRenDianHua NVARCHAR(36)
	,@SongHuoTime DATETIME
	,@ChanPinXml NVARCHAR(MAX)
	,@GysFaHuoShuoMing NVARCHAR(MAX)
	,@YuJiDaoHuoTime DATETIME
	,@GysSongHuoRenId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @hdoc INT
	SET @RetCode=0
	
	UPDATE tbl_DingDan SET SongHuoRenName=@SongHuoRenName,SongHuoRenDianHua=@SongHuoRenDianHua
		,SongHuoTime=@SongHuoTime,GysFaHuoShuoMing=@GysFaHuoShuoMing
		,YuJiDaoHuoTime=@YuJiDaoHuoTime,GysSongHuoRenId=@GysSongHuoRenId
	WHERE DingDanId=@DingDanId
	
	IF(@ChanPinXml IS NOT NULl AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		UPDATE tbl_DingDanChanPin SET FaHuoShuLiang=B.FaHuoShuLiang
		FROM tbl_DingDanChanPin AS A INNER JOIN(
			SELECT * 
			FROM OPENXML(@hdoc,'/root/info',3)
			WITH(MingXiId CHAR(36),FaHuoShuLiang MONEY)
		)B ON A.MingXiId=B.MingXiId
		WHERE A.DingDanId=@DingDanId
		EXEC sp_xml_removedocument @hdoc
	END
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-03-20
-- Description:	采购订单
-- =============================================
ALTER VIEW [dbo].[view_DingDan]
AS
SELECT A.[DingDanId]
	,A.[CaiGouDanId]
	,A.[GysId]
	,A.[GysName]
	,A.[Status]
	,A.[JinE]
	,A.[SongHuoRenName]
	,A.[SongHuoRenDianHua]
	,A.[SongHuoTime]
	,A.[DaoHuoTime]
	,A.[GysBaoJiaRenId]
	,A.[GysBaoJiaTime]
	,A.[CgsQueRenRenId]
	,A.[CgsQueRenTime]
	,A.[GysFaHuoRenId]
	,A.[GysFaHuoTime]
	,A.[CgsShouHuoRenId]
	,A.[CgsShouHuoTime]
	,A.[GysDaoHuoQueRenStatus]
	,A.[GysDaoHuoQueRenRenId]
	,A.[GysDaoHuoQueRenTime]
	,A.[QuXiaoRenId]
	,A.[QuXiaoTime]
	,A.[IdentityId]	
	,A.[IsDelete]
	,A.[GysFaHuoShuoMing]
	,B.[CgsId]
	,B.[CaiGouDanHao]
	,B.[CaiGouDanName]
	,B.[FaBuRenId]
	,B.[FaBuTime]
	,B.[IssueTime]
	,B.CaiGouBuMen
	,B.MoBanId
	,B.YaoQiuDaoHuoTime
	,B.CaoZuoRenId
	,A.[YuJiDaoHuoTime]	
	,A.[CgsShouHuoRen]--采购商收货人姓名
	,A.[CgsFuKuanStatus]
	,A.[CgsFuKuanTime]
	,A.[CgsFuKuanCaoZuoRenId]
	,A.[CgsYiFuKuanJinE]
	,(SELECT Y.Name FROM dbo.tbl_YongHu Y WHERE Y.YongHuId=B.FaBuRenId) FaBuRenName--采购商发布人姓名 
	,(SELECT A1.Name FROM tbl_GongSi AS A1 WHERE A1.GongSiid=B.CgsId) AS CgsName--采购商名称
	,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=B.CaoZuoRenId) AS CaoZuoRenName--采购商操作人姓名
	,(SELECT A1.Name FROM [tbl_YongHu] AS A1 WHERE A1.[YongHuId]=A.[CgsFuKuanCaoZuoRenId]) AS CgsFuKuanCaoZuoRenName--采购商操作人姓名
	,(SELECT COUNT(*) FROM tbl_DingDanChanPin AS A1 WHERE A1.DingDanId=A.DingDanId) AS CaiGouChanPinXiangShu--采购产品项数
	,A.[GysSongHuoRenId]
FROM [tbl_DingDan] AS A INNER JOIN tbl_CaiGouDan AS B
ON A.CaiGouDanId=B.CaiGouDanId

GO
ALTER TABLE dbo.tbl_GongSi ADD
	LxQQ nvarchar(255) NULL
GO
DECLARE @v sql_variant 
SET @v = N'联系QQ'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_GongSi', N'COLUMN', N'LxQQ'
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-21
-- Description:	采购商、供应商信息添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_GongSi_CU]
	@GongSiId CHAR(36)
	,@LeiXing INT
	,@Name NVARCHAR(255)
	,@FanRenName NVARCHAR(255)
	,@ShengFenId INT
	,@ChengShiId INT
	,@DiZhi NVARCHAR(255)
	,@YingYeZhiZhaoFilepath NVARCHAR(255)
	,@ZuZhiJiGouFilepath NVARCHAR(255)
	,@FuZeRenName NVARCHAR(255)
	,@FuZeRenDianHua NVARCHAR(255)
	,@FuZeRenShenFenZhengHao NVARCHAR(255)
	,@FuZeRenZhaoPianFilepath NVARCHAR(255)
	,@CaiWuName NVARCHAR(255)
	,@CaiWuDianHua NVARCHAR(255)
	,@CaiWuShenFenZhengHao NVARCHAR(255)
	,@CaiWuZhaoPianFilepath NVARCHAR(255)
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@LogoFilepath NVARCHAR(255)
	,@LxQQ NVARCHAR(255)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	SET @FS='C'
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE GongSiId=@GongSiId)
	BEGIN
		SET @FS='U'
	END
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE Name=@Name AND GongSiId<>@GongSiId AND LeiXing=@LeiXing)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_GongSi]([GongSiId],[LeiXing],[Name]
			,[FanRenName],[ShengFenId],[ChengShiId]
			,[DiZhi],[YingYeZhiZhaoFilepath],[ZuZhiJiGouFilepath]
			,[FuZeRenName],[FuZeRenDianHua],[FuZeRenShenFenZhengHao]
			,[FuZeRenZhaoPianFilepath],[CaiWuName],[CaiWuDianHua]
			,[CaiWuShenFenZhengHao],[CaiWuZhaoPianFilepath],[CaoZuoRenId]
			,[IssueTime],[IsDelete],[LogoFilepath]
			,[LxQQ])
		VALUES(@GongSiId,@LeiXing,@Name
			,@FanRenName,@ShengFenId,@ChengShiId
			,@DiZhi,@YingYeZhiZhaoFilepath,@ZuZhiJiGouFilepath
			,@FuZeRenName,@FuZeRenDianHua,@FuZeRenShenFenZhengHao
			,@FuZeRenZhaoPianFilepath,@CaiWuName,@CaiWuDianHua
			,@CaiWuShenFenZhengHao,@CaiWuZhaoPianFilepath,@CaoZuoRenId
			,@IssueTime,'0',@LogoFilepath
			,@LxQQ)
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_GongSi] SET [Name]=@Name,[FanRenName]=@FanRenName
			,[ShengFenId]=@ShengFenId,[ChengShiId]=@ChengShiId
			,[DiZhi]=@DiZhi,[YingYeZhiZhaoFilepath]=@YingYeZhiZhaoFilepath
			,[ZuZhiJiGouFilepath]=@ZuZhiJiGouFilepath,[FuZeRenName]=@FuZeRenName
			,[FuZeRenDianHua]=@FuZeRenDianHua,[FuZeRenShenFenZhengHao]=@FuZeRenShenFenZhengHao
			,[FuZeRenZhaoPianFilepath]=@FuZeRenZhaoPianFilepath,[CaiWuName]=@CaiWuName
			,[CaiWuDianHua]=@CaiWuDianHua,[CaiWuShenFenZhengHao]=@CaiWuShenFenZhengHao
			,[CaiWuZhaoPianFilepath]=@CaiWuZhaoPianFilepath,[LogoFilepath]=@LogoFilepath
			,[LxQQ]=@LxQQ
		WHERE [GongSiId]=@GongSiId
	END
	
	SET @RetCode=1
	RETURN @RetCode	
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-03-20
-- Description:	采购订单
-- =============================================
ALTER VIEW [dbo].[view_DingDan]
AS
SELECT A.[DingDanId]
	,A.[CaiGouDanId]
	,A.[GysId]
	,A.[GysName]
	,A.[Status]
	,A.[JinE]
	,A.[SongHuoRenName]
	,A.[SongHuoRenDianHua]
	,A.[SongHuoTime]
	,A.[DaoHuoTime]
	,A.[GysBaoJiaRenId]
	,A.[GysBaoJiaTime]
	,A.[CgsQueRenRenId]
	,A.[CgsQueRenTime]
	,A.[GysFaHuoRenId]
	,A.[GysFaHuoTime]
	,A.[CgsShouHuoRenId]
	,A.[CgsShouHuoTime]
	,A.[GysDaoHuoQueRenStatus]
	,A.[GysDaoHuoQueRenRenId]
	,A.[GysDaoHuoQueRenTime]
	,A.[QuXiaoRenId]
	,A.[QuXiaoTime]
	,A.[IdentityId]	
	,A.[IsDelete]
	,A.[GysFaHuoShuoMing]
	,B.[CgsId]
	,B.[CaiGouDanHao]
	,B.[CaiGouDanName]
	,B.[FaBuRenId]
	,B.[FaBuTime]
	,B.[IssueTime]
	,B.CaiGouBuMen
	,B.MoBanId
	,B.YaoQiuDaoHuoTime
	,B.CaoZuoRenId
	,A.[YuJiDaoHuoTime]	
	,A.[CgsShouHuoRen]--采购商收货人姓名
	,A.[CgsFuKuanStatus]
	,A.[CgsFuKuanTime]
	,A.[CgsFuKuanCaoZuoRenId]
	,A.[CgsYiFuKuanJinE]
	,(SELECT Y.Name FROM dbo.tbl_YongHu Y WHERE Y.YongHuId=B.FaBuRenId) FaBuRenName--采购商发布人姓名 
	,(SELECT A1.Name FROM tbl_GongSi AS A1 WHERE A1.GongSiid=B.CgsId) AS CgsName--采购商名称
	,(SELECT A1.Name FROM tbl_YongHu AS A1 WHERE A1.YongHuId=B.CaoZuoRenId) AS CaoZuoRenName--采购商操作人姓名
	,(SELECT A1.Name FROM [tbl_YongHu] AS A1 WHERE A1.[YongHuId]=A.[CgsFuKuanCaoZuoRenId]) AS CgsFuKuanCaoZuoRenName--采购商操作人姓名
	,(SELECT COUNT(*) FROM tbl_DingDanChanPin AS A1 WHERE A1.DingDanId=A.DingDanId) AS CaiGouChanPinXiangShu--采购产品项数
	,A.[GysSongHuoRenId]
	,(SELECT A1.LxQQ FROM tbl_GongSi AS A1 WHERE A1.GongSiId=A.GysId) AS GysLxQQ
	,(SELECT A1.LxQQ FROM tbl_GongSi AS A1 WHERE A1.GongSiId=B.CgsId) AS CgsLxQQ
FROM [tbl_DingDan] AS A INNER JOIN tbl_CaiGouDan AS B
ON A.CaiGouDanId=B.CaiGouDanId

GO
--以上日志已更新
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-22
-- Description:	供应商产品删除
-- =============================================
ALTER PROCEDURE [dbo].[proc_ChanPin_D]
	@ChanPinId CHAR(36)
	,@GysId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	SET @RetCode=0
	
	--判断(待完成)
	
	UPDATE [tbl_ChanPin] SET IsDelete='1' WHERE ChanPinId=@ChanPinId AND GysId=@GysId
	
	SET @RetCode=1
	RETURN @RetCode
END
GO
--以上日志已更新 汪奇志 06 11 2015  3:39PM
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-21
-- Description:	采购商、供应商信息添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_GongSi_CU]
	@GongSiId CHAR(36)
	,@LeiXing INT
	,@Name NVARCHAR(255)
	,@FanRenName NVARCHAR(255)
	,@ShengFenId INT
	,@ChengShiId INT
	,@DiZhi NVARCHAR(255)
	,@YingYeZhiZhaoFilepath NVARCHAR(255)
	,@ZuZhiJiGouFilepath NVARCHAR(255)
	,@FuZeRenName NVARCHAR(255)
	,@FuZeRenDianHua NVARCHAR(255)
	,@FuZeRenShenFenZhengHao NVARCHAR(255)
	,@FuZeRenZhaoPianFilepath NVARCHAR(255)
	,@CaiWuName NVARCHAR(255)
	,@CaiWuDianHua NVARCHAR(255)
	,@CaiWuShenFenZhengHao NVARCHAR(255)
	,@CaiWuZhaoPianFilepath NVARCHAR(255)
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@LogoFilepath NVARCHAR(255)
	,@LxQQ NVARCHAR(255)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	SET @FS='C'
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE GongSiId=@GongSiId)
	BEGIN
		SET @FS='U'
	END
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE Name=@Name AND GongSiId<>@GongSiId AND LeiXing=@LeiXing AND IsDelete='0')
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_GongSi]([GongSiId],[LeiXing],[Name]
			,[FanRenName],[ShengFenId],[ChengShiId]
			,[DiZhi],[YingYeZhiZhaoFilepath],[ZuZhiJiGouFilepath]
			,[FuZeRenName],[FuZeRenDianHua],[FuZeRenShenFenZhengHao]
			,[FuZeRenZhaoPianFilepath],[CaiWuName],[CaiWuDianHua]
			,[CaiWuShenFenZhengHao],[CaiWuZhaoPianFilepath],[CaoZuoRenId]
			,[IssueTime],[IsDelete],[LogoFilepath]
			,[LxQQ])
		VALUES(@GongSiId,@LeiXing,@Name
			,@FanRenName,@ShengFenId,@ChengShiId
			,@DiZhi,@YingYeZhiZhaoFilepath,@ZuZhiJiGouFilepath
			,@FuZeRenName,@FuZeRenDianHua,@FuZeRenShenFenZhengHao
			,@FuZeRenZhaoPianFilepath,@CaiWuName,@CaiWuDianHua
			,@CaiWuShenFenZhengHao,@CaiWuZhaoPianFilepath,@CaoZuoRenId
			,@IssueTime,'0',@LogoFilepath
			,@LxQQ)
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_GongSi] SET [Name]=@Name,[FanRenName]=@FanRenName
			,[ShengFenId]=@ShengFenId,[ChengShiId]=@ChengShiId
			,[DiZhi]=@DiZhi,[YingYeZhiZhaoFilepath]=@YingYeZhiZhaoFilepath
			,[ZuZhiJiGouFilepath]=@ZuZhiJiGouFilepath,[FuZeRenName]=@FuZeRenName
			,[FuZeRenDianHua]=@FuZeRenDianHua,[FuZeRenShenFenZhengHao]=@FuZeRenShenFenZhengHao
			,[FuZeRenZhaoPianFilepath]=@FuZeRenZhaoPianFilepath,[CaiWuName]=@CaiWuName
			,[CaiWuDianHua]=@CaiWuDianHua,[CaiWuShenFenZhengHao]=@CaiWuShenFenZhengHao
			,[CaiWuZhaoPianFilepath]=@CaiWuZhaoPianFilepath,[LogoFilepath]=@LogoFilepath
			,[LxQQ]=@LxQQ
		WHERE [GongSiId]=@GongSiId
	END
	
	SET @RetCode=1
	RETURN @RetCode	
END
GO
--以上日志已更新 汪奇志 06 11 2015  5:06PM
GO

ALTER TABLE dbo.tbl_GongSi ADD
	LaiYuan int NOT NULL CONSTRAINT DF_tbl_GongSi_LaiYuan DEFAULT 0,
	ShenHeStatus int NOT NULL CONSTRAINT DF_tbl_GongSi_ShenHeStatus DEFAULT 0,
	ShenHeTime datetime NULL,
	ShenHeRenId char(36) NULL
GO
DECLARE @v sql_variant 
SET @v = N'来源'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_GongSi', N'COLUMN', N'LaiYuan'
GO
DECLARE @v sql_variant 
SET @v = N'审核状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_GongSi', N'COLUMN', N'ShenHeStatus'
GO
DECLARE @v sql_variant 
SET @v = N'审核时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_GongSi', N'COLUMN', N'ShenHeTime'
GO
DECLARE @v sql_variant 
SET @v = N'审核人编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_GongSi', N'COLUMN', N'ShenHeRenId'
GO
UPDATE tbl_GongSi SET ShenHeStatus=1,ShenHeTime=IssueTime,ShenHeRenId=CaoZuoRenId
GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-21
-- Description:	采购商、供应商信息添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_GongSi_CU]
	@GongSiId CHAR(36)
	,@LeiXing INT
	,@Name NVARCHAR(255)
	,@FanRenName NVARCHAR(255)
	,@ShengFenId INT
	,@ChengShiId INT
	,@DiZhi NVARCHAR(255)
	,@YingYeZhiZhaoFilepath NVARCHAR(255)
	,@ZuZhiJiGouFilepath NVARCHAR(255)
	,@FuZeRenName NVARCHAR(255)
	,@FuZeRenDianHua NVARCHAR(255)
	,@FuZeRenShenFenZhengHao NVARCHAR(255)
	,@FuZeRenZhaoPianFilepath NVARCHAR(255)
	,@CaiWuName NVARCHAR(255)
	,@CaiWuDianHua NVARCHAR(255)
	,@CaiWuShenFenZhengHao NVARCHAR(255)
	,@CaiWuZhaoPianFilepath NVARCHAR(255)
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@LogoFilepath NVARCHAR(255)
	,@LxQQ NVARCHAR(255)
	,@LaiYuan INT
	,@ShenHeStatus INT
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	SET @FS='C'
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE GongSiId=@GongSiId)
	BEGIN
		SET @FS='U'
	END
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE Name=@Name AND GongSiId<>@GongSiId AND LeiXing=@LeiXing AND IsDelete='0')
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_GongSi]([GongSiId],[LeiXing],[Name]
			,[FanRenName],[ShengFenId],[ChengShiId]
			,[DiZhi],[YingYeZhiZhaoFilepath],[ZuZhiJiGouFilepath]
			,[FuZeRenName],[FuZeRenDianHua],[FuZeRenShenFenZhengHao]
			,[FuZeRenZhaoPianFilepath],[CaiWuName],[CaiWuDianHua]
			,[CaiWuShenFenZhengHao],[CaiWuZhaoPianFilepath],[CaoZuoRenId]
			,[IssueTime],[IsDelete],[LogoFilepath]
			,[LxQQ],[LaiYuan],[ShenHeStatus])
		VALUES(@GongSiId,@LeiXing,@Name
			,@FanRenName,@ShengFenId,@ChengShiId
			,@DiZhi,@YingYeZhiZhaoFilepath,@ZuZhiJiGouFilepath
			,@FuZeRenName,@FuZeRenDianHua,@FuZeRenShenFenZhengHao
			,@FuZeRenZhaoPianFilepath,@CaiWuName,@CaiWuDianHua
			,@CaiWuShenFenZhengHao,@CaiWuZhaoPianFilepath,@CaoZuoRenId
			,@IssueTime,'0',@LogoFilepath
			,@LxQQ,@LaiYuan,@ShenheStatus)
		
		IF(@ShenHeStatus=1)
		BEGIN
			UPDATE [tbl_GongSi] SET [ShenHeTime]=@IssueTime,[ShenHeRenId]=@CaoZuoRenId WHERE GongSiId=@GongSiId
		END
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_GongSi] SET [Name]=@Name,[FanRenName]=@FanRenName
			,[ShengFenId]=@ShengFenId,[ChengShiId]=@ChengShiId
			,[DiZhi]=@DiZhi,[YingYeZhiZhaoFilepath]=@YingYeZhiZhaoFilepath
			,[ZuZhiJiGouFilepath]=@ZuZhiJiGouFilepath,[FuZeRenName]=@FuZeRenName
			,[FuZeRenDianHua]=@FuZeRenDianHua,[FuZeRenShenFenZhengHao]=@FuZeRenShenFenZhengHao
			,[FuZeRenZhaoPianFilepath]=@FuZeRenZhaoPianFilepath,[CaiWuName]=@CaiWuName
			,[CaiWuDianHua]=@CaiWuDianHua,[CaiWuShenFenZhengHao]=@CaiWuShenFenZhengHao
			,[CaiWuZhaoPianFilepath]=@CaiWuZhaoPianFilepath,[LogoFilepath]=@LogoFilepath
			,[LxQQ]=@LxQQ
		WHERE [GongSiId]=@GongSiId
	END
	
	SET @RetCode=1
	RETURN @RetCode	
END
GO

ALTER TABLE dbo.tbl_YongHu ADD
	LaiYuan int NOT NULL CONSTRAINT DF_tbl_YongHu_LaiYuan DEFAULT 0,
	ShenHeStatus int NOT NULL CONSTRAINT DF_tbl_YongHu_ShenHeStatus DEFAULT 0
GO
DECLARE @v sql_variant 
SET @v = N'用户来源'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_YongHu', N'COLUMN', N'LaiYuan'
GO
DECLARE @v sql_variant 
SET @v = N'审核状态'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_YongHu', N'COLUMN', N'ShenHeStatus'
GO

UPDATE tbl_YongHu SET ShenHeStatus=1
GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-21
-- Description:	采购商、供应商、平台用户信息添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_YongHu_CU]
	@YongHuId CHAR(36)
	,@LeiXing INT
	,@GongSiId CHAR(36)
	,@Username NVARCHAR(255)
	,@PasswordMD5 NVARCHAR(255)
	,@JueSeId CHAR(36)
	,@Status INT
	,@BuMenName NVARCHAR(255)
	,@Name NVARCHAR(255)
	,@ZhaoPianFilepath NVARCHAR(255)
	,@ZhiWu NVARCHAR(255)
	,@XingBie INT
	,@ChuShengRiQi DATETIME
	,@ShouJi NVARCHAR(255)
	,@DianHua NVARCHAR(255)
	,@Fax NVARCHAR(255)
	,@Email NVARCHAR(255)
	,@DiZhi NVARCHAR(255)
	,@RuZhiRiQi DATETIME
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@Privs NVARCHAR(MAX)
	,@LaiYuan INT
	,@ShenHeStatus INT
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	SET @FS='C'
	SET @RetCode=0	
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE Username=@Username AND YongHuId<>@YongHuId /*AND LeiXing=@LeiXing*/)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE YongHuId=@YongHuId)
	BEGIN
		SET @FS='U'		
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_YongHu]([YongHuId],[LeiXing],[GongSiId]
			,[Username],[PasswordMd5],[JueSeId]
			,[Status],[BuMenName],[Name]
			,[ZhaoPianFilepath],[ZhiWu],[XingBie]
			,[ChuShengRiQi],[ShouJi],[DianHua]
			,[Fax],[Email],[DiZhi]
			,[RuZhiRiQi],[CaoZuoRenId],[IssueTime]
			,[Privs],[IsDelete],[LaiYuan]
			,[ShenHeStatus])
		VALUES(@YongHuId,@LeiXing,@GongSiId
			,@Username,@PasswordMd5,@JueSeId
			,@Status,@BuMenName,@Name
			,@ZhaoPianFilepath,@ZhiWu,@XingBie
			,@ChuShengRiQi,@ShouJi,@DianHua
			,@Fax,@Email,@DiZhi
			,@RuZhiRiQi,@CaoZuoRenId,@IssueTime
			,@Privs,'0',@LaiYuan
			,@ShenHeStatus)
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_YongHu] SET [JueSeId]=@JueSeId/*,[Status]=@Status*/
			,[BuMenName]=@BuMenName,[Name]=@Name
			,[ZhaoPianFilepath]=@ZhaoPianFilepath,[ZhiWu]=@ZhiWu,[XingBie]=@XingBie
			,[ChuShengRiQi]=@ChuShengRiQi,[ShouJi]=@ShouJi,[DianHua]=@DianHua
			,[Fax]=@Fax,[Email]=@Email,[DiZhi]=@DiZhi
			,[RuZhiRiQi]=@RuZhiRiQi--,[Privs]=@Privs
			,GongSiId=@GongSiId
		WHERE [YongHuId]=@YongHuId
	END	
	
	IF(@FS='U' AND @PasswordMD5 IS NOT NULL AND LEN(@PasswordMD5)>0)
	BEGIN
		UPDATE [tbl_YongHu] SET [PasswordMD5]=@PasswordMD5
		WHERE [YongHuId]=@YongHuId
	END
	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2015-06-12
-- Description:	注册用户
-- =============================================
CREATE VIEW [view_ZhuCeYongHu]
AS
SELECT B.GongSiId
	,B.LeiXing
	,B.Name AS GongSiName
	,B.FanRenName
	,B.FuZeRenName
	,B.FuZeRenDianHua
	,B.IssueTime
	,A.LaiYuan
	,A.ShenHeStatus	
	,A.YongHuId
	,A.Username AS YongHuMing
	,A.Name AS YongHuName
FROM tbl_YongHu AS A INNER JOIN tbl_GongSi AS B
ON A.GongSiId=B.GongSiId AND B.IsDelete='0'
WHERE A.LaiYuan=1 AND A.IsDelete='0'
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-06-12
-- Description:	用户注册
-- =============================================
CREATE PROCEDURE proc_YongHu_ZhuCe
	@GongSiId CHAR(36)
	,@YongHuId CHAR(36)
	,@GongSiName NVARCHAR(255)
	,@FaRenName NVARCHAR(255)
	,@FuZeRenName NVARCHAR(255)
	,@FuZeRenDianHua NVARCHAR(255)
	,@YongHuMing NVARCHAR(255)
	,@PasswordMD5 NVARCHAR(255)
	,@LeiXing INT
	,@ShenHeStatus INT
	,@IssueTime DATETIME
	,@LaiYuan INT
	,@RetCode INT OUTPUT
AS
BEGIN	
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE Name=@GongSiName AND GongSiId<>@GongSiId AND LeiXing=@LeiXing AND IsDelete='0')
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE Username=@YongHuMing AND YongHuId<>@YongHuId)
	BEGIN
		SET @RetCode=-98
		RETURN @RetCode
	END
	
	INSERT INTO [tbl_GongSi]([GongSiId],[LeiXing],[Name]
		,[FanRenName],[ShengFenId],[ChengShiId]
		,[DiZhi],[YingYeZhiZhaoFilepath],[ZuZhiJiGouFilepath]
		,[FuZeRenName],[FuZeRenDianHua],[FuZeRenShenFenZhengHao]
		,[FuZeRenZhaoPianFilepath],[CaiWuName],[CaiWuDianHua]
		,[CaiWuShenFenZhengHao],[CaiWuZhaoPianFilepath],[CaoZuoRenId]
		,[IssueTime],[IsDelete],[LogoFilepath]
		,[LxQQ],[LaiYuan],[ShenHeStatus]
		,[ShenHeTime],[ShenHeRenId])
	VALUES(@GongSiId,@LeiXIng,@GongSiName
		,@FaRenName,0,0
		,'','',''
		,@FuZeRenName,@FuZeRenDianHua,''
		,'','',''
		,'','',@YongHuId
		,@IssueTime,'0',''
		,'',@LaiYuan,@ShenHeStatus
		,NULL,NULL)
		
	IF(@ShenHeStatus=1)
	BEGIN
		UPDATE [tbl_GongSi] SET ShenHeTime=@IssueTime,ShenHeRenId=@YongHuId WHERE GongSiId=@GongSiId
	END
	
	INSERT INTO [tbl_YongHu]([YongHuId],[LeiXing],[GongSiId]
		,[Username],[PasswordMD5],[JueSeId]
		,[Status],[BuMenName],[Name]
		,[ZhaoPianFilepath],[ZhiWu],[XingBie]
		,[ChuShengRiQi],[ShouJi],[DianHua]
		,[Fax],[Email],[DiZhi]
		,[RuZhiRiQi],[CaoZuoRenId],[IssueTime]
		,[Privs],[IsDelete],[LaiYuan]
		,[ShenHeStatus])
	VALUES(@YongHuId,@LeiXing,@GongSiId
		,@YongHuMing,@PasswordMD5,''
		,0,'',@FuZeRenName
		,'','',0
		,NULL,'',''
		,'','',''
		,NULL,@YongHuId,@IssueTime
		,'','0',@LaiYuan
		,@ShenHeStatus)
	
	SET @RetCode=1
	RETURN @RetCode	
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-06-12
-- Description:	用户审核
-- =============================================
CREATE PROCEDURE proc_YongHu_ShenHe
	@GongSiId CHAR(36)
	,@YongHuId CHAR(36)
	,@ShenHeStatus INT
	,@ShenHeTime DATETIME
	,@ShenHeRenId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	SET @RetCode=0
	IF NOT EXISTS(SELECT 1 FROM tbl_GongSi WHERE GongSiId=@GongSiId)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF NOT EXISTS(SELECT 1 FROM tbl_YongHu WHERE YongHuId=@YongHuId AND GongSiId=@GongSiId)
	BEGIN
		SET @RetCode=-98
		RETURN @RetCode
	END
	
	UPDATE tbl_GongSi SET ShenHeStatus=@ShenHeStatus,ShenHeTime=@ShenHeTime,@ShenHeRenId=@ShenHeRenId WHERE GongSiId=@GongSiId
	UPDATE tbl_YongHu SET ShenHeStatus=@ShenHeStatus WHERE YongHuId=@YongHuId	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-06-12
-- Description:	用户注册
-- =============================================
ALTER PROCEDURE proc_YongHu_ZhuCe
	@GongSiId CHAR(36)
	,@YongHuId CHAR(36)
	,@GongSiName NVARCHAR(255)
	,@FaRenName NVARCHAR(255)
	,@FuZeRenName NVARCHAR(255)
	,@FuZeRenDianHua NVARCHAR(255)
	,@YongHuMing NVARCHAR(255)
	,@PasswordMD5 NVARCHAR(255)
	,@LeiXing INT
	,@ShenHeStatus INT
	,@IssueTime DATETIME
	,@LaiYuan INT
	,@DiZhi NVARCHAR(255)
	,@RetCode INT OUTPUT
AS
BEGIN	
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_GongSi WHERE Name=@GongSiName AND GongSiId<>@GongSiId AND LeiXing=@LeiXing AND IsDelete='0')
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE Username=@YongHuMing AND YongHuId<>@YongHuId)
	BEGIN
		SET @RetCode=-98
		RETURN @RetCode
	END
	
	INSERT INTO [tbl_GongSi]([GongSiId],[LeiXing],[Name]
		,[FanRenName],[ShengFenId],[ChengShiId]
		,[DiZhi],[YingYeZhiZhaoFilepath],[ZuZhiJiGouFilepath]
		,[FuZeRenName],[FuZeRenDianHua],[FuZeRenShenFenZhengHao]
		,[FuZeRenZhaoPianFilepath],[CaiWuName],[CaiWuDianHua]
		,[CaiWuShenFenZhengHao],[CaiWuZhaoPianFilepath],[CaoZuoRenId]
		,[IssueTime],[IsDelete],[LogoFilepath]
		,[LxQQ],[LaiYuan],[ShenHeStatus]
		,[ShenHeTime],[ShenHeRenId])
	VALUES(@GongSiId,@LeiXIng,@GongSiName
		,@FaRenName,0,0
		,@DiZhi,'',''
		,@FuZeRenName,@FuZeRenDianHua,''
		,'','',''
		,'','',@YongHuId
		,@IssueTime,'0',''
		,'',@LaiYuan,@ShenHeStatus
		,NULL,NULL)
		
	IF(@ShenHeStatus=1)
	BEGIN
		UPDATE [tbl_GongSi] SET ShenHeTime=@IssueTime,ShenHeRenId=@YongHuId WHERE GongSiId=@GongSiId
	END
	
	INSERT INTO [tbl_YongHu]([YongHuId],[LeiXing],[GongSiId]
		,[Username],[PasswordMD5],[JueSeId]
		,[Status],[BuMenName],[Name]
		,[ZhaoPianFilepath],[ZhiWu],[XingBie]
		,[ChuShengRiQi],[ShouJi],[DianHua]
		,[Fax],[Email],[DiZhi]
		,[RuZhiRiQi],[CaoZuoRenId],[IssueTime]
		,[Privs],[IsDelete],[LaiYuan]
		,[ShenHeStatus])
	VALUES(@YongHuId,@LeiXing,@GongSiId
		,@YongHuMing,@PasswordMD5,''
		,0,'',@FuZeRenName
		,'','',0
		,'1985-01-01','',''
		,'','',''
		,@IssueTime,@YongHuId,@IssueTime
		,'','0',@LaiYuan
		,@ShenHeStatus)
	
	SET @RetCode=1
	RETURN @RetCode	
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-06-12
-- Description:	用户审核
-- =============================================
ALTER PROCEDURE [dbo].[proc_YongHu_ShenHe]
	@GongSiId CHAR(36)
	,@YongHuId CHAR(36)
	,@ShenHeStatus INT
	,@ShenHeTime DATETIME
	,@ShenHeRenId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	SET @RetCode=0
	IF NOT EXISTS(SELECT 1 FROM tbl_GongSi WHERE GongSiId=@GongSiId)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF NOT EXISTS(SELECT 1 FROM tbl_YongHu WHERE YongHuId=@YongHuId AND GongSiId=@GongSiId)
	BEGIN
		SET @RetCode=-98
		RETURN @RetCode
	END
	
	UPDATE tbl_GongSi SET ShenHeStatus=@ShenHeStatus,ShenHeTime=@ShenHeTime,@ShenHeRenId=@ShenHeRenId WHERE GongSiId=@GongSiId
	UPDATE tbl_YongHu SET ShenHeStatus=@ShenHeStatus WHERE YongHuId=@YongHuId	
	
	SET @RetCode=1
	RETURN @RetCode
END

GO
--以上日志已更新 汪奇志 06 12 2015  5:39PM
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-23
-- Description:	采购单添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_CaiGouDan_CU]
	@CaiGouDanId CHAR(36)
	,@CgsId CHAR(36)
	,@CaiGouDanName NVARCHAR(255)
	,@MoBanId CHAR(36)
	,@Status INT
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@ShouHuoDiZhi NVARCHAR(255)
	,@ShouHuoRenName NVARCHAR(255)
	,@ShouHuoRenDianHua NVARCHAR(255)
	,@CaiGouBuMen NVARCHAR(255)
	,@DingDanXml NVARCHAR(MAX)
	,@ChanPinXml NVARCHAR(MAX)
	,@CaiGouDanShuoMing NVARCHAR(MAX)
	,@YaoQiuDaoHuoTime DATETIME
	,@ShouHuoDiZhiId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	DECLARE @hdoc INT
	DECLARE @YuanStatus INT
	DECLARE @JiShuQi INT
	DECLARE @ChanPinId CHAR(36)
	DECLARE @ChanPinJiaGe1 MONEY
	DECLARE @GysId CHAR(36)
	DECLARE @TEMP_TABLE TABLE([MingXiId] CHAR(36),[DingDanId] CHAR(36),[ChanPinId] CHAR(36),[ShuLiang] MONEY,ChanPinJiaGe1 MONEY,GysId CHAR(36),IdentityId INT IDENTITY)	
	
	SET @FS='C'
	SET @RetCode=0	
	
	IF EXISTS(SELECT 1 FROM tbl_CaiGouDan WHERE CaiGouDanId=@CaiGouDanId AND CgsId=@CgsId)
	BEGIN
		SET @FS='U'
		SELECT @YuanStatus=[Status] FROM tbl_CaiGouDan WHERE CaiGouDanId=@CaiGouDanId
	END
	
	IF(@YuanStatus=1)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_CaiGouDan]([CaiGouDanId],[CgsId],[CaiGouDanHao]
			,[CaiGouDanName],[MoBanId],[Status]
			,[CaoZuoRenId],[IssueTime],[IsDelete]
			,[ShouHuoDiZhi],[ShouHuoRenName],[ShouHuoRenDianHua]
			,[CaiGouBuMen],[FaBuRenId],[FaBuTime]
			,[CaiGouDanShuoMing],[YaoQiuDaoHuoTime],[ShouHuoDiZhiId])
		VALUES(@CaiGouDanId,@CgsId,''
			,@CaiGouDanName,@MoBanId,@Status
			,@CaoZuoRenId,@IssueTime,'0'
			,@ShouHuoDiZhi,@ShouHuoRenName,@ShouHuoRenDianHua
			,@CaiGouBuMen,'',NULL
			,@CaiGouDanShuoMing,@YaoQiuDaoHuoTime,@ShouHuoDiZhiId)
		
		--BM:'CGD'+CONVERT(NVARCHAR(8),@IssueTime,112)+dbo.fn_PadLeft(IdentityId,'0',5)
		--BM:'CGD'+dbo.fn_PadLeft(IdentityId,'0',5)
		UPDATE [tbl_CaiGouDan] SET [CaiGouDanHao]='CGD'+CONVERT(NVARCHAR(8),@IssueTime,112)+dbo.fn_PadLeft(IdentityId,'0',5) WHERE [CaiGouDanId]=@CaiGouDanId
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_CaiGouDan] SET [CaiGouDanName]=@CaiGouDanName,[MoBanId]=@MoBanId
			,[ShouHuoDiZhi]=@ShouHuoDiZhi,[ShouHuoRenName]=@ShouHuoRenName
			,[ShouHuoRenDianHua]=@ShouHuoRenDianHua,[CaiGouBuMen]=@CaiGouBuMen
			,[CaiGouDanShuoMing]=@CaiGouDanShuoMing,[YaoQiuDaoHuoTime]=@YaoQiuDaoHuoTime
			,[ShouHuoDiZhiId]=@ShouHuoDiZhiId
		WHERE CaiGouDanId=@CaiGouDanId
	END
	
	DELETE FROM tbl_DingDanChanPin WHERE CaiGouDanId=@CaiGouDanId
	DELETE FROM tbl_DingDan WHERE CaiGouDanId=@CaiGouDanId
	
	IF(@DingDanXml IS NOT NULL AND LEN(@DingDanXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@DingDanXml
		
		INSERT INTO [tbl_DingDan]([DingDanId],[CaiGouDanId],[GysId]
			,[GysName],[Status],[JinE]
			,[SongHuoRenName],[SongHuoRenDianHua],[SongHuoTime]
			,[DaoHuoTime],[GysBaoJiaRenId],[GysBaoJiaTime]
			,[CgsQueRenRenId],[CgsQueRenTime],[GysFaHuoRenId]
			,[GysFaHuoTime],[CgsShouHuoRenId],[CgsShouHuoTime]
			,[GysDaoHuoQueRenStatus],[GysDaoHuoQueRenRenId],[GysDaoHuoQueRenTime]
			,[QuXiaoRenId],[QuXiaoTime],[GysFaHuoShuoMing])
		SELECT A.[DingDanId],@CaiGouDanId,A.[GysId]
			,B.Name,A.[Status],A.[JinE]
			,'','',NULL
			,NULL,'',NULL
			,'',NULL,''
			,NULL,'',NULL
			,0,'',NULL
			,'',NULL,''
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([DingDanId] CHAR(36),[GysId] CHAR(36),[Status] INT,[JinE] MONEY) AS A
		INNER JOIN tbl_GongSi AS B ON A.GysId=B.GongSiId
		EXEC sp_xml_removedocument @hdoc
	END
	
	IF(@ChanPinXml IS NOT NULL AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		INSERT INTO @TEMP_TABLE([MingXiId],[DingDanId],[ChanPinId],[ShuLiang],[GysId],ChanPinJiaGe1)
		SELECT A.[MingXiId],A.[DingDanId],A.[ChanPinId],A.[ShuLiang],B.GysId,0
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([MingXiId] CHAR(36),[DingDanId] CHAR(36),[ChanPinId] CHAR(36),[ShuLiang] MONEY) AS A
		INNER JOIN tbl_ChanPin AS B ON A.[ChanPinId]=B.ChanPinId
		EXEC sp_xml_removedocument @hdoc
		
		DECLARE @i INT
		SET @i=1
		SELECT @JiShuQi=COUNT(*) FROM @TEMP_TABLE
		
		WHILE(@i<=@JiShuQi)
		BEGIN
			SELECT @ChanPinId=ChanPinId,@GysId=GysId FROM @TEMP_TABLE WHERE IdentityId=@i
			SET @ChanPinJiaGe1=0
			
			SELECT TOP 1 @ChanPinJiaGe1=ChanPinJiaGe FROM tbl_DingDanChanPinJiaGe WHERE GysId=@GysId AND CgsId=@CgsId AND ChanPinId=@ChanPinId ORDER BY IdentityId DESC
		
			SET @ChanPinJiaGe1=ISNULL(@ChanPinJiaGe1,0)	
			
			UPDATE @TEMP_TABLE SET ChanPinJiaGe1=@ChanPinJiaGe1 WHERE IdentityId=@i
			
			SET @i=@i+1
		END		
		
		INSERT INTO [tbl_DingDanChanPin]([MingXiId],[CaiGouDanId],[DingDanId]
			,[ChanPinId],[ChanPinName],[ChanPinGuiGe]
			,[JiLiangDanWei],[ShuLiang],[ChanPinJiaGe]
			,[JinE],[FaHuoShuLiang],[DaoHuoShuLiang]
			,[CgsDaoHuoShuoMing],[GysBaoJiaShuoMing],[ChanPinJiaGe1])
		SELECT A.[MingXiId],@CaiGouDanId,A.[DingDanId]
			,A.[ChanPinId],B.Name,B.GuiGe
			,B.JiLiangDanWei,A.[ShuLiang],A.ChanPinJiaGe1
			,0,0,0
			,'','',A.ChanPinJiaGe1
		FROM @TEMP_TABLE AS A INNER JOIN tbl_ChanPin AS B ON A.[ChanPinId]=B.ChanPinId	
	END
	
	UPDATE [tbl_DingDanChanPin] SET [JinE]=[ChanPinJiaGe]*[ShuLiang] WHERE [CaiGouDanId]=@CaiGouDanId
	UPDATE tbl_DingDan SET JinE=ISNULL((SELECT SUM(A1.JinE) FROM [tbl_DingDanChanPin] AS A1 WHERE A1.DingDanId=tbl_DingDan.DingDanId),0) WHERE [CaiGouDanId]=@CaiGouDanId	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-22
-- Description:	采购模板添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_CaiGouMoBan_CU]
	@MoBanId CHAR(36)
	,@CgsId CHAR(36)
	,@Name NVARCHAR(255)
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@ChanPinXml NVARCHAR(MAX)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	DECLARE @hdoc INT
	SET @FS='C'
	SET @RetCode=0
	
	IF EXISTS(SELECT 1 FROM tbl_CaiGouMoBan WHERE MoBanId=@MoBanId AND CgsId=@CgsId)
	BEGIN
		SET @FS='U'
	END
	
	IF EXISTS(SELECT 1 FROM tbl_CaiGouMoBan WHERE MoBanId<>@MoBanId AND CgsId=@CgsId AND Name=@Name AND IsDelete='0')
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_CaiGouMoBan]([MoBanId],[CgsId],[Name]
			,[CaoZuoRenId],[IssueTime],[IsDelete])
		VALUES(@MoBanId,@CgsId,@Name
			,@CaoZuoRenId,@IssueTime,'0')
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_CaiGouMoBan] SET [Name]=@Name,CgsId=@CgsId
		WHERE [MoBanId]=@MoBanId
	END
	
	DELETE FROM tbl_CaiGouMoBanChanPin WHERE [MoBanId]=@MoBanId
	
	IF(@ChanPinXml IS NOT NULL AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		INSERT INTO [tbl_CaiGouMoBanChanPin]([Id],[MoBanId],[ChanPinId]
			,[GysId],[ShuLiang])
		SELECT [Id],@MoBanId,[ChanPinId]
			,[GysId],[ShuLiang]
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([Id] CHAR(36),[ChanPinId] CHAR(36),[GysId] CHAR(36),[ShuLiang] MONEY)
		EXEC sp_xml_removedocument @hdoc
	END
	
	SET @RetCode=1
	RETURN @RetCode
END
GO
--以上日志已更新 汪奇志 06 15 2015  1:51PM
GO

/****** Object:  Table [dbo].[tbl_DingDanChanPinJiaGe]    Script Date: 06/15/2015 15:03:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DingDanChanPinJiaGe](
	[IdentityId] [int] IDENTITY(1,1) NOT NULL,
	[MingXiId] [char](36) NOT NULL,
	[GysId] [char](36) NOT NULL,
	[CgsId] [char](36) NOT NULL,
	[ChanPinId] [char](36) NOT NULL,
	[ChanPinJiaGe] [money] NOT NULL,
	[ChanPinJiaGe1] [money] NOT NULL,
	[IssueTime] [datetime] NOT NULL,
	[CaoZuoRenId] [char](36) NOT NULL,
 CONSTRAINT [PK_TBL_DINGDANCHANPINJIAGE] PRIMARY KEY CLUSTERED 
(
	[IdentityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'IdentityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单产品明细编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'MingXiId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'GysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购商编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'CgsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'ChanPinId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'ChanPinJiaGe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品单价（上次）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'ChanPinJiaGe1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'IssueTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe', @level2type=N'COLUMN',@level2name=N'CaoZuoRenId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购单-订单-产品报价信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_DingDanChanPinJiaGe'
GO
/****** Object:  Default [DF__tbl_DingD__ChanP__4830B400]    Script Date: 06/15/2015 15:03:07 ******/
ALTER TABLE [dbo].[tbl_DingDanChanPinJiaGe] ADD  CONSTRAINT [DF__tbl_DingD__ChanP__4830B400]  DEFAULT ((0)) FOR [ChanPinJiaGe]
GO
/****** Object:  Default [DF__tbl_DingD__ChanP__4924D839]    Script Date: 06/15/2015 15:03:07 ******/
ALTER TABLE [dbo].[tbl_DingDanChanPinJiaGe] ADD  CONSTRAINT [DF__tbl_DingD__ChanP__4924D839]  DEFAULT ((0)) FOR [ChanPinJiaGe1]
GO
/****** Object:  Default [DF__tbl_DingD__Issue__4A18FC72]    Script Date: 06/15/2015 15:03:07 ******/
ALTER TABLE [dbo].[tbl_DingDanChanPinJiaGe] ADD  CONSTRAINT [DF__tbl_DingD__Issue__4A18FC72]  DEFAULT (getdate()) FOR [IssueTime]
GO
--以上日志已更新 汪奇志 06 15 2015  3:05PM
GO

















GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-21
-- Description:	采购商、供应商、平台用户信息添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_YongHu_CU]
	@YongHuId CHAR(36)
	,@LeiXing INT
	,@GongSiId CHAR(36)
	,@Username NVARCHAR(255)
	,@PasswordMD5 NVARCHAR(255)
	,@JueSeId CHAR(36)
	,@Status INT
	,@BuMenName NVARCHAR(255)
	,@Name NVARCHAR(255)
	,@ZhaoPianFilepath NVARCHAR(255)
	,@ZhiWu NVARCHAR(255)
	,@XingBie INT
	,@ChuShengRiQi DATETIME
	,@ShouJi NVARCHAR(255)
	,@DianHua NVARCHAR(255)
	,@Fax NVARCHAR(255)
	,@Email NVARCHAR(255)
	,@DiZhi NVARCHAR(255)
	,@RuZhiRiQi DATETIME
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@Privs NVARCHAR(MAX)
	,@LaiYuan INT
	,@ShenHeStatus INT
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	SET @FS='C'
	SET @RetCode=0	
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE Username=@Username AND YongHuId<>@YongHuId AND IsDelete='0' /*AND LeiXing=@LeiXing*/)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF EXISTS(SELECT 1 FROM tbl_YongHu WHERE YongHuId=@YongHuId)
	BEGIN
		SET @FS='U'		
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_YongHu]([YongHuId],[LeiXing],[GongSiId]
			,[Username],[PasswordMd5],[JueSeId]
			,[Status],[BuMenName],[Name]
			,[ZhaoPianFilepath],[ZhiWu],[XingBie]
			,[ChuShengRiQi],[ShouJi],[DianHua]
			,[Fax],[Email],[DiZhi]
			,[RuZhiRiQi],[CaoZuoRenId],[IssueTime]
			,[Privs],[IsDelete],[LaiYuan]
			,[ShenHeStatus])
		VALUES(@YongHuId,@LeiXing,@GongSiId
			,@Username,@PasswordMd5,@JueSeId
			,@Status,@BuMenName,@Name
			,@ZhaoPianFilepath,@ZhiWu,@XingBie
			,@ChuShengRiQi,@ShouJi,@DianHua
			,@Fax,@Email,@DiZhi
			,@RuZhiRiQi,@CaoZuoRenId,@IssueTime
			,@Privs,'0',@LaiYuan
			,@ShenHeStatus)
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_YongHu] SET [JueSeId]=@JueSeId/*,[Status]=@Status*/
			,[BuMenName]=@BuMenName,[Name]=@Name
			,[ZhaoPianFilepath]=@ZhaoPianFilepath,[ZhiWu]=@ZhiWu,[XingBie]=@XingBie
			,[ChuShengRiQi]=@ChuShengRiQi,[ShouJi]=@ShouJi,[DianHua]=@DianHua
			,[Fax]=@Fax,[Email]=@Email,[DiZhi]=@DiZhi
			,[RuZhiRiQi]=@RuZhiRiQi--,[Privs]=@Privs
			,GongSiId=@GongSiId
		WHERE [YongHuId]=@YongHuId
	END	
	
	IF(@FS='U' AND @PasswordMD5 IS NOT NULL AND LEN(@PasswordMD5)>0)
	BEGIN
		UPDATE [tbl_YongHu] SET [PasswordMD5]=@PasswordMD5
		WHERE [YongHuId]=@YongHuId
	END
	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

ALTER TABLE dbo.tbl_DingDanChanPin ADD
	ChanPinPinPai nvarchar(255) NULL
GO
DECLARE @v sql_variant 
SET @v = N'产品品牌'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_DingDanChanPin', N'COLUMN', N'ChanPinPinPai'
GO
UPDATE tbl_DingDanChanPin SET ChanPinPinPai=B.PinPai
FROM tbl_DingDanChanPin AS A INNER JOIN tbl_ChanPin AS B 
ON A.ChanPinId=B.ChanPinId
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-23
-- Description:	采购单添加、修改
-- =============================================
ALTER PROCEDURE [dbo].[proc_CaiGouDan_CU]
	@CaiGouDanId CHAR(36)
	,@CgsId CHAR(36)
	,@CaiGouDanName NVARCHAR(255)
	,@MoBanId CHAR(36)
	,@Status INT
	,@CaoZuoRenId CHAR(36)
	,@IssueTime DATETIME
	,@ShouHuoDiZhi NVARCHAR(255)
	,@ShouHuoRenName NVARCHAR(255)
	,@ShouHuoRenDianHua NVARCHAR(255)
	,@CaiGouBuMen NVARCHAR(255)
	,@DingDanXml NVARCHAR(MAX)
	,@ChanPinXml NVARCHAR(MAX)
	,@CaiGouDanShuoMing NVARCHAR(MAX)
	,@YaoQiuDaoHuoTime DATETIME
	,@ShouHuoDiZhiId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @FS CHAR(1)
	DECLARE @hdoc INT
	DECLARE @YuanStatus INT
	DECLARE @JiShuQi INT
	DECLARE @ChanPinId CHAR(36)
	DECLARE @ChanPinJiaGe1 MONEY
	DECLARE @GysId CHAR(36)
	DECLARE @TEMP_TABLE TABLE([MingXiId] CHAR(36),[DingDanId] CHAR(36),[ChanPinId] CHAR(36),[ShuLiang] MONEY,ChanPinJiaGe1 MONEY,GysId CHAR(36),IdentityId INT IDENTITY)	
	
	SET @FS='C'
	SET @RetCode=0	
	
	IF EXISTS(SELECT 1 FROM tbl_CaiGouDan WHERE CaiGouDanId=@CaiGouDanId AND CgsId=@CgsId)
	BEGIN
		SET @FS='U'
		SELECT @YuanStatus=[Status] FROM tbl_CaiGouDan WHERE CaiGouDanId=@CaiGouDanId
	END
	
	IF(@YuanStatus=1)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@FS='C')
	BEGIN
		INSERT INTO [tbl_CaiGouDan]([CaiGouDanId],[CgsId],[CaiGouDanHao]
			,[CaiGouDanName],[MoBanId],[Status]
			,[CaoZuoRenId],[IssueTime],[IsDelete]
			,[ShouHuoDiZhi],[ShouHuoRenName],[ShouHuoRenDianHua]
			,[CaiGouBuMen],[FaBuRenId],[FaBuTime]
			,[CaiGouDanShuoMing],[YaoQiuDaoHuoTime],[ShouHuoDiZhiId])
		VALUES(@CaiGouDanId,@CgsId,''
			,@CaiGouDanName,@MoBanId,@Status
			,@CaoZuoRenId,@IssueTime,'0'
			,@ShouHuoDiZhi,@ShouHuoRenName,@ShouHuoRenDianHua
			,@CaiGouBuMen,'',NULL
			,@CaiGouDanShuoMing,@YaoQiuDaoHuoTime,@ShouHuoDiZhiId)
		
		--BM:'CGD'+CONVERT(NVARCHAR(8),@IssueTime,112)+dbo.fn_PadLeft(IdentityId,'0',5)
		--BM:'CGD'+dbo.fn_PadLeft(IdentityId,'0',5)
		UPDATE [tbl_CaiGouDan] SET [CaiGouDanHao]='CGD'+CONVERT(NVARCHAR(8),@IssueTime,112)+dbo.fn_PadLeft(IdentityId,'0',5) WHERE [CaiGouDanId]=@CaiGouDanId
	END
	
	IF(@FS='U')
	BEGIN
		UPDATE [tbl_CaiGouDan] SET [CaiGouDanName]=@CaiGouDanName,[MoBanId]=@MoBanId
			,[ShouHuoDiZhi]=@ShouHuoDiZhi,[ShouHuoRenName]=@ShouHuoRenName
			,[ShouHuoRenDianHua]=@ShouHuoRenDianHua,[CaiGouBuMen]=@CaiGouBuMen
			,[CaiGouDanShuoMing]=@CaiGouDanShuoMing,[YaoQiuDaoHuoTime]=@YaoQiuDaoHuoTime
			,[ShouHuoDiZhiId]=@ShouHuoDiZhiId
		WHERE CaiGouDanId=@CaiGouDanId
	END
	
	DELETE FROM tbl_DingDanChanPin WHERE CaiGouDanId=@CaiGouDanId
	DELETE FROM tbl_DingDan WHERE CaiGouDanId=@CaiGouDanId
	
	IF(@DingDanXml IS NOT NULL AND LEN(@DingDanXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@DingDanXml
		
		INSERT INTO [tbl_DingDan]([DingDanId],[CaiGouDanId],[GysId]
			,[GysName],[Status],[JinE]
			,[SongHuoRenName],[SongHuoRenDianHua],[SongHuoTime]
			,[DaoHuoTime],[GysBaoJiaRenId],[GysBaoJiaTime]
			,[CgsQueRenRenId],[CgsQueRenTime],[GysFaHuoRenId]
			,[GysFaHuoTime],[CgsShouHuoRenId],[CgsShouHuoTime]
			,[GysDaoHuoQueRenStatus],[GysDaoHuoQueRenRenId],[GysDaoHuoQueRenTime]
			,[QuXiaoRenId],[QuXiaoTime],[GysFaHuoShuoMing])
		SELECT A.[DingDanId],@CaiGouDanId,A.[GysId]
			,B.Name,A.[Status],A.[JinE]
			,'','',NULL
			,NULL,'',NULL
			,'',NULL,''
			,NULL,'',NULL
			,0,'',NULL
			,'',NULL,''
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([DingDanId] CHAR(36),[GysId] CHAR(36),[Status] INT,[JinE] MONEY) AS A
		INNER JOIN tbl_GongSi AS B ON A.GysId=B.GongSiId
		EXEC sp_xml_removedocument @hdoc
	END
	
	IF(@ChanPinXml IS NOT NULL AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		INSERT INTO @TEMP_TABLE([MingXiId],[DingDanId],[ChanPinId],[ShuLiang],[GysId],ChanPinJiaGe1)
		SELECT A.[MingXiId],A.[DingDanId],A.[ChanPinId],A.[ShuLiang],B.GysId,0
		FROM OPENXML(@hdoc,'/root/info',3)
		WITH([MingXiId] CHAR(36),[DingDanId] CHAR(36),[ChanPinId] CHAR(36),[ShuLiang] MONEY) AS A
		INNER JOIN tbl_ChanPin AS B ON A.[ChanPinId]=B.ChanPinId
		EXEC sp_xml_removedocument @hdoc
		
		DECLARE @i INT
		SET @i=1
		SELECT @JiShuQi=COUNT(*) FROM @TEMP_TABLE
		
		WHILE(@i<=@JiShuQi)
		BEGIN
			SELECT @ChanPinId=ChanPinId,@GysId=GysId FROM @TEMP_TABLE WHERE IdentityId=@i
			SET @ChanPinJiaGe1=0
			
			SELECT TOP 1 @ChanPinJiaGe1=ChanPinJiaGe FROM tbl_DingDanChanPinJiaGe WHERE GysId=@GysId AND CgsId=@CgsId AND ChanPinId=@ChanPinId ORDER BY IdentityId DESC
		
			SET @ChanPinJiaGe1=ISNULL(@ChanPinJiaGe1,0)	
			
			UPDATE @TEMP_TABLE SET ChanPinJiaGe1=@ChanPinJiaGe1 WHERE IdentityId=@i
			
			SET @i=@i+1
		END		
		
		INSERT INTO [tbl_DingDanChanPin]([MingXiId],[CaiGouDanId],[DingDanId]
			,[ChanPinId],[ChanPinName],[ChanPinGuiGe]
			,[JiLiangDanWei],[ShuLiang],[ChanPinJiaGe]
			,[JinE],[FaHuoShuLiang],[DaoHuoShuLiang]
			,[CgsDaoHuoShuoMing],[GysBaoJiaShuoMing],[ChanPinJiaGe1]
			,[ChanPinPinPai])
		SELECT A.[MingXiId],@CaiGouDanId,A.[DingDanId]
			,A.[ChanPinId],B.Name,B.GuiGe
			,B.JiLiangDanWei,A.[ShuLiang],A.ChanPinJiaGe1
			,0,0,0
			,'','',A.ChanPinJiaGe1
			,B.PinPai
		FROM @TEMP_TABLE AS A INNER JOIN tbl_ChanPin AS B ON A.[ChanPinId]=B.ChanPinId	
	END
	
	UPDATE [tbl_DingDanChanPin] SET [JinE]=[ChanPinJiaGe]*[ShuLiang] WHERE [CaiGouDanId]=@CaiGouDanId
	UPDATE tbl_DingDan SET JinE=ISNULL((SELECT SUM(A1.JinE) FROM [tbl_DingDanChanPin] AS A1 WHERE A1.DingDanId=tbl_DingDan.DingDanId),0) WHERE [CaiGouDanId]=@CaiGouDanId	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO
--以上日志已更新 汪奇志 06 16 2015  2:02PM
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-04-24
-- Description:	设置订单收货信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_DingDan_SheZhiShouHuo]
	@DingDanId CHAR(36)
	,@ShouHuoTime DATETIME
	,@ChanPinXml NVARCHAR(MAX)
	,@ShouHuoRen NVARCHAR(255)
	,@RetCode INT OUTPUT
AS
BEGIN
	DECLARE @hdoc INT
	DECLARE @JinE MONEY
	SET @RetCode=0
	
	UPDATE [tbl_DingDan] SET [DaoHuoTime]=@ShouHuoTime,[CgsShouHuoRen]=@ShouHuoRen
	WHERE [DingDanId]=@DingDanId
	
	IF(@ChanPinXml IS NOT NULl AND LEN(@ChanPinXml)>0)
	BEGIN
		EXEC sp_xml_preparedocument @hdoc OUTPUT,@ChanPinXml
		UPDATE tbl_DingDanChanPin SET DaoHuoShuLiang=B.DaoHuoShuLiang
			,CgsDaoHuoShuoMing=B.CgsDaoHuoShuoMing
		FROM tbl_DingDanChanPin AS A INNER JOIN(
			SELECT * 
			FROM OPENXML(@hdoc,'/root/info',3)
			WITH(MingXiId CHAR(36),DaoHuoShuLiang MONEY,CgsDaoHuoShuoMing NVARCHAR(MAX))
		)B ON A.MingXiId=B.MingXiId
		WHERE A.DingDanId=@DingDanId
		EXEC sp_xml_removedocument @hdoc
	END
	
	UPDATE tbl_DingDanChanPin SET JinE=DaoHuoShuLiang*ChanPinJiaGe WHERE DingDanId=@DingDanId
	SELECT @JinE=ISNULL(SUM(JinE),0) FROM tbl_DingDanChanPin WHERE DingDanId=@DingDanId
	UPDATE tbl_DingDan SET JinE=@JinE WHERE DingDanID=@DingDanId
	
	SET @RetCode=1
	RETURN @RetCode
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2015-06-12
-- Description:	用户审核
-- =============================================
ALTER PROCEDURE [dbo].[proc_YongHu_ShenHe]
	@GongSiId CHAR(36)
	,@YongHuId CHAR(36)
	,@ShenHeStatus INT
	,@ShenHeTime DATETIME
	,@ShenHeRenId CHAR(36)
	,@RetCode INT OUTPUT
AS
BEGIN
	SET @RetCode=0
	IF NOT EXISTS(SELECT 1 FROM tbl_GongSi WHERE GongSiId=@GongSiId)
	BEGIN
		SET @RetCode=-99
		RETURN @RetCode
	END
	
	IF(@YongHuId IS NULL OR LEN(@YongHuId)<>36)
	BEGIN
		SELECT TOP 1 @YongHuId=YongHuId FROM tbl_YongHu WHERE GongSiId=@GongSiId AND LaiYuan=1
	END
	
	IF NOT EXISTS(SELECT 1 FROM tbl_YongHu WHERE YongHuId=@YongHuId AND GongSiId=@GongSiId)
	BEGIN
		SET @RetCode=-98
		RETURN @RetCode
	END
	
	UPDATE tbl_GongSi SET ShenHeStatus=@ShenHeStatus,ShenHeTime=@ShenHeTime,@ShenHeRenId=@ShenHeRenId WHERE GongSiId=@GongSiId
	UPDATE tbl_YongHu SET ShenHeStatus=@ShenHeStatus WHERE YongHuId=@YongHuId	
	
	SET @RetCode=1
	RETURN @RetCode
END
GO
--以上日志已更新 汪奇志 06 24 2015  4:26PM
GO
