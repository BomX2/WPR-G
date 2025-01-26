namespace WebprojectG.Tests
{
    public class Calculator()
    {
        public int Add(int getal1, int getal2) 
        {
            return  getal1 + getal2;
        }
    }
    public class CalculatorTests
    {
        [Fact]
        public void Add_WhenCalled_ReturnsCorrectSum()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Add(1, 2);

            // Assert
            Assert.Equal(3, result);
        }
    }
}