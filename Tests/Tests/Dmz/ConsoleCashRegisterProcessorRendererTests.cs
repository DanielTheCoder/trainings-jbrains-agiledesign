using System;
using System.IO;
using Tests.Implementation.Dmz;
using Tests.Implementation.HappyZone;
using Xunit;

namespace Tests.Tests.Dmz
{
    public class ConsoleCashRegisterProcessorRendererTests
    {
        private readonly StringWriter _stringWriter;
        private readonly ConsoleCashRegisterRenderer _consoleCashRegisterRenderer;

        public ConsoleCashRegisterProcessorRendererTests()
        {

            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            _consoleCashRegisterRenderer = new ConsoleCashRegisterRenderer();
        }

        [Fact]
        public void RenderingOfKnownProductPrice()
        {
            _consoleCashRegisterRenderer.Render(new KnownProductPriceResult(1234.12m));

            Assert.Equal("1234,12", _stringWriter.ToString());
        }

        [Fact]
        public void RenderingOfUnknownProductPrice()
        {
            _consoleCashRegisterRenderer.Render(new UnknownProductPriceResult());

            //ToDo: check requirements !!!
            Assert.Equal("Unknown product", _stringWriter.ToString());
        }
    }
}