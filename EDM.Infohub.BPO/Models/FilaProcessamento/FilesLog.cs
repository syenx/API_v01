using Dapper.Contrib.Extensions;
using System;

namespace EDM.Infohub.BPO
{
    public class FilesLog
    {
        public int PK_Files_Log { get; set; }
        public int FK_Files { get; set; }
        public int FK_Status { get; set; }
        public DateTime DT_Criacao { get; set; }
        public string TX_Message_Error { get; set; }
    }
    [Table("edm.tb_files_log")]
    public class FilesLogDAO
    {
        public int pk_files_log { get; set; }
        public int fk_files { get; set; }
        public int fk_status { get; set; }
        public DateTime dt_criacao { get; set; }
        public string tx_message_error { get; set; }
    }
}