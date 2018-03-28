using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.Utils;
using FluentAssertions;
using Xunit;

namespace Polygen.Core.Tests
{
    public class EventHubTests
    {
        [Fact]
        public void Empty_event_hub_fire_event()
        {
            var hub = new EventHub<TestEvent>();

            hub.FireEvent(new TestEvent());
        }

        [Fact]
        public void Fire_event_with_one_handler()
        {
            var hub = new EventHub<TestEvent>();
            var list = new List<string>();
            var handler = new Handler("one", list);

            hub.AddListener(handler.Handle, handler.PluginId);
            hub.FireEvent(new TestEvent { Value = 10 });

            list.ShouldBeEquivalentTo(new[] { "one" });
            handler.Event.Should().NotBeNull();
            handler.Event.Value.ShouldBeEquivalentTo(10);
        }

        [Fact]
        public void Fire_event_with_two_handlers()
        {
            var hub = new EventHub<TestEvent>();
            var list = new List<string>();
            var handler1 = new Handler("one", list);
            var handler2 = new Handler("two", list);

            hub.AddListener(handler1.Handle, handler1.PluginId);
            hub.AddListener(handler2.Handle, handler2.PluginId);
            hub.FireEvent(new TestEvent { Value = 10 });

            list.ShouldBeEquivalentTo(new[] { "one", "two" });
            handler1.Event.Should().NotBeNull();
            handler1.Event.Value.ShouldBeEquivalentTo(10);

            handler2.Event.Should().NotBeNull();
            handler2.Event.Value.ShouldBeEquivalentTo(10);
        }

        [Fact]
        public void Fire_event_with_two_handlers_with_dependency()
        {
            var hub = new EventHub<TestEvent>();
            var list = new List<string>();
            var handler1 = new Handler("one", list);
            var handler2 = new Handler("two", list);

            hub.AddListener(handler1.Handle, handler1.PluginId, new[] { handler2.PluginId });
            hub.AddListener(handler2.Handle, handler2.PluginId);
            hub.FireEvent(new TestEvent { Value = 10 });

            list.ShouldBeEquivalentTo(new[] { "two" , "one" });
            handler1.Event.Should().NotBeNull();
            handler1.Event.Value.ShouldBeEquivalentTo(10);

            handler2.Event.Should().NotBeNull();
            handler2.Event.Value.ShouldBeEquivalentTo(10);
        }

        [Fact]
        public void Fire_event_with_two_handlers_with_missing_dependency()
        {
            var hub = new EventHub<TestEvent>();
            var list = new List<string>();
            var handler1 = new Handler("one", list);
            var handler2 = new Handler("two", list);

            hub.AddListener(handler1.Handle, handler1.PluginId, new[] { "dummy" });
            hub.AddListener(handler2.Handle, handler2.PluginId);
            hub.FireEvent(new TestEvent { Value = 10 });

            list.ShouldBeEquivalentTo(new[] { "one", "two" });
        }

        [Fact]
        public void Fire_event_with_three_handlers_with_dependency_chain()
        {
            var hub = new EventHub<TestEvent>();
            var list = new List<string>();
            var handler1 = new Handler("one", list);
            var handler2 = new Handler("two", list);
            var handler3 = new Handler("three", list);

            hub.AddListener(handler1.Handle, handler1.PluginId, new[] { handler2.PluginId });
            hub.AddListener(handler2.Handle, handler2.PluginId, new[] { handler3.PluginId });
            hub.AddListener(handler3.Handle, handler3.PluginId);
            hub.FireEvent(new TestEvent { Value = 10 });

            list.ShouldBeEquivalentTo(new[] { "three", "two", "one" });
        }

        [Fact]
        public void Fire_event_with_three_handlers_with_dependency_tree()
        {
            var hub = new EventHub<TestEvent>();
            var list = new List<string>();
            var handler1 = new Handler("one", list);
            var handler2 = new Handler("two", list);
            var handler3 = new Handler("three", list);

            hub.AddListener(handler1.Handle, handler1.PluginId, new[] { handler3.PluginId });
            hub.AddListener(handler2.Handle, handler2.PluginId, new[] { handler3.PluginId });
            hub.AddListener(handler3.Handle, handler3.PluginId);
            hub.FireEvent(new TestEvent { Value = 10 });

            list.ShouldBeEquivalentTo(new[] { "three", "one", "two" });
        }

        public class TestEvent
        {
            public int Value { get; set; }
        }

        public class Handler
        {
            private List<string> _eventList;

            public Handler(string pluginId, List<string> eventList = null)
            {
                this.PluginId = pluginId;
                this._eventList = eventList;
            }

            public string PluginId { get; set; }
            public TestEvent Event { get; set; }

            public void Handle(TestEvent evt)
            {
                this.Event = evt;
                this._eventList?.Add(this.PluginId);
            }
        }
    }
}
