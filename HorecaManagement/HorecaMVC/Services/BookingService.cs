using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Bookings;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class BookingService : IBookingService
    {
        public HttpClient httpClient { get; }
        public IConfiguration configuration { get; }

        public BookingService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<int> GetPendingBookings()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.Admin}/{ClassConstants.ListCount}");

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return 0;
            }
            return 0;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByStatus(string status)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{status}");

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<BookingDto>>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<BookingDto> GetBookingByNumber(string bookingNo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.Details}" +
                $"/{ClassConstants.BookingNo}/{bookingNo}");

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<BookingDto>(response.Content.ReadAsStringAsync().Result);
                return result;
            } else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<BookingHistoryDto>> GetBookingsByUserId(string userId, string status)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.Member}/{userId}/{status}");

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<BookingHistoryDto>>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddBooking(MakeBookingDto bookingDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}");
            request.Content = new StringContent(JsonConvert.SerializeObject(bookingDto), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateBooking(EditBookingDto bookingDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}");
            request.Content = new StringContent(JsonConvert.SerializeObject(bookingDto), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteBooking(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{id}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}
