using FluentAssertions;
using GoRest.Api.Client.Client.Models;
using System.Net;

namespace GoRest.Api.Client.Client.Extentions
{
    public static class ResponseHttpStatusCodeValidationExtention 
    {
        public static void ShouldBeOK <TResponse>(this GeneralResponse<TResponse> response)
        {
            response.Code.Should().Be(HttpStatusCode.OK);
        } 
        
        public static void ShouldBeCreated<TResponse>(this GeneralResponse<TResponse> response)
        {
            response.Code.Should().Be(HttpStatusCode.Created);
        }
        
        public static void ShouldBeNoContent<TResponse>(this GeneralResponse<TResponse> response)
        {
            response.Code.Should().Be(HttpStatusCode.NoContent);
        }

        public static void ShouldBeNotFound<TResponse>(this GeneralResponse<TResponse> response)
        {
            response.Code.Should().Be(HttpStatusCode.NotFound);
        }
        
        public static void ShouldBeUnathorized<TResponse>(this GeneralResponse<TResponse> response)
        {
            response.Code.Should().Be(HttpStatusCode.Unauthorized);
        } 

        public static void ShouldBeUnprocessableEntity<TResponse>(this GeneralResponse<TResponse> response)
        {
            response.Code.Should().Be(HttpStatusCode.UnprocessableEntity);
        } 
    }
}
