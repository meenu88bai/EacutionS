using AspNetCoreMultipleProject.Commands;
using DomainModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Handler
{
    public class CleanAllDatatHandler : IRequestHandler<CleanAllDataCommand, Unit>
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CleanAllDatatHandler(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        public async Task<Unit> Handle(CleanAllDataCommand request, CancellationToken cancellationToken)
        {
            await _dataAccessProvider.CleanAllData();
            return Unit.Value;
        }
    }
}