using Xunit;
using FluentAssertions;
using Polygen.Core.Utils;

namespace Polygen.Core.Tests.Utils
{
    public class FilterTests
    {
        [Fact]
        public void Single_include()
        {
            var filter = new Filter();

            filter.AddInclude("test");
            filter.Match("test").Should().Be(Filter.MatchStatus.Included);
        }

        [Fact]
        public void Single_include_no_match()
        {
            var filter = new Filter();

            filter.AddExclude("test");
            filter.Match("tes").Should().Be(Filter.MatchStatus.None);
        }

        [Fact]
        public void Single_exclude()
        {
            var filter = new Filter();

            filter.AddExclude("test");
            filter.Match("test").Should().Be(Filter.MatchStatus.Excluded);
        }
        
        [Fact]
        public void Include_and_exclude()
        {
            var filter = new Filter();

            filter.AddInclude("test");
            filter.AddExclude("test");
            filter.Match("test").Should().Be(Filter.MatchStatus.Excluded);
        }

        [Fact]
        public void Simple_glob_include()
        {
            var filter = new Filter();

            filter.AddInclude("t*");
            filter.Match("test").Should().Be(Filter.MatchStatus.Included);
        }

        [Fact]
        public void Glob_path_include()
        {
            var filter = new Filter();

            filter.AddInclude("test/**");
            filter.Match("test").Should().Be(Filter.MatchStatus.None);
            filter.Match("test/a").Should().Be(Filter.MatchStatus.Included);
        }

        [Fact]
        public void Glob_path_include_with_custom_separator()
        {
            var filter = new Filter('.');

            filter.AddInclude("test.*");
            filter.Match("test").Should().Be(Filter.MatchStatus.None);
            filter.Match("testxa").Should().Be(Filter.MatchStatus.None);
            filter.Match("test.a").Should().Be(Filter.MatchStatus.Included);
        }
    }
}