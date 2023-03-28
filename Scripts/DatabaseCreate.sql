USE [LocationServices]
GO

/****** Drop Functions ******/
DROP FUNCTION [dbo].[GetNearestId]
GO

/****** Drop Tables ******/
ALTER TABLE [dbo].[LuklaLocation] DROP CONSTRAINT [FK_Locations_Locations]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LuklaLocation]') AND type in (N'U'))
DROP TABLE [dbo].[LuklaLocation]
GO

/****** Create Tables ******/
CREATE TABLE [dbo].[LuklaLocation](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[GeoJson] [varchar](max) NULL,
	[Geometry] [geometry] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[LuklaLocation]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[LuklaLocation] ([LocationId])
GO

ALTER TABLE [dbo].[LuklaLocation] CHECK CONSTRAINT [FK_Locations_Locations]
GO

/****** Create Functions ******/
CREATE FUNCTION [dbo].[GetNearestId] (@geom geometry, @dist DECIMAL(10, 5))
RETURNS TABLE
AS
RETURN
	SELECT TOP 1 *
	FROM LuklaLocation a
	WHERE geometry::STGeomFromText(@geom.STAsText(), 4326).STDistance(a.Geometry) < @dist
	ORDER BY geometry::STGeomFromText(@geom.STAsText(), 4326).STDistance(a.Geometry) ASC
GO