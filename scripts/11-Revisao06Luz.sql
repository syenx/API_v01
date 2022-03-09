ALTER TABLE edm.tb_dados_caracteristicos 
ADD COLUMN IF NOT EXISTS dt_ultima_alteracao timestamp;

UPDATE edm.tb_dados_caracteristicos
SET dt_ultima_alteracao = '2020-12-14 00:00'
WHERE dt_ultima_alteracao is null;

ALTER TABLE edm.tb_precos 
ADD COLUMN IF NOT EXISTS status_pgto varchar(50),
ADD COLUMN IF NOT EXISTS dt_att_status_pgto timestamp;