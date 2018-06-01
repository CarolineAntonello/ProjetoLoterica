CREATE TABLE [dbo].[TBFaturamentoLoterica]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Lucro] DECIMAL(18, 2) NOT NULL, 
    [Faturamento] DECIMAL(18, 2) NOT NULL, 
    [ConcursoId] INT NOT NULL, 
    CONSTRAINT [FK_TBFaturamentoLoterica_TBConcurso] FOREIGN KEY (ConcursoId) REFERENCES TBConcurso(Id),
)
