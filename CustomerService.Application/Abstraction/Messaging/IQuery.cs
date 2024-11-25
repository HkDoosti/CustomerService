﻿namespace CustomerService.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
