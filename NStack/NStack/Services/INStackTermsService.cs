﻿namespace NStack.SDK.Services;

public interface INStackTermsService
{
    /// <summary>
    /// Get a list of the available terms for the given <paramref name="language"/>.
    /// </summary>
    /// <param name="language">The ISO language code to get the list of terms for e.g. en-GB.</param>
    /// <exception cref="ArgumentNullException"></exception>
    Task<DataWrapper<List<TermsEntry>>> GetAllTermsAsync(string language);

    /// <summary>
    /// Get a list of versions for the terms with the given <paramref name="termsName"/>.
    /// </summary>
    /// <param name="termsName">The name of the terms to fetch.</param>
    /// <param name="userId">The unique ID of the user who is going to read the terms.</param>
    /// <param name="language">The ISO language code to get the terms in e.g. en-GB.</param>
    /// <exception cref="ArgumentNullException"></exception>
    Task<DataWrapper<List<Terms>>> GetTermsVersionsAsync(string termsName, string userId, string language);

    /// <summary>
    /// Get the latest version of the terms with the given <paramref name="termsName"/>.
    /// </summary>
    /// <param name="termsName">The name of the terms to fetch.</param>
    /// <param name="userId">The unique ID of the user who is going to read the terms.</param>
    /// <param name="language">The ISO language code to get the terms in e.g. en-GB.</param>
    /// <exception cref="ArgumentNullException"></exception>
    Task<DataWrapper<TermsWithContent>> GetNewestTermsAsync(string termsName, string userId, string language);

    /// <summary>
    /// Get the specified version of the terms with the given <paramref name="termsId"/>.
    /// </summary>
    /// <param name="termsId">The ID of the terms to fetch.</param>
    /// <param name="userId">The unique ID of the user who is going to read the terms.</param>
    /// <param name="language">The ISO language code to get the terms in e.g. en-GB.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    Task<DataWrapper<TermsWithContent>> GetTermsAsync(int termsId, string userId, string language);

    /// <summary>
    /// Mark terms as read.
    /// </summary>
    /// <param name="termsId">The ID of the terms to mark as read.</param>
    /// <param name="userId">The unique ID of the user who has read the terms.</param>
    /// <param name="language">The ISO language code of the terms to mark as read e.g. en-GB.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    Task<bool> MarkReadAsync(int termsId, string userId, string language);
}
