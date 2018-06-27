DBCC CHECKIDENT (TM_Order, RESEED, 0)

-- -- Sample data for TM_ParamType
DBCC CHECKIDENT (TM_ParamType, RESEED, 0)
insert into TM_ParamType(TypeName)
Values(N'Lâm sàng')

insert into TM_ParamType(TypeName)
Values(N'Cận lâm sàng')

-- Sample data for TM_MeasureParam
DBCC CHECKIDENT (TM_MeasureParam, RESEED, 0) -- reset autoincrement number
insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values('Height',N'Chiều cao','cm',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values('Weight',N'Cân nặng','kg',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values('LowPressure',N'Huyết áp tâm thu','mmHg',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values('HighPressure',N'Huyết áp tâm trương','mmHg',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values('HeartBeat',N'Nhịp tim',N'Lần/phút',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values(N'Cholesteron',N'Cholesteron','mmHg',2,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values(N'Insulin',N'Insulin','mmHg',2,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Description,Unit,Type,CreatedDate,Status)
Values(N'GPT',N'GPT','mmHg',2,GETDATE(),1)

