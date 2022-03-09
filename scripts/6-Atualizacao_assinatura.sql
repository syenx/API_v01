ALTER TABLE edm.tb_assinatura
ADD COLUMN IF NOT EXISTS impacta_mdp boolean;

UPDATE edm.tb_assinatura
SET impacta_mdp = false
WHERE impacta_mdp is null;
	
ALTER TABLE edm.tb_assinatura_log
ADD COLUMN IF NOT EXISTS usuario varchar(50);

UPDATE edm.tb_assinatura_log
SET usuario = 'BPOUSER'
WHERE usuario is null;

ALTER TABLE edm.tb_assinatura_log
add column if not exists tx_estado jsonb;