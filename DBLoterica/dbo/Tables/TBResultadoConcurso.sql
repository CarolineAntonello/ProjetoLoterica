CREATE TABLE [dbo].[TBResultadoConcurso]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PremioTotal] DECIMAL(18, 2) NOT NULL, 
    [PremioQuadraPorJogador] DECIMAL(18, 2) NOT NULL, 
    [PremioQuinaPorJogador] DECIMAL(18, 2) NOT NULL, 
    [PremioSenaPorJogador] DECIMAL(18, 2) NOT NULL, 
    [QtdAcertadoresQuadra] INT NOT NULL, 
    [QtdAcertadoresQuina] INT NOT NULL, 
    [QtdAcertadoresSena] INT NOT NULL, 
    [NumerosResultado] NCHAR(20) NOT NULL, 
    [ConcursoId] INT NOT NULL,
	CONSTRAINT [FK_TBResultadoConcurso_TBConcurso] FOREIGN KEY (ConcursoId) REFERENCES TBConcurso(Id)
)
