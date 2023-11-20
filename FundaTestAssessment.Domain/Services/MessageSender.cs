using MediatR;

namespace FundaTestAssessment.Domain.Services
{
    public class MessageSender : IMessageSender
    {
        private readonly IMediator _mediator;

        public MessageSender(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<T> Query<T>(IRequest<T> query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }
    }
}

