using EDM.Infohub.BPO.Models;
using EDM.Infohub.BPO.Models.SQS;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using static Dapper.SqlMapper;

namespace EDM.Infohub.BPO.DataAccess
{
    public class JsonHandler : TypeHandler<JObject>
    {
        private JsonHandler() { }
        public static JsonHandler Instance { get; } = new JsonHandler();
        public override JObject Parse(object value)
        {
            var json = (string)value;
            return json == null ? null : JObject.Parse(json);
        }
        public override void SetValue(IDbDataParameter parameter, JObject value)
        {
            parameter.Value = value?.ToString(Newtonsoft.Json.Formatting.None);
            ((NpgsqlParameter)parameter).NpgsqlDbType = NpgsqlDbType.Jsonb;
        }
    }

    public class StatusMensagemEnumTypeHandler : TypeHandler<StatusMensagemEnum>
    {
        public override StatusMensagemEnum Parse(object value)
        {
            switch (value)
            {
                case int i: return (StatusMensagemEnum)i;
                case string s: return (StatusMensagemEnum)Enum.Parse(typeof(StatusMensagemEnum), s);
                default: throw new NotSupportedException($"{value} not a valid MyPostgresEnum value");
            }
        }

        public override void SetValue(IDbDataParameter parameter, StatusMensagemEnum value)
        {
            parameter.DbType = (DbType)NpgsqlDbType.Unknown;
            // assuming the enum case names match the ones defined in Postgres
            parameter.Value = Enum.GetName(typeof(StatusMensagemEnum), (int)value).ToString().ToLowerInvariant();
        }
    }

    public class TipoLogEnumTypeHandler : TypeHandler<TipoLogEnum>
    {
        public override TipoLogEnum Parse(object value)
        {
            switch (value)
            {
                case int i: return (TipoLogEnum)i;
                case string s: return (TipoLogEnum)Enum.Parse(typeof(TipoLogEnum), s);
                default: throw new NotSupportedException($"{value} not a valid MyPostgresEnum value");
            }
        }

        public override void SetValue(IDbDataParameter parameter, TipoLogEnum value)
        {
            parameter.DbType = (DbType)NpgsqlDbType.Unknown;
            // assuming the enum case names match the ones defined in Postgres
            parameter.Value = Enum.GetName(typeof(TipoLogEnum), (int)value).ToString().ToLowerInvariant();
        }
    }
}
