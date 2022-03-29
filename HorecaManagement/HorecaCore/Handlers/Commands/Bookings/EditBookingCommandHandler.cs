using FluentValidation;
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

                var bookingFromDB = repository.BookingRepository.Get(request.Model.BookingId);

                if (bookingFromDB == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }

                bookingFromDB = UpdateEntity(request, bookingFromDB);
                bookingFromDB.BookingStatus = Constants.BookingStatus.PENDING;

                repository.BookingRepository.Update(bookingFromDB);

                await repository.CommitAsync();
                return bookingFromDB.Id;
            }

            private static Booking UpdateEntity(EditBookingCommand request, Booking bookingFromDB)
            {
                if (bookingFromDB.BookingDate != request.Model.Booking.BookingDate)
                {
                    bookingFromDB.BookingDate = request.Model.Booking.BookingDate;
                }

                if (bookingFromDB.CheckIn != request.Model.Booking.CheckIn)
                {
                    bookingFromDB.CheckIn = request.Model.Booking.CheckIn;
                }
                if (bookingFromDB.CheckOut != request.Model.Booking.CheckOut)
                {
                    bookingFromDB.CheckOut = request.Model.Booking.CheckOut;
                }
                if (bookingFromDB.FullName != request.Model.Booking.FullName)
                {
                    bookingFromDB.FullName = request.Model.Booking.FullName;
                }
                if (bookingFromDB.PhoneNo != request.Model.Booking.PhoneNo)
                {
                    bookingFromDB.PhoneNo = request.Model.Booking.PhoneNo;
                }
                if (bookingFromDB.UserId != request.Model.Booking.UserID)
                {
                    bookingFromDB.UserId = request.Model.Booking.UserID;
                }
                return bookingFromDB;
            }
        }
    }
}