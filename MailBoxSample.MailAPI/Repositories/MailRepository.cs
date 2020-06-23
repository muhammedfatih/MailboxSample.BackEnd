using MailBoxSample.APIHelper.Repositories;
using MailBoxSample.APIHelper.Builders;
using MailBoxSample.MailAPI.Configurations;
using MailBoxSample.MailAPI.Entities;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace MailBoxSample.MailAPI.Repositories
{
    public class MailRepository : BaseRepository<MailEntity>
    {
        public MailRepository(IOptions<DatabaseConfiguration> databaseConfiguration, ILogger logger, DapperQueryBuilder<MailEntity> dapperQueryBuilder) : base(databaseConfiguration.Value.ConnectionString, logger, dapperQueryBuilder)
        {
        }
        public List<MailEntity> ListForUser(int page, int pageSize, int toUserId)
        {
            var query = $"select * from mail where isactive=true and isdeleted=false and touserid={toUserId} order by id desc limit {pageSize} offset {pageSize * page}";
            var result = this._mysqlConnection.Query<MailEntity>(query);
            return result.ToList();
        }
        public List<MailEntity> ListUnreadMails(int toUserId)
        {
            var query = $"select * from mail where isread=0 and isactive=true and isdeleted=false and touserid={toUserId} order by id";
            var result = this._mysqlConnection.Query<MailEntity>(query);
            return result.ToList();
        }
    }
}