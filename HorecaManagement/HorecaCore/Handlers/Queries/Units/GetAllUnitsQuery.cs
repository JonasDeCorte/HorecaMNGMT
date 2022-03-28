using AutoMapper;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Units;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Queries.Units
{
    public class GetAllUnitsQuery : IRequest<IEnumerable<UnitDto>>
    {
    }

    public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, IEnumerable<UnitDto>>

    {
        private readonly IUnitOfWork repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllUnitsQueryHandler(IUnitOfWork repository, IMapper mapper, UserManager<ApplicationUser> userManager)

        {
            this.repository = repository;
            _mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UnitDto>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(repository.Units.GetAll());
            logger.Info("{amount} of {nameof} have been returned", entities.Count(), nameof(UnitDto));

            return _mapper.Map<IEnumerable<UnitDto>>(entities);
        }
    }
}