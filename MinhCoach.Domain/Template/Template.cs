using MinhCoach.Domain.Common.Models;
using MinhCoach.Domain.Common.ValueObjects;
using MinhCoach.Domain.Template.ValueObjects;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.Domain.Template;

public sealed class Template : Model<TemplateId, Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsPrivateTemplate { get; private set; }
    public FullTimestamps Timestamps { get; private set; }
    public UserId? UserId { get; private set; }

#pragma warning disable CS8618
    private Template()
    {

    }
#pragma warning disable CS8618
}