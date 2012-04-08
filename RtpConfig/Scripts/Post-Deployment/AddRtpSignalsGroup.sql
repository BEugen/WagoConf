/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'1 шлаковый 1ст.', 1, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'1 штейновый 1ст.', 2, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'2 шлаковый 1ст.', 3, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'2 штейновый 1ст.', 4, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'3 шлаковый 1ст.', 5, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'3 штейновый 1ст.', 6, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'4 шлаковый 1ст.', 7, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'4 штейновый 1ст.', 8, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'5 шлаковый 1ст.', 9, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'5 штейновый 1ст.', 10, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'6 шлаковый 1ст.', 11, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'6 штейновый 1ст.', 12, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'6 штейновый 2ст.', 13, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'6 шлаковый 2ст.', 14, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'5 штейновый 2ст.', 15, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'5 шлаковый 2ст.', 16, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'4 штейновый 2ст.', 17, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'4 шлаковый 2ст.', 18, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'3 штейновый 2ст.', 19, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'3 шлаковый 2ст.', 20, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'2 штейновый 2ст.', 21, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'2 шлаковый 2ст.', 22, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'1 штейновый 2ст.', 23, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'1 шлаковый 2ст.', 24, 1)
go

insert into RtpSignalsGroup (
signalgroupdescription,
	signalattrnumber,
	signalgroup)
values (N'Общие сигналы', 25, 2)
go

