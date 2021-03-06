﻿using FTSS.Models;
using Microsoft.AspNetCore.Http;
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
	public class SendMessageController : BaseController
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
		public SendMessageController(Logic.Database.IDBCTX dbCTX, Logic.Log.ILog logger, IConfiguration configuration)
	   : base(dbCTX, logger, configuration)
		{
		}
		/// <summary>
		/// Send SMS
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut]
		[Filters.Auth]
		public async Task<IActionResult> Send([FromBody]SMS model)
		{
			try
			{
				var dbResult = await Logic.Security.SMS.Send(model, _configuration,JWTKey,JWTIssuer);
				return FromDatabase(dbResult);
			}
			catch (Exception e)
			{
				_logger.Add(e, "Error in SendMessageController.Send(filterParams)");
				return Problem(e.Message, e.StackTrace, 500, "Error in Get");
			}
		}
	}
}
