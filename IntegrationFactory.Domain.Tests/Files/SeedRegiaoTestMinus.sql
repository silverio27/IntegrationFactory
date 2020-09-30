drop table if exists RegiaoTestMinus;

CREATE TABLE [dbo].[RegiaoTestMinus](
    	[Identidade] [int] NOT NULL,
    	[Sigla] [varchar](100) NULL
    PRIMARY KEY CLUSTERED 
    (
    	[Identidade] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
