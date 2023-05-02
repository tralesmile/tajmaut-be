using AutoMapper;
using Azure;
using FakeItEasy;
using System.Xml.Linq;
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

        private readonly CommentService _commentService;
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        private readonly IHelperValidationClassService _helperService;

        public CommentTests()
        {
            //DEPENDENCIES
            _commentRepo = A.Fake<ICommentRepository>();
            _mapper = A.Fake<IMapper>();
            _helperService = A.Fake<IHelperValidationClassService>();

            //SUT
            _commentService = new CommentService(_commentRepo, _mapper, _helperService);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task CommentService_CreateComment_ReturnsComment()
        {
            // Arrange
            var request = new CommentREQUEST
            {
                VenueId = 1,
                Body = "Test",
                Review = 5,
            };
            var comment = A.Fake<Comment>();
            var expectedResponse = A.Fake<ServiceResponse<CommentRESPONSE>>();

            A.CallTo(() => _helperService.ValidateId(request.VenueId)).Returns(true);
            A.CallTo(() => _helperService.CheckIdVenue(request.VenueId)).Returns(true);
            A.CallTo(() => _commentRepo.AddToDB(request)).Returns(comment);
            A.CallTo(() => _mapper.Map<CommentRESPONSE>(comment)).Returns(expectedResponse.Data);


            // Act
            var response = await _commentService.CreateComment(request);

            // Assert
            Assert.That(response.Data, Is.EqualTo(expectedResponse.Data));
            Assert.That(response.isError, Is.EqualTo(expectedResponse.isError));
            Assert.That(response.statusCode, Is.EqualTo(expectedResponse.statusCode));
            Assert.That(response.ErrorMessage, Is.EqualTo(expectedResponse.ErrorMessage));
            //A.CallTo(() => _helperService.ValidateId(request.VenueId)).MustHaveHappenedOnceExactly();
            //A.CallTo(() => _helperService.CheckIdVenue(request.VenueId)).MustHaveHappenedOnceExactly();
            //A.CallTo(() => _commentRepo.AddToDB(request)).MustHaveHappenedOnceExactly();
            //A.CallTo(() => _mapper.Map<CommentRESPONSE>(comment)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task CommentService_GetCommentsByVenueID_ReturnsListOfComments()
        {
            // Arrange
            int venueId = 1;
            var comments = new List<Comment>
            {
                A.Fake<Comment>(),
                A.Fake<Comment>(),
                A.Fake < Comment >(),
            };

            var expectedResponse = new ServiceResponse<List<CommentRESPONSE>>
            {
                Data = new List<CommentRESPONSE>
                    {
                        A.Fake<CommentRESPONSE>(),
                        A.Fake < CommentRESPONSE >(),
                        A.Fake < CommentRESPONSE >(),
                    }
            };

            A.CallTo(() => _commentRepo.GetAllCommentsByVenue(venueId)).Returns(comments);
            A.CallTo(() => _helperService.ValidateId(venueId)).Returns(true);
            A.CallTo(() => _helperService.CheckIdVenue(venueId)).Returns(true);
            A.CallTo(() => _mapper.Map<List<CommentRESPONSE>>(A<List<Comment>>._)).Returns(expectedResponse.Data);

            // Act
            var response = await _commentService.GetCommentsByVenueID(venueId);

            // Assert
            Assert.AreEqual(expectedResponse.Data.Count, response.Data.Count);
            Assert.AreEqual(expectedResponse.Data,response.Data);
            for (int i = 0; i < expectedResponse.Data.Count; i++)
            {
                Assert.AreEqual(expectedResponse.Data[i].VenueId, response.Data[i].VenueId);
            }
            Assert.IsFalse(response.isError);
        }
    }
}