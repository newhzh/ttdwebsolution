--贷款申请记录状态
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCaseState', '0', '申请状态', '初始', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCaseState', '1', '申请状态', '已发送', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCaseState', '2', '申请状态', '已处理', null, null);

--产品类型 0车贷，1房贷，2企业贷款，3消费贷款，4信用卡，5保险
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sProductType', '0', '产品类型', '车贷', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sProductType', '1', '产品类型', '房贷', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sProductType', '2', '产品类型', '企业贷款', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sProductType', '3', '产品类型', '消费贷款', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sProductType', '4', '产品类型', '信用卡', null, null);
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sProductType', '5', '产品类型', '保险', null, null);


--车贷-房产
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarProperty', '0', '车贷-房产', '没有本地商品房', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarProperty', '1', '车贷-房产', '本人名下有本地商品房（有房产证）', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarProperty', '2', '车贷-房产', '父母、配偶名下有本地商品房（有房产证）', null, null);

--车贷-购车阶段
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarPurchasingPeriod', '0', '购车阶段', '未选好车型', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarPurchasingPeriod', '1', '购车阶段', '准备去4S店看车', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sCarPurchasingPeriod', '2', '购车阶段', '已经去4S店看过车', null, null);

--企业-公司类型
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmType', '0', '请问您的公司类型', '无固定职业', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmType', '1', '请问您的公司类型', '私营企业', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmType', '2', '请问您的公司类型', '公务员/事业单位', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmType', '3', '请问您的公司类型', '国企/上市企业', null, null);

--企业-经营年限
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmAge', '0', '经营年限', '不足半年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmAge', '1', '经营年限', '1年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmAge', '2', '经营年限', '2年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmAge', '3', '经营年限', '3年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmAge', '4', '经营年限', '4年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmAge', '5', '经营年限', '5年（及以上）', null, null);

--企业-房产
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmProperty', '0', '是否有本地商品房', '没有本地商品房', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sFirmProperty', '1', '是否有本地商品房', '本人名下有本地商品房（有房产证）', null, null);

--个人-职业
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslEmployment', '0', '请问您的公司类型', '无固定职业', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslEmployment', '1', '请问您的公司类型', '私营企业', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslEmployment', '2', '请问您的公司类型', '公务员/事业单位', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslEmployment', '3', '请问您的公司类型', '国企/上市企业', null, null);

--个人-工作时间
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '0', '您的工作时间', '无工作经验', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '1', '您的工作时间', '1年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '2', '您的工作时间', '2年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '3', '您的工作时间', '3年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '4', '您的工作时间', '4年', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '5', '您的工作时间', '5年以上', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslWorkingAge', '10', '您的工作时间', '10年以上', null, null);


--个人-公资发放
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslSalaryType', '0', '工资发放形式', '公司发到银行工资卡', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslSalaryType', '1', '工资发放形式', '领取现金', null, null);

--个人-是否有信用卡
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslCreditOwner', '0', '您是否有信用卡', '有', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslCreditOwner', '1', '您是否有信用卡', '没有', null, null);

--个人-信用卡是否负债
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslCardPaid', '0', '是否有负债（信用卡）', '有', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslCardPaid', '1', '是否有负债（信用卡）', '没有', null, null);

--个人-是否成功贷款
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslLoanSucc', '0', '您之前是否成功申请贷款', '有', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslLoanSucc', '1', '您之前是否成功申请贷款', '没有', null, null);

--个人-贷款是否负债
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslLoan', '0', '是否有负债（贷款）', '有', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sPerslLoan', '1', '是否有负债（贷款）', '没有', null, null);


--房贷-房屋类型
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseType', '0', '房屋类型', '商品房', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseType', '1', '房屋类型', '商铺', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseType', '2', '房屋类型', '写字楼', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseType', '3', '房屋类型', '房改房', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseType', '4', '房屋类型', '经济适用房', null, null);

--房贷-户籍
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseLocalorNot', '0', '户籍', '本地', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseLocalorNot', '1', '户籍', '外地', null, null);

--房贷-新旧房
insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseNew', '0', '是否二手房', '新房', null, null);

insert into AA10 (AAA100, AAA102, AAA101, AAA103, AAA104, AAA105)
values ('sHouseNew', '1', '是否二手房', '二手房', null, null);


