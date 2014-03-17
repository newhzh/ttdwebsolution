--贷款申请记录状态
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCaseState', '0', '申请状态', '初始', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCaseState', '1', '申请状态', '已发送', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCaseState', '2', '申请状态', '已处理', null, null);


--车贷-房产
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarProperty', '0', '车贷-房产', '没有本地商品房', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarProperty', '1', '车贷-房产', '本人名下有本地商品房（有房产证）', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarProperty', '2', '车贷-房产', '父母、配偶名下有本地商品房（有房产证）', null, null);
