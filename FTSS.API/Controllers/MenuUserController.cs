﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
	public class MenuUserController : BaseController
    {
        /// <summary>
        /// Read JWT key from appsettings.json
        /// </summary>
        public string JWTKey
        {
            get
            {
                var rst = this._configuration.GetValue<string>("JWT:Key");
                return (rst);
            }
        }

        public string JWTIssuer
        {
            get
            {
                var rst = this._configuration.GetValue<string>("JWT:Issuer");
                return (rst);
            }
        }
        public MenuUserController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger, IConfiguration configuration)
           : base(dbCTX, logger, configuration)
        {
        }

        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> GetAll([FromBody] Models.Database.BaseIdModel filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_User_Menu_GetAll.Call(_ctx, filterParams,JWTKey,JWTIssuer);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in MenuUserController.GetAll(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetAll");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Models.Database.StoredProcedures.SP_User_Access_Menu_Insert_Params data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_Access_Menu_Insert.Call(_ctx, data, JWTKey, JWTIssuer);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in MenuUserController.Insert(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Insert");
            }
        }
    }
}
