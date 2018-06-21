-- -- Sample data for TM_ParamType
DBCC CHECKIDENT (TM_ParamType, RESEED, 0)
insert into TM_ParamType(TypeName)
Values(N'Lâm sàng')

insert into TM_ParamType(TypeName)
Values(N'Cận lâm sàng')

-- Sample data for TM_MeasureParam
DBCC CHECKIDENT (TM_MeasureParam, RESEED, 0) -- reset autoincrement number
insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Chiều cao','cm',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Cân nặng','kg',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Huyết áp tâm thu','mmHg',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Huyết áp tâm trương','mmHg',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Nhịp tim',N'Lần/phút',1,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Cholesteron','mmHg',2,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'Insulin','mmHg',2,GETDATE(),1)

insert into TM_MeasureParam(CodeName,Unit,Type,CreatedDate,Status)
Values(N'GPT','mmHg',2,GETDATE(),1)

