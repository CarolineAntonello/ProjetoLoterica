CREATE TABLE [dbo].[TBBolao]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ValorTotalApostasBolao] DECIMAL(18, 2) NOT NULL, 
    [ConcursoId] INT NOT NULL,
	CONSTRAINT [FK_TBBolao_TBConcurso] FOREIGN KEY (ConcursoId) REFERENCES TBConcurso(Id)
)
