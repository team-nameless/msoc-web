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
    
    [Fact]
    public async Task InvalidFriendCodeTest()
    {
        using var scope = _factory.Services.CreateAsyncScope();
        
        var maimai = scope.ServiceProvider.GetService<MaimaiInquiryService>()!;
        var result = await maimai.PerformFriendCodeLookupAsync(177013);

        Assert.StrictEqual(0, result.Length);
    }

    [Fact]
    public async Task ValidFamiliarFriendCodeTest()
    {
        using var scope = _factory.Services.CreateAsyncScope();
        
        var maimai = scope.ServiceProvider.GetService<MaimaiInquiryService>()!;
        var result = await maimai.PerformFriendCodeLookupAsync(9020119099087);

        Assert.StrictEqual(3, result.Length);
        Assert.NotEmpty(result[0].TextContent);
        Assert.NotEmpty(result[1].TextContent);
        Assert.NotEmpty((result[2] as IHtmlImageElement)!.Source!);
    }

    [Fact]
    public async Task ValidStrangerFriendCodeTest()
    {
        using var scope = _factory.Services.CreateAsyncScope();
        
        var maimai = scope.ServiceProvider.GetService<MaimaiInquiryService>()!;
        var result = await maimai.PerformFriendCodeLookupAsync(8069933165057);

        Assert.StrictEqual(3, result.Length);
        Assert.NotEmpty(result[0].TextContent);
        Assert.NotEmpty(result[1].TextContent);
        Assert.NotEmpty((result[2] as IHtmlImageElement)!.Source!);
    }
}