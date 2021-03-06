﻿using DemoNStack.Extensions;
using DemoNStack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NStack.SDK.Models;
using NStack.SDK.Services;
using System;
using System.Threading.Tasks;

namespace DemoNStack.Controllers
{
    public abstract class NStackController : Controller
    {
        protected INStackAppService NStackAppService { get; }

        protected NStackController(INStackAppService nStackAppService)
        {
            NStackAppService = nStackAppService ?? throw new ArgumentNullException(nameof(nStackAppService));
        }

        protected async Task<DataMetaWrapper<Translation>> GetTranslations()
        {
            return await NStackAppService.GetResourceAsync<Translation>(Request.GetCurrentLanguage(), NStackPlatform.Web, "1.3.0");
        }

        protected Guid GetUserId()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                userId = Guid.NewGuid();

                HttpContext.Session.SetString("UserId", userId.ToString());
            }

            return userId;
        }
    }
}
