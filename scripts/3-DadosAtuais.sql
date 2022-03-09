CREATE TABLE IF NOT EXISTS edm.TB_FLUXOS(
	PK_FLUXOS SERIAL NOT null PRIMARY KEY,
	CD_SNA VARCHAR(20),
	CD_SNA_HASH BYTEA,
	TP_PAPEL SMALLINT,
	DT_BASE TIMESTAMP,
	DT_CRIACAO TIMESTAMP,
	DT_ATUALIZACAO TIMESTAMP,
	ES_ATIVO BOOLEAN,
	DT_LIQUIDACAO TIMESTAMP,
	TP_EVENTO VARCHAR(20),
	VL_TAXA DOUBLE PRECISION,
	VL_AMORTIZACAO DOUBLE PRECISION
);

CREATE TABLE IF NOT EXISTS edm.TB_DADOS_CARACTERISTICOS(
	PK_DADOS_CARACTERISTICOS SERIAL NOT null PRIMARY KEY,
	CD_SNA VARCHAR(20),
	CD_SNA_HASH BYTEA,
	TP_EVENTO SMALLINT,
	DT_CRIACAO TIMESTAMP,
	DT_ATUALIZACAO TIMESTAMP,
	ES_ATIVO BOOLEAN,
	TP_PAPEL VARCHAR(20),
	CD_ISIN VARCHAR(20),
	TX_EMISSOR VARCHAR(100),
	TX_CNPJ_EMISSOR VARCHAR(18),
	DT_EMISSAO TIMESTAMP,
	DT_INICIO_RENTABILIDADE TIMESTAMP,
	DT_VENCIMENTO TIMESTAMP,
	VL_NOMINAL_EMISSAO DOUBLE PRECISION,
	TX_INSTRUCAO_CVM VARCHAR(100),
	TP_CLEARING VARCHAR(20),
	TX_AGENTE_FIDUCIARIO VARCHAR(100),
	ES_POSSIBILIDADE_RESGATE_ANTECIPADO BOOLEAN,
	ES_CONVERSIVEL_ACAO BOOLEAN,
	ES_DEBENTURE_INCENTIVADA BOOLEAN,
	TP_CRITERIO_CALCULO_INDEXADOR VARCHAR(20),
	TP_CRITERIO_CALCULO_JUROS VARCHAR(20),
	TP_INDEXADOR VARCHAR(20),
	VL_TAXA_PRE DOUBLE PRECISION,
	VL_TAXA_POS DOUBLE PRECISION,
	TX_PROJECAO VARCHAR(20),
	TP_AMORTIZACAO VARCHAR(20),
	TP_PERIODICIDADE_CORRECAO VARCHAR(20),
	TP_UNIDADE_INDEXADOR VARCHAR(20),
	VL_DEFASAGEM_INDEXADOR INTEGER,
	DI_REFERENCIA_INDEXADOR SMALLINT,
	ME_REFERENCIA_INDEXADOR SMALLINT,
	TX_DEVEDOR  VARCHAR(100),
	TP_REGIME VARCHAR(20),
	TP_ANIVERSARIO VARCHAR(20),
	ES_CONSIDERA_DEFLACAO BOOLEAN
);

CREATE TABLE IF NOT EXISTS edm.TB_PRECOS(
	PK_PRECOS SERIAL NOT null PRIMARY KEY,
	CD_SNA VARCHAR(20),
	CD_SNA_HASH BYTEA,
	TP_EVENTO SMALLINT,
	DT_CRIACAO TIMESTAMP,
	DT_ATUALIZACAO TIMESTAMP,
	ES_ATIVO BOOLEAN,
	TP_PAPEL VARCHAR(20),
	TP_INDEXADOR VARCHAR(20),
	VL_TAXA_POS DOUBLE PRECISION,
	VL_TAXA_PRE DOUBLE PRECISION,
	DT_EVENTO TIMESTAMP,
	VL_NOMINAL_BASE DOUBLE PRECISION,
	VL_NOMINAL_ATUALIZADO DOUBLE PRECISION,
	VL_FATOR_CORRECAO DOUBLE PRECISION,
	VL_FATOR_JUROS DOUBLE PRECISION,
	VL_PU_ABERTURA DOUBLE PRECISION,
	VL_PAGAMENTOS DOUBLE PRECISION,
	VL_PU_FECHAMENTO DOUBLE PRECISION,
	VL_PRINCIPAL DOUBLE PRECISION,
	VL_INFLACAO DOUBLE PRECISION,
	VL_JUROS DOUBLE PRECISION,
	VL_INCORPORADO DOUBLE PRECISION,
	VL_INCORPORAR INTEGER,
	VL_AMORTIZACAO DOUBLE PRECISION,
	VL_AMEX DOUBLE PRECISION,
	VL_VENCIMENTO DOUBLE PRECISION,
	VL_PREMIO DOUBLE PRECISION
);