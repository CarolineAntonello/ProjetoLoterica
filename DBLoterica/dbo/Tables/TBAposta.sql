CREATE TABLE [dbo].[TBAposta]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [NumerosApostados] VARCHAR(50) NOT NULL, 
    [ConcursoId] INT NOT NULL,
	CONSTRAINT [FK_TBAposta_TBConcurso] FOREIGN KEY (ConcursoId) REFERENCES TBConcurso(Id)
)
