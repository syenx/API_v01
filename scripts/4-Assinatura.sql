CREATE TABLE IF NOT EXISTS edm.TB_ASSINATURA(
    PK_ASSINATURAS SERIAL NOT null PRIMARY KEY,
    CD_SNA VARCHAR(20),
    CD_SNA_HASH BYTEA,
    DT_ASSINATURA TIMESTAMP,
    DT_ATUALIZACAO TIMESTAMP,
    ES_ASSINADO BOOLEAN
);

CREATE TABLE IF NOT EXISTS edm.TB_ASSINATURA_LOG(
	PK_ASSINATURAS_LOG SERIAL NOT null PRIMARY KEY,
	FK_ASSINATURAS INTEGER REFERENCES edm.TB_ASSINATURA(PK_ASSINATURAS),
	DT_CRIACAO TIMESTAMP,
	ES_ASSINADO BOOLEAN,
	CD_CGE VARCHAR(20)
);