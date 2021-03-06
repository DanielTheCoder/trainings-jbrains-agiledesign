﻿using System;
using Tests.Implementation.HappyZone;

namespace Tests.Implementation.Dmz
{
    public class ConsoleCashRegisterRenderer : ICashRegisterRenderer
    {
        public void Render(IProductPriceResult result)
        {
            var formatedResult = GetProductPriceResultedFormated(result);

            Console.Write(formatedResult);
        }

        private static string GetProductPriceResultedFormated(IProductPriceResult result)
        {
            string formatedResult = string.Empty;
            if (result is KnownProductPriceResult knownProductPriceResult)
            {
                formatedResult = knownProductPriceResult.Value.ToString();
            }
            else
            {
                formatedResult = "Unknown product";
            }

            return formatedResult;
        }
    }
}