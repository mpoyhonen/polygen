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
    public class DependencyMapTests
    {
        [Fact]
        public void Adding_duplicate_id_fails()
        {
            var map = new DependencyMap<string>();

            map.Add("one", "1");
            
            map.Invoking(x => x.Add("two", "1"))
                .Should().Throw<ArgumentException>().WithMessage("Item already added with with ID '1'.");
        }
        
        [Fact]
        public void Registration_cannot_have_a_dependency_on_itself()
        {
            var map = new DependencyMap<string>();

            map.Invoking(x => x.Add("one", "1", new[] { "1" }))
                .Should().Throw<ArgumentException>().WithMessage("Item cannot have a dependency on itself.");
        }
        
        [Fact]
        public void Missing_dependency_fails()
        {
            var map = new DependencyMap<string>();
            
            map.Add("one", "1", new[] { "2" });
            map.Add("two", "2", new[] { "3" });

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            map.Invoking(x => x.Entries.ToList())
                .Should().Throw<ConfigurationException>().WithMessage("Entry '2' has a dependency on missing entry '3'.");
        }
        
        [Fact]
        public void Direct_circular_dependency_is_detected()
        {
            var map = new DependencyMap<string>();
            
            map.Add("one", "1", new[] { "2" });
            map.Add("two", "2", new[] { "1" });

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            map.Invoking(x => x.Entries.ToList())
                .Should().Throw<ConfigurationException>().WithMessage("Circular dependency with items: 1, 2");
        }
        
        [Fact]
        public void Two_levels_circular_dependency_is_detected()
        {
            var map = new DependencyMap<string>();
            
            map.Add("one",   "1", new[] { "3" });
            map.Add("two",   "2", new[] { "1" });
            map.Add("three", "3", new[] { "2" });

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            map.Invoking(x => x.Entries.ToList())
                .Should().Throw<ConfigurationException>().WithMessage("Circular dependency with items: 1, 2, 3");
        }
    }
}