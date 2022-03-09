CREATE OR REPLACE FUNCTION PR_VERIFICA_BPO() RETURNS int
LANGUAGE plpgsql  
AS $$
BEGIN 
SET TIMEZONE = 'America/Sao_Paulo';
	IF date_part('dow', CURRENT_DATE) in (1,2,3,4,5) THEN
		RAISE NOTICE 'Dia da Semana';
		IF date_part('hour', CURRENT_TIME) = 22 THEN
			IF (select count(*) FROM edm.TB_CONTROLE_MENSAGEM WHERE dh_processamento::DATE = CURRENT_DATE) > 0 THEN
				RAISE NOTICE 'Mensagens Processadas';
				Return 1;
			ELSE 
				RAISE NOTICE 'Mensagens Nao Processadas';
				Return 0;
			END IF;
		END IF;
  END IF;
  Return 1;
END; $$