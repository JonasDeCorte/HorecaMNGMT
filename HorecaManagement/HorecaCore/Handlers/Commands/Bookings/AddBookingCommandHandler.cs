using AutoMapper;
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
            private readonly IMapper mapper;
            private readonly UserManager<ApplicationUser> userManager;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public AddBookingCommandHandler(IUnitOfWork repository, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                this.repository = repository;
                this.mapper = mapper;
                this.userManager = userManager;
            }

            public async Task<BookingDto> Handle(AddBookingCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {object} with request: {@Id}", nameof(Booking), request);
                var user = await userManager.FindByIdAsync(request.Model.UserId);

                if (user == null)
                {
                    logger.Error(UserNotFoundException.Instance);
                    throw new UserNotFoundException();
                }
                var schedule = repository.Schedules.Get(request.Model.ScheduleId);

                if (schedule == null)
                {
                    logger.Error(EntityNotFoundException.Instance);
                    throw new EntityNotFoundException();
                }

                int remainingSeats = schedule.AvailableSeat - request.Model.Pax;

                if (remainingSeats < 0)
                {
                    logger.Error($"Unable to add member new booking {request.Model} due to insufficient seat");
                    logger.Error(UnAvailableSeatException.Instance);
                    throw new UnAvailableSeatException();
                }

                Booking entity = CreateBookingObject(request, user, schedule, logger);

                entity = await repository.Bookings.Add(entity);

                logger.Info("adding {bookingno} with id {id}", entity.BookingNo, entity.Id);
                return mapper.Map<BookingDto>(entity);
            }

            private static Booking CreateBookingObject(AddBookingCommand request, ApplicationUser user, Schedule schedule, Logger logger)
            {
                Booking booking = new Booking();
                booking.BookingStatus = Constants.BookingStatus.PENDING;
                booking.BookingNo = Guid.NewGuid().ToString();
                booking.BookingDate = schedule.ScheduleDate;
                booking.FullName = request.Model.FullName;
                booking.PhoneNo = request.Model.PhoneNo;
                booking.UserId = user.Id;
                booking.Pax = request.Model.Pax;
                booking.ScheduleId = schedule.Id;
                IsTimeWithinScheduleRange(request, schedule, logger);
                booking.CheckIn = request.Model.CheckIn;
                booking.CheckOut = request.Model.CheckOut;
                return booking;
            }

            private static void IsTimeWithinScheduleRange(AddBookingCommand request, Schedule schedule, Logger logger)
            {
                if (request.Model.CheckIn < schedule.StartTime || request.Model.CheckIn > schedule.EndTime
                    ||
                    request.Model.CheckOut < schedule.StartTime || request.Model.CheckOut > schedule.EndTime)
                {
                    logger.Error(TimeIsNotWithinRangeException.Instance);
                    throw new TimeIsNotWithinRangeException();
                }
            }
        }
    }
}