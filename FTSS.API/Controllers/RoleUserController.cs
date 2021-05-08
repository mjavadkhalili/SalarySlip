﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTSS.API.Controllers
{
	[Route("/api/[controller]/[action]")]
	[ApiController]
	public class RoleUserController : BaseController
    {
        public RoleUserController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger)
        : base(dbCTX, logger)
        {
        }
        [HttpPut]
        [Filters.Auth]
        public async Task<IActionResult> GetAll([FromBody] Models.Database.StoredProcedures.SP_User_Roles_GetAll_Params filterParams)
        {
            try
            {
                var dbResult = await Logic.Database.StoredProcedure.SP_User_Roles_GetAll.Call(_ctx,filterParams);
                return FromDatabase(dbResult);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in RoleUserController.GetAll(filterParams)");
                return Problem(e.Message, e.StackTrace, 500, "Error in GetAll");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Models.Database.StoredProcedures.SP_User_Roles_Insert_Params data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_Roles_Insert.Call(_ctx, data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in RoleUserController.Insert(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Insert");
            }
        }
        /// <summary>
        /// جهت حذف انتصاب نقش کاربری
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Models.Database.BaseIdModel data)
        {
            try
            {
                var rst = await Logic.Database.StoredProcedure.SP_User_Roles_Delete.Call(_ctx, data);
                return FromDatabase(rst);
            }
            catch (Exception e)
            {
                _logger.Add(e, "Error in RoleUserController.Delete(data)");
                return Problem(e.Message, e.StackTrace, 500, "Error in Delete");
            }
        }
    }
}