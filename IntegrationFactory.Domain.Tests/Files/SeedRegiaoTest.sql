drop table if exists RegiaoTest;

CREATE TABLE [dbo].[RegiaoTest](
    	[Identidade] [int] NOT NULL,
    	[Sigla] [varchar](100) NULL,
    	[NomeDaRegiao] [varchar](100) NULL,
    PRIMARY KEY CLUSTERED 
    (
    	[Identidade] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]

insert into RegiaoTest (Identidade, Sigla, NomeDaRegiao) 
values 
(1,'MG','Minas Gerais'),
(2,'SP','São Paulo'),
(3,'BA','Bahia'),
(4,'CA','California');