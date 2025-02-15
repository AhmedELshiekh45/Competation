USE [CompitionDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdateSerialNumberAndDateTime]    Script Date: 2/4/2025 1:20:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[UpdateSerialNumberAndDateTime]
    @Id NVARCHAR(50),       -- Input: ID of the record to update
    @NewSerialNumber INT OUTPUT  -- Output parameter to return the new serial number
AS
BEGIN
    SET NOCOUNT ON;

    -- Fetch the current SerialNumber and increment it by 1
    SELECT @NewSerialNumber = ISNULL(SerialNumber, 0) + 1
    FROM PartRegistrations
    WHERE Id = @Id;

    -- Update the record with the new SerialNumber
    UPDATE PartRegistrations
    SET SerialNumber = @NewSerialNumber
    WHERE Id = @Id;
END;
GO


