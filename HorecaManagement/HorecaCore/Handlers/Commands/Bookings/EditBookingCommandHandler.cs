using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Bookings;
using Horeca.Shared.Utils;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Bookings
{
    public class EditBookingCommand : IRequest<int>
    {
        public EditBookingDto Model { get; }

        public EditBookingCommand(EditBookingDto model)
        {
            Model = model;
        }

        public class EditBookingCommandHandler : IRequestHandler<EditBookingCommand, int>
        {
            private readonly IUnitOfWork repository;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public EditBookingCommandHandler(IUnitOfWork repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(EditBookingCommand request, CancellationToken cancellationToken)
            {
                logger.Info("trying to create {object} with request: {@Id}", nameof(Booking), request);

                var bookingFromDb = await repository.Bookings.GetBookingById(request.Model.Id);
                if (bookingFromDb == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }

                bookingFromDb = UpdateEntity(request, bookingFromDb);
                if (bookingFromDb.Schedule == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                if (request.Model.Pax != bookingFromDb.Pax)
                {
                    bookingFromDb.Schedule.AvailableSeat += bookingFromDb.Pax;
                    bookingFromDb.Pax = request.Model.Pax;
                    bookingFromDb.Schedule.AvailableSeat -= bookingFromDb.Pax;
                }
                bookingFromDb.BookingStatus = Constants.BookingStatus.COMPLETE;
                repository.Schedules.Update(bookingFromDb.Schedule);
                repository.Bookings.Update(bookingFromDb);
                await repository.CommitAsync();
                return bookingFromDb.Id;
            }

            private static Booking UpdateEntity(EditBookingCommand request, Booking bookingFromDB)
            {
                if (bookingFromDB.BookingDate != request.Model.BookingDate)
                {
                    bookingFromDB.BookingDate = request.Model.BookingDate;
                }

                if (bookingFromDB.CheckIn != request.Model.CheckIn)
                {
                    bookingFromDB.CheckIn = request.Model.CheckIn;
                }
                if (bookingFromDB.CheckOut != request.Model.CheckOut)
                {
                    bookingFromDB.CheckOut = request.Model.CheckOut;
                }
                if (bookingFromDB.FullName != request.Model.FullName)
                {
                    bookingFromDB.FullName = request.Model.FullName;
                }
                if (bookingFromDB.PhoneNo != request.Model.PhoneNo)
                {
                    bookingFromDB.PhoneNo = request.Model.PhoneNo;
                }

                return bookingFromDB;
            }
        }
    }
}