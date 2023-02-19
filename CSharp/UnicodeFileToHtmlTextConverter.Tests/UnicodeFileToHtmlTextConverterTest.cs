
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using Shouldly;
using Xunit;
namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class HikerTest
    {
        [Fact(Skip = "Internal Step of TDD")]
        public void Initial_ConvertToHtml_WithExistingFile_ShouldReturnValidHtml()
        {
            // Arrange
            var fileName = "foobar.txt";
            var fullpath = $"C:\\Projetos\\Katas\\Racing-Car-Katas\\{fileName}";
            var part1 = "\"Mercedes\"&\"Hamilton\"";
            var part2 = "<TirePressure>12<TirePressure>";
            var httpUtility = new HttpUtility();
            var expectedResult = httpUtility.HtmlEncode(part1) +
                                 "<br />" +
                                 httpUtility.HtmlEncode(part2) +
                                 "<br />";
            var converter = new UnicodeFileToHtmlTextConverter(fullpath);

            // Act
            var result = converter.ConvertToHtml(); 

            // Assert
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }
        
        [Fact]
        public void ConvertToHtml_2Lines_ShouldReturn2LinesOnHtml()
        {
            // Arrange
            var httpUtility = new Mock<IHttpUtility>();
            httpUtility.SetupSequence(x => x.HtmlEncode(It.IsAny<string>()))
                .Returns("1")
                .Returns("2");

            var textReader = new Mock<ITextReader>();
            var reader = new Mock<TextReader>();

            textReader.Setup(x => x.GetTextReader()).Returns(reader.Object);
            reader.SetupSequence(x => x.ReadLine())
                .Returns("1")
                .Returns("2")
                .Returns((string)null);

            var expectedResult = $"1<br />2<br />";
            var converter = new UnicodeFileToHtmlTextConverter(httpUtility.Object, textReader.Object);

            // Act
            var result = converter.ConvertToHtml(); 

            // Assert
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }
        
        [Fact]
        public void ConvertToHtml_1Line_ShouldReturn1LineOnHtml()
        {
            // Arrange
            var httpUtility = new Mock<IHttpUtility>();
            var textReader = new Mock<ITextReader>();
            var reader = new Mock<TextReader>();

            httpUtility.SetupSequence(x => x.HtmlEncode(It.IsAny<string>()))
                .Returns("1");
            textReader.Setup(x => x.GetTextReader())
                .Returns(reader.Object);
            reader.SetupSequence(x => x.ReadLine())
                .Returns("1")
                .Returns((string)null);

            var expectedResult = $"1<br />";
            var converter = new UnicodeFileToHtmlTextConverter(httpUtility.Object, textReader.Object);

            // Act
            var result = converter.ConvertToHtml(); 

            // Assert
            result.ShouldNotBeNullOrEmpty();
            result.ShouldBe(expectedResult);
        }
    }
}
