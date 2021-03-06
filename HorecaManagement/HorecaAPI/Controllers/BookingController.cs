using Horeca.Core.Handlers.Commands.Bookings;
using Horeca.Core.Handlers.Queries.Bookings;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.AuthUtils.PolicyProvider;
using Horeca.Shared.Constants;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Bookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Horeca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator mediator;

        public BookingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Retrive the bookings by schedule id .
        /// </summary>
        /// <returns>
        ///Retrive the bookings by schedule id .
        /// </returns>
        /// <response code="200">Success retrieving  the bookings by schedule id .list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [Route(RouteConstants.BookingConstants.GetAllBookingsByScheduleId)]
        [PermissionAuthorize(nameof(Booking), Permissions.Read)]
        [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetAllBookingsByScheduleIdQuery(int scheduleId)
        {
            return Ok(await mediator.Send(new GetAllBookingsByScheduleIdQuery(scheduleId)));
        }

        /// <summary>
        /// Get all members' bookings of a specific status for a specific schedule.
        /// </summary>
        /// <param name="status">Booking status, e.g. Pending, Expired, Completed</param>
        /// <param name="scheduleId"> schedule Id </param>
        /// <returns>
        /// A list of members' booking records will be returned.
        /// </returns>
        /// <response code="200">Success retrieving Booking list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [Route(RouteConstants.BookingConstants.GetAllBookingsOfStatus)]
        [PermissionAuthorize(nameof(Booking), Permissions.Read)]
        [ProducesResponseType(typeof(IEnumerable<BookingDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetAllBookingsOfStatus(int scheduleId, string status = "all")
        {
            return Ok(await mediator.Send(new GetAllBookingsOfStatusQuery(status, scheduleId)));
        }

        /// <summary>
        /// Retrieve a specific booking details based on the Booking Number given.
        /// </summary>
        /// <param name="bookingNo">Booking Number</param>
        /// <returns>
        /// Relevant booking details will be returned
        /// </returns>
        /// <response code="200">Success retrieving booking details list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [Route(RouteConstants.BookingConstants.GetByBookingNo)]
        [PermissionAuthorize(nameof(Booking), Permissions.Read)]
        [ProducesResponseType(typeof(IEnumerable<BookingDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetByBookingNo(string bookingNo)
        {
            return Ok(await mediator.Send(new GetByBookingNoQuery(bookingNo)));
        }

        /// <summary>
        /// Get all bookings of a specific status for a specific user.
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="status">Order Status, eg. Pending, Complete, Expired</param>
        /// <returns>
        /// Relevant booking details will be returned based on the user id and status given
        /// </returns>
        /// <response code="200">Success retrieving booking history list</response>
        /// <response code="400">Bad request</response>
        [HttpGet]
        [Route(RouteConstants.BookingConstants.GetAllBookingsByUserId)]
        [PermissionAuthorize(nameof(Booking), Permissions.Read)]
        [ProducesResponseType(typeof(IEnumerable<BookingHistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> GetAllBookingsByUserId(string userId, string status = "all")
        {
            return Ok(await mediator.Send(new GetAllBookingsByUserIdQuery(userId, status)));
        }

        /// <summary>
        /// Add a new member booking to the database. This checks for number of seat avaiable for each session also.
        /// </summary>
        /// <param name="MakeBookingDto">MakeBookingDto object</param>
        /// <returns>
        /// New Member Booking and the number of schedule available seat will be updated from the database
        /// </returns>
        ///  /// <response code="201">Success creating new booking</response>
        /// <response code="400">Bad request</response
        [HttpPost]
        [PermissionAuthorize(nameof(Booking), Permissions.Create)]
        [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Post([FromBody] MakeBookingDto model)
        {
            return StatusCode((int)HttpStatusCode.Created, await mediator.Send(new AddBookingCommand(model)));
        }

        /// <summary>
        /// Update an existing member's booking in the database.
        /// </summary>
        /// <param name="EditBookingDto">Booking Object Information</param>
        /// <returns>
        /// No response return back.
        /// Otherwise, update member's booking status process unable to be conducted due to booking ID not found.
        /// </returns>
        /// <response code="200">Success updating existing booking</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [PermissionAuthorize(nameof(Booking), Permissions.Update)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> Update([FromBody] EditBookingDto model)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new EditBookingCommand(model)));
        }

        /// <summary>
        ///  Delete an existing Booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Success delete an existing booking</response>
        /// <response code="400">Bad request</response
        [HttpDelete]
        [PermissionAuthorize(nameof(Booking), Permissions.Delete)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDto))]
        public async Task<IActionResult> DeleteById(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, await mediator.Send(new DeleteBookingCommand(id)));
        }
    }
}