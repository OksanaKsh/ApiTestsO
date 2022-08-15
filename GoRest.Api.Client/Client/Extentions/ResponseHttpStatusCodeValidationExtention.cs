using FluentAssertions;
using GoRest.Api.Client.Client.Models;
using System.Net;

namespace GoRest.Api.Client.Client.Extentions
{
    public static class ResponseHttpStatusCodeValidationExtention
    {
        public static void ShouldBeOK(this GeneralResponse<List<GetUserResponseModel>> response)
        {
            response.Code.Should().Be(HttpStatusCode.OK);
        } 

        public static void ShouldBeOK(this GeneralResponse<GetUserResponseModel> response)
        {
            response.Code.Should().Be(HttpStatusCode.OK);
        } 
        
        public static void ShouldBeOK(this GeneralResponse<List<GetUserErrorResponseModel>> response)
        {
            response.Code.Should().Be(HttpStatusCode.OK);
        }

        public static void ShouldBeCreated(this GeneralResponse<GetUserResponseModel> response)
        {
            response.Code.Should().Be(HttpStatusCode.Created);
        }
        
        public static void ShouldBeNoContent(this GeneralResponse<GetUserResponseModel> response)
        {
            response.Code.Should().Be(HttpStatusCode.NoContent);
        }

        public static void ShouldBeNotFound(this GeneralResponse<GetUserResponseModel> response)
        {
            response.Code.Should().Be(HttpStatusCode.NotFound);
        }
        public static void ShouldBeNotFound(this GeneralResponse<GetUserErrorResponseModel> response)
        {
            response.Code.Should().Be(HttpStatusCode.NotFound);
        }

        public static void ShouldBeUnathorized(this GeneralResponse<AuthentificationFailedModel> response)
        {
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
        } 
        public static void ShouldBeUnprocessableEntity(this GeneralResponse<List<CreateUserErrorResponseModel>> response)
        {
            response.Code.Should().Be(HttpStatusCode.UnprocessableEntity);
        } 
        public static void ShouldBeUnprocessableEntity(this GeneralResponse<List<UpdateUserErrorResponseModel>> response)
        {
            response.Code.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

    }
}
