﻿
using System.Threading.Tasks;
using Models;
using RepoLayer;
using Moq;
using System;
using BusinessLayer;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Test.Yoink
{
    public class YoinkRepoLayerClassTests
    {


        private Helpers helpers = new Helpers();

        // This method would test the CreateUserProfile

        [Fact]
        public async Task TestingCreateUserProfileReturnsBool()
        {

            // Arrange
            bool expectedReturn = true;
            Profile profile = helpers.fakeProfile();

            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            string sql = "DELETE FROM Profiles WHERE fk_userID = @test1";

            SqlConnection conn = new SqlConnection(helpers.ConnString);

            bool? result = null;

            // Act

            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@test1", "test1");

                bool truncated = await helpers.TruncateTableAsync(command, conn);

                if (truncated)
                {
                    result = await TheClassBeingTested.CreateProfileAsync("test1", profile.Name, profile.Email, profile.Picture, profile.PrivacyLevel);
                }
            }


            // Assert
            Assert.NotNull(result);
            if (result != null)
            {
                Assert.Equal(expectedReturn, result);
            }
        }

        public async Task TestingGetProfileByUserIDReturnsProfile()
        {
            // Arrange

            Profile expectedReturn = helpers.fakeProfile();
            expectedReturn.Fk_UserID = "test1";

            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            Profile? result = await TheClassBeingTested.GetProfileByUserIDAsync(expectedReturn.Fk_UserID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReturn.ProfileID, result?.ProfileID);
        }

        [Fact]
        public async Task TestingUpdateInvestmentAsyncReturnsInvestment()
        {
            // Arrange
            Investment i = helpers.fakeInvestment();
            i.InvestmentID = new Guid("11322563-cf3a-462e-90f6-03bd0d7e0fdb");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            bool result = await TheClassBeingTested.UpdateInvestmentAsync(i);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// This method tests to see if the user has gotten their recently bought order from the database by their portfolio ID
        /// </summary>
        /// <returns>returns an asyc Task as a Buy Object</returns>
        [Fact]
        public async Task Testing_GetRecentBuyByPortfolioId_if_GOTTEN()
        {
            // Arrange
            Buy expectedOBJ = helpers.fakeBuy();
            expectedOBJ.Fk_PortfolioID = new Guid("86d66000-5874-427a-8682-1ed02f4bb2ca");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            Buy? result = null;
            
            // Act
            result = await TheClassBeingTested.GetRecentBuyByPortfolioId(expectedOBJ.Fk_PortfolioID);


            // Assert
            Assert.NotNull(result);
            if(result != null)
            {
                Assert.IsType<Guid>(result?.BuyID);
                Assert.Equal(expectedOBJ.Fk_PortfolioID, result?.Fk_PortfolioID);
            }
        }//End of GetRecentBuyByPortfolioId GOTTEN


        /// <summary>
        /// This method tests to see if the user did not get their recently bought order from the database by their portfolio ID
        /// </summary>
        /// <returns>returns an asyc Task as a Buy Object</returns>
        [Fact]
        public async Task Testing_GetRecentBuyByPortfolioId_if_NOT_GOTTEN()
        {
            // Arrange
            Buy expectedOBJ = helpers.fakeBuy();
            //This portfolioID will be incorrect and therefore will return a null OBJ
            expectedOBJ.Fk_PortfolioID = Guid.NewGuid();


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            Buy? result = await TheClassBeingTested.GetRecentBuyByPortfolioId(expectedOBJ.Fk_PortfolioID);

            // Assert
            Assert.Null(result);
            if(result == null)
            {
                Assert.Equal(null, result?.Fk_PortfolioID);
            }
        }//End of GetRecentBuyByPortfolioId NOT GOTTEN


        /// <summary>
        /// This method tests to see if the user has gotten their recently sold order from the database by their portfolio ID
        /// </summary>
        /// <returns>returns an asyc Task as a Sell Object</returns>
        [Fact]
        public async Task Testing_GetRecentSellByPortfolioId_if_GOTTEN()
        {
            // Arrange
            Sell expectedOBJ = helpers.fakeSell();
            expectedOBJ.Fk_PortfolioID = new Guid("218e6e41-3932-4eb4-a12d-dce5ff367b73");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            Sell? result = await TheClassBeingTested.GetRecentSellByPortfolioId(expectedOBJ.Fk_PortfolioID);

            // Assert
            Assert.NotNull(result);
            if(result != null)
            {
                Assert.IsType<Guid>(result?.SellID);
                Assert.Equal(expectedOBJ.Fk_PortfolioID, result?.Fk_PortfolioID);
            }
        }//End of GetRecentSellByPortfolioId Test - GOTTEN



        /// <summary>
        /// This method tests to see if the user did not get their recently sold order from the database by their portfolio ID
        /// </summary>
        /// <returns>returns an asyc Task as a Sell Object</returns>
        [Fact]
        public async Task Testing_GetRecentSellByPortfolioId_if_NOT_GOTTEN()
        {
            // Arrange
            Sell expectedOBJ = helpers.fakeSell();
            expectedOBJ.Fk_PortfolioID = new Guid("000e6e41-3932-4eb4-a12d-dce5ff367b00");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            Sell? result = await TheClassBeingTested.GetRecentSellByPortfolioId(expectedOBJ.Fk_PortfolioID);

            // Assert
            Assert.Null(result);
            if(result == null)
            {
                Assert.Equal(null, result?.Fk_PortfolioID);
            }
        }//End of GetRecentSellByPortfolioId Test - NOT GOTTEN


        /// <summary>
        /// This method tests to see if the user has successfully updated their investment's current stock price
        /// </summary>
        /// <returns>returns an asyc Task as a Sell Object</returns>
        [Fact]
        public async Task Testing_UpdateCurrentPriceAsync_if_Updated()
        {
            // Arrange
            GetInvestmentDto dto = helpers.fakeGetInvestmentDto();
            dto.PortfolioId = new Guid("218e6e41-3932-4eb4-a12d-dce5ff367b73");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            bool result = await TheClassBeingTested.UpdateCurrentPriceAsync(dto, 101);

            // Assert
            Assert.True(result);
            if(result == true)
            {
                Assert.Equal(true, result);
            }
        }//End of UpdateCurrentPriceAsync Test - Updated


        /// <summary>
        /// This method tests to see if the user has successfully updated their investment's current stock price
        /// </summary>
        /// <returns>returns an asyc Task as a Sell Object</returns>
        [Fact]
        public async Task Testing_UpdateCurrentPriceAsync_if_Not_Updated()
        {
            // Arrange
            GetInvestmentDto dto = helpers.fakeGetInvestmentDto();

            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            bool result = await TheClassBeingTested.UpdateCurrentPriceAsync(dto, 101);

            // Assert
            Assert.False(result);
            if(result == false)
            {
                Assert.Equal(false, result);
            }
        }//End of UpdateCurrentPriceAsync Test - Not Updated


        /// <summary>
        /// This method tests to see if the user has successfully updated their investment's current stock price
        /// </summary>
        /// <returns>returns an asyc Task as a Sell Object</returns>
        [Fact]
        public async Task Testing_GetInvestmentByPortfolioIDAsync_if_Updated()
        {
            // Arrange
            GetInvestmentDto dto = helpers.fakeGetInvestmentDto();
            dto.PortfolioId = new Guid("218e6e41-3932-4eb4-a12d-dce5ff367b73");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            bool result = await TheClassBeingTested.UpdateCurrentPriceAsync(dto, 101);

            // Assert
            Assert.True(result);
            if(result == true)
            {
                Assert.Equal(true, result);
            }
        }//End of UpdateCurrentPriceAsync Test - Updated


        // [Fact]
        // public void TestingCreateUserProfile()
        // {
        //     //Hardcode data for mock ProfileDto
        [Fact]
        public async Task TestingGetRecentPostByUserIdAsyncReturnsPost()
        {
            //Arrange 
            Post p = helpers.fakePost();
            string auth0MockId = "test2";
            p.PostID =new Guid("3555b90f-4fde-45c8-a19c-1ef72af0ae6c");
            var fakeConfig = new Mock<IConfiguration>();
            
            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);
            dbsRequests TestedClass = new dbsRequests(fakeConfig.Object);
            // Act
            Post? result = await TestedClass.GetRecentPostByUserId(auth0MockId);
            //Assert
            Assert.Equal(p.PostID,result?.PostID);
        }
        [Fact]
        public async Task TestingGetAllPostAsync()
        {
        //Arrange
        Post p = helpers.fakePost();
        p.PostID = new Guid("48f34239-7bb8-4d3b-92eb-0100e83277cf");
        var fakeConfig = new Mock<IConfiguration>();
            
            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);
            dbsRequests TestedClass = new dbsRequests(fakeConfig.Object);
        //Act
        List<Post> result = await TestedClass.GetAllPostAsync();
        //Assert
        Assert.NotEqual(p.PostID,result[0].PostID);
        }
        [Fact]
       public async Task TestingGetAllInvestmentsByPortfolioIDAsync()
       {
        //Arrange 
         Guid TestporfolioID =new Guid("86d66000-5874-427a-8682-1ed02f4bb2ca");
         var fakeConfig = new Mock<IConfiguration>();
         fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);
            dbsRequests TestedClass = new dbsRequests(fakeConfig.Object);
        //Act
        List<Investment> result = await TestedClass.GetAllInvestmentsByPortfolioIDAsync(TestporfolioID);
        //Assert
        Assert.Equal(TestporfolioID,result[0].Fk_PortfolioID);
       }
       [Fact]
        public async Task TestingGetNumberOfCommentsByPostIdAsync()
        {
            //Arrange
         Post p = helpers.fakePost();
         p.PostID = new Guid("3555b90f-4fde-45c8-a19c-1ef72af0ae6c"); 
          var fakeConfig = new Mock<IConfiguration>();
         fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);
            dbsRequests TestedClass = new dbsRequests(fakeConfig.Object);
            //Act
            int? result = await TestedClass.GetNumberOfCommentsByPostIdAsync(p.PostID);
            //Assert
            Assert.NotNull(result);
        }
        

        [Fact]
        public async Task CreateLikeOnPostAsyncReturnsTrueOnSuccessfulCreate()
        {
            // Arrange
            Guid postId = new Guid("e367da77-cdde-4930-8d44-2f180c90ab69");
            LikeDto likeDto = new LikeDto(postId);
            string auth0UserId = "test1";



            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            string sql = "Delete FROM LikesPosts WHERE fk_userID = @test1 AND fk_postID=@postId";

            SqlConnection conn = new SqlConnection(helpers.ConnString);

            bool? result = null;

            // Act

            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@test1", auth0UserId);
                command.Parameters.AddWithValue("@postId", postId);

                bool truncated = await helpers.TruncateTableAsync(command, conn);

                if (truncated)
                {
                    result = await TheClassBeingTested.CreateLikeOnPostAsync(likeDto, auth0UserId);
                }
            }
            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task DeleteLikeOnPostAsyncReturnsTrueOnSuccessfulDelete()
        {
            // Arrange
            Guid postId = new Guid("e367da77-cdde-4930-8d44-2f180c90ab69");
            LikeDto unlikeDto = new LikeDto(postId);
            string auth0UserId = "test3";


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            string sql = "INSERT INTO LikesPosts (fk_userID, fk_postID) VALUES (@auth0UserId, @postId)";

            SqlConnection conn = new SqlConnection(helpers.ConnString);

            bool? result = null;

            // Act

            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@auth0UserId", auth0UserId);
                command.Parameters.AddWithValue("@postId", postId);

                bool truncated = await helpers.TruncateTableAsync(command, conn);

                if (truncated)
                {
                    result = await TheClassBeingTested.DeleteLikeOnPostAsync(unlikeDto, auth0UserId);
                }
            }
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckIfUserAlreadyHasLikeOnPostReturnsTrueIfExists()
        {
            // Arrange
            string auth0UserId = "test1";
            Guid postId = new Guid("e367da77-cdde-4930-8d44-2f180c90ab69");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            bool result = await TheClassBeingTested.CheckIfUserAlreadyHasLike_OnPost(auth0UserId, postId);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task CheckIfUserAlreadyHasLikeOnCommentReturnsTrueIfExists()
        {
            // Arrange
            string auth0UserId = "test1";
            Guid postId = new Guid("dc935607-d264-4b1c-bfb4-2ecd80dec5a0");


            var fakeConfig = new Mock<IConfiguration>();

            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);


            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act

            bool result = await TheClassBeingTested.CheckIfUserAlreadyHasLike_OnComment(auth0UserId, postId);

            // Assert
            Assert.True(result);
        }

        [Fact]

        public async Task TestingEditProfileAsyncReturnsBoolAsync()
        {
            // Arrange
            Profile p = helpers.fakeProfile();
            p.Fk_UserID = "test1";

            var fakeConfig = new Mock<IConfiguration>();
            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);

            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            bool result = await TheClassBeingTested.EditProfileAsync(p.Fk_UserID, p.Name, p.Email, p.Picture, p.PrivacyLevel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestingGetAllPortfoliosByUserIDAsyncReturnsListOfPortfolios()
        {
            // Arrange
            string userid = "test1";

            var fakeConfig = new Mock<IConfiguration>();
            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);

            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            List<Portfolio?> result = await TheClassBeingTested.GetALL_PortfoliosByUserIDAsync(userid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userid, result[0]?.Fk_UserID);

        }

        [Fact]
        public async Task UpdatePostAsync()
        {
            //Arrange
            Guid postId = new Guid("9fa85f64-5717-4562-b3fc-2c963f66afa6");
            EditPostDto p = new EditPostDto(postId, "edited", 0);
            var fakeconfig = new Mock<IConfiguration>();
            fakeconfig.SetupGet(fConf => fConf["ConnectionStrings: DefaultConnection"]).Returns(helpers.ConnString);
            var TheClassBeingTested = new dbsRequests(fakeconfig.Object);
            //Act
            bool updated = await TheClassBeingTested.UpdatePostAsync(p);
            //Assert
            Assert.True(updated);

        }

        [Fact]
        public async Task TestingGetPortfolioByPorfolioIDAsyncReturnsPortfolio()
        {
            // Arrange
            Guid portfolioID = new Guid("446eb468-eb65-45dc-8fc3-12cbce43879d");

            var fakeConfig = new Mock<IConfiguration>();
            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);

            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            Portfolio? result = await TheClassBeingTested.GetPortfolioByPorfolioIDAsync(portfolioID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(portfolioID, result?.PortfolioID);
        }

        [Fact]
        public async Task TestingGetRecentPortfoliosByUserIDAsyncReturnsAPortfolio()
        {
            // Arrange
            string userid = "test1";

            var fakeConfig = new Mock<IConfiguration>();
            fakeConfig.SetupGet(fConf => fConf["ConnectionStrings:DefaultConnection"])
                .Returns(helpers.ConnString);

            var TheClassBeingTested = new dbsRequests(fakeConfig.Object);

            // Act
            Portfolio? result = await TheClassBeingTested.GetRecentPortfoliosByUserIDAsync(userid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userid, result?.Fk_UserID);
        }
    }
}
