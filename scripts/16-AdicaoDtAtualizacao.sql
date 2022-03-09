ALTER TABLE edm.TB_ASSINATURA_TIPO_IMPACTO 
ADD COLUMN IF NOT EXISTS dt_atualizacao_flag timestamp;

UPDATE edm.TB_ASSINATURA_TIPO_IMPACTO
SET dt_atualizacao_flag = '2021-01-08 00:00'
WHERE dt_atualizacao_flag is null;
