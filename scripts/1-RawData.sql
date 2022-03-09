ALTER TABLE edm.tb_controle_mensagem 
ADD es_processamento2 INTEGER;
UPDATE edm.tb_controle_mensagem 
SET es_processamento2 = es_processamento;
ALTER TABLE edm.tb_controle_mensagem
DROP COLUMN es_processamento;
ALTER TABLE edm.tb_controle_mensagem
RENAME COLUMN es_processamento2 TO es_processamento;

CREATE TABLE IF NOT EXISTS edm.TB_RAW_DATA_EVENTOS_PROCESSADOS(
	PK_EVENTOS_PROCESSADOS SERIAL NOT null PRIMARY KEY,
	CD_SNA VARCHAR(20),
	CD_SNA_HASH BYTEA,
	TP_DADO SMALLINT,
	TX_JSON JSONB,
	DT_CRIACAO TIMESTAMP,
	NR_RANK bigint
);