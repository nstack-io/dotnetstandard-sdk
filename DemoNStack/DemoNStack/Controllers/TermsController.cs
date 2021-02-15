﻿using DemoNStack.Extensions;
using DemoNStack.Models;
using DemoNStack.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NStack.SDK.Models;
using NStack.SDK.Services;
using System;
using System.Threading.Tasks;

namespace DemoNStack.Controllers
{
    public class TermsController : NStackController
    {
        private INStackTermsService TermsService { get; }

        public TermsController(IMemoryCache cache, INStackLocalizeService localizeService, INStackTermsService termsService) : base(cache, localizeService)
        {
            TermsService = termsService ?? throw new ArgumentNullException(nameof(termsService));
        }

        public async Task<IActionResult> Index()
        {
            Guid userId = GetUserId();

            TermsWithContent newestTerms = await GetNewestTerms(userId, Request.GetCurrentLanguage());

            DataMetaWrapper<Translation> translations = await GetTranslations();

            var viewModel = new TermsViewModel
            {
                Content = newestTerms.Content.Data,
                HasApproved = newestTerms.HasViewed,
                Headline = translations.Data.Terms.Headline,
                ApproveButton = translations.Data.Terms.Approve,
                HasApprovedString = translations.Data.Terms.HasApproved
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptTerms()
        {
            Guid userId = GetUserId();
            string language = Request.GetCurrentLanguage();

            TermsWithContent newestTerms = await GetNewestTerms(userId, language);

            await TermsService.MarkRead(newestTerms.Id, userId.ToString(), language);

            //Clear cache so we get the updated boolean that the user has read the terms
            Cache.Remove($"terms-{language}-{userId}");

            return RedirectToAction("index", new { lang = language });
        }

        protected async Task<TermsWithContent> GetNewestTerms(Guid userId, string language)
        {
            return (await Cache.GetOrCreateAsync($"terms-{language}-{userId}", async e => {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);

                return await TermsService.GetNewestTerms("terms", userId.ToString(), language);
            })).Data;
        }
    }
}
