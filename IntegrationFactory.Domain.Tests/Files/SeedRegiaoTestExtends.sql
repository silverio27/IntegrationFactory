drop table if exists RegiaoTestExtends;

CREATE TABLE [dbo].[RegiaoTestExtends](
    	[Identidade] [int] NOT NULL,
    	[Sigla] [varchar](100) NULL,
    	[NomeDaRegiao] [varchar](100) NULL,
        [ColumnExtend] [bit]
    PRIMARY KEY CLUSTERED 
    (
    	[Identidade] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]

insert into RegiaoTestExtends (Identidade, Sigla, NomeDaRegiao, ColumnExtend) 
values 
(1,'MG','Minas Gerais', 'True'),
(2,'SP','São Paulo', 'True'),
(3,'BA','Bahia', 'True'),
(4,'CA','California', 'False');