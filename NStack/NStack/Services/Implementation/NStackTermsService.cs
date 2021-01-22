﻿using NStack.SDK.Models;
using NStack.SDK.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStack.SDK.Services.Implementation
{
    public class NStackTermsService : INStackTermsService
    {
        private readonly INStackRepository _repository;

        public NStackTermsService(INStackRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<DataWrapper<IEnumerable<TermsEntry>>> GetAllTerms(string language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest("api/v2/content/terms", Method.GET);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequest<DataWrapper<IEnumerable<TermsEntry>>>(request);
        }

        public Task<DataWrapper<IEnumerable<Terms>>> GetTermsVersions(string termsId, string userId, string language)
        {
            if (termsId == null)
                throw new ArgumentNullException(nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/{termsId}/versions");
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequest<DataWrapper<IEnumerable<Terms>>>(request);
        }

        public Task<DataWrapper<TermsWithContent>> GetNewestTerms(string termsId, string userId, string language)
        {
            if (termsId == null)
                throw new ArgumentNullException(nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/{termsId}/versions/newest", Method.GET);
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequest<DataWrapper<TermsWithContent>>(request);
        }

        public Task<DataWrapper<TermsWithContent>> GetTerms(int termsId, string userId, string language)
        {
            if (termsId < 0)
                throw new ArgumentException($"Expected an ID of 0 or higher. Got {termsId}", nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/versions/{termsId}", Method.GET);
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequest<DataWrapper<TermsWithContent>>(request);
        }

        public Task<TermsReadConfirmation> MarkRead(int termsId, string userId, string language)
        {
            if (termsId < 0)
                throw new ArgumentException($"Expected an ID of 0 or higher. Got {termsId}", nameof(termsId));
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            var request = new RestRequest($"api/v2/content/terms/versions/{termsId}/views", Method.POST);
            request.AddQueryParameter("guid", userId);
            request.AddHeader("Accept-Language", language);

            return _repository.DoRequest<TermsReadConfirmation>(request);
        }
    }
}
