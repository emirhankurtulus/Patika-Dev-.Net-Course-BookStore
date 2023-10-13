namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void FirstTest_WithoutFluent()
        {
            string hello = "hello world";
            Assert.Equal("hello world", hello);
            Assert.StartsWith("hel", hello);
            Assert.EndsWith("orld", hello);
        }

    }
}