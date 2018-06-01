CREATE TABLE [dbo].[TBApostaBolao]
(
	[ApostaId] INT NULL, 
    [BolaoId] INT NULL,
	CONSTRAINT [FK_TBApostaBolao_TBAposta] FOREIGN KEY (ApostaId) REFERENCES TBAposta(Id),
	CONSTRAINT [FK_TBApostaBolao_TBBolao] FOREIGN KEY (BolaoId) REFERENCES TBBolao(Id)
)
