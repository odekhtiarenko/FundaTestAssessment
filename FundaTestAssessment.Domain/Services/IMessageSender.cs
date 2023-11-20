using MediatR;

namespace FundaTestAssessment.Domain.Services
{
    public interface IMessageSender
    {
        Task<T> Query<T>(IRequest<T> query, CancellationToken cancellationToken);
    }
}

