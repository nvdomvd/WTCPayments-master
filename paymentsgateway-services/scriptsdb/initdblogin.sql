USE [master]
GO
CREATE LOGIN [user_wtcpayments] WITH PASSWORD=N'test123!', DEFAULT_DATABASE=[WBCPayments], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO