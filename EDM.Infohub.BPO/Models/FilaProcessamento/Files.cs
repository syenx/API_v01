using Dapper.Contrib.Extensions;
using System;

namespace EDM.Infohub.BPO
{
    public class Files
    {
        public int PK_Files { get; set; }
        public int ID_Processamento { get; set; }
        public DateTime DT_Criacao { get; set; }
        public DateTime DT_Ultima_Atualizacao { get; set; }
    }
    [Table("edm.tb_files")]
    public class FilesDAO
    {
        public int pk_files { get; set; }
        public int id_processamento { get; set; }
        public DateTime dt_criacao { get; set; }
        public DateTime dt_ultima_atualizacao { get; set; }
    }
}