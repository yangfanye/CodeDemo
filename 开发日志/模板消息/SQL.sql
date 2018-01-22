drop table if exists V_TemplateMessageField;

/*==============================================================*/
/* Table: V_TemplateMessageField                                */
/*==============================================================*/
create table V_TemplateMessageField
(
   TFID                 int comment '自增ID',
   TemplateFieldName    varchar(50) comment '模板可选字段显示的名称',
   Type                 int comment '类型:1:代表字段;2:代表URL',
   Content              varchar(200) comment '信息内容'
);

alter table V_TemplateMessageField comment '模板消息可选字段表';

drop table if exists V_TemplateMessageSet;

/*==============================================================*/
/* Table: V_TemplateMessageSet                                  */
/*==============================================================*/
create table V_TemplateMessageSet
(
   ID                   int comment '自增ID',
   BossID               varchar(20) comment '客户编号',
   TemplateType         varchar(50) comment '模板编号',
   TemplateID           varchar(80) comment '模板ID',
   Status               int comment '状态:0:不启用;1:启用;',
   CompetenceLevel      int comment '权限等级:0:全部可用;超出一级根据BossID的权限登记判定',
   URL                  varchar(200) comment '模板URL链接',
   First                varchar(300) comment '公用模板头部',
   Remark               varchar(500) comment '公用模板备注',
   keynote1             varchar(100) comment '模板主体字段1',
   keynote2             varchar(100) comment '模板主体字段2',
   keynote3             varchar(100) comment '模板主体字段3',
   keynote4             varchar(100) comment '模板主体字段4',
   keynote5             varchar(100) comment '模板主体字段5',
   keynote6             varchar(100) comment '模板主体字段6',
   keynote7             varchar(100) comment '模板主体字段7',
   keynote8             varchar(100) comment '模板主体字段8',
   keynote9             varchar(100) comment '模板主体字段9',
   keynote10            varchar(100) comment '模板主体字段10',
   AddTime              datetime comment '添加时间',
   UpdateTime           datetime comment '更新时间',
   OperatorID           varchar(20) comment '操作人',
   IPAddress            varchar(20) comment '最后一次更新用户IP'
);

alter table V_TemplateMessageSet comment '模板消息设置表';

