ALTER TABLE edm.tb_precos ADD COLUMN IF NOT EXISTS VL_PAGAMENTO_JUROS DOUBLE PRECISION;
ALTER TABLE edm.tb_precos ADD COLUMN IF NOT EXISTS VL_PORCENTUAL_AMORTIZADO DOUBLE PRECISION;
ALTER TABLE edm.tb_precos ADD COLUMN IF NOT EXISTS VL_PORCENTUAL_JUROS_INCORPORADO DOUBLE PRECISION;