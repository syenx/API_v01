using Amazon.SecretsManager;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EDM.Infohub.BPO.DataAccess
{
    public abstract class SqlRepository<TEntity> : IRepository where TEntity : class
    {
        private NpgsqlConnection _connection;

        protected IConfiguration _config;
        protected IAmazonSecretsManager _secret;
        protected string TableName { get; private set; }

        private string ConnString = null;
        public SqlRepository()
        {
            var type = typeof(TEntity);
            var attribute = type.GetCustomAttribute<TableAttribute>();
            if (attribute == null)
                throw new MissingFieldException($"The Type {type.Name} does not has the TableAttribute");

            this.TableName = attribute.Name;
        }

        protected NpgsqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    ConnString ??= Utils.GetSecret(_config, _secret, "Base");
                    _connection = new NpgsqlConnection(ConnString);
                    SqlMapper.AddTypeHandler(typeof(JObject), JsonHandler.Instance);
                    SqlMapper.AddTypeHandler(new StatusMensagemEnumTypeHandler());
                    SqlMapper.AddTypeHandler(new TipoLogEnumTypeHandler());
                }

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return _connection;

            }
        }

        public virtual TEntity Get(TEntity item) => Connection.Get<TEntity>(item);
        public virtual IEnumerable<TEntity> GetAll() => Connection.GetAll<TEntity>();
        public virtual bool Update(TEntity item) => Connection.Update(item);
        public virtual bool Delete(TEntity item) => Connection.Delete<TEntity>(item);
        public virtual long Insert(TEntity item) => Connection.Insert<TEntity>(item);

        public override void Dispose()
        {
            this._connection?.Close();
            this._connection = null;
        }

        public void CloseConnection()
        {
            this._connection?.Close();
        }
    }
}
