using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Polygen.Core.Impl;
using Polygen.Core.Schema;
using Xunit;
using FluentAssertions;
using Polygen.Core.Impl.Schema;
using System.Xml.Linq;
using System.IO;
using Polygen.Core.Exceptions;
using System.Xml;
using Polygen.Core.Impl.Project;
using Polygen.TestUtils.DataType;
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
            filter.Match("test").ShouldBeEquivalentTo(Filter.MatchStatus.Included);
        }

        [Fact]
        public void Single_include_no_match()
        {
            var filter = new Filter();

            filter.AddExclude("test");
            filter.Match("tes").ShouldBeEquivalentTo(Filter.MatchStatus.None);
        }

        [Fact]
        public void Single_exclude()
        {
            var filter = new Filter();

            filter.AddExclude("test");
            filter.Match("test").ShouldBeEquivalentTo(Filter.MatchStatus.Excluded);
        }
        
        [Fact]
        public void Include_and_exclude()
        {
            var filter = new Filter();

            filter.AddInclude("test");
            filter.AddExclude("test");
            filter.Match("test").ShouldBeEquivalentTo(Filter.MatchStatus.Excluded);
        }

        [Fact]
        public void Simple_glob_include()
        {
            var filter = new Filter();

            filter.AddInclude("t*");
            filter.Match("test").ShouldBeEquivalentTo(Filter.MatchStatus.Included);
        }

        [Fact]
        public void Glob_path_include()
        {
            var filter = new Filter();

            filter.AddInclude("test/**");
            filter.Match("test").ShouldBeEquivalentTo(Filter.MatchStatus.None);
            filter.Match("test/a").ShouldBeEquivalentTo(Filter.MatchStatus.Included);
        }

        [Fact]
        public void Glob_path_include_with_custom_separator()
        {
            var filter = new Filter('.');

            filter.AddInclude("test.*");
            filter.Match("test").ShouldBeEquivalentTo(Filter.MatchStatus.None);
            filter.Match("testxa").ShouldBeEquivalentTo(Filter.MatchStatus.None);
            filter.Match("test.a").ShouldBeEquivalentTo(Filter.MatchStatus.Included);
        }
    }
}