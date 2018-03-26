IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND type in (N'U'))
    DROP TABLE [dbo].[Invoice]
GO
CREATE TABLE [dbo].[Invoice] (
[invoice_id] int identity  NOT NULL  
, [invoice_number] varchar(20)  NOT NULL  
, [date_created] datetime  NOT NULL  
, [client_id] varchar(20)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Invoice] ADD CONSTRAINT [Invoice_PK] PRIMARY KEY CLUSTERED (
[invoice_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
    DROP TABLE [dbo].[Role]
GO
CREATE TABLE [dbo].[Role] (
[role_id] varchar(20)  NOT NULL  
, [role_name] varchar(50)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Role] ADD CONSTRAINT [Role_PK] PRIMARY KEY CLUSTERED (
[role_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClientRole]') AND type in (N'U'))
    DROP TABLE [dbo].[ClientRole]
GO
CREATE TABLE [dbo].[ClientRole] (
[client_id] varchar(20)  NOT NULL  
, [role_id] varchar(20)  NOT NULL  
)
GO

ALTER TABLE [dbo].[ClientRole] ADD CONSTRAINT [ClientRole_PK] PRIMARY KEY CLUSTERED (
[client_id]
, [role_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gateway]') AND type in (N'U'))
    DROP TABLE [dbo].[Gateway]
GO
CREATE TABLE [dbo].[Gateway] (
[gateway_id] varchar(20)  NOT NULL  
, [gateway_name] varchar(50)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Gateway] ADD CONSTRAINT [Gateway_PK] PRIMARY KEY CLUSTERED (
[gateway_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payment]') AND type in (N'U'))
    DROP TABLE [dbo].[Payment]
GO
CREATE TABLE [dbo].[Payment] (
[payment_id] int identity  NOT NULL  
, [client_id] varchar(20)  NOT NULL  
, [invoice_id] int  NOT NULL  
, [amount] decimal(10,2)  NOT NULL  
, [currency_id] varchar(20)  NOT NULL  
, [date_created] datetime  NOT NULL  
, [gateway_id] varchar(20)  NOT NULL  
, [payment_status_id] varchar(20)  NOT NULL  
, [description] varchar(200)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [Payment_PK] PRIMARY KEY CLUSTERED (
[payment_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND type in (N'U'))
    DROP TABLE [dbo].[Operation]
GO
CREATE TABLE [dbo].[Operation] (
[operation_id] varchar(20)  NOT NULL  
, [operation_name] varchar(50)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Operation] ADD CONSTRAINT [Operation_PK] PRIMARY KEY CLUSTERED (
[operation_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentStatus]') AND type in (N'U'))
    DROP TABLE [dbo].[PaymentStatus]
GO
CREATE TABLE [dbo].[PaymentStatus] (
[payment_status_id] varchar(20)  NOT NULL  
, [payment_status_name] varchar(50)  NOT NULL  
)
GO

ALTER TABLE [dbo].[PaymentStatus] ADD CONSTRAINT [PaymentStatus_PK] PRIMARY KEY CLUSTERED (
[payment_status_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleOperation]') AND type in (N'U'))
    DROP TABLE [dbo].[RoleOperation]
GO
CREATE TABLE [dbo].[RoleOperation] (
[role_id] varchar(20)  NOT NULL  
, [operation_id] varchar(20)  NOT NULL  
)
GO

ALTER TABLE [dbo].[RoleOperation] ADD CONSTRAINT [RoleOperation_PK] PRIMARY KEY CLUSTERED (
[role_id]
, [operation_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GatewayPaymentData]') AND type in (N'U'))
    DROP TABLE [dbo].[GatewayPaymentData]
GO
CREATE TABLE [dbo].[GatewayPaymentData] (
[payment_id] int  NOT NULL  
, [gateway_reference] varchar(50)  NOT NULL  
, [gateway_response] varchar(2000)  NOT NULL  
, [date_created] datetime  NOT NULL  
)
GO

ALTER TABLE [dbo].[GatewayPaymentData] ADD CONSTRAINT [GatewayPaymentData_PK] PRIMARY KEY CLUSTERED (
[payment_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
    DROP TABLE [dbo].[Currency]
GO
CREATE TABLE [dbo].[Currency] (
[currency_id] varchar(20)  NOT NULL  
, [currency_name] varchar(50)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Currency] ADD CONSTRAINT [Currency_PK] PRIMARY KEY CLUSTERED (
[currency_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
    DROP TABLE [dbo].[Settings]
GO
CREATE TABLE [dbo].[Settings] (
[setting_id] varchar(20)  NOT NULL  
, [value] varchar(100)  NOT NULL  
, [description] varchar(100)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Settings] ADD CONSTRAINT [Settings_PK] PRIMARY KEY CLUSTERED (
[setting_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client]') AND type in (N'U'))
    DROP TABLE [dbo].[Client]
GO
CREATE TABLE [dbo].[Client] (
[client_id] varchar(20)  NOT NULL  
, [client_name] varchar(50)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Client] ADD CONSTRAINT [Client_PK] PRIMARY KEY CLUSTERED (
[client_id]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClientPassword]') AND type in (N'U'))
    DROP TABLE [dbo].[ClientPassword]
GO
CREATE TABLE [dbo].[ClientPassword] (
[client_id] varchar(20)  NOT NULL  
, [password] varchar(100)  NOT NULL  
, [date_created] datetime  NOT NULL  
)
GO

ALTER TABLE [dbo].[ClientPassword] ADD CONSTRAINT [ClientPassword_PK] PRIMARY KEY CLUSTERED (
[client_id]
, [date_created]
)
GO
GO

ALTER TABLE [dbo].[Invoice] WITH CHECK ADD CONSTRAINT [Client_Invoice_FK1] FOREIGN KEY (
[client_id]
)
REFERENCES [dbo].[Client] (
[client_id]
)
GO

GO

ALTER TABLE [dbo].[ClientRole] WITH CHECK ADD CONSTRAINT [Client_ClientRole_FK1] FOREIGN KEY (
[client_id]
)
REFERENCES [dbo].[Client] (
[client_id]
)
ALTER TABLE [dbo].[ClientRole] WITH CHECK ADD CONSTRAINT [Role_ClientRole_FK1] FOREIGN KEY (
[role_id]
)
REFERENCES [dbo].[Role] (
[role_id]
)
GO

GO

ALTER TABLE [dbo].[Payment] WITH CHECK ADD CONSTRAINT [Client_Payment_FK1] FOREIGN KEY (
[client_id]
)
REFERENCES [dbo].[Client] (
[client_id]
)
ALTER TABLE [dbo].[Payment] WITH CHECK ADD CONSTRAINT [PaymentStatus_Payment_FK1] FOREIGN KEY (
[payment_status_id]
)
REFERENCES [dbo].[PaymentStatus] (
[payment_status_id]
)
ALTER TABLE [dbo].[Payment] WITH CHECK ADD CONSTRAINT [Invoice_Payment_FK1] FOREIGN KEY (
[invoice_id]
)
REFERENCES [dbo].[Invoice] (
[invoice_id]
)
ALTER TABLE [dbo].[Payment] WITH CHECK ADD CONSTRAINT [Gateway_Payment_FK1] FOREIGN KEY (
[gateway_id]
)
REFERENCES [dbo].[Gateway] (
[gateway_id]
)
ALTER TABLE [dbo].[Payment] WITH CHECK ADD CONSTRAINT [Currency_Payment_FK1] FOREIGN KEY (
[currency_id]
)
REFERENCES [dbo].[Currency] (
[currency_id]
)
GO

GO

GO

ALTER TABLE [dbo].[RoleOperation] WITH CHECK ADD CONSTRAINT [Role_RoleOperation_FK1] FOREIGN KEY (
[role_id]
)
REFERENCES [dbo].[Role] (
[role_id]
)
ALTER TABLE [dbo].[RoleOperation] WITH CHECK ADD CONSTRAINT [Operation_RoleOperation_FK1] FOREIGN KEY (
[operation_id]
)
REFERENCES [dbo].[Operation] (
[operation_id]
)
GO

ALTER TABLE [dbo].[GatewayPaymentData] WITH CHECK ADD CONSTRAINT [Payment_GatewayPaymentData_FK1] FOREIGN KEY (
[payment_id]
)
REFERENCES [dbo].[Payment] (
[payment_id]
)
GO

GO

GO

GO

ALTER TABLE [dbo].[ClientPassword] WITH CHECK ADD CONSTRAINT [Client_ClientPassword_FK1] FOREIGN KEY (
[client_id]
)
REFERENCES [dbo].[Client] (
[client_id]
)
GO

