using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Bookings
{
    public class AddBookingCommand : IRequest<BookingDto>
    {
        public MakeBookingDto Model { get; }

        public AddBookingCommand(MakeBookingDto model)
        {
            Model = model;
        }

        public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, BookingDto>
        {
            private readonly IUnitOfWork repository;
            private readonly IValidator<MakeBookingDto> validator;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public AddBookingCommandHandler(IUnitOfWork repository, IValidator<MakeBookingDto> validator)
            {
                this.repository = repository;
                this.validator = validator;
            }

            public async Task<BookingDto> Handle(AddBookingCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {object} with request: {@Id}", nameof(Booking), request);

                var scheduleToBeFound = repository.RestaurantSchedules.Get(request.Model.ScheduleID);
                int pax = scheduleToBeFound.AvailableSeat - request.Model.Pax;

                if (pax < 0)
                {
                    logger.Error($"Unable to add member new booking {request.Model.Booking} due to insufficient seat");
                    return null;
                }
                var result = validator.Validate(request.Model);

                if (!result.IsValid)
                {
                    logger.Error("Invalid model with errors: ", result.Errors);

                    var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                    throw new InvalidRequestBodyException
                    {
                        Errors = errors
                    };
                }

                var entity = new Booking
                {
                    BookingDate = request.Model.Booking.BookingDate,
                    BookingNo = Guid.NewGuid().ToString(),
                    BookingStatus = Constants.BookingStatus.PENDING,
                    CheckIn = request.Model.Booking.CheckIn,
                    CheckOut = request.Model.Booking.CheckOut,
                    FullName = request.Model.Booking.FullName,
                    PhoneNo = request.Model.Booking.PhoneNo,
                    UserId = request.Model.Booking.UserID,
                };

                entity = await repository.Bookings.Add(entity);
                BookingDetail bookingDetail = new()
                {
                    BookingId = entity.Id,
                    RestaurantScheduleId = request.Model.ScheduleID,
                    Pax = request.Model.Pax
                };
                await repository.BookingDetails.CreateBookingDetail(bookingDetail);

                logger.Info("adding {bookingno} with id {id}", entity.BookingNo, entity.Id);
                return new BookingDto()
                {
                    BookingDate = entity.BookingDate,
                    Id = entity.Id,
                    BookingNo = entity.BookingNo,
                    BookingStatus = entity.BookingStatus,
                    CheckIn = entity.CheckIn,
                    CheckOut = entity.CheckOut,
                    FullName = entity.FullName,
                    PhoneNo = entity.PhoneNo,
                    UserID = entity.UserId,
                };
            }
        }
    }
}