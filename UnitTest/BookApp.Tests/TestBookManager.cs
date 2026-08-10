using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using BookApp;

namespace BookApp.Tests
{
    public class TestBookManager
    {
        [Fact]
        public async Task TestBookFound_WithFact()
        {
            var mockRepo = new Mock<iBookRepo>();
            var mockMap = new Mock<iMyMapper>();

            book fakeBook = new book(); fakeBook.id = 5; fakeBook.title = "book"; fakeBook.price = 150;
            bookDto fakeDto = new bookDto(); fakeDto.id = 5; fakeDto.title = "book";

            mockRepo.Setup(x => x.getBook(5)).ReturnsAsync(fakeBook);
            mockMap.Setup(x => x.convertToDto(fakeBook)).Returns(fakeDto);

            BookManager manager = new BookManager(mockRepo.Object, mockMap.Object);
            var res = await manager.findBookProcess(5);

            Assert.NotNull(res);
            Assert.Equal("book", res.title);
            Assert.Equal(5, res.id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-38)]
        [InlineData(-580)]
        public async Task TestBadId_WithTheory(int badid)
        {
            var mockRepo = new Mock<iBookRepo>();
            var mockMap = new Mock<iMyMapper>();
            BookManager manager = new BookManager(mockRepo.Object, mockMap.Object);

            var error = await Assert.ThrowsAsync<Exception>(async () => await manager.findBookProcess(badid));
            Assert.NotNull(error.Message);
        }
    }
}