using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Response;
using MinhCoach.App.TemplateManagement.Common;

namespace MinhCoach.App.TemplateManagement.Queries.GetTemplates;

public record GetTemplatesQuery(
    string? templateType
    ) : IRequest<ErrorOr<ObjectResponse<List<GetTemplatesResult>>>>;