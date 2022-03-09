--Ajuste assinaturas duplicadas em produção
delete from edm.tb_assinatura_log where fk_assinaturas in (5, 12, 14, 1318, 1382, 1407, 1408, 1410, 1411, 1414, 1416, 1418, 1420, 1422);
delete from edm.tb_assinatura where pk_assinaturas in (5, 12, 14, 1318, 1382, 1407, 1408, 1410, 1411, 1414, 1416, 1418, 1420, 1422);
	
--Criação da tabela de Rastreamento
CREATE TABLE if not exists log.tb_rastreamento
(
    pk_tb_rastreamento SERIAL, 
    cd_sna character varying(30),
    dh_evento timestamp without time zone,
    dh_rank bigint,
    en_tipo log.en_tipo_log,
    en_status log.en_status_mensagem,
    tx_erro character varying,
    nm_login_usuario character varying(100),
    PRIMARY KEY (pk_tb_rastreamento)
);

ALTER TABLE log.tb_rastreamento
    OWNER to ledm;