using AutoMapper;
using FakeItEasy;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Models.EntityClasses;
using TajmautMK.Common.Models.ModelsREQUEST;
using TajmautMK.Common.Models.ModelsRESPONSE;
using TajmautMK.Common.Services.Implementations;
using TajmautMK.Core.Services.Implementations;
using TajmautMK.Repository.Interfaces;

namespace TajmautMK.Test
{
    public class CommentTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task CreateCommentTest()
        {
            // Arrange
            var fakeHelper = A.Fake<IHelperValidationClassService>();
            var fakeRepo = A.Fake<ICommentRepository>();
            var fakeMapper = A.Fake<IMapper>();
            var request = new CommentREQUEST 
            { 
                VenueId= 1,
                Body = "Test",
                Review=5,

            };
            var comment = new Comment { /* properties here */ };
            var expectedResponse = new ServiceResponse<CommentRESPONSE> { Data = new CommentRESPONSE() };

            A.CallTo(() => fakeHelper.ValidateId(request.VenueId)).Returns(true);
            A.CallTo(() => fakeHelper.CheckIdVenue(request.VenueId)).Returns(true);
            A.CallTo(() => fakeRepo.AddToDB(request)).Returns(comment);
            A.CallTo(() => fakeMapper.Map<CommentRESPONSE>(comment)).Returns(expectedResponse.Data);

            var sut = new CommentService(fakeRepo, fakeMapper, fakeHelper);

            // Act
            var response = await sut.CreateComment(request);

            // Assert
            Assert.That(response.Data, Is.EqualTo(expectedResponse.Data));
            Assert.That(response.isError, Is.EqualTo(expectedResponse.isError));
            Assert.That(response.statusCode, Is.EqualTo(expectedResponse.statusCode));
            Assert.That(response.ErrorMessage, Is.EqualTo(expectedResponse.ErrorMessage));
            A.CallTo(() => fakeHelper.ValidateId(request.VenueId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeHelper.CheckIdVenue(request.VenueId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeRepo.AddToDB(request)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMapper.Map<CommentRESPONSE>(comment)).MustHaveHappenedOnceExactly();
        }
    }
}