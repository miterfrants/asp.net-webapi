CREATE TABLE [dbo].[members] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [email]      NVARCHAR (512) NOT NULL,
    [password]   NVARCHAR (512) NOT NULL,
    [created_at] DATETIME       CONSTRAINT [DF_members_created_at] DEFAULT (getdate()) NOT NULL,
    [updated_at] DATETIME       NULL,
    [deleted_at] DATETIME       NULL,
    [login_at]   DATETIME       NULL,
    [salt] NVARCHAR(512) NOT NULL, 
    CONSTRAINT [PK_members] PRIMARY KEY CLUSTERED ([id] ASC)
);

