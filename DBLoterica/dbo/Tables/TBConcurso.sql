CREATE TABLE [dbo].[TBConcurso]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DataFechamento] DATETIME NOT NULL, 
    [ValorTotalApostas] DECIMAL(18, 2) NOT NULL
)
