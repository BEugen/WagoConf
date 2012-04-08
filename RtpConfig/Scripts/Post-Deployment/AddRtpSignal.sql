-- =============================================
-- Script Template
-- =============================================


insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'закрыт', 0, 1, 0, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'авто/ручной', 0, 1, 1, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'открыт', 0, 1, 2, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'открыть/закрыть', 1, 1, 3, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'Работа от батарей', 0, 2, 1, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'Калитка открыта', 0, 2, 2, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'РТП в работе', 0, 2, 3, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'Выход ошибки', 1, 2, 4, 1)
go

insert into RtpSignals (signaldescription,
	signalcontrain,
	signalgroup,
	signaltype,
	signalattribute)
values (N'Индикация автомат', 1, 2, 5, 1)
go