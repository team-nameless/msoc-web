using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace MSOC.Backend.Service;

/// <summary>
///     A simple DI service to deal with maimai game inquiries. Written to deal with MVC model's headaches.
/// </summary>
public class MaimaiInquiryService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient = new();
    private readonly HtmlParser _parser = new();

    public MaimaiInquiryService(IConfiguration configuration)
    {
        _configuration = configuration;

        _httpClient.Send(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri =
                new Uri(
                    "https://lng-tgk-aime-gw.am-all.net/common_auth/login?site_id=maimaidxex&redirect_url=https://maimaidx-eng.com/maimai-mobile/&back_url=https://maimai.sega.com/")
        });

        _httpClient.Send(new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://lng-tgk-aime-gw.am-all.net/common_auth/login/sid"),
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("sid", _configuration.GetValue<string>("SEGA:USERNAME") ?? ""),
                new("password", _configuration.GetValue<string>("SEGA:PASSWORD") ?? ""),
                new("retention", "1")
            })
        });
    }

    /// <summary>
    ///     Perform lookup on friend code.
    /// </summary>
    /// <param name="friendCode">Friend code</param>
    /// <returns>Deserialized IElement[] array containing raw value of *what is needed*</returns>
    public async Task<IElement[]> PerformFriendCodeLookupAsync(ulong friendCode)
    {
        var result = await _httpClient
            .GetAsync($"https://maimaidx-eng.com/maimai-mobile/friend/search/searchUser/?friendCode={friendCode}");

        var html = await result.Content.ReadAsStreamAsync();
        var document = _parser.ParseDocument(html).All;

        try
        {
            List<IElement> results = new()
            {
                document.First(x => x.LocalName == "div" && x.ClassName == "name_block f_l f_16"),
                document.First(x => x.LocalName == "div" && x.ClassName == "rating_block"),
                document.First(x => x.LocalName == "img" && x.ClassName == "w_112 f_l"),
            };

            return results.ToArray();
        }
        catch (InvalidOperationException)
        {
            return [];
        }
    }
}