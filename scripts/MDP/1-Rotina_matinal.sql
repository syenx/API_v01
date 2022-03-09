--ALTERACAO DA PROCEDURE DE CALCULO DAS 04 DA MANHA
IF OBJECT_ID('[EDM].[PR_CONS_ATIVOS_RF_CALCULO_PRICING]', 'P') IS NULL
	EXEC('CREATE PROCEDURE [EDM].[PR_CONS_ATIVOS_RF_CALCULO_PRICING] AS')
GO  
ALTER PROCEDURE [EDM].[PR_CONS_ATIVOS_RF_CALCULO_PRICING] 
@DT_REFERENCIA DATETIME       
AS      
BEGIN
--tabela temporaria para pegar papeis assinados bpo para retirar do calculo
IF OBJECT_ID('tempdb..#tmp_bpo') IS NOT NULL DROP TABLE #tmp_bpo

select *
    into #tmp_bpo
    from EDM.TB_ASSINATURA_BPO b
--alterar o FROM para tabela de assinatura BPO
  WHERE b.ES_ASSINADO = 1
    and b.IMPACTA_PRECO = 1;

--declare @DT_REFERENCIA DATETIME   = '2020-03-20'  
 SELECT RF.COD_ATIVO,         
  COALESCE(COD_CETIP_21, DESCRICAO_ATIVO) AS COD_CLEARING,      
  DATA_VENCIMENTO,         
  COD_GRUPO_CONTABIL      
       FROM ATIVOS_RF RF WITH (NOLOCK)    
    INNER JOIN ATIVOS AT WITH (NOLOCK)   
   ON RF.COD_ATIVO = AT.COD_ATIVO    
       LEFT JOIN DOMINIOEXCECAO DE WITH (NOLOCK)  
   ON DE.IDEXCECAO = RF.IDEXCECAO
     LEFT JOIN #tmp_bpo bpo WITH (NOLOCK)
  on bpo.cod_ativo = rf.cod_ativo 
       WHERE AT.COD_TIPO_ATIVO IN (5,6)    
       AND DATA_VENCIMENTO >= @DT_REFERENCIA        
       AND @DT_REFERENCIA >= DATA_EMISSAO     
       AND (DE.ID_TIPO_EXCECAO <> 722 OR  DE.ID_TIPO_EXCECAO IS NULL) -- 722 - Erro na Curva  
    --remove SNAs fake  
       AND RTRIM(LTRIM(COALESCE(RF.COD_CETIP_21, RF.DESCRICAO_ATIVO))) NOT LIKE '%PACT'   
    AND RTRIM(LTRIM(COALESCE(RF.COD_CETIP_21, RF.DESCRICAO_ATIVO))) NOT LIKE '%XX'  
  --prioriza o cálculo de emissoes BTG  
    and bpo.cod_ativo is null
    --retirando pepeis assinados pelo bpo
  ORDER BY RF.COD_CGE_EMISSOR ASC  
END;