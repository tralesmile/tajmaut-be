using AutoMapper;
using FakeItEasy;
using TajmautMK.Common.Interfaces;
using TajmautMK.Common.Middlewares.Exceptions;
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
        public async Task CommentService_CreateComment_ReturnsComment()
        {
            // Arrange
            var request = new CommentREQUEST
            {
                VenueId = 1,
                Body = "Test",
                Review = 5,
            };
            var expectedMappedResponseData = new CommentRESPONSE()
            {
                Body = "Test",
                CommentId = 1,
                Review = 5,
                DateTime = DateTime.Now,
                UserId = 1,
                VenueId = 1
            };

            A.CallTo(() => _helperService.ValidateId(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _helperService.CheckIdVenue(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _commentRepo.AddToDB(request)).Returns(new Comment());
            A.CallTo(() => _commentRepo.DeleteComment(A<Comment>.Ignored)).Returns(true);
            A.CallTo(() => _mapper.Map<CommentRESPONSE>(A<Comment>.Ignored)).Returns(expectedMappedResponseData);

            // Act
            var response = await _commentService.CreateComment(request);

            // Assert
            Assert.That(response.Data.CommentId, Is.EqualTo(1));
            Assert.That(response.isError, Is.EqualTo(false));
            Assert.That(response.ErrorMessage, Is.EqualTo("None"));
        }

        [Test]
        public async Task CommentService_DeleteComment_ReturnsComment()
        {
            // Arrange
            var request = new CommentREQUEST
            {
                VenueId = 1,
                Body = "Test",
                Review = 5,
            };
            var expectedMappedResponseData = new CommentRESPONSE()
            {
                Body = "Test",
                CommentId = 1,
                Review = 5,
                DateTime = DateTime.Now,
                UserId = 1,
                VenueId = 1
            };

            A.CallTo(() => _helperService.CheckIdComment(1)).Returns(true);
            A.CallTo(() => _helperService.CheckManagerVenueRelation(1,1)).Returns(true);
            A.CallTo(() => _helperService.GetCommentId(1)).Returns(new Comment());
            A.CallTo(() => _helperService.CheckUserAdminOrManager()).Returns(true);
            A.CallTo(() => _helperService.GetMe()).Returns(1);
            A.CallTo(() => _mapper.Map<CommentRESPONSE>(A<Comment>.Ignored)).Returns(expectedMappedResponseData);


            // Act
            var response = await _commentService.DeleteComment(1);

            // Assert
            Assert.That(response.Data,Is.Null);
            Assert.That(response.isError, Is.EqualTo(false));
            Assert.That(response.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task CommentService_UpdateComment_ReturnsUpdatedComment()
        {
            // Arrange
            var currentUserID = 1;
            var commentByID = await _helperService.GetCommentId(1);

            var request = new CommentREQUEST
            {
                VenueId = 1,
                Body = "Test",
                Review = 5,
            };
            var expectedMappedResponseData = new CommentRESPONSE()
            {
                Body = "Test",
                CommentId = 1,
                Review = 5,
                DateTime = DateTime.Now,
                UserId = currentUserID,
                VenueId = 1
            };

            A.CallTo(() => _helperService.CheckIdComment(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _helperService.CheckIdVenue(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _helperService.GetMe()).Returns(currentUserID);
            A.CallTo(() => _helperService.GetCommentId(1)).Returns(commentByID);
            A.CallTo(() => _helperService.CheckUserCommentRelation(commentByID, 1)).Returns(true);
            A.CallTo(() => _commentRepo.UpdateComment(commentByID, request)).Returns(new Comment());
            A.CallTo(() => _mapper.Map<CommentRESPONSE>(A<Comment>.Ignored)).Returns(expectedMappedResponseData);

            // Act
            var response = await _commentService.UpdateComment(request,1);

            // Assert
            Assert.IsNotNull(response.Data);
            Assert.IsFalse(response.isError);
            Assert.That(response.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task CommentService_GetCommentsByVenueID_ReturnsListOfComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment(),
                new Comment(),
                new Comment(),
            };

            var expectedResponse = new ServiceResponse<List<CommentRESPONSE>>
            {
                Data = new List<CommentRESPONSE>
                    {
                        new CommentRESPONSE(),
                        new CommentRESPONSE(),
                        new CommentRESPONSE(),
                    }
            };

            A.CallTo(() => _commentRepo.GetAllCommentsByVenue(A<int>.Ignored)).Returns(comments);
            A.CallTo(() => _helperService.ValidateId(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _helperService.CheckIdVenue(A<int>.Ignored)).Returns(true);
            A.CallTo(() => _mapper.Map<List<CommentRESPONSE>>(A<List<Comment>>.Ignored)).Returns(expectedResponse.Data);

            // Act
            var response = await _commentService.GetCommentsByVenueID(1);

            // Assert
            Assert.AreEqual(expectedResponse.Data.Count, response.Data.Count);
            Assert.AreEqual(expectedResponse.Data,response.Data);
            for (int i = 0; i < expectedResponse.Data.Count; i++)
            {
                Assert.AreEqual(expectedResponse.Data[i].VenueId, response.Data[i].VenueId);
                Assert.AreEqual(expectedResponse.Data[i].DateTime, response.Data[i].DateTime);
                Assert.AreEqual(expectedResponse.Data[i].CommentId, response.Data[i].CommentId);
                Assert.AreEqual(expectedResponse.Data[i].UserId, response.Data[i].UserId);
                Assert.AreEqual(expectedResponse.Data[i].Body, response.Data[i].Body);
                Assert.AreEqual(expectedResponse.Data[i].Review, response.Data[i].Review);
            }
            Assert.IsFalse(response.isError);
            Assert.That(response.ErrorMessage, Is.EqualTo(string.Empty));
        }


        //ERROR THROWING TESTS

        [Test]
        public async Task CommentService_CreateComment_ThrowsCustomError()
        {
            // Arrange
            var request = new CommentREQUEST
            {
                VenueId = 1,
                Body = "Test",
                Review = 5,
            };
            var expectedMappedResponseData = new CommentRESPONSE()
            {
                Body = "Test",
                CommentId = 1,
                Review = 5,
                DateTime = DateTime.Now,
                UserId = 1,
                VenueId = 1
            };

            A.CallTo(() => _helperService.ValidateId(A<int>.Ignored)).Returns(false);
            A.CallTo(() => _helperService.ValidateId(A<int>.Ignored)).Throws(new CustomError(400,"Invalid ID"));

            // Act
            var response = await _commentService.CreateComment(request);

            // Assert
            Assert.That(response.Data, Is.EqualTo(null));
            Assert.That(response.isError, Is.EqualTo(true));
            Assert.That(response.statusCode, Is.EqualTo(400));
            Assert.That(response.ErrorMessage, Is.EqualTo("Invalid ID"));

        }

        [Test]
        public async Task CommentService_GetCommentsByVenueID_ThrowsCustomError()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment(),
                new Comment(),
                new Comment(),
            };

            var expectedResponse = new ServiceResponse<List<CommentRESPONSE>>
            {
                Data = new List<CommentRESPONSE>
                    {
                        new CommentRESPONSE(),
                        new CommentRESPONSE(),
                        new CommentRESPONSE(),
                    }
            };

            A.CallTo(() => _commentRepo.GetAllCommentsByVenue(A<int>.Ignored)).Throws(new CustomError(404, "This venue has no comments"));

            // Act
            var response = await _commentService.GetCommentsByVenueID(1);

            // Assert
            Assert.IsNull(response.Data);
            Assert.IsTrue(response.isError);
            Assert.That(response.ErrorMessage, Is.EqualTo("This venue has no comments"));
        }


        [Test]
        public async Task CommentService_UpdateComment_ThrowsCustomError()
        {
            // Arrange
            var request = new CommentREQUEST
            {
                VenueId = 1,
                Body = "Test",
                Review = 5,
            };

            A.CallTo(() => _helperService.CheckIdComment(A<int>.Ignored)).Throws(new CustomError(404, $"Comment not found"));

            // Act
            var response = await _commentService.UpdateComment(request, 1);

            // Assert
            Assert.That(response.Data, Is.EqualTo(null));
            Assert.That(response.isError, Is.EqualTo(true));
            Assert.That(response.ErrorMessage, Is.EqualTo($"Comment not found"));
        }

        [Test]
        public async Task CommentService_DeleteComment_ThrowsCustomError()
        {
            // Arrange

            A.CallTo(() => _helperService.CheckIdComment(1)).Throws(new CustomError(404, $"Comment not found"));

            // Act
            var response = await _commentService.DeleteComment(1);

            // Assert
            Assert.That(response.Data, Is.Null);
            Assert.IsTrue(response.isError);
            Assert.That(response.ErrorMessage, Is.EqualTo($"Comment not found"));
        }

    }
}