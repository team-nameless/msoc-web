﻿using MSOC.Backend.Service;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace MSOC.Backend.Tests.Unit;

[CollectionDefinition("Dependency Injection")]
public class MaimaiInquiryServiceTest(ITestOutputHelper testOutputHelper, BackendTestBedFixture fixture) 
    : TestBed<BackendTestBedFixture>(testOutputHelper, fixture)
{
    [Theory]
    [InlineData(1234)]
    [InlineData(69420)]
    [InlineData(177013)]
    public async Task InvalidFriendCodeTest(ulong friendCode)
    {
        var maimai = _fixture.GetService<MaimaiInquiryService>(_testOutputHelper)!;
        var result = await maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(0, result.Length);
    }

    [Theory]
    [InlineData(9051555929120)]
    [InlineData(8095773611588)]
    [InlineData(9020119099087)]
    public async Task ValidFamiliarFriendCodeTest(ulong friendCode)
    {
        var maimai = _fixture.GetService<MaimaiInquiryService>(_testOutputHelper)!;
        var result = await maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(2, result.Length);
    }

    [Theory]
    [InlineData(8069933165057)]
    public async Task ValidStrangerFriendCodeTest(ulong friendCode)
    {
        var maimai = _fixture.GetService<MaimaiInquiryService>(_testOutputHelper)!;
        var result = await maimai.PerformFriendCodeLookupAsync(friendCode);

        Assert.StrictEqual(2, result.Length);
    }
}