using AngleSharp.Html.Dom;
using Microsoft.Extensions.DependencyInjection;
using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Unit;

public class MaimaiInquiryServiceTest : IClassFixture<GameApplicationFactory<Program>>
{
    private readonly GameApplicationFactory<Program> _factory;

    public MaimaiInquiryServiceTest(GameApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [InlineData(1234)]
    [InlineData(69420)]
    [InlineData(177013)]
    public async Task InvalidFriendCodeTest(ulong friendCode)
    {
        using var scope = _factory.Services.CreateAsyncScope();
        
        var maimai = scope.ServiceProvider.GetService<MaimaiInquiryService>()!;
        var result = await maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(0, result.Length);
    }

    [Theory]
    [InlineData(9051555929120)]
    [InlineData(8095773611588)]
    [InlineData(9020119099087)]
    public async Task ValidFamiliarFriendCodeTest(ulong friendCode)
    {
        using var scope = _factory.Services.CreateAsyncScope();
        
        var maimai = scope.ServiceProvider.GetService<MaimaiInquiryService>()!;
        var result = await maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(3, result.Length);
        Assert.NotEmpty(result[0].TextContent);
        Assert.NotEmpty(result[1].TextContent);
        Assert.NotEmpty((result[2] as IHtmlImageElement)!.Source!);
    }

    [Theory]
    [InlineData(8069933165057)]
    public async Task ValidStrangerFriendCodeTest(ulong friendCode)
    {
        using var scope = _factory.Services.CreateAsyncScope();
        
        var maimai = scope.ServiceProvider.GetService<MaimaiInquiryService>()!;
        var result = await maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(3, result.Length);
        Assert.NotEmpty(result[0].TextContent);
        Assert.NotEmpty(result[1].TextContent);
        Assert.NotEmpty((result[2] as IHtmlImageElement)!.Source!);
    }
}