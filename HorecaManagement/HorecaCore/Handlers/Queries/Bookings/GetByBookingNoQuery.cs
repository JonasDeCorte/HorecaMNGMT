﻿using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Bookings;
using MediatR;

namespace Horeca.Core.Handlers.Queries.Bookings
{
    public class GetByBookingNoQuery : IRequest<BookingDto>
    {
        public GetByBookingNoQuery(string bookingNo)
        {
            BookingNo = bookingNo;
        }

        public string BookingNo { get; }
    }

    public class GetByBookingNoQueryHandler : IRequestHandler<GetByBookingNoQuery, BookingDto>
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public GetByBookingNoQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BookingDto> Handle(GetByBookingNoQuery request, CancellationToken cancellationToken)
        {
            Booking? booking = await repository.Bookings.GetByNumber(request.BookingNo);
            return mapper.Map<BookingDto>(booking);
        }
    }
}