using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Polygen.Core.Stage;

namespace Polygen.Core.Tests
{
    public class StageHandlerTests
    {
        [Fact]
        public void Single_stage_handler_created()
        {
            var runner = TestRunner.Create(new[] { typeof(StageHandlerOne) });

            runner.Initialize();

            var stageHandlers = runner.Context.StageHandlers.GetHandlers(StageType.Initialize).ToList();

            stageHandlers.Count.Should().Be(1);
            stageHandlers[0].Id.Should().Be("one");
        }

        [Fact]
        public void Two_stage_handlers_created()
        {
            var runner = TestRunner.Create(new[] { typeof(StageHandlerOne), typeof(StageHandlerTwo)  });

            runner.Initialize();

            var stageHandlers = runner.Context.StageHandlers.GetHandlers(StageType.Initialize).ToList();

            var ids = new HashSet<string>(runner.Context.StageHandlers.GetHandlers(StageType.Initialize).Select(x => x.Id));

            ids.Should().BeEquivalentTo(new[] { "one", "two" });
        }

        [Fact]
        public void Initialize_event_called_on_stage_handler()
        {
            var runner = TestRunner.Create(new[] { typeof(StageHandlerWithInitializeEventHandler) });

            runner.Initialize();

            var stageHandler = (StageHandlerWithInitializeEventHandler)runner.Context.StageHandlers.GetHandlers(StageType.Initialize).FirstOrDefault();

            stageHandler.InitializeCalled.Should().BeTrue();
        }

        public class StageHandlerOne : StageHandlerBase
        {
            public StageHandlerOne() : base(StageType.Initialize, "one")
            {
            }

            public override void Execute()
            {
            }
        }

        public class StageHandlerTwo : StageHandlerBase
        {
            public StageHandlerTwo() : base(StageType.Initialize, "two")
            {
            }

            public override void Execute()
            {
            }
        }

        public class StageHandlerWithInitializeEventHandler : StageHandlerBase
        {
            public StageHandlerWithInitializeEventHandler() : base(StageType.Initialize, "init-event")
            {
            }

            public bool InitializeCalled { get; set; }

            public override void Execute()
            {
                InitializeCalled = true;
            }
        }
    }
}
