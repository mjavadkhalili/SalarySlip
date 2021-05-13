﻿using Dapper;
using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM.StoredProcedure
{
	public class SP_FishDetail_Sum_Get : ISP<Models.Database.StoredProcedures.SP_Fish_Get_Sum_Params>
	{
        private readonly string _cns;
        public SP_FishDetail_Sum_Get(string cns)
        {
            _cns = cns;
        }

        public async Task<DBResult> Call(Models.Database.StoredProcedures.SP_Fish_Get_Sum_Params filterParams)
        {
            string sql = "dbo.SP_FishDetail_Get_Sum";
            DBResult rst = null;
            using (var connection = new SqlConnection(_cns))
            {
                var p = Common.GetSearchParams(filterParams.Token);
                p.AddDynamicParams(Common.GenerateParams(filterParams, new List<string> { "Token" }));

                var dbResult = await connection.QueryAsync<Models.Database.StoredProcedures.SP_FishDetail_Get_Sum>(
                    sql, p, commandType: System.Data.CommandType.StoredProcedure);
                rst = Common.GetResult(p, dbResult);
            }
            return rst;
        }
    }
}
