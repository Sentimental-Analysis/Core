using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models;
using Core.Services.Implementations;
using Xunit;
using FluentAssertions;
using System.Linq;
using Core.Tests.TestAbstractions;
using static Core.Tests.Builders.TweetBuilder;

namespace Core.Tests.Services
{
    public class SimpleAnalysisSerivice : TestWithData
    {
        private readonly SimpleAnalysisService _simpleAnalysisService;

        public SimpleAnalysisSerivice()
        {
            var dictionary = ImmutableDictionary<string, int>.Empty.Add("love", 5).Add("hate", -5);
            _simpleAnalysisService = new SimpleAnalysisService(dictionary);
        }

        [Fact]
        public void Test_Analyze_When_Tweet_Is_Positive()
        {
            var tweets = new List<Tweet>()
            {
                A(Tweet().WithText("love")),
                A(Tweet().WithText("love love love"))
            };
            var testResult = _simpleAnalysisService.Analyze(tweets);
            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.Should().NotBeNullOrEmpty();
            testResult.Value.All(x => x.Sentiment > 0).Should().BeTrue();
        }

        [Fact]
        public void Test_Analyze_When_Tweet_Is_Negative()
        {
            var tweets = new List<Tweet>()
            {
                A(Tweet().WithText("hate")),
                A(Tweet().WithText("hate hate hate"))
            };
            var testResult = _simpleAnalysisService.Analyze(tweets);
            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.Should().NotBeNullOrEmpty();
            testResult.Value.All(x => x.Sentiment < 0).Should().BeTrue();
        }

        [Fact]
        public void Test_Analyze_When_Tweet_IsNegate()
        {
            var tweets = new List<Tweet>()
            {
                A(Tweet().WithText("no love")),
            };
            var testResult = _simpleAnalysisService.Analyze(tweets);
            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.Should().NotBeNullOrEmpty();
            testResult.Value.All(x => x.Sentiment < 0).Should().BeTrue();
        }
    }
}