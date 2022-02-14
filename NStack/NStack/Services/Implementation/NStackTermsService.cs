﻿namespace NStack.SDK.Services.Implementation;

public class NStackTermsService : INStackTermsService
{
    private readonly INStackRepository _repository;

    public NStackTermsService(INStackRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task<DataWrapper<List<TermsEntry>>> GetAllTermsAsync(string language)
    {
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("No string received", nameof(language));

        var request = new RestRequest("api/v2/content/terms", Method.Get);
        request.AddHeader("Accept-Language", language);

        return _repository.DoRequestAsync<DataWrapper<List<TermsEntry>>>(request);
    }

    public Task<DataWrapper<List<Terms>>> GetTermsVersionsAsync(string termsId, string userId, string language)
    {
        if (string.IsNullOrWhiteSpace(termsId))
            throw new ArgumentException("No string received", nameof(termsId));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("No string received", nameof(userId));
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("No string received", nameof(language));

        var request = new RestRequest($"api/v2/content/terms/{termsId}/versions");
        request.AddQueryParameter("guid", userId);
        request.AddHeader("Accept-Language", language);

        return _repository.DoRequestAsync<DataWrapper<List<Terms>>>(request);
    }

    public Task<DataWrapper<TermsWithContent>> GetNewestTermsAsync(string termsId, string userId, string language)
    {
        if (string.IsNullOrWhiteSpace(termsId))
            throw new ArgumentException("No string received", nameof(termsId));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("No string received", nameof(userId));
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("No string received", nameof(language));

        var request = new RestRequest($"api/v2/content/terms/{termsId}/versions/newest", Method.Get);
        request.AddQueryParameter("guid", userId);
        request.AddHeader("Accept-Language", language);

        return _repository.DoRequestAsync<DataWrapper<TermsWithContent>>(request);
    }

    public Task<DataWrapper<TermsWithContent>> GetTermsAsync(int termsId, string userId, string language)
    {
        if (termsId < 0)
            throw new ArgumentException($"Expected an ID of 0 or higher. Got {termsId}", nameof(termsId));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("No string received", nameof(userId));
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("No string received", nameof(language));

        var request = new RestRequest($"api/v2/content/terms/versions/{termsId}", Method.Get);
        request.AddQueryParameter("guid", userId);
        request.AddHeader("Accept-Language", language);

        return _repository.DoRequestAsync<DataWrapper<TermsWithContent>>(request);
    }

    public async Task<bool> MarkReadAsync(int termsId, string userId, string language)
    {
        if (termsId < 0)
            throw new ArgumentException($"Expected an ID of 0 or higher. Got {termsId}", nameof(termsId));
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("No string received", nameof(userId));
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("No string received", nameof(language));

        var request = new RestRequest($"api/v2/content/terms/versions/views", Method.Post);
        request.AddJsonBody(new
        {
            term_version_id = termsId,
            guid = userId,
            identifier = userId,
            locale = language
        }, "application/x-www-form-urlencoded");
        request.AddHeader("Accept-Language", language);

        var data = await _repository.DoRequestAsync<object>(request);

        return data != null;
    }
}
