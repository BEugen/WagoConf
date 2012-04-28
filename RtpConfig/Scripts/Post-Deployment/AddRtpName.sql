-- =============================================
-- Script Template
-- =============================================

INSERT [dbo].[RtpName] ([plcName], [plcNumber], [plcType], [changegroupconfig], [changehardware], [changeshiberconfig], [changesingleconfig], [rtpdescr], [rtpid])
VALUES(N'RTP3', 1, N'Wago 750-841', 0, 0, 0, 0, N'РТП3', 0)

INSERT [dbo].[RtpName] ([plcName], [plcNumber], [plcType], [changegroupconfig], [changehardware], [changeshiberconfig], [changesingleconfig], [rtpdescr], [rtpid])
VALUES(N'RTP4', 2, N'Wago 750-841', 0, 0, 0, 0, N'РТП4', 1)
go

INSERT [dbo].[ShangeStore]([datetimestore], [countchange])
VALUES(GETDATE(), 0)
go