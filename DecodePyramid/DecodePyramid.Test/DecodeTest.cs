using FluentAssertions;

namespace DecodePyramid.Test
{
    public class DecodeTest
    {
        [Test]
        public async Task Decode_Sample_OK()
        {
            string result = await DecodeUtils.decode(@"data\sample.txt");
            result.Should().Be("I love computers");
        }

        [Test]
        public async Task Decode_CodingQualInput_OK()
        {
            string result = await DecodeUtils.decode(@"data\coding_qual_input.txt");
            result.Should().Be("design money copy finger hole machine skin noun discuss term duck young require land single trade produce stop gun blow visit big produce chart");
        }
    }
}