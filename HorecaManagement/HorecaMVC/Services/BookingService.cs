using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Bookings;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class BookingService : IBookingService
    {
        public HttpClient HttpClient { get; }
        public IConfiguration Configuration { get; }

        public BookingService(HttpClient httpClient, IConfiguration configuration)
        {
            this.HttpClient = httpClient;
            this.Configuration = configuration;
        }

        public async Task<int> GetPendingBookings()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.ListCount}");

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return 0;
                }
                return result;
            }
            return 0;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsBySchedule(int scheduleId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.Schedule}?{ClassConstants.ScheduleId}={scheduleId}");

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<BookingDto>>(
                    response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return new List<BookingDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByStatus(int scheduleId, string status)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.Schedule}" +
                $"?{ClassConstants.ScheduleId}={scheduleId}&{ClassConstants.Status}={status}");

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<BookingDto>>(response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return new List<BookingDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<BookingDto> GetBookingByNumber(string bookingNo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.Details}/{ClassConstants.BookingNo}" +
                $"?bookingNo={bookingNo}");

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<BookingDto>(response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return new BookingDto();
                }
                return result;
            }
            return null;
        }

        public async Task<BookingHistoryDto> GetBookingsByUserId(string userId, string status)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}/{ClassConstants.User}/{ClassConstants.BookingStatus}" +
                $"?{ClassConstants.UserId}={userId}&{ClassConstants.Status}={status}");

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<BookingHistoryDto>(response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return new BookingHistoryDto();
                }
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddBooking(MakeBookingDto bookingDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(bookingDto), Encoding.UTF8, "application/json")
            };

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateBooking(EditBookingDto bookingDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(bookingDto), Encoding.UTF8, "application/json")
            };

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteBooking(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{Configuration.GetSection("BaseURL").Value}/{ClassConstants.Booking}?id={id}");

            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}