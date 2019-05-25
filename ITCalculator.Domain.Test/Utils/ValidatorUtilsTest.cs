using ITCalculator.Utils;
using Xunit;

namespace ITCalculator.Test.Utils
{
    public class ValidatorUtilsTest
    {
        [Theory]
        [InlineData("504.249.900-83")]
        [InlineData("643.185.010-60")]
        [InlineData("396.358.640-00")]
        [InlineData("769.166.530-00")]
        public void IsCpf_should_returnTrue(string value)
        {
            Assert.True(ValidatorUtils.IsCpf(value));
        }
        
        [Theory]
        [InlineData("224.249.900-83")]
        [InlineData("6010-60")]
        [InlineData("396.6.640-00")]
        [InlineData("bcpf")]
        public void IsCpf_should_returnFalse_because_invalidCpf(string value)
        {
            Assert.False(ValidatorUtils.IsCpf(value));
        }
    }
}