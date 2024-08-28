using MSOC.Backend.Service;

namespace MSOC.Backend.Tests.Unit;

public class MaimaiInquiryServiceTest
{
    private readonly MaimaiInquiryService _maimai;

    public MaimaiInquiryServiceTest(MaimaiInquiryService maimai)
    {
        _maimai = maimai;
    }

    [Theory]
    [InlineData(1234)]
    [InlineData(69420)]
    [InlineData(177013)]
    public async Task InvalidFriendCodeTest(ulong friendCode)
    {
        var result = await _maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(0, result.Length);
    }

    [Theory]
    [InlineData(9051555929120)]
    [InlineData(8095773611588)]
    [InlineData(9020119099087)]
    public async Task ValidFamiliarFriendCodeTest(ulong friendCode)
    {
        var result = await _maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(2, result.Length);
    }

    [Theory]
    [InlineData(8069933165057)]
    public async Task ValidStrangerFriendCodeTest(ulong friendCode)
    {
        var result = await _maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(2, result.Length);
    }
}