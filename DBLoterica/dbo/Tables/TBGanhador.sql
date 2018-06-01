CREATE TABLE [dbo].[TBGanhador]
(
    [TipoPremio] INT NOT NULL, 
    [ValorPremio] DECIMAL(18, 2) NOT NULL, 
    [ApostaId] INT NOT NULL,
	[ResultadoConcursoId] INT NOT NULL, 
    CONSTRAINT [FK_TBGanhador_TBAposta] FOREIGN KEY (ApostaId) REFERENCES TBAposta(Id),
	CONSTRAINT [FK_TBGanhador_TBResultadoConcurso] FOREIGN KEY (ResultadoConcursoId) REFERENCES TBResultadoConcurso(Id)
)
