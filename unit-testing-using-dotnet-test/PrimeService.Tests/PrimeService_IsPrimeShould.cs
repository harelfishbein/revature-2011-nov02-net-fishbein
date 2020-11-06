using Xunit;
using Prime.Services;

namespace Prime.UnitTests.Services
{
    public class PrimeService_IsPrimeShould
    {
        [Fact]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            var primeService = new PrimeService();
            bool result = primeService.IsPrime(1);

            Assert.False(result, "1 should not be prime");
        }
        [Fact]
        public void IsPrime_InputIs0_ReturnFalse()
        {
            var primeService = new PrimeService();
            bool result = primeService.IsPrime(0);

            Assert.False(result, "0 should not be prime");
        }
        [Fact]
        public void IsPrime_InputIsN1_ReturnFalse()
        {
            var primeService = new PrimeService();
            bool result = primeService.IsPrime(-1);

            Assert.False(result, "-1 should not be prime");
        }
    }
}
