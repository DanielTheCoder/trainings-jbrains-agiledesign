using System;
using System.IO;
using Tests.Implementation;
using Tests.Implementation.Dmz;
using Tests.Implementation.HappyZone;
using Xunit;

namespace Tests.HappyZone.Dmz
{
    public class ConsoleCashRegisterProcessorRendererTests
    {
        private readonly StringWriter _stringWriter;
        private readonly ConsoleCashRegisterProcessorRenderer _consoleCashRegisterProcessorRenderer;

        public ConsoleCashRegisterProcessorRendererTests()
        {

            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            _consoleCashRegisterProcessorRenderer = new ConsoleCashRegisterProcessorRenderer();
        }

        [Fact]
        public void RenderingOfKnownProductPrice()
        {
            _consoleCashRegisterProcessorRenderer.Render(new KnownProductPriceResult(1234.12m));

            Assert.Equal("1234,12", _stringWriter.ToString());
        }

        [Fact]
        public void RenderingOfUnknownProductPrice()
        {
            _consoleCashRegisterProcessorRenderer.Render(new UnknownProductPriceResult());

            //ToDo: check requirements !!!
            Assert.Equal("Unknown product", _stringWriter.ToString());
        }
    }
}