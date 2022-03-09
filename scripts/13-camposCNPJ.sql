ALTER TABLE edm.tb_dados_caracteristicos ADD COLUMN IF NOT EXISTS tx_cnpj_devedor VARCHAR(50);
ALTER TABLE edm.tb_dados_caracteristicos ADD COLUMN IF NOT EXISTS tx_cnpj_agente_fiduciario VARCHAR(50);