USE [WBCPayments];  
GO  

EXEC sp_addrolemember N'db_datareader', N'user_wtcpayments';
EXEC sp_addrolemember N'db_datawriter', N'user_wtcpayments';