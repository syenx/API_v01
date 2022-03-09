using Dapper.Contrib.Extensions;

namespace EDM.Infohub.BPO
{
    public class Status
    {
        public int PK_Status { get; set; }
        public bool ES_Status { get; set; }
        public string TP_Status_Name { get; set; }
    }
    [Table("edm.tb_status")]
    public class StatusDAO
    {
        public int pk_status { get; set; }
        public bool es_status { get; set; }
        public string tp_status_name { get; set; }
    }
}