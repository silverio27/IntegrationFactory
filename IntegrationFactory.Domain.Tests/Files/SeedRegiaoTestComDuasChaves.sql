drop table if exists RegiaoComDuasChaves;


CREATE TABLE [dbo].[RegiaoComDuasChaves](
	[Identidade] [int] NOT NULL,
	[Sigla] [varchar](100) NOT NULL,
	[NomeDaRegiao] [varchar](100) NULL,
PRIMARY KEY (Identidade, Sigla));

insert into RegiaoComDuasChaves (Identidade, Sigla, NomeDaRegiao) 
values 
(1,'MG','Minas Gerais'),
(2,'SP','São Paulo'),
(3,'BA','Bahia'),
(4,'CA','California');