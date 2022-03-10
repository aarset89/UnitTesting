using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.Tests.MockingTest
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);

        }

        [Test]
        public void ReadVideoTitle_EmptyFileString_ReturnErrorMessage()
        {

            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService  .ReadVideoTitle();

            Assert.That(result, Is.EqualTo("Error parsing the video.").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewUnprocessedVideos_ReturnStringWithIdsAsString()
        {
            var videoList = new List<Video>();
            videoList.Add(new Video { Id = 1, IsProcessed = false, Title = "A" });
            videoList.Add(new Video { Id = 2, IsProcessed = false, Title = "B" });
            videoList.Add(new Video { Id = 3, IsProcessed = false, Title = "C" });
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(videoList);

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
        
        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }
    }
}
